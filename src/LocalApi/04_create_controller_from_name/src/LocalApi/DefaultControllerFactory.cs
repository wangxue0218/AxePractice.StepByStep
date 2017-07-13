using System;
using System.Collections.Generic;
using System.Linq;

namespace LocalApi
{
    class DefaultControllerFactory : IControllerFactory
    {
        public HttpController CreateController(
            string controllerName,
            ICollection<Type> controllerTypes,
            IDependencyResolver resolver)
        {
            #region Please modify the following code to pass the test.

            /*
             * The controller factory will create controller by its name. It will search
             * form the controllerTypes collection to get the correct controller type,
             * then create instance from resolver.
             */
            Type controllerType;
            try
            {
                controllerType =
                    controllerTypes.SingleOrDefault(
                        c => c.Name.Equals(controllerName, StringComparison.OrdinalIgnoreCase));
            }
            catch
            {
                throw new ArgumentException();
            }
            return controllerType == null ? null : (HttpController)resolver.GetService(controllerType);

            #endregion
        }
    }
}