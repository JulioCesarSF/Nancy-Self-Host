using MySql.Data.MySqlClient;
using System;

namespace NancyHostMySql.DAL
{
    public sealed class MySqlContext : IDisposable
    {
        private MySqlConnection connection = null;
        //get this value from app.config
        private const string ConnectionString = "Server=localhost;Port=3306;Database=testdatabase;Uid=newuser;Pwd=1234;";

        public MySqlContext()
        {
            OpenConnection();
        }

        public MySqlConnection GetConnection()
        {
            return connection;
        }

        private void OpenConnection()
        {
            var conString = new MySqlConnectionStringBuilder(ConnectionString);
            connection = new MySqlConnection(conString.ConnectionString);
            connection.Open();
        }

        public void Dispose()
        {
            if (connection is null) return;
            connection.Dispose();
        }
    }
}
