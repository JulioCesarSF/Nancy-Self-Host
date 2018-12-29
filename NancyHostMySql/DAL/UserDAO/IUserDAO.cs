using NancyHostMySql.DAL.Models;

namespace NancyHostMySql.DAL.UserDAO
{
    public interface IUserDAO : IGenericDAO<User>
    {
        bool Find(User user);
    }
}
