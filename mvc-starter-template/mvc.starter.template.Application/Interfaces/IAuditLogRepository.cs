using mvc.starter.template.Domain.Entities;

namespace mvc.starter.template.Application.Interfaces
{
    public interface IAuditLogRepository
    {
        void Insert(AuditLog auditLog);

        IReadOnlyList<AuditLog> Search(
            DateTime? startDate,
            DateTime? endDate,
            string? userLogin,
            string? action,
            int limit);

        AuditLog? GetById(long id);
    }
}
