using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Share.Data.Domain.Core.ShardingExtension;
using WP.Share.Data.Domain.Orm.Repositories;
using WP.Share.Data.Domain.Orm.ShardingExtension.Route;

namespace WP.Share.Data.Domain.Orm.ShardingExtension.Repositories
{
    public interface IShardingRepository<TEntity> : IRepository<TEntity, long> where TEntity : class, IEntity<long>
    {
        ShardingRoute Route { get; set; }

        #region 调用方法需要传入tableInfo。作用：1.避免多次路由
        TEntity Get(TableInfo table, long id, IDbConnection conn=null,IDbTransaction tran = null);

        long Insert(TableInfo table, TEntity entity, IDbConnection conn = null, IDbTransaction tran = null);

        bool Update(TableInfo table, TEntity entity, IDbConnection conn = null, IDbTransaction tran = null);

        bool Delete(TableInfo table, long id, IDbConnection conn = null, IDbTransaction tran = null);
        #endregion
    }
}
