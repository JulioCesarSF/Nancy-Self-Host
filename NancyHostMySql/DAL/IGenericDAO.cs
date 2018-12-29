using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NancyHostMySql.DAL
{
    public interface IGenericDAO<T> where T : class
    {
        bool Add(T entity);
        bool Update(T entity);
        List<T> GetAll();
        bool Delete(T entity);
    }
}
