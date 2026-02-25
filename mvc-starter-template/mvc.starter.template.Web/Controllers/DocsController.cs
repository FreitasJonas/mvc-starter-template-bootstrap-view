using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace mvc.starter.template.Web.Controllers
{
    public class DocsController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("GettingStarted");
        }

        public IActionResult GettingStarted()
        {
            return View();
        }

        public IActionResult DesignPattern()
        {
            return View();
        }

        public IActionResult Database()
        {
            return View();
        }

        public IActionResult FrontEnd()
        {
            return View();
        }

        public IActionResult Security()
        {
            return View();
        }

        public IActionResult AuditingAndLogging()
        {
            return View();
        }
    }
}
