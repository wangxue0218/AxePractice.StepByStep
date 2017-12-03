using System;
using System.Collections.Generic;
using FluentNHibernate;
using FluentNHibernate.Automapping;

namespace Orm.Practice
{
    public class TypeSpecificAutomappingConfigration : DefaultAutomappingConfiguration
    {
        static readonly HashSet<Type> types = new HashSet<Type>
        {
            typeof(Address)
        };

        public override bool ShouldMap(Type type)
        {
            return type != null && types.Contains(type);
        }
    }
}