using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Share.Data.Dapper.Contrib;

namespace WP.Share.Data.Domain.Orm.Repositories
{
    public abstract class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        [ExplicitKey]
        public virtual TPrimaryKey Id { get; set; }
    }

    public interface IEntity<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }
    }
}
