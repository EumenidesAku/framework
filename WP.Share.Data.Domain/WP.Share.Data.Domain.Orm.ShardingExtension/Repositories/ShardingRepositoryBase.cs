using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Share.Data.Dapper.Contrib;
using WP.Share.Data.Domain.Core.ShardingExtension;
using WP.Share.Data.Domain.Orm.Repositories;
using WP.Share.Data.Domain.Orm.ShardingExtension.Extensions;
using WP.Share.Data.Domain.Orm.ShardingExtension.Route;

namespace WP.Share.Data.Domain.Orm.ShardingExtension.Repositories
{
    /// <summary>
    /// 分库分表 仓储  例如：订单，佣金
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class ShardingRepositoryBase<TEntity> : IShardingRepository<TEntity>
       where TEntity : class, IEntity<long>
    {
        public ShardingRoute Route { get; set; }

        public IDbConnection DbConnection { get; }

        public virtual TEntity Get(long id)
        {
            TableInfo info = Route.RouteById(id);
            return info.DbConnection.GetSharding<TEntity>(info.GetTableName<TEntity>(), id);
        }

        public virtual bool Update(TEntity entity)
        {
            TableInfo info = Route.RouteById(entity.Id);
            return info.DbConnection.UpdateSharding(info.GetTableName<TEntity>(), entity);
        }

        public virtual long Insert(TEntity entity)
        {
            TableInfo info = Route.LastTableInfo;
            return info.DbConnection.InsertSharding(info.GetTableName<TEntity>(), entity);
        }

        public virtual bool Delete(long id)
        {
            TableInfo info = Route.RouteById(id);
            return info.DbConnection.DeleteShardingById<TEntity>(info.GetTableName<TEntity>(), id);
        }

        public TEntity Get(IDbConnection conn, long id, IDbTransaction tran = null)
        {
            TableInfo info = Route.RouteById(id);
            return conn.GetSharding<TEntity>(info.GetTableName<TEntity>(), id, tran);
        }

        public long Insert(IDbConnection conn, TEntity entity, IDbTransaction tran = null)
        {
            TableInfo info = Route.LastTableInfo;
            return conn.InsertSharding(info.GetTableName<TEntity>(), entity, tran);
        }

        public bool Update(IDbConnection conn, TEntity entity, IDbTransaction tran = null)
        {
            TableInfo info = Route.RouteById(entity.Id);
            return conn.UpdateSharding(info.GetTableName<TEntity>(), entity, tran);
        }

        public bool Delete(IDbConnection conn, long id, IDbTransaction tran = null)
        {
            TableInfo info = Route.RouteById(id);
            return conn.DeleteShardingById<TEntity>(info.GetTableName<TEntity>(), id, tran);
        }

        public TEntity Get(TableInfo table, long id, IDbConnection conn = null, IDbTransaction tran = null)
        {
            if (conn == null)
            {
                conn = table.DbConnection;
            }
            return conn.GetSharding<TEntity>(table.GetTableName<TEntity>(), id, tran);
        }

        public long Insert(TableInfo table, TEntity entity, IDbConnection conn = null, IDbTransaction tran = null)
        {
            if (conn == null)
            {
                conn = table.DbConnection;
            }
            return conn.InsertSharding<TEntity>(table.GetTableName<TEntity>(), entity, tran);
        }

        public bool Update(TableInfo table, TEntity entity, IDbConnection conn = null, IDbTransaction tran = null)
        {
            if (conn == null)
            {
                conn = table.DbConnection;
            }
            return conn.UpdateSharding<TEntity>(table.GetTableName<TEntity>(), entity, tran);
        }

        public bool Delete(TableInfo table, long id, IDbConnection conn = null, IDbTransaction tran = null)
        {
            if (conn == null)
            {
                conn = table.DbConnection;
            }
            return conn.DeleteShardingById<TEntity>(table.GetTableName<TEntity>(), id, tran);
        }
    }
}
