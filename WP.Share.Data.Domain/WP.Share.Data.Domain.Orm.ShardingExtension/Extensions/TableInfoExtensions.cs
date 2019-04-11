using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WP.Share.Data.Dapper.Contrib;
using WP.Share.Data.Domain.Core.ShardingExtension;
using WP.Share.Data.Domain.Orm.Repositories;

namespace WP.Share.Data.Domain.Orm.ShardingExtension.Extensions
{
    public static class TableInfoExtensions
    {
        public static string GetTableName<T>(this TableInfo table) where T : class, IEntity<long>
        {
            var info = typeof(T);
            string tableAttrName =
                    info.GetCustomAttribute<TableAttribute>(false)?.Name
                    ?? (info.GetCustomAttributes(false).FirstOrDefault(attr => attr.GetType().Name == "TableAttribute") as dynamic)?.Name;

            return table.TableName.Replace(ShardingConst.TableName, tableAttrName.Replace("[", string.Empty).Replace("]", string.Empty));
        }

        public static string GetHeadName<T>() where T : class, IEntity<long>
        {
            var info = typeof(T);
            string tableAttrName =
                    info.GetCustomAttribute<TableAttribute>(false)?.Name
                    ?? (info.GetCustomAttributes(false).FirstOrDefault(attr => attr.GetType().Name == "TableAttribute") as dynamic)?.Name;

            return tableAttrName.Replace("[", string.Empty).Replace("]", string.Empty);
        }
    }
}
