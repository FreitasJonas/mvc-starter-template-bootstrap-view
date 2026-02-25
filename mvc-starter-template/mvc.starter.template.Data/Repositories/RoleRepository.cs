using mvc.starter.template.Application.Interfaces;
using mvc.starter.template.Domain.Entities;
using mvc.starter.template.Data.Infrastructure;
using MySql.Data.MySqlClient;

namespace mvc.starter.template.Data.Repositories
{
    public class RoleRepository
        : BaseRepository, IRoleRepository
    {
        public RoleRepository(MySqlConnectionFactory factory)
            : base(factory) { }

        public RoleRepository(DbSession session)
            : base(session) { }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> GetAll()
        {
            throw new NotImplementedException();
        }

        public Role GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int Insert(Role role)
        {
            throw new NotImplementedException();
        }

        public bool Update(Role role)
        {
            throw new NotImplementedException();
        }
    }
}
