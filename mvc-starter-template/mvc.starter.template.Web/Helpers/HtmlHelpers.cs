using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using mvc.starter.template.Shared;

namespace mvc.starter.template.Web.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlContent RouteIf(
            this IHtmlHelper helper,
            string valueController,
            string attribute)
        {
            var routeData = helper.ViewContext.RouteData;

            var currentController =
                (routeData.Values["controller"]?.ToString() ?? string.Empty)
                .UnDash();

            var hasController = false;

            foreach (var controller in valueController.Trim().Split(';'))
            {
                if (controller.Equals(
                    currentController,
                    StringComparison.InvariantCultureIgnoreCase))
                {
                    hasController = true;
                    break;
                }
            }

            return hasController
                ? new HtmlString(attribute)
                : HtmlString.Empty;
        }

        public static IHtmlContent RouteIf(
            this IHtmlHelper helper,
            string valueAction,
            string valueController,
            string attribute)
        {
            var routeData = helper.ViewContext.RouteData;

            var currentController =
                (routeData.Values["controller"]?.ToString() ?? string.Empty)
                .UnDash();

            var currentAction =
                (routeData.Values["action"]?.ToString() ?? string.Empty)
                .UnDash();

            var hasController = valueController.Equals(
                currentController,
                StringComparison.InvariantCultureIgnoreCase);

            var hasAction = valueAction.Equals(
                currentAction,
                StringComparison.InvariantCultureIgnoreCase);

            return hasAction && hasController
                ? new HtmlString(attribute)
                : HtmlString.Empty;
        }

        public static async Task RenderPartialIfAsync(
            this IHtmlHelper htmlHelper,
            string partialViewName,
            bool condition)
        {
            if (!condition)
                return;

            await htmlHelper.PartialAsync(partialViewName);
        }

        public static IHtmlContent GetSvg(this IHtmlHelper helper, string svg)
        {
            return new HtmlString($"/img/sprite.svg#{svg}");
        }
    }
}
