using mvc.starter.template.Application.Interfaces;
using mvc.starter.template.Application.ViewModels;
using mvc.starter.template.Data.Infrastructure;
using mvc.starter.template.Domain.Entities;

namespace mvc.starter.template.Data.Repositories
{
    public class UserRepository
        : BaseRepository, IUserRepository
    {
        public UserRepository(MySqlConnectionFactory factory)
            : base(factory) { }

        public UserRepository(DbSession session)
            : base(session) { }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserListItemViewModel> GetAllWithRole()
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public User GetByLogin(string login)
        {
            throw new NotImplementedException();
        }

        public int Insert(User user)
        {
            throw new NotImplementedException();
        }

        public bool Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
