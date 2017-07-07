using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SimpleSolution.WebApp
{
    public class UserController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetUserById(int id)
        {
            return Request.Text(HttpStatusCode.OK, $"get user by id({id})");
        }
    }
}