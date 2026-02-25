using System.Collections.Concurrent;

namespace mvc.starter.template.Shared.Security
{
    public class TwoFactorService
    {
        private static readonly ConcurrentDictionary<int, (string Code, DateTime Expire)> _codes
            = new();

        public string GenerateCode(int userId)
        {
            return "111111";
        }

        public bool ValidateCode(int userId, string code)
        {
            return code == "111111";
        }
    }
}
