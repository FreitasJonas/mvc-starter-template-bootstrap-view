using Microsoft.AspNetCore.Authorization;

namespace mvc.starter.template.Web.Security
{
    public class PermissionHandler
        : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            return Task.CompletedTask;
        }
    }
}
