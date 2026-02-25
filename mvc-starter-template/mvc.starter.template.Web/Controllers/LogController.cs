using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mvc.starter.template.Shared.Logging;
using mvc.starter.template.Web.Security;
using mvc.starter.template.Web.ViewModels;
using static mvc.starter.template.Shared.Security.PermissionMap;

namespace mvc.starter.template.Web.Controllers
{
    [Authorize]
    public class LogController : Controller
    {
        private const int MaxLines = 2000;
        private readonly ILogFileWriter _logger;

        public LogController(
            ILogFileWriter logger)
        {
            _logger = logger;
        }

        [PermissionAuthorize(Permission.LogView)]
        public IActionResult Index(DateTime? date)
        {
            try
            {
                SetIndexBreadcrumb();

                if (!date.HasValue)
                    date = DateTime.Now;

                var model = new LogIndexViewModel
                {
                    Date = date
                };

                var fileName = $"{date:yyyyMMdd}.txt";
                var filePath = Path.Combine(_logger.GetLogDirectory(), fileName);

                if (!System.IO.File.Exists(filePath))
                {
                    model.FileExists = false;
                    return View(model);
                }

                var lines = System.IO.File.ReadLines(filePath).ToList();

                model.TotalLines = lines.Count;
                model.FileExists = true;

                // Limitar para evitar página pesada
                var limitedLines = lines
                    .TakeLast(MaxLines);

                model.LogContent = string.Join(Environment.NewLine, limitedLines);

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error loading log file.", ex);
                TempData["Error"] = "Error loading log file.";
                return RedirectToAction("Index", "Home");
            }
        }

        [PermissionAuthorize(Permission.LogDownload)]
        public IActionResult Download(DateTime date)
        {
            try
            {
                var fileName = $"{date:yyyyMMdd}.txt";
                var filePath = Path.Combine(_logger.GetLogDirectory(), fileName);

                if (!System.IO.File.Exists(filePath))
                {
                    TempData["Error"] = "Log file missing.";
                    return RedirectToAction("Index");
                }

                var bytes = System.IO.File.ReadAllBytes(filePath);

                return File(
                    bytes,
                    "text/plain",
                    fileName
                );
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error downloading log file. Date: {date}", ex);
                TempData["Error"] = "Error downloading log file.";
                return RedirectToAction(nameof(Index));
            }
        }

        private void SetIndexBreadcrumb()
        {
            ViewBag.Subheader = new IndexSubheaderViewModel
            {
                Title = "System Logs",
                Icon = "bi bi-braces",

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
                        Text = "Logs",
                        IsActive = true
                    }
                }
            };
        }
    }
}
