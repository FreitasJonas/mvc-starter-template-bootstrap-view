using System.Net;
using System.Text.Json;
using mvc.starter.template.Shared.Logging;

namespace mvc.starter.template.Web.Middlewares;

public sealed class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogFileWriter _logger;
    private readonly IWebHostEnvironment _env;

    public GlobalExceptionMiddleware(
        RequestDelegate next,
        ILogFileWriter logger,
        IWebHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            LogException(context, ex);

            if (IsAjaxRequest(context.Request))
            {
                await HandleJsonAsync(context, ex);
            }
            else
            {
                HandleRedirect(context);
            }
        }
    }

    private void HandleRedirect(HttpContext context)
    {
        context.Response.Clear();
        context.Response.Redirect("/Login/Error");
    }

    private async Task HandleJsonAsync(HttpContext context, Exception ex)
    {
        context.Response.Clear();
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";

        var response = new
        {
            success = false,
            message = "Unexpected error.",
            details = _env.IsDevelopment() ? ex.Message : null
        };

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(response));
    }

    private static bool IsAjaxRequest(HttpRequest request)
    {
        return request.Headers["X-Requested-With"] == "XMLHttpRequest"
            || request.Headers["Accept"].Any(a => a.Contains("application/json"));
    }

    private void LogException(HttpContext context, Exception ex)
    {
        var req = context.Request;

        var message =
$"""
Unhandled Exception
Method: {req.Method}
Path: {req.Path}
QueryString: {req.QueryString}
User: {context.User?.Identity?.Name ?? "Anonymous"}
IP: {context.Connection.RemoteIpAddress}
""";

        _logger.LogError(message, ex);
    }
}
