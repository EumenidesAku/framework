using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.Share.Data.Domain.Core.Entities
{
    public class PageResult<T> : IPageResult<T>
    {
        public PageResult(long totalCount, IReadOnlyList<T> datas)
        {
            TotalCount = totalCount;
            Datas = datas;
        }

        public long TotalCount { get; set; }
        public IReadOnlyList<T> Datas { get; set; }
    }

    public interface IPageResult<T>
    {
        long TotalCount { get; set; }

        IReadOnlyList<T> Datas { get; set; }
    }
}
