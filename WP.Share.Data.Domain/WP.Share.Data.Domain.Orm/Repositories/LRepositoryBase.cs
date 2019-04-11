using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Share.Data.Dapper.Contrib;

namespace WP.Share.Data.Domain.Orm.Repositories
{
    public abstract class LRepositoryBase<TEntity> : IRepository<TEntity, long>
       where TEntity : class, IEntity<long>
    {
        public abstract IDbConnection DbConnection { get; }

        public bool Delete(long id)
        {
            return DbConnection.DeleteById<TEntity>(id);
        }

        public bool Delete(IDbConnection conn, long id, IDbTransaction tran = null)
        {
            return conn.DeleteById<TEntity>(id);
        }

        public TEntity Get(long id)
        {
            return DbConnection.Get<TEntity>(id);
        }

        public TEntity Get(IDbConnection conn, long id, IDbTransaction tran = null)
        {
            return conn.Get<TEntity>(id, tran);
        }

        public long Insert(TEntity entity)
        {
            if (entity.Id == 0L)
            {
                entity.Id = Id.Single.Id.NewId();
            }
            long num= DbConnection.Insert<TEntity>(entity);
            if (num == 0)
            {
                return entity.Id;
            }
            return num;
        }

        public long Insert(IDbConnection conn, TEntity entity, IDbTransaction tran = null)
        {
            if (entity.Id == 0L)
            {
                entity.Id = Id.Single.Id.NewId();
            }
            long num = conn.Insert<TEntity>(entity, tran);
            if (num == 0)
            {
                return entity.Id;
            }
            return num;
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
