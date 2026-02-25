using mvc.starter.template.Application.Interfaces;
using mvc.starter.template.Data.Infrastructure;

namespace mvc.starter.template.Data.Repositories
{
    public class RolePermissionRepository
        : BaseRepository, IRolePermissionRepository
    {
        public RolePermissionRepository(MySqlConnectionFactory factory)
            : base(factory) { }

        public RolePermissionRepository(DbSession session)
            : base(session) { }

        public bool DeleteByRoleId(int roleId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetPermissionCodesByRoleId(int roleId)
        {
            throw new NotImplementedException();
        }

        public void Insert(int roleId, int permissionId)
        {
            throw new NotImplementedException();
        }
    }
}
