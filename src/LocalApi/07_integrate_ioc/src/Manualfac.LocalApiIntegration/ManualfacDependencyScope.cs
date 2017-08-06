using System;
using LocalApi;
using Manualfac.Services;

namespace Manualfac.LocalApiIntegration
{
    class ManualfacDependencyScope : IDependencyScope
    {
        #region Please implement the class

        /*
         * We should create a manualfac dependency scope so that we can integrate it
         * to LocalApi.
         * 
         * You can add a public/internal constructor and non-public fields if needed.
         */
        readonly ILifetimeScope lifetimeScope;

        public ManualfacDependencyScope(ILifetimeScope lifetimeScope)
        {
            this.lifetimeScope = lifetimeScope;
        }

        public void Dispose()
        {
            lifetimeScope.Dispose();
        }

        public object GetService(Type type)
        {
            object resolved;
            lifetimeScope.TryResolve(type, out resolved);
            return resolved;
        }

        #endregion
    }
}