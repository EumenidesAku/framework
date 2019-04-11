using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Share.Data.Domain.Core.ShardingExtension;

namespace WP.Share.Data.Domain.Orm.ShardingExtension.Route
{
    public interface IShardingRoute
    {
        TableInfo LastTableInfo { get; }
        TableInfo RouteById(long id);
        TableInfo RouteByTime(DateTime time);

        SortedDictionary<DateTime, TableInfo> GetAllTableInfo();
        SortedDictionary<DateTime, TableInfo> RouteBySectionTime(DateTime startTime, DateTime endTime);
    }
}
