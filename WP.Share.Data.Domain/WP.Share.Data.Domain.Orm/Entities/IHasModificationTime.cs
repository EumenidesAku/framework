using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.Share.Data.Domain.Orm.Entities
{
    public interface IHasModificationTime
    {
        /// <summary>
        /// 最后修改实体的时间
        /// </summary>
        DateTime? LastModificationTime { get; set; }
    }
}
