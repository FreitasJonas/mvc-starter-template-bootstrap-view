namespace mvc.starter.template.Shared.Security
{
    public static class PasswordHasher
    {

        public static string Hash(string password)
        {
            return password;
        }

        public static bool Verify(string password, string hash)
        {
           throw new NotImplementedException();
        }
    }
}
