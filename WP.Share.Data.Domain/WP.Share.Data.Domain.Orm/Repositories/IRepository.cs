using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Share.Data.Domain.Core.Dependency;
using WP.Share.Data.Domain.Core.Entities;

namespace WP.Share.Data.Domain.Orm.Repositories
{
    public interface IRepository<TEntity, TPrimaryKey> : IDependency where TEntity : class, IEntity<TPrimaryKey>
    {
        IDbConnection DbConnection { get; }
        #region 每次调用都是新的数据库连接
        TEntity Get(TPrimaryKey id);

        long Insert(TEntity entity);

        bool Update(TEntity entity);

        bool Delete(TPrimaryKey id);
        #endregion 

        #region 调用方法需要传入IDbConnection。作用：1.便于在调用层的事务操作 2.避免同一库多次创建、开启关闭数据库连接
        TEntity Get(IDbConnection conn, TPrimaryKey id, IDbTransaction tran = null);

        long Insert(IDbConnection conn, TEntity entity, IDbTransaction tran = null);

        bool Update(IDbConnection conn, TEntity entity, IDbTransaction tran = null);

        bool Delete(IDbConnection conn, TPrimaryKey id, IDbTransaction tran = null);
        #endregion
    }
}
