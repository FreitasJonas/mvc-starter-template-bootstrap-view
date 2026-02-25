using mvc.starter.template.Domain.Entities;

namespace mvc.starter.template.Application.Interfaces
{
    public interface IUserRoleRepository
    {
        void Insert(int userId, int roleId);
        bool DeleteByUserId(int userId);
        UserRole GetByUserId(int userId);
    }
}
