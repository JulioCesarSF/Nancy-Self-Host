using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace NancyHostMySql.DAL.Seed
{
    public sealed class CreateTables
    {
        private MySqlConnection connection = null;

        public CreateTables()
        {
            using (var ctx = new MySqlContext())
            {
                connection = ctx.GetConnection();
                CreateUserTable();
                CreateApiKeyTable();
            }
        }

        private void CreateUserTable()
        {
            try
            {
                using (var cmd = new MySqlCommand(DataBaseSchema.TableUserExists, connection))
                {
                    var exists = false;
                    using (var reader = cmd.ExecuteReader(CommandBehavior.SingleResult))
                    {
                        exists = reader.HasRows;
                    }

                    if (exists) return;
                    cmd.CommandText = DataBaseSchema.CreateTableUser;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void CreateApiKeyTable()
        {
            try
            {
                using (var cmd = new MySqlCommand(DataBaseSchema.TableApiKeyExists, connection))
                {
                    var exists = false;
                    using (var reader = cmd.ExecuteReader(CommandBehavior.SingleResult))
                    {
                        exists = reader.HasRows;
                    }

                    if (exists) return;
                    cmd.CommandText = DataBaseSchema.CreateTableApiKey;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
