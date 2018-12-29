using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using NancyHostMySql.DAL;

namespace NancyHostMySql.Modules
{
    public class DataBaseTest : NancyModule
    {
        public DataBaseTest() : base("db")
        {
            Get["/ping"] = _ =>
            {
                try
                {
                    using (var con = new MySqlContext())
                    {
                        if (con.GetConnection().Ping())
                        {
                            return "Connected";
                        }
                    }
                }
                catch (Exception e)
                {
                    return e.Message;
                }

                return "Failed";
            };
        }
    }
}
