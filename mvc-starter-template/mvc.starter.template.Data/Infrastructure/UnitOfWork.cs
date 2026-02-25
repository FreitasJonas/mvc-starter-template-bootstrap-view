using mvc.starter.template.Application.Interfaces;
using mvc.starter.template.Data.Repositories;

namespace mvc.starter.template.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbSession _session;

        public IUserRepository Users { get; }
        public IRoleRepository Roles { get; }
        public IUserRoleRepository UserRoles { get; }
        public IRolePermissionRepository RolePermissions { get; }
        public IPermissionRepository Permissions { get; }

        public UnitOfWork(MySqlConnectionFactory connectionFactory)
        {
            _session = new DbSession(connectionFactory);

            Users = new UserRepository(_session);
            Roles = new RoleRepository(_session);
            UserRoles = new UserRoleRepository(_session);
            RolePermissions = new RolePermissionRepository(_session);
            Permissions = new PermissionRepository(_session);
        }

        public void Begin() => _session.BeginTransaction();
        public void Commit() => _session.Commit();
        public void Rollback() => _session.Rollback();
        public void Dispose() => _session.Dispose();
    }
}
