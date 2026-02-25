using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using mvc.starter.template.Application.Interfaces;
using mvc.starter.template.Application.Logging;
using mvc.starter.template.Application.Services;
using mvc.starter.template.Data.Infrastructure;
using mvc.starter.template.Data.Repositories;
using mvc.starter.template.Shared.Logging;
using mvc.starter.template.Shared.Security;
using mvc.starter.template.Web.Filters;
using mvc.starter.template.Web.Middlewares;
using mvc.starter.template.Web.Security;

var builder = WebApplication.CreateBuilder(args);

//live demo
builder.Configuration.AddEnvironmentVariables();

// ----------------------------------------------------------------------
// MVC
// ----------------------------------------------------------------------
builder.Services
    .AddControllersWithViews()
    .AddCookieTempDataProvider();

// ----------------------------------------------------------------------
// Authentication (Cookie)
// ----------------------------------------------------------------------
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.LogoutPath = "/Login/Logout";
        options.AccessDeniedPath = "/Login";
        options.Cookie.Name = "MvcStarterAuth";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
    });

// ----------------------------------------------------------------------
// Authorization
// ----------------------------------------------------------------------
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Permission", policy =>
        policy.Requirements.Add(new PermissionRequirement("")));
});

builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
builder.Services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
builder.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, PermissionResultHandler>();


// ----------------------------------------------------------------------
// Infraestruture
// ----------------------------------------------------------------------
builder.Services.AddSingleton<MySqlConnectionFactory>();
builder.Services.AddSingleton<DatabaseInitializer>();

builder.Services.AddHttpContextAccessor();

// ----------------------------------------------------------------------
// Repositories
// ----------------------------------------------------------------------
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IAuditLogRepository, AuditLogRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// ----------------------------------------------------------------------
// Application Services
// ----------------------------------------------------------------------
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IPermissionSyncService, PermissionSyncService>();
builder.Services.AddScoped<IAuditService, AuditService>();
builder.Services.AddScoped<IAuditLogService, AuditLogService>();

builder.Services.AddSingleton<TwoFactorService>();

// ----------------------------------------------------------------------
// Filters
// ----------------------------------------------------------------------
builder.Services.AddScoped<AuditActionFilter>();

// ----------------------------------------------------------------------
// Logging
// ----------------------------------------------------------------------
builder.Services.AddSingleton<ILogFileWriter, FileLogWriter>();

// ----------------------------------------------------------------------
// Build app
// ----------------------------------------------------------------------
var app = builder.Build();

// ----------------------------------------------------------------------
// Database initialize
// ----------------------------------------------------------------------
using (var scope = app.Services.CreateScope())
{
    var dbInitializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();
    //dbInitializer.Initialize();
}

// ----------------------------------------------------------------------
// Permission Sync
// ----------------------------------------------------------------------
using (var scope = app.Services.CreateScope())
{
    var permissionSync = scope.ServiceProvider
        .GetRequiredService<IPermissionSyncService>();

    //permissionSync.Sync();
}

// ----------------------------------------------------------------------
// Pipeline HTTP
// ----------------------------------------------------------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Login/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Global middleware exception
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
