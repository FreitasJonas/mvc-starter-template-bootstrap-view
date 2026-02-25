using mvc.starter.template.Application.ViewModels;
using mvc.starter.template.Domain.Entities;

namespace mvc.starter.template.Application.Interfaces
{
    public interface IUserRepository
    {
        User GetByLogin(string login);
        User GetById(int id);
        IEnumerable<User> GetAll();
        IEnumerable<UserListItemViewModel> GetAllWithRole();
        int Insert(User user);
        bool Update(User user);
        bool Delete(int id);
    }
}
