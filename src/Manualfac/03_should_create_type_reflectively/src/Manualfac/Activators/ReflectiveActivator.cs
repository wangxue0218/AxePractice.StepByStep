using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Manualfac.Services;

namespace Manualfac.Activators
{
    class ReflectiveActivator : IInstanceActivator
    {
        readonly Type serviceType;

        public ReflectiveActivator(Type serviceType)
        {
            this.serviceType = serviceType;
        }

        #region Please modify the following code to pass the test

        /*
         * This method create instance via reflection. Try evaluating its parameters and
         * inject them using componentContext.
         * 
         * No public members are allowed to add.
         */

        public object Activate(IComponentContext componentContext)
        {
            var constructorInfos = serviceType.GetConstructors();
            if(constructorInfos.Length != 1) throw new DependencyResolutionException();

            var constructor = constructorInfos.First();

            var parameters = constructor.GetParameters()
                .Select(p => componentContext.ResolveComponent(new TypedService(p.ParameterType)))
                .ToArray();
            return constructor.Invoke(parameters);
        }

        #endregion
    }
}