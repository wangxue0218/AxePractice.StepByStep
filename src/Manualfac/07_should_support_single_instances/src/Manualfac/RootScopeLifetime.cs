using System;

namespace Manualfac
{
    class RootScopeLifetime : IComponentLifetime
    {
        public ILifetimeScope FindLifetimeScope(ILifetimeScope mostNestedLifetimeScope)
        {
            #region Please implement this method

            /*
             * This class will always create and share instaces in root scope.
             */

            ILifetimeScope lifetimeScope = mostNestedLifetimeScope;
            while (lifetimeScope.RootScope != null)
            {
                lifetimeScope = lifetimeScope.RootScope;
            }

            return lifetimeScope;

            #endregion
        }
    }
}