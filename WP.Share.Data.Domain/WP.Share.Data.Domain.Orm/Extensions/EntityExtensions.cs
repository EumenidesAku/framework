using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WP.Share.Data.Dapper.Contrib;
using WP.Share.Data.Domain.Core.Entities;
using WP.Share.Data.Domain.Orm.Repositories;

namespace WP.Share.Data.Domain.Orm.Extensions
{
    public static class EntityExtensions
    {
        public static string GetTableName<TPrimaryKey>(this IEntity<TPrimaryKey> entity)
        {
            var info = entity.GetType();
            var tableAttrName =
                    info.GetCustomAttribute<TableAttribute>(false)?.Name
                    ?? (info.GetCustomAttributes(false).FirstOrDefault(attr => attr.GetType().Name == "TableAttribute") as dynamic)?.Name;
            return tableAttrName;
        }
    }
}
