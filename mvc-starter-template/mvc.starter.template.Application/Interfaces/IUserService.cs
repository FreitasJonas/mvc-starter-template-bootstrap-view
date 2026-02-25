using mvc.starter.template.Application.ViewModels;
using mvc.starter.template.Domain.Entities;

namespace mvc.starter.template.Application.Interfaces
{
    public interface IUserService
    {
        void Create(User user, int roleId);
        void Update(User user, int roleId);
        User GetById(int id);
        UserRole GetUserRole(int userId);
        IEnumerable<UserListItemViewModel> GetAllWithRole();
        IEnumerable<User> GetAll();

        bool Delete(int id);
    }
}
