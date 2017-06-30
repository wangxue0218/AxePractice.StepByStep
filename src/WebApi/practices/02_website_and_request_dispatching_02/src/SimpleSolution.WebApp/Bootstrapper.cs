using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;

namespace SimpleSolution.WebApp
{
    public class Bootstrapper
    {
        public static void Init(HttpConfiguration configuration)
        {
            // Note. Since response message generation is out of scope
            // of our test. So I have create an extension method called
            // Request.Text(HttpStatusCode, string) to help you generating
            // a textual response.

            configuration.Routes.MapHttpRoute(
                "get user dependent by id",
                "users/{id}/dependents",
                new {controller = "Users", action = "GetUserDepedentById"},
                new {httpMethod = new HttpMethodConstraint(HttpMethod.Get), id = @"\d+" });

            configuration.Routes.MapHttpRoute(
                "get user dependents",
                "users/dependents",
                new {controller = "Users", action = "GetUserDepedents"},
                new {httpMethod = new HttpMethodConstraint(HttpMethod.Get)});


            configuration.Routes.MapHttpRoute(
                "get user by id",
                "users/{id}",
                new {controller = "Users", action = "GetUserById"},
                new {httpMethod = new HttpMethodConstraint(HttpMethod.Get), id = @"\d+" });

            configuration.Routes.MapHttpRoute(
                "update user by id",
                "users/{id}",
                new {controller = "Users", action = "UpdateUserById"},
                new {httpMethod = new HttpMethodConstraint(HttpMethod.Put), id = @"\d+" });

            configuration.Routes.MapHttpRoute(
                "get user by name",
                "users",
                new { controller = "Users", action = "GetUserByName" },
                new { httpMethod = new HttpMethodConstraint(HttpMethod.Get)});
        }
    }
}