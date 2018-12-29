using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.Security;

namespace NancyHostMySql.DAL.Models
{
    public class ApiKey : IUserIdentity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public DateTime CreateTime { get; set; }

        //max valid time for a key in seconds
        private int Max = 60;

        public bool IsValidKey()
        {
            var tempCreateTime = CreateTime.AddSeconds(Max);
            return CreateTime != DateTime.MinValue && tempCreateTime.Subtract(DateTime.Now).Seconds > 0;
        }

        public string UserName
        {
            get => Name;
        }
        public IEnumerable<string> Claims { get; }
    }
}
