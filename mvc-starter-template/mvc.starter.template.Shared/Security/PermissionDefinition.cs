namespace mvc.starter.template.Shared.Security
{
    public sealed class PermissionDefinition
    {
        public string Code { get; set; }
        public string Description { get; set; }

        public PermissionDefinition(string code, string description)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("Code is required", nameof(code));

            Code = code;
            Description = description ?? string.Empty;
        }
    }
}
