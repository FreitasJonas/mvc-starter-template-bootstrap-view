using mvc.starter.template.Application.Interfaces;
using mvc.starter.template.Domain.Entities;

namespace mvc.starter.template.Application.Services
{
    public class AuthService : IAuthService
    {
        public User Authenticate(string login, string senha)
        {
            return new User() { Login = login, IsActive = true, Id = 1 };
        }
    }
}
