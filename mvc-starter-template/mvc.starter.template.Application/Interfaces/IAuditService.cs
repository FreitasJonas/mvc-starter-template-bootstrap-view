namespace mvc.starter.template.Application.Interfaces
{
    public interface IAuditService
    {
        void LogCreate<T>(string entityName, object entityId, T newEntity);
        void LogUpdate<T>(string entityName, object entityId, T oldEntity, T newEntity);
        void LogUpdate<T>(string entityName, object entityId, T oldEntity, T newEntity, object? metadata = null);
        void LogDelete<T>(string entityName, object entityId, T oldEntity);
        void LogEvent(string entityName, object entityId, string action, object? data = null);
    }
}
