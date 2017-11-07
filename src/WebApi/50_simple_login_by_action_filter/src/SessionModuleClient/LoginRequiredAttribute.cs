using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;
using System.Web.Http.Filters;

namespace SessionModuleClient
{
    public class LoginRequiredAttribute : ActionFilterAttribute
    {
        public override bool AllowMultiple { get; } = false;

        public override async Task OnActionExecutingAsync(
            HttpActionContext context, 
            CancellationToken cancellationToken)
        {
            #region Please implement the method

            // This filter will try resolve session cookies. If the cookie can be
            // parsed correctly, then it will try calling session API to get the
            // specified session. To ease user session access, it will store the
            // session object in request message properties.
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            HttpRequestMessage request = context.Request;
            string token = GetSessionToken(request);

            UserSessionDto session = await GetSessionAsync(context, cancellationToken, token);
            request.SetUserSession(session);
            #endregion
        }

        static async Task<UserSessionDto> GetSessionAsync(HttpActionContext context, CancellationToken cancellationToken, string token)
        {
            IDependencyScope scope = context.Request.GetDependencyScope();
            HttpClient client = (HttpClient)scope.GetService(typeof(HttpClient));
            Uri requestUri = context.Request.RequestUri;
            HttpResponseMessage response = await client.GetAsync($"{requestUri.Scheme}://{requestUri.UserInfo}{requestUri.Authority}/session/{token}");
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }
            UserSessionDto session = await response.Content.ReadAsAsync<UserSessionDto>(
                context.ControllerContext.Configuration.Formatters,
                cancellationToken);

            return session;
        }

        string GetSessionToken(HttpRequestMessage request)
        {

            const string sessionCookieKey = "X-Session-Token";
            Collection<CookieHeaderValue> cookieHeaderValues = request.Headers.GetCookies(sessionCookieKey);
            //Cookie:key=value;key=value; key可以重复
            //Cookie:key=value;key=value; 可以有多个Cookie
            CookieState sessionCookie = cookieHeaderValues
//                .Where(chv => chv.Expires == null || chv.Expires > DateTimeOffset.Now)
                .SelectMany(chv => chv.Cookies)
                .FirstOrDefault(c => c.Name == sessionCookieKey);
            if (sessionCookie == null)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }
            string sessionCookieValue = sessionCookie.Value;
            if (string.IsNullOrEmpty(sessionCookieValue))
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }
            return sessionCookieValue;
        }
    }
}