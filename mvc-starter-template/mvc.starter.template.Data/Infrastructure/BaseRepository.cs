using MySql.Data.MySqlClient;

namespace mvc.starter.template.Data.Infrastructure
{
    public abstract class BaseRepository
    {
        protected readonly MySqlConnectionFactory ConnectionFactory;
        protected readonly DbSession Session;

        protected BaseRepository(MySqlConnectionFactory connectionFactory)
        {
            ConnectionFactory = connectionFactory;
        }

        protected BaseRepository(DbSession session)
        {
            Session = session;
        }

        // Sempre retorna conexão ABERTA
        protected MySqlConnection GetConnection()
        {
            if (Session != null)
                return Session.GetConnection();

            var conn = ConnectionFactory.Create();
            conn.Open();
            return conn;
        }

        protected void AttachTransaction(MySqlCommand cmd)
        {
            if (Session?.Transaction != null)
                cmd.Transaction = Session.Transaction;
        }

        protected void DisposeIfNeeded(MySqlConnection conn)
        {
            // Só fecha se NÃO estiver usando UnitOfWork
            if (Session == null)
                conn.Dispose();
        }

        protected int ExecuteScalar(
            string sql,
            Action<MySqlCommand> paramBuilder)
        {
            var conn = GetConnection();
            try
            {
                using var cmd = new MySqlCommand(sql, conn);
                AttachTransaction(cmd);
                paramBuilder(cmd);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            finally
            {
                DisposeIfNeeded(conn);
            }
        }

        protected int ExecuteNonQuery(
            string sql,
            Action<MySqlCommand> paramBuilder)
        {
            var conn = GetConnection();
            try
            {
                using var cmd = new MySqlCommand(sql, conn);
                AttachTransaction(cmd);
                paramBuilder(cmd);

                return cmd.ExecuteNonQuery();
            }
            finally
            {
                DisposeIfNeeded(conn);
            }
        }

        protected List<T> ExecuteReader<T>(
            string sql,
            Action<MySqlCommand> paramBuilder,
            Func<MySqlDataReader, T> mapper)
        {
            var list = new List<T>();
            var conn = GetConnection();

            try
            {
                using var cmd = new MySqlCommand(sql, conn);
                AttachTransaction(cmd);
                paramBuilder(cmd);

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                    list.Add(mapper(reader));

                return list;
            }
            finally
            {
                DisposeIfNeeded(conn);
            }
        }

        protected T ExecuteSingle<T>(
            string sql,
            Action<MySqlCommand> paramBuilder,
            Func<MySqlDataReader, T> mapper)
        {
            var conn = GetConnection();

            try
            {
                using var cmd = new MySqlCommand(sql, conn);
                AttachTransaction(cmd);
                paramBuilder(cmd);

                using var reader = cmd.ExecuteReader();
                return reader.Read() ? mapper(reader) : default;
            }
            finally
            {
                DisposeIfNeeded(conn);
            }
        }
    }
}
