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
        readonly ILifetimeScope rootScope;
        public ManualfacDependencyResolver(Container rootScope)
        {
            this.rootScope = rootScope;
        }

        public void Dispose()
        {
            rootScope.Dispose();
        }

        public object GetService(Type type)
        {
            object resolved;
            rootScope.TryResolve(type, out resolved);
            return resolved;
        }

        public IDependencyScope BeginScope()
        {
            return new ManualfacDependencyScope(rootScope.BeginLifetimeScope());
        }

        #endregion
    }
}