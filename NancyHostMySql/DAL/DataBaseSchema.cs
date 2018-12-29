using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NancyHostMySql.DAL
{
    public static class DataBaseSchema
    {
        //user table
        public static string TableUser = "user";
        public static string TableUserExists = $"SHOW TABLES LIKE '{TableUser}'";
        public static string CreateTableUser = $"CREATE TABLE {TableUser} (ID int(200) NOT NULL auto_increment, NAME varchar(50) NOT NULL, PASSWORD varchar(600), PRIMARY KEY (ID) )";
        public static string InsertUser = $"INSERT INTO {TableUser} (NAME, PASSWORD) VALUE(@name, @password)";
        public static string FindUserByNameAndPassword = $"SELECT * FROM {TableUser} WHERE NAME=@name AND PASSWORD=@password";

        //apikey table
        public static string TableApiKey = "apikey";
        public static string TableApiKeyExists = $"SHOW TABLES LIKE '{TableApiKey}'";
        public static string CreateTableApiKey =
            $"CREATE TABLE {TableApiKey} (ID int(200) NOT NULL auto_increment, NAME varchar(50) NOT NULL, APIKEY varchar(600), CREATETIME DATETIME, PRIMARY KEY(ID))";
        public static string UpdateApiKeyByName = $"UPDATE {TableApiKey} SET APIKEY = @key, CREATETIME = @createTime WHERE NAME = @name";
        public static string NewApiKeyEntry = $"INSERT INTO {TableApiKey} (NAME, APIKEY, CREATETIME) VALUES (@name, @key, @createTime)";
        public static string FindApiKeyEntryByName = $"SELECT * FROM {TableApiKey} WHERE NAME = @name";
        public static string FindApiKeyEntryByKey = $"SELECT * FROM {TableApiKey} WHERE APIKEY = @key";
        public static string DeleteApiKey = $"DELETE FROM {TableApiKey} WHERE KEY=@key";
    }
}
