using System;
using System.Collections.Generic;

namespace Manualfac
{
    public class ContainerBuilder
    {
        #region Please modify the following code to pass the test

        /*
         * Hello, boys and girls. The container builder is a very good guy to store
         * all the definitions for instantiation as well as lifetime managing. Now,
         * let's forget about lifetime management.
         * 
         * ContainerBuilder however, has no idea how to create an instance. So it is
         * the users' job (func). We just store the procedure and call it when needed.
         * 
         * You can add non-public member functions or member variables as you like.
         */
        bool hasBeenBuilt;
        readonly Dictionary<Type, Func<IComponentContext, object>> registerReposity = new Dictionary<Type, Func<IComponentContext, object>>();
        public void Register<T>(Func<IComponentContext, T> func)
        {
            if(func == null) throw new ArgumentNullException(nameof(func));
            registerReposity[typeof(T)] = c => func(c);
        }

        public IComponentContext Build()
        {
            if (hasBeenBuilt)
            {
                throw new InvalidOperationException();
            }

            var componentContext = new ComponentContext(registerReposity);
            hasBeenBuilt = true;
            return componentContext;
        }

        #endregion
    }
}