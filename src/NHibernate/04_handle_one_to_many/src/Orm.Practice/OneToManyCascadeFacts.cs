using System.Linq;
using NHibernate;
using Orm.Practice.Entities;
using Xunit;
using Xunit.Abstractions;

namespace Orm.Practice
{
    public class OneToManyModifyCascadeFacts : OrmFactBase
    {
        public OneToManyModifyCascadeFacts(ITestOutputHelper output) : base(output)
        {
            ExecuteNonQuery("DELETE FROM [dbo].[parent] WHERE IsForQuery=0");
            ExecuteNonQuery("DELETE FROM [dbo].[child] WHERE IsForQuery=0");
        }


        [Fact]
        public void should_remove_child_from_parent()
        {
            SaveParentAndChildren(
                "nq-parent-1",
                new [] { "nq-child-1-parent-1" });
            Session.Clear();

            DeleteChildFromParent("nq-parent-1", "nq-child-1-parent-1");
            Session.Clear();
        }


        void DeleteChildFromParent(string parentName, string childName)
        {
            var parent = Session.Query<Parent>().Single(p => p.Name == parentName);
            var child = Session.Query<Child>().Single(p => p.Name == childName);
            parent.Children.Remove(child);

            Session.Flush();
        }

        void SaveParentAndChildren(string parentName, string[] childrenNames)
        {
            var parent = new Parent
            {
                IsForQuery = false,
                Name = parentName
            };

            parent.Children = childrenNames.Select(c => new Child {IsForQuery = false, Name = c}).ToList();

            Session.Save(parent);

            Session.Flush();
        }

        void ExecuteNonQuery(string sql)
        {
            ISQLQuery query = StatelessSession.CreateSQLQuery(sql);
            query.ExecuteUpdate();
        }
    }
}