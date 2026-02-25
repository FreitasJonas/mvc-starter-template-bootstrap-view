using Microsoft.AspNetCore.Authorization;
using static mvc.starter.template.Shared.Security.PermissionMap;

namespace mvc.starter.template.Web.Security
{
    public class PermissionAuthorizeAttribute : AuthorizeAttribute
    {
        public Permission Permission { get; }

        public PermissionAuthorizeAttribute(Permission permission)
        {
            Policy = $"Permission:{GetCode(permission)}";
        }       
    }
}
