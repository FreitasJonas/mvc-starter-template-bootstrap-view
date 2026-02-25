using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using mvc.starter.template.Shared.Security;
using static mvc.starter.template.Shared.Security.PermissionMap;

namespace mvc.starter.template.Web.Helpers
{
    public static class AuthorizationHtmlHelper
    {
        public static IHtmlContent RoutePermissionIf(
            this IHtmlHelper helper,
            string controllers,
            string? actions = null,
            Permission? permission = null,
            string attribute = "")
        {
            if (!HasRoute(helper, controllers, actions))
                return HtmlString.Empty;

            if (permission != null)
            {
                if (!HasPermission(helper, permission.Value))
                    return HtmlString.Empty;
            }   

            return new HtmlString(attribute);
        }

        public static async Task RenderIfAllowed(
            this IHtmlHelper helper,
            string controllers,
            string? actions = null,
            Permission? permission = null,
            string partialView = "")
        {
            if (!HasRoute(helper, controllers, actions))
                return;

            if(permission != null)
            {
                if (!HasPermission(helper, permission.Value))
                    return;
            }            

            await helper.PartialAsync(partialView);
        }

        private static bool HasRoute(IHtmlHelper helper, string controllers, string? actions)
        {
            var routeData = helper.ViewContext.RouteData;

            var currentController =
                routeData.Values["controller"]?.ToString() ?? string.Empty;

            var currentAction =
                routeData.Values["action"]?.ToString() ?? string.Empty;

            var controllerMatch = false;

            foreach (var controller in controllers.Split(';'))
            {
                if (controller.Equals(
                    currentController,
                    StringComparison.InvariantCultureIgnoreCase))
                {
                    controllerMatch = true;
                    break;
                }
            }

            if (!controllerMatch)
                return false;

            if (string.IsNullOrWhiteSpace(actions))
                return true;

            foreach (var action in actions.Split(';'))
            {
                if (action.Equals(
                    currentAction,
                    StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool HasPermission(this IHtmlHelper helper, Permission permission)
        {
            var permissionCode = PermissionMap.GetCode(permission);
            return helper.ViewContext.HttpContext.User.HasClaim("permission", permissionCode);
        }

        public static string RouteIsActive(this IHtmlHelper helper, string controllers, string? actions = null)
        {
            var routeData = helper.ViewContext.RouteData;

            var currentController =
                routeData.Values["controller"]?.ToString() ?? string.Empty;

            var currentAction =
                routeData.Values["action"]?.ToString() ?? string.Empty;

            if (!controllers
                .Split(';')
                .Any(c => c.Equals(currentController,
                    StringComparison.InvariantCultureIgnoreCase)))
                return string.Empty;

            if (string.IsNullOrWhiteSpace(actions))
                return "active";

            return actions
                .Split(';')
                .Any(a => a.Equals(currentAction,
                    StringComparison.InvariantCultureIgnoreCase))
                    ? "active"
                    : string.Empty;
        }
    }
}
