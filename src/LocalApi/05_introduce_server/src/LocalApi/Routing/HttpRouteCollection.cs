using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace LocalApi.Routing
{
    public class HttpRouteCollection
    {
        #region Please implement the following method to pass the test

        /*
         * An http route collection stores all the routes for application. You can
         * add additional field or private method but you should not modify the 
         * public interfaces.
         */
        readonly List<HttpRoute> routes = new List<HttpRoute>();
        public void Add(HttpRoute route)
        {
            if(route == null) throw new ArgumentNullException(nameof(route));
            if(route.UriTemplate == null) throw new ArgumentException();
            routes.Add(route);
        }

        public HttpRoute GetRouteData(HttpRequestMessage request)
        {
            if(request == null) throw new ArgumentNullException(nameof(request));
            return routes.FirstOrDefault(r => r.IsMatch(request.RequestUri, request.Method));
        }

        #endregion
    }
}