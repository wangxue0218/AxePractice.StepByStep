using System;
using System.Collections.Generic;
using System.Linq;
using Manualfac.Services;
using Manualfac.Sources;

namespace Manualfac
{
    public class ComponentRegistry
    {
        readonly Dictionary<Service, ComponentRegistration> serviceInfos =
            new Dictionary<Service, ComponentRegistration>();

        readonly List<IRegistrationSource> sources = new List<IRegistrationSource>();

        public void Register(ComponentRegistration registration)
        {
            if (registration == null) { throw new ArgumentNullException(nameof(registration)); }
            serviceInfos[registration.Service] = registration;
        }

        public bool TryGetRegistration(Service service, out ComponentRegistration registration)
        {
            if (serviceInfos.ContainsKey(service))
            {
                registration = serviceInfos[service];
                return true;
            }

            #region Please implement the source query logic

            /*
             * The following code will go through the source list and find the first source who
             * can match the service. If no-one can match, then query failed. The function will 
             * return fasle. If we have found one. Then create a concrete component registration
             * and add it to serviceInfos for speed acceleration.
             */
            var componentRegisteration = sources.Select(s => s.RegistrationFor(service)).FirstOrDefault(e => e != null);
            if (componentRegisteration != null)
            {
                registration = componentRegisteration;
                Register(registration);
                return true;
            }

            registration = null;
            return false;
            #endregion
        }

        public void RegisterSource(IRegistrationSource source)
        {
            if (source == null) { throw new ArgumentNullException(nameof(source)); }
            sources.Add(source);
        }
    }
}