using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NancyHostMySql.DAL.Models;

namespace NancyHostMySql.DAL.ApiKeyDAO
{
    public interface IApiKeyDAO : IGenericDAO<ApiKey>
    {
        ApiKey FindByName(string userName);
        ApiKey FindByKey(string key);
        bool DeleteByKey(ApiKey entity);
    }
}
