using System.Linq;
using NHibernate;
using Orm.Practice.Entities;
using Xunit;
using Xunit.Abstractions;

namespace Orm.Practice
{
    public class OneToManyModifyWhenSetInverseFalseFacts : OrmFactBase
    {
        public OneToManyModifyWhenSetInverseFalseFacts(ITestOutputHelper output) : base(output)
        {
            ExecuteNonQuery("DELETE FROM [dbo].[parent] WHERE IsForQuery=0");
            ExecuteNonQuery("DELETE FROM [dbo].[child] WHERE IsForQuery=0");
        }

        [Fact]
        public void should_insert_parent_and_children()
        {
            SaveParentAndChildren(
                "nq-parent-1", new [] {"nq-child-1-parent-1"});
            Session.Clear();
        }

        [Fact]
        public void should_delete_in_a_cascade_manner()
        {
            SaveParentAndChildren(
                "nq-parent-1",
                new [] { "nq-child-1-parent-1" });
            Session.Clear();

            DeleteParentAndChild("nq-parent-1");
            Session.Clear();
        }


        void DeleteParentAndChild(string parentName)
        {
            var parents = Session.Query<Parent>().Where(p => p.Name == parentName).ToList();

            parents.ForEach(p => Session.Delete(p));

            Session.Flush();
        }

        void SaveParentAndChildren(string parentName, string[] childrenNames)
        {
            var parent = new Parent
            {
                IsForQuery = false,
                Name = parentName
            };

            parent.Children = childrenNames.Select(c => new Child {IsForQuery = false, Name = c, Parent = parent}).ToList();

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