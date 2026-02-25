using mvc.starter.template.Domain.Entities;

namespace mvc.starter.template.Application.Interfaces
{
    public interface IRoleService
    {
        IEnumerable<Role> GetAll();
        Role GetById(int id);

        void Create(
            string nome,
            string descricao,
            IEnumerable<string> permissionCodes);

        void Update(
            int id,
            string nome,
            string descricao,
            IEnumerable<string> permissionCodes);

        IEnumerable<string> GetPermissionCodesByRoleId(int roleId);

        IEnumerable<string> GetAllPermissionCodes();

        IEnumerable<PermissionEntity> GetAllPermission();
    }
}
