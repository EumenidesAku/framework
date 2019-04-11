using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Share.Data.Dapper.Contrib;
using WP.Share.Data.Domain.Core.Entities;

namespace WP.Share.Data.Domain.Orm.Repositories
{
    /// <summary>
    /// 单表仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public abstract class RepositoryBase<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
       where TEntity : class, IEntity<TPrimaryKey>
    {

        public abstract IDbConnection DbConnection { get; }


        public bool Delete(TPrimaryKey id)
        {
            return DbConnection.DeleteById<TEntity>(id);
        }

        public bool Delete(IDbConnection conn, TPrimaryKey id, IDbTransaction tran = null)
        {
            return conn.DeleteById<TEntity>(id);
        }

        public TEntity Get(TPrimaryKey id)
        {
            return DbConnection.Get<TEntity>(id);
        }

        public TEntity Get(IDbConnection conn, TPrimaryKey id, IDbTransaction tran = null)
        {
            return conn.Get<TEntity>(id, tran);
        }

        public long Insert(TEntity entity)
        {
            return DbConnection.Insert<TEntity>(entity);
        }

        public long Insert(IDbConnection conn, TEntity entity, IDbTransaction tran = null)
        {
            return conn.Insert<TEntity>(entity, tran);
        }

        public bool Update(TEntity entity)
        {
            return DbConnection.Update<TEntity>(entity);
        }

        public bool Update(IDbConnection conn, TEntity entity, IDbTransaction tran = null)
        {
            return conn.Update<TEntity>(entity, tran);
        }
    }
}
