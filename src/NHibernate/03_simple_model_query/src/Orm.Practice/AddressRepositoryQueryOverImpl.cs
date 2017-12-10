using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Util;

namespace Orm.Practice
{
    public class AddressRepositoryQueryOverImpl : RepositoryBase, IAddressRepository
    {
        public AddressRepositoryQueryOverImpl(ISession session) : base(session)
        {
        }

        public Address Get(int id)
        {
            #region Please implement the method

            return Session.QueryOver<Address>().Where(a => a.Id == id).SingleOrDefault();

            #endregion
        }

        public IList<Address> Get(IEnumerable<int> ids)
        {
            #region Please implement the method

            return Session.QueryOver<Address>()
                .WhereRestrictionOn(e => e.Id)
                .IsIn(ids.ToList())
                .OrderBy(e => e.Id).Asc
                .List();

            #endregion
        }

        public IList<Address> GetByCity(string city)
        {
            #region Please implement the method

            return Session.QueryOver<Address>()
                .Where(a => a.City == city)
                .OrderBy(a => a.Id).Asc
                .List();

            #endregion
        }

        public Task<IList<Address>> GetByCityAsync(string city)
        {
            #region Please implement the method

            return GetByCityAsync(city, CancellationToken.None);

            #endregion
        }

        public async Task<IList<Address>> GetByCityAsync(string city, CancellationToken cancellationToken)
        {
            #region Please implement the method

            return await Session.QueryOver<Address>()
                .Where(a => a.City == city)
                .OrderBy(a => a.Id).Asc
                .ListAsync(cancellationToken);

            #endregion
        }

        public IList<KeyValuePair<int, string>> GetOnlyTheIdAndTheAddressLineByCity(string city)
        {
            #region Please implement the method

            return Session.QueryOver<Address>()
                .Where(a => a.City == city)
                .List<Address>()
                .Select(a => new KeyValuePair<int, string>(a.Id, a.AddressLine1))
                .OrderBy(a => a.Key)
                .ToList();

            #endregion
        }

        public IList<string> GetPostalCodesByCity(string city)
        {
            #region Please implement the method

            return Session.QueryOver<Address>()
                .Where(a => a.City == city)
                .Select(Projections.Distinct(Projections.Property<Address>(e => e.PostalCode)))
                .List<String>();

            #endregion
        }
    }
}