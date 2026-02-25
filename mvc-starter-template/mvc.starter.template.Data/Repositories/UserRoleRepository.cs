using mvc.starter.template.Application.Interfaces;
using mvc.starter.template.Data.Infrastructure;
using mvc.starter.template.Domain.Entities;

namespace mvc.starter.template.Data.Repositories
{
    public class UserRoleRepository
        : BaseRepository, IUserRoleRepository
    {
        public UserRoleRepository(MySqlConnectionFactory factory)
            : base(factory) { }

        public UserRoleRepository(DbSession session)
            : base(session) { }

        public bool DeleteByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public UserRole GetByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public void Insert(int userId, int roleId)
        {
            throw new NotImplementedException();
        }
    }
}
