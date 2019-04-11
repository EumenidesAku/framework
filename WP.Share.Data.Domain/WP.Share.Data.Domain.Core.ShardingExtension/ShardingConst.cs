using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.Share.Data.Domain.Core.ShardingExtension
{
    public class ShardingConst
    {
        public const string TableName = "%TableName%";
        public const string Year = "%Year%";
        public const string HalfYear = "%HalfYear%";
        public const string Quarter = "%Quarter%";
        public const string Month = "%Month%";
    }

    public enum TimeShardingEnum
    {
        [Description("按年切分")]
        Year = 1,
        [Description("按半年切分")]
        HalfYear = 2,
        [Description("按季度切分")]
        Quarter = 4,
        [Description("按月切分")]
        Month = 12,
    }
}
