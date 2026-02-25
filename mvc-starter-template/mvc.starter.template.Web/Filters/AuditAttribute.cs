namespace mvc.starter.template.Web.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AuditAttribute : Attribute
    {
        public string EntityName { get; }
        public string Action { get; }

        public AuditAttribute(string entityName, string action)
        {
            EntityName = entityName;
            Action = action;
        }
    }
}
