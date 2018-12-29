using Nancy;
using NancyHostMySql.DAL.ApiKeyDAO;
using NancyHostMySql.DAL.Models;
using NancyHostMySql.DAL.UserDAO;
using System;
using System.Security.Cryptography;
using System.Text;
using Nancy.Authentication.Stateless;
using Nancy.Responses;

namespace NancyHostMySql.Modules
{
    public class UserModule : NancyModule
    {
        private DateTime CreateTime;
        public UserModule() : base("user")
        {
            StatelessAuthentication.Enable(this, GetConfiguration());
            Get["/"] = _ => { return View["login.html"]; };
            Post["/"] = Login;
        }

        private Response Login(object arg)
        {
            var user = (string)this.Request.Form.Name;
            var password = (string)this.Request.Form.Password;
            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
            {
                return new Response { StatusCode = HttpStatusCode.Unauthorized };
            }

            var userDao = new UserDAOImpl();
            if (userDao.Find(new User
            {
                Name = user,
                Password = password
            }))
            {
                var apiKeyDao = new ApiKeyDAOImpl();
                var apiKey = apiKeyDao.FindByName(user);
                if (apiKey is null)
                {
                    apiKey = new ApiKey
                    {
                        Name = user,
                        Key = CreateApiKey(user, out CreateTime),
                        CreateTime = this.CreateTime
                    };
                    apiKeyDao.Add(apiKey);
                }
                else
                {
                    if (!apiKey.IsValidKey())
                    {
                        apiKey.Key = CreateApiKey(user, out CreateTime);
                        apiKey.CreateTime = this.CreateTime;
                        apiKeyDao.Update(apiKey);
                    }
                }

                return this.Response.AsJson(new { ApiKey = apiKey.Key, Redirect = "main" });
            }

            return new Response { StatusCode = HttpStatusCode.Unauthorized };
        }

        private string CreateApiKey(string user, out object createTime)
        {
            return CreateApiKey(user, out createTime);
        }

        private string CreateApiKey(string userName, out DateTime createTime)
        {
            createTime = DateTime.Now;
            using (var sha256 = new SHA256Managed())
            {
                var textArrayBytes = Encoding.UTF8.GetBytes(userName + createTime);
                var hashCompute = sha256.ComputeHash(textArrayBytes);
                return BitConverter.ToString(hashCompute).Replace("-", string.Empty);
            }
        }

        private StatelessAuthenticationConfiguration GetConfiguration()
        {
            return new StatelessAuthenticationConfiguration(ctx =>
            {
                var apiKey = RequestHelper.GetApiKey(ctx);

                if (string.IsNullOrEmpty(apiKey)) return null;

                var apikeyDao = new ApiKeyDAOImpl();
                var key = apikeyDao.FindByKey(apiKey);

                if (key != null && key.IsValidKey())
                {
                    ctx.Response = new RedirectResponse("main", RedirectResponse.RedirectType.SeeOther);
                }

                return key;
            });
        }
    }
}
