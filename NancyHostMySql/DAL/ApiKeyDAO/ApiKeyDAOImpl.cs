using MySql.Data.MySqlClient;
using NancyHostMySql.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace NancyHostMySql.DAL.ApiKeyDAO
{
    public class ApiKeyDAOImpl : IApiKeyDAO
    {
        public bool Add(ApiKey entity)
        {
            using (var connection = new MySqlContext().GetConnection())
            {
                using (var cmd = new MySqlCommand(DataBaseSchema.NewApiKeyEntry, connection))
                {
                    cmd.Parameters.AddWithValue("@name", entity.Name);
                    cmd.Parameters.AddWithValue("@key", entity.Key);
                    cmd.Parameters.AddWithValue("@createTime", entity.CreateTime);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Update(ApiKey entity)
        {
            using (var connection = new MySqlContext().GetConnection())
            {
                using (var cmd = new MySqlCommand(DataBaseSchema.UpdateApiKeyByName, connection))
                {
                    cmd.Parameters.AddWithValue("@name", entity.Name);
                    cmd.Parameters.AddWithValue("@key", entity.Key);
                    cmd.Parameters.AddWithValue("@createTime", entity.CreateTime);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public List<ApiKey> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Delete(ApiKey entity)
        {
            throw new NotImplementedException();
        }

        public bool DeleteByKey(ApiKey entity)
        {
            using (var connection = new MySqlContext().GetConnection())
            {
                using (var cmd = new MySqlCommand(DataBaseSchema.DeleteApiKey, connection))
                {
                    cmd.Parameters.AddWithValue("@key", entity.Key);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public ApiKey FindByName(string userName)
        {
            using (var connection = new MySqlContext().GetConnection())
            {
                using (var cmd = new MySqlCommand(DataBaseSchema.FindApiKeyEntryByName, connection))
                {
                    cmd.Parameters.AddWithValue("@name", userName);
                    using (var reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if (!reader.HasRows) return null;
                        reader.Read();
                        
                        return new ApiKey
                        {
                            Id = reader.GetInt32("ID"),
                            Name = reader.GetString("NAME"),
                            Key = reader.IsDBNull(reader.GetOrdinal("APIKEY")) ? null : reader.GetString("APIKEY"),
                            CreateTime = reader.IsDBNull(reader.GetOrdinal("CREATETIME")) ? DateTime.MinValue : reader.GetDateTime("CREATETIME")
                        };
                    }
                }
            }
        }

        public ApiKey FindByKey(string key)
        {
            using (var connection = new MySqlContext().GetConnection())
            {
                using (var cmd = new MySqlCommand(DataBaseSchema.FindApiKeyEntryByKey, connection))
                {
                    cmd.Parameters.AddWithValue("@key", key);
                    using (var reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if (!reader.HasRows) return null;
                        reader.Read();
                        return new ApiKey
                        {
                            Id = reader.GetInt32("ID"),
                            Name = reader.GetString("NAME"),
                            Key = reader.IsDBNull(reader.GetOrdinal("APIKEY")) ? null : reader.GetString("APIKEY"),
                            CreateTime = reader.IsDBNull(reader.GetOrdinal("CREATETIME")) ? DateTime.MinValue : reader.GetDateTime("CREATETIME")
                        };
                    }
                }
            }
        }
    }
}
