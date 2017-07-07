using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;

namespace SimpleSolution.WebApp
{
    public class Bootstrapper
    {
        public static void Init(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                "get user by id",
                "user/{id}",
                new {controller = "User"},
                new {httpMethod = new HttpMethodConstraint(HttpMethod.Get)});
        }
    }
}