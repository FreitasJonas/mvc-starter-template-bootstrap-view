using mvc.starter.template.Application.Interfaces;
using mvc.starter.template.Domain.Entities;

namespace mvc.starter.template.Application.Services
{
    public class RoleService : IRoleService
    {
        public void Create(string nome, string descricao, IEnumerable<string> permissionCodes)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PermissionEntity> GetAllPermission()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetAllPermissionCodes()
        {
            throw new NotImplementedException();
        }

        public Role GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetPermissionCodesByRoleId(int roleId)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, string nome, string descricao, IEnumerable<string> permissionCodes)
        {
            throw new NotImplementedException();
        }
    }
}
