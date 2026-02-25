using System.Security.Claims;
using mvc.starter.template.Shared.Security;
using static mvc.starter.template.Shared.Security.PermissionMap;

namespace mvc.starter.template.Web.Helpers
{
    public static class PermissionExtensions
    {
        public static bool HasPermission(this ClaimsPrincipal user, Permission permission)
        {
            var permissionCode = PermissionMap.GetCode(permission);

            return user.Claims.Any(c => 
                c.Type == "permission" &&
                c.Value.Equals(permissionCode, StringComparison.OrdinalIgnoreCase));
        }
    }
}
