using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using Nancy.Authentication.Stateless;
using Nancy.Extensions;
using Nancy.Responses;
using Nancy.Security;
using NancyHostMySql.DAL.ApiKeyDAO;

namespace NancyHostMySql.Modules
{
    public class MainModule : NancyModule
    {
        public MainModule() : base("main")
        {
            StatelessAuthentication.Enable(this, GetConfiguration());
            Get["/"] = _ => { return View["index.html"]; };
            Post["/test"] = _ => { return "OK"; };
        }

        private StatelessAuthenticationConfiguration GetConfiguration()
        {
            return new StatelessAuthenticationConfiguration(ctx =>
            {
                var isJsonRequest = false;
                var apiKey = RequestHelper.GetApiKey(ctx);

                if (string.IsNullOrEmpty(apiKey))
                {
                    ctx.Response = new Response { StatusCode = HttpStatusCode.Unauthorized };
                    return null;
                }

                var apikeyDao = new ApiKeyDAOImpl();
                var key = apikeyDao.FindByKey(apiKey);

                if (key != null)
                {
                    if (!key.IsValidKey())
                    {
                        key.Key = null;
                        key.CreateTime = DateTime.MinValue;
                        apikeyDao.Update(key);
                    }
                    else
                    {
                        return key;
                    }
                }

                if (key is null && isJsonRequest)
                {
                    ctx.Response = Response.AsJson(new {message = "Invalid Key"}, HttpStatusCode.BadRequest);
                    return null;
                }

                ctx.Response = new RedirectResponse("user", RedirectResponse.RedirectType.SeeOther);
                return null;
            });
        }
    }
}
