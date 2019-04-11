using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.Share.Data.Domain.Orm.Entities
{
    public interface ICreationAudited : IHasCreationTime
    {
        /// <summary>
        /// 创建实体的用户id
        /// </summary>
        long? CreatorUserId { get; set; }
    }
}
