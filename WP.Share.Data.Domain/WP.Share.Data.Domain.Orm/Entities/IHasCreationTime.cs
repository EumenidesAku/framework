using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.Share.Data.Domain.Orm.Entities
{
    public interface IHasCreationTime
    {
        /// <summary>
        /// 实体创建的时间
        /// </summary>
        DateTime CreationTime { get; set; }
    }
}
