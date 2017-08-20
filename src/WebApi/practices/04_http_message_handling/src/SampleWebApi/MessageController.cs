using System;
using System.Net;
using System.Net.Http;
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

            return Request.CreateResponse(HttpStatusCode.OK, new ResultMessage {Message = "Hello"});

            #endregion
        }

    }

    [Serializable]
    public class ResultMessage
    {
        public string Message { get; set; }
    }
}