using mvc.starter.template.Application.Interfaces;

namespace mvc.starter.template.Application.Services
{
    public class AuditService : IAuditService
    {
        public void LogCreate<T>(string entityName, object entityId, T newEntity)
        {
            throw new NotImplementedException();
        }

        public void LogDelete<T>(string entityName, object entityId, T oldEntity)
        {
            throw new NotImplementedException();
        }

        public void LogEvent(string entityName, object entityId, string action, object? data = null)
        {
            throw new NotImplementedException();
        }

        public void LogUpdate<T>(string entityName, object entityId, T oldEntity, T newEntity)
        {
            throw new NotImplementedException();
        }

        public void LogUpdate<T>(string entityName, object entityId, T oldEntity, T newEntity, object? metadata = null)
        {
            throw new NotImplementedException();
        }
    }
}
