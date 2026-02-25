using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mvc.starter.template.Shared.Logging;
using mvc.starter.template.Web.ViewModels;

namespace mvc.starter.template.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogFileWriter _logger;

        public HomeController(ILogFileWriter logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInfo($"[{HttpContext.Request.Host.Host}][{HttpContext.User.Identity.Name}]Login system detected!");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var login = User.Identity?.Name;

            SetIndexBreadcrumb();
            return View();
        }

        private void SetIndexBreadcrumb()
        {
            ViewBag.Subheader = new IndexSubheaderViewModel
            {
                Title = "Application",
                Icon = "",

                Breadcrumb = new[]
                {
                    new BreadcrumbItemViewModel
                    {
                        Text = "Home",
                        Controller = "Home",
                        Action = "Index"
                    },
                     new BreadcrumbItemViewModel
                    {
                        Text = "Application",
                        Controller = "Home",
                        Action = "Index",
                        IsActive = true
                    }
                },

                ActionText = "",
                ActionUrl = ""
            };
        }
    }
}
