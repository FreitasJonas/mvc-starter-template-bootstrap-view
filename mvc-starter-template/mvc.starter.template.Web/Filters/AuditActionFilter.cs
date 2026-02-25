using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Filters;
using mvc.starter.template.Application.Interfaces;

namespace mvc.starter.template.Web.Filters
{
    public class AuditActionFilter : IActionFilter
    {

        public AuditActionFilter(IAuditService auditService)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
    }
}
