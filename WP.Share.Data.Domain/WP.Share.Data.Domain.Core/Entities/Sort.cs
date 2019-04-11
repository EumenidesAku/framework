using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.Share.Data.Domain.Core.Entities
{
    /// <summary>
    ///  排序
    /// </summary>
    public class Sort
    {
        public string Field { get; set; }
        public SortTypeEnum SortType { get; set; }
    }

    public enum SortTypeEnum
    {
        DESC = 1,
        ASC = 2
    }
}
