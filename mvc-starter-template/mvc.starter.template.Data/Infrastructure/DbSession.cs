using MySql.Data.MySqlClient;
using System.Data;

namespace mvc.starter.template.Data.Infrastructure
{
    public class DbSession : IDisposable
    {
        private readonly MySqlConnectionFactory _factory;
        private bool _disposed;

        public MySqlConnection Connection { get; private set; }
        public MySqlTransaction Transaction { get; private set; }

        public DbSession(MySqlConnectionFactory factory)
        {
            _factory = factory;
        }

        public MySqlConnection GetConnection()
        {
            if (Connection == null)
                Connection = _factory.Create();

            if (Connection.State != ConnectionState.Open)
                Connection.Open();

            return Connection;
        }

        public void BeginTransaction()
        {
            if (Transaction != null)
                return;

            var conn = GetConnection();
            Transaction = conn.BeginTransaction();
        }

        public void Commit()
        {
            if (Transaction == null)
                return;

            Transaction.Commit();
            CleanupTransaction();
        }

        public void Rollback()
        {
            if (Transaction == null)
                return;

            Transaction.Rollback();
            CleanupTransaction();
        }

        private void CleanupTransaction()
        {
            Transaction.Dispose();
            Transaction = null;
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            Transaction?.Dispose();
            Connection?.Dispose();

            _disposed = true;
        }
    }
}
