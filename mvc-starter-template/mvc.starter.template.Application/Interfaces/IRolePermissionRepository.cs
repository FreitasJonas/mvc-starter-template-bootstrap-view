namespace mvc.starter.template.Application.Interfaces
{
    public interface IRolePermissionRepository
    {
        void Insert(int roleId, int permissionId);
        bool DeleteByRoleId(int roleId);
        IEnumerable<string> GetPermissionCodesByRoleId(int roleId);
    }
}
