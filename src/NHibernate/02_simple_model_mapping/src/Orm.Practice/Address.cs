using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using FluentNHibernate.Mapping;
using NHibernate.Event.Default;

namespace Orm.Practice
{
    public class Address
    {
        #region you can change something here if needed

        /*
         * But you cannot change the name of the property XD~.
         */

        public virtual int Id { get; set; }
        public virtual string AddressLine1 { get; set; }
        public virtual string AddressLine2 { get; set; }
        public virtual string City { get; set; }
        public virtual string PostalCode { get; set; }

        #endregion
    }

    #region you may want to add a mapping class in order to do the query

    /*
     * Before querying object using the `ISession` object, we have to map object
     * first. This can be done by using an mapping class.
     *
     * Since the `AddressID` is primary key of the table while we would like to
     * use `Id` as its name in C#, we should explicitly specify its name as
     * `AddressID`.
     */
     public class AddressMap : IAutoMappingOverride<Address>
     {
         public void Override(AutoMapping<Address> mapping)
         {
            mapping.Table("[Person].[Address]");
            mapping.Id(m => m.Id).Column("AddressId");
        }
    }

    #endregion
}