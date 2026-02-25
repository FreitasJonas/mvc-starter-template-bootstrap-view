using mvc.starter.template.Application.Interfaces;
using mvc.starter.template.Data.Infrastructure;
using mvc.starter.template.Domain.Entities;

namespace mvc.starter.template.Data.Repositories
{
    public class PermissionRepository : BaseRepository, IPermissionRepository
    {
        public PermissionRepository(MySqlConnectionFactory factory)
           : base(factory) { }

        public PermissionRepository(DbSession session)
            : base(session) { }


        public bool Delete(PermissionEntity entity)
        {
            throw new NotImplementedException();
        }

        public List<PermissionEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetPermissionsByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public int Insert(PermissionEntity entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(PermissionEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
