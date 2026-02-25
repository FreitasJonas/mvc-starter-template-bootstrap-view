using mvc.starter.template.Application.Interfaces;
using mvc.starter.template.Application.ViewModels;
using mvc.starter.template.Domain.Entities;

namespace mvc.starter.template.Application.Services
{
    public class UserService : IUserService
    {
        public void Create(User user, int roleId)
        {
            throw new NotImplementedException();
        }

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

        public UserRole GetUserRole(int userId)
        {
            throw new NotImplementedException();
        }

        public void Update(User user, int roleId)
        {
            throw new NotImplementedException();
        }
    }
}
