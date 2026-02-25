using mvc.starter.template.Application.DTOs;
using mvc.starter.template.Application.Interfaces;

namespace mvc.starter.template.Application.Services
{
    public class AuditLogService : IAuditLogService
    {
        public AuditLogDetailDto? GetDetails(long id)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<AuditLogListItemDto> Search(AuditLogSearchRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
