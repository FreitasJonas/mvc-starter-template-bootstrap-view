using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mvc.starter.template.Application.DTOs;
using mvc.starter.template.Application.Interfaces;
using mvc.starter.template.Shared.Logging;
using mvc.starter.template.Web.ViewModels;

namespace mvc.starter.template.Web.Controllers
{
    [Authorize]
    public class AuditController : Controller
    {
        private readonly IAuditLogService _auditLogService;
        private readonly ILogFileWriter _logger;

        public AuditController(
            IAuditLogService auditLogService,
            ILogFileWriter logger)
        {
            _auditLogService = auditLogService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                SetIndexBreadcrumb();
                return View(new List<AuditLogListItemDto>());
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while loading the audit screen.", ex);
                throw;
            }
        }

        [HttpPost]
        public IActionResult Search([FromBody] AuditLogSearchRequest request)
        {
            try
            {
                var result = new List<AuditLogListItemDto>();

                return Json(new
                {
                    success = true,
                    total = result.Count,
                    data = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while searching audit logs.", ex);

                return Json(new
                {
                    success = false,
                    message = "Error while performing the audit search."
                });
            }
        }

        [HttpGet]
        public IActionResult Details(long id)
        {
            try
            {
                var detail = _auditLogService.GetDetails(id);
                if (detail == null)
                    return NotFound();

                return Json(detail);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while retrieving audit details. Id: {id}", ex);
                return StatusCode(500);
            }
        }

        private void SetIndexBreadcrumb()
        {
            ViewBag.Subheader = new IndexSubheaderViewModel
            {
                Title = "System Audit",
                Icon = "bi bi-binoculars",

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
                        Text = "Audit",
                        IsActive = true
                    }
                }
            };
        }
    }
}
