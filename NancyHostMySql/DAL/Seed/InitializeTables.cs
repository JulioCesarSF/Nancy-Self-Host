using NancyHostMySql.DAL.ApiKeyDAO;
using NancyHostMySql.DAL.Models;
using NancyHostMySql.DAL.UserDAO;

namespace NancyHostMySql.DAL.Seed
{
    public sealed class InitializeTables
    {

        public InitializeTables()
        {
            SeedUser();
            SeedApiKey();
        }

        private void SeedUser()
        {
            var userDao = new UserDAOImpl();
            var admin = new User
            {
                Name = "admin",
                Password = "1234"
            };
            if (!userDao.Find(admin))
            {
                userDao.Add(admin);
            }
        }

        private void SeedApiKey()
        {
        }
    }
}
