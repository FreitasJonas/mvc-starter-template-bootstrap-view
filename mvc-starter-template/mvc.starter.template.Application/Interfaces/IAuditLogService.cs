using mvc.starter.template.Application.DTOs;

namespace mvc.starter.template.Application.Interfaces
{
    public interface IAuditLogService
    {
        IReadOnlyList<AuditLogListItemDto> Search(AuditLogSearchRequest request);
        AuditLogDetailDto? GetDetails(long id);
    }
}
