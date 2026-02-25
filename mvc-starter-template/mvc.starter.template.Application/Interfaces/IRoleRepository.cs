using mvc.starter.template.Domain.Entities;

namespace mvc.starter.template.Application.Interfaces
{
    public interface IRoleRepository
    {
        IEnumerable<Role> GetAll();
        Role GetById(int id);
        int Insert(Role role);
        bool Update(Role role);
        bool Delete(int id);
    }
}
