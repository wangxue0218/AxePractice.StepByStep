﻿using System;
using System.CodeDom;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using LocalApi.Routing;

namespace LocalApi
{
    public class HttpServer : HttpMessageHandler
    {
        #region Please implement the following method to pass the test

        /*
         * An http server is an HttpMessageHandler that accept request and create response.
         * You can add non-public fields and members for help but you should not modify
         * the public interfaces.
         */

        readonly HttpConfiguration configuration;
        public HttpServer(HttpConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpRoute route = configuration.Routes.GetRouteData(request);
            if(route == null) return new HttpResponseMessage(HttpStatusCode.NotFound);
//            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound));
            return ControllerActionInvoker.InvokeAction(
                route,
                configuration.CachedControllerTypes,
                configuration.DependencyResolver,
                configuration.ControllerFactory);
        }

        #endregion
    }
}