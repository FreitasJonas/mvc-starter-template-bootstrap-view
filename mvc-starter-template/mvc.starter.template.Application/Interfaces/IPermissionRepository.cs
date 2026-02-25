using mvc.starter.template.Domain.Entities;

namespace mvc.starter.template.Application.Interfaces
{
    public interface IPermissionRepository
    {
        IEnumerable<string> GetPermissionsByUserId(int userId);
        List<PermissionEntity> GetAll();
        int Insert(PermissionEntity entity);
        bool Update(PermissionEntity entity);
        bool Delete(PermissionEntity entity);
    }
}
