using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Share.Data.Domain.Orm.Repositories;

namespace WP.Share.Data.Domain.Orm.Entities
{

    [Serializable]
    public abstract class AuditedEntity<TPrimaryKey> : CreationAuditedEntity<TPrimaryKey>, IAudited
    {
        /// <summary>
        /// 上次修改实体时间
        /// </summary>
        public virtual DateTime? LastModificationTime { get; set; }

        /// <summary>
        ///上次修改实体的用户id
        /// </summary>
        public virtual long? LastModifierUserId { get; set; }
    }

}
