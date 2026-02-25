namespace mvc.starter.template.Shared.Security
{
    public static class PermissionMap
    {
        public enum Permission
        {
            System,

            Users,
            UsersView,
            UserCreate,
            UserEdit,
            UserDelete,

            Roles,
            RolesView,
            RoleCreate,
            RoleEdit,

            Audit,
            AuditView,

            Log,
            LogView,
            LogDownload
        }

        private static readonly Dictionary<Permission, PermissionDefinition> _map = new()
        {
            { Permission.System, new("system", "") },

            { Permission.Users,             new("system||users", "") },
            { Permission.UsersView,         new("system||users||view",          "View users") },
            { Permission.UserCreate,        new("system||users||create",        "Create users") },
            { Permission.UserEdit,          new("system||users||edit",          "Edit user") },
            { Permission.UserDelete,        new("system||users||delete",       "Delete user") },

            { Permission.Roles,             new("system||roles",  "") },
            { Permission.RolesView,         new("system||roles||view",          "View roles") },
            { Permission.RoleCreate,        new("system||roles||create",        "Create role") },
            { Permission.RoleEdit,          new("system||roles||edit",          "Edit role") },

            { Permission.Audit,         new("system||audit", "") },
            { Permission.AuditView,     new("system||audit||view",              "View audit details") },

            { Permission.Log,     new("system||log",              "Log system") },
            { Permission.LogView,     new("system||log||view",              "View log system") },
            { Permission.LogDownload,     new("system||log||download",              "Download log system") },
        };



        public static string GetCode(Permission permission)
            => _map[permission].Code;

        public static IReadOnlyDictionary<Permission, PermissionDefinition> All => _map;
    }
}
