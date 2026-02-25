using mvc.starter.template.Application.Interfaces;
using mvc.starter.template.Domain.Entities;

namespace mvc.starter.template.Data.Repositories
{
    public class AuditLogRepository : IAuditLogRepository
    {
        public AuditLog? GetById(long id)
        {
            throw new NotImplementedException();
        }

        public void Insert(AuditLog auditLog)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<AuditLog> Search(DateTime? startDate, DateTime? endDate, string? userLogin, string? action, int limit)
        {
            throw new NotImplementedException();
        }
    }
}
