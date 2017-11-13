using FluentNHibernate.Mapping;

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
     public class AddressMap : ClassMap<Address>
     {
         public AddressMap()
         {
             Table("Person.Address");
             Id(a => a.Id, "AddressID");
             Map(a => a.AddressLine1);
             Map(a => a.AddressLine2);
             Map(a => a.City);
             Map(a => a.PostalCode);
         }
     }

    #endregion
}