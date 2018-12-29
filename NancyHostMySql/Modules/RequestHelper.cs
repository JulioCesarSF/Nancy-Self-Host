using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using Nancy.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NancyHostMySql.Modules
{
    public static class RequestHelper
    {
        public static string GetApiKey(NancyContext ctx)
        {
            //string query
            string apiKey = ctx.Request.Query.Count > 0 ? (string)ctx.Request.Query["apyKey"] : null;
            if (string.IsNullOrEmpty(apiKey))
            {
                //form
                apiKey = (string)ctx.Request.Form["apiKey"];
                if (string.IsNullOrEmpty(apiKey))
                {
                    //cookie
                    ctx.Request.Cookies.TryGetValue("apikey", out apiKey);
                    
                    if (string.IsNullOrEmpty(apiKey))
                    {
                        //body
                        var bodyString = ctx.Request.Body.AsString(Encoding.UTF8);
                        dynamic jsonObject = JsonConvert.DeserializeObject<ExpandoObject>(bodyString);
                        apiKey = jsonObject?.apiKey;
                    }
                }
            }
            return apiKey;
        }
    }
}
