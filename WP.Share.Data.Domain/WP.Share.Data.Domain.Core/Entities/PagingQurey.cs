using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.Share.Data.Domain.Core.Entities
{
    public class PagingQurey : IPagingQurey
    {
        private List<Sort> _sorts = null;
        public List<Sort> Sorts { get { return _sorts; } set { _sorts = value; } }
        public int PageSize { get; set; } = 20;

        public int PageIndex { get; set; } = 1;

        public string GetSortString()
        {
            if (_sorts == null || _sorts.Count <= 0)
            {
                return string.Empty;
            }
            StringBuilder sortStr = new StringBuilder();
            foreach (Sort item in _sorts)
            {
                if ("Id".Equals(item.Field))
                {
                    continue;
                }
                sortStr.Append($" , t.{item.Field } {item.SortType.ToString()} ");
            }
            return sortStr.ToString();
        }
    }

    public interface IPagingQurey
    {
        List<Sort> Sorts { get; set; }
        int PageSize { get; set; }

        int PageIndex { get; set; }
        string GetSortString();

    }
}
