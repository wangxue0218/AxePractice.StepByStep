using System;
using LocalApi;

namespace Manualfac.LocalApiIntegration
{
    public class ManualfacDependencyResolver : IDependencyResolver
    {
        #region Please implement the following class

        /*
         * We should create a manualfac dependency resolver so that we can integrate it
         * to LocalApi.
         * 
         * You can add a public/internal constructor and non-public fields if needed.
         */
        readonly ILifetimeScope container;
        readonly IDependencyScope dependencyScope;
        public ManualfacDependencyResolver(Container rootScope)
        {
            container = rootScope;
            dependencyScope = new ManualfacDependencyScope(rootScope);
        }

        public void Dispose()
        {
            container.Dispose();
        }

        public object GetService(Type type)
        {
            return dependencyScope.GetService(type);
        }

        public IDependencyScope BeginScope()
        {
            return new ManualfacDependencyScope(container);
        }

        #endregion
    }
}