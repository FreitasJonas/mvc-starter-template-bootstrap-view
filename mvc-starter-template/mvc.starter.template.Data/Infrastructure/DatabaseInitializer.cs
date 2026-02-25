using Microsoft.Extensions.Configuration;

namespace mvc.starter.template.Data.Infrastructure
{
    public class DatabaseInitializer
    {
        private readonly IConfiguration _configuration;

        public DatabaseInitializer(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        private bool IsAutoCreateEnabled()
        {
           throw new NotImplementedException();
        }

        private string GetDatabaseVersion()
        {
            throw new NotImplementedException();
        }

        private void EnsureDatabaseExists()
        {
            throw new NotImplementedException();
        }

        private bool IsVersionApplied(string version)
        {
            throw new NotImplementedException();
        }

        private void ExecuteMigration(string version)
        {
            throw new NotImplementedException();
        }

        private string LoadScript(string scriptName)
        {
            throw new NotImplementedException();
        }

    }
}
