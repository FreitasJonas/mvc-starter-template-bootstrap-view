using mvc.starter.template.Domain.Entities;

namespace mvc.starter.template.Application.Interfaces
{
    public interface IAuthService
    {
        User Authenticate(string login, string senha);
    }
}
