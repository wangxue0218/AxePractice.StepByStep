using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;

namespace LocalApi
{
    static class ControllerActionInvoker
    {
        public static HttpResponseMessage InvokeAction(ActionDescriptor actionDescriptor)
        {
            Type type = actionDescriptor.Controller.GetType();
            var instance = Activator.CreateInstance(type);
            var methodInfo = type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .SingleOrDefault(m => m.Name.ToLower().Equals(actionDescriptor.ActionName.ToLower()));

            if (methodInfo == null) return new HttpResponseMessage(HttpStatusCode.NotFound);
            object resultValue;
            try
            {
                resultValue = methodInfo.Invoke(instance, null);

            }
            catch (TargetInvocationException ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            return (HttpResponseMessage)resultValue;
        }
    }
}
