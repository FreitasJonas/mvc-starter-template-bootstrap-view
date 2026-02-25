using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;

namespace mvc.starter.template.Data.Infrastructure
{
    public class MySqlConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public MySqlConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public MySqlConnection Create()
        {
            var connectionString =
                _configuration.GetConnectionString("DefaultConnection");

            return new MySqlConnection(connectionString);
        }
    }
}
