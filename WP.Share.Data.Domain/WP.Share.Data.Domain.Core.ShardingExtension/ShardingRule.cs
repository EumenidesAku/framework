using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.Share.Data.Domain.Core.ShardingExtension
{
    /// <summary>
    /// 分库分表规则
    /// </summary>
    public class ShardingRule
    {
        #region 规则配置
        public string Server { get; set; }

        public string UserId { get; set; }

        public string Password { get; set; }
        /// <summary>
        /// 规则生效时间
        /// </summary>
        public DateTime EffectiveTime { get; set; }

        /// <summary>
        /// 规则终止时间
        /// </summary>
        public DateTime FailureTime { get; set; }

        public string DbNormal { get; set; }
        public string TableNormal { get; set; }

        // TODO:数据库暂时只默认支持年切
        //public TimeShardingEnum DbTimeSharding { get; set; }

        /// <summary>
        /// 表的切分方式
        /// </summary>
        public TimeShardingEnum TableTimeSharding { get; set; }
        #endregion

        private string _connectionString = default(string);
        public string ConnectionString
        {
            get
            {
                if (_connectionString != default(string))
                {
                    return _connectionString;
                }
                return _connectionString = $"Server={Server};Database={DbNormal};User Id={UserId};Password={Password};";
            }
        }

        private SortedDictionary<DateTime, TableInfo> _TableInfos = null;

        /// <summary>
        /// 当前规则包含的所有表信息集合
        /// </summary>
        /// <returns></returns>
        public SortedDictionary<DateTime, TableInfo> TableInfos
        {
            get
            {
                if (_TableInfos != null)
                {
                    return _TableInfos;
                }
                return _TableInfos = GetTableInfos();
            }
        }

        /// <summary>
        /// 获取当前规则包含的所有表信息集合
        /// </summary>
        /// <returns></returns>
        private SortedDictionary<DateTime, TableInfo> GetTableInfos()
        {
            SortedDictionary<DateTime, TableInfo> tables = new SortedDictionary<DateTime, TableInfo>();
            int eYear = EffectiveTime.Year;
            int fYear = FailureTime.Year;
            int tableSharding = (int)TableTimeSharding;
            for (int i = eYear; i < fYear; i++)
            {
                string year = i.ToString();
                string dbName = DbNormal.Replace(ShardingConst.Year, year);
                string connectionString = ConnectionString.Replace(ShardingConst.Year, year);
                string tableName = TableNormal.Replace(ShardingConst.Year, year);

                for (int j = 1; j <= tableSharding; j++)
                {
                    TableInfo ti = new TableInfo
                    {
                        DbName = dbName,
                        ConnectionString = connectionString
                    };

                    int month = 12 * (j - 1) / tableSharding + 1;
                    string halfYear = month <= 6 ? "fhy" : "lhy";
                    string quarter = "q" + j;
                    string shortTableName = tableName
                        .Replace(ShardingConst.HalfYear, halfYear)
                        .Replace(ShardingConst.Quarter, quarter)
                        .Replace(ShardingConst.Month, month.ToString());

                    ti.TableName = $@"[{dbName}].[dbo].[{shortTableName}]";
                    DateTime dt = Convert.ToDateTime($"{year}-{month}-01");
                    tables.Add(dt, ti);
                }
            }
            return tables;
        }

        /// <summary>
        /// 根据时间获取表信息
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public TableInfo GetTableInfoByTime(DateTime time)
        {
            DateTime t = GetTableTimeByTime(time);
            return TableInfos[t];
        }

        /// <summary>
        /// 根据时间段获取表信息集合
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public SortedDictionary<DateTime, TableInfo> GetTableInfosBySectionTime(DateTime startTime, DateTime endTime)
        {
            // TODO:是否有更好的优化方式，或者用别的集合类型来装载TableInfo
            DateTime start = GetTableTimeByTime(startTime);
            DateTime end = GetTableTimeByTime(endTime);
            SortedDictionary<DateTime, TableInfo> tables = new SortedDictionary<DateTime, TableInfo>();
            foreach (var item in TableInfos)
            {
                if (item.Key >= start && item.Key <= end)
                {
                    tables.Add(item.Key, item.Value);
                }
            }
            return tables;
        }

        /// <summary>
        /// 获取传入时间存在的表时间key
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private DateTime GetTableTimeByTime(DateTime time)
        {
            int mouth = time.Month;
            int spread = 12 / (int)TableTimeSharding;
            mouth = ((mouth - 1) / spread) * spread + 1;
            return Convert.ToDateTime($"{time.Year}-{mouth}-01");
        }


    }
}
