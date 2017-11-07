using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace SampleWebApi
{
    public class MessageController : ApiController
    {
        public HttpResponseMessage Get()
        {
            #region Please modify the code to pass the test

            // Please note that you may have to run this program in IIS or IISExpress first in
            // order to pass the test.
            // You can add new files if you want. But you cannot change any existed code.
            IContentNegotiator contentNegotiator = Configuration.Services.GetContentNegotiator();
            ContentNegotiationResult contentNegotiationResult = contentNegotiator.Negotiate(typeof(object), Request, Configuration.Formatters);
            if (contentNegotiationResult == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotAcceptable);
            }

            return Request.CreateResponse(HttpStatusCode.OK,
               new ResultMessage{Message = "Hello"},
               contentNegotiationResult.Formatter,
               contentNegotiationResult.MediaType);

            #endregion
        }

    }

    public class ResultMessage
    {
        public string Message { get; set; }
    }
}