using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;

namespace LocalApi
{
    static class ControllerActionInvoker
    {
        #region Please modify the code to pass the test

        /*
         * Now we will create a framework just like the ASP.NET WebApi. We called
         * it LocalApi. The core part of the Api framework is generating response.
         * And the most important type for generating response is the HttpController.
         * In this practice, we try invoking the specified action of the controller
         * to generate a response.
         * 
         * The class to invoke controller action is called a ControllerActionInvoker,
         * and the entry point is called InvokeAction. It accepts an actionDescriptor
         * which contains the instance of the controller (currently we have no idea
         * where it comes from), the name of the action to invoke. For simplicity,
         * we assume that all actions contains no parameter.
         */

        public static HttpResponseMessage InvokeAction(ActionDescriptor actionDescriptor)
        {
            Type type = actionDescriptor.Controller.GetType();
            var instance = Activator.CreateInstance(type);

            var methodInfo = type.GetMethod(
                actionDescriptor.ActionName,
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if(methodInfo == null) return new HttpResponseMessage(HttpStatusCode.NotFound);

            try
            {
                return (HttpResponseMessage)methodInfo.Invoke(instance, null);
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        #endregion
    }
}
