using MySql.Data.MySqlClient;
using NancyHostMySql.DAL.Models;
using System;
using System.Collections.Generic;

namespace NancyHostMySql.DAL.UserDAO
{
    public class UserDAOImpl : IUserDAO
    {
        private MySqlConnection connection = null;

        public bool Add(User entity)
        {
            using (connection = new MySqlContext().GetConnection())
            {
                using (var cmd = new MySqlCommand(DataBaseSchema.InsertUser, connection))
                {
                    cmd.Parameters.AddWithValue("@name", entity.Name);
                    cmd.Parameters.AddWithValue("@password", entity.Password);
                    return cmd.ExecuteNonQuery() == 1;
                }
            }
        }

        public bool Update(User entity)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public bool Find(User user)
        {
            using (connection = new MySqlContext().GetConnection())
            {
                using (var cmd = new MySqlCommand(DataBaseSchema.FindUserByNameAndPassword, connection))
                {
                    cmd.Parameters.AddWithValue("@name", user.Name);
                    cmd.Parameters.AddWithValue("@password", user.Password);
                    using (var reader = cmd.ExecuteReader())
                    {
                        return reader.HasRows;
                    }
                }
            }
        }
    }
}
