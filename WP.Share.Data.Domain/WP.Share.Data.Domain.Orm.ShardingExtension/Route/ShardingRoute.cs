using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Share.Data.Domain.Core.ShardingExtension;
using WP.Share.Data.Id.Helper;

namespace WP.Share.Data.Domain.Orm.ShardingExtension.Route
{
    public abstract class ShardingRoute : IShardingRoute
    {
        protected List<ShardingRule> _rules;

        protected abstract void SetRules();
        protected ShardingRoute()
        {
            SetRules();
        }

        private TableInfo _lastTableInfo = null;
        private DateTime _lastGetTime = default(DateTime);

        /// <summary>
        /// 最新一张的表信息
        /// </summary>
        public virtual TableInfo LastTableInfo
        {
            get
            {
                DateTime now = DateTime.Now;
                // 如果上次取最新的表信息在今天，则直接取内存数据。即每天刷新一次最新表
                if (_lastGetTime != default(DateTime) && _lastGetTime.Year == now.Year
                        && _lastGetTime.DayOfYear == now.DayOfYear && _lastTableInfo != null)
                {
                    return _lastTableInfo;
                }

                _lastTableInfo = RouteByTime(now);
                _lastGetTime = now;
                return _lastTableInfo;
            }
        }

        public virtual TableInfo RouteById(long id)
        {
            IdModel idModel = IdHelper.Parser(id);

            return RouteByTime(idModel.CreateTime);
        }

        public virtual TableInfo RouteByTime(DateTime time)
        {
            ShardingRule currentRule = null;
            foreach (var rule in _rules)
            {
                if (time >= rule.EffectiveTime && time < rule.FailureTime)
                {
                    currentRule = rule;
                    break;
                }
            }
            if (currentRule == null)
            {
                return null;
            }
            return currentRule.GetTableInfoByTime(time);
        }

        public virtual SortedDictionary<DateTime, TableInfo> RouteBySectionTime(DateTime startTime, DateTime endTime)
        {
            SortedDictionary<DateTime, TableInfo> tableDic = new SortedDictionary<DateTime, TableInfo>();

            foreach (var rule in _rules)
            {
                SortedDictionary<DateTime, TableInfo> temps = rule.GetTableInfosBySectionTime(startTime, endTime);
                foreach (var item in temps)
                {
                    tableDic.Add(item.Key, item.Value);
                }
            }
            return tableDic;
        }

        public virtual SortedDictionary<DateTime, TableInfo> GetAllTableInfo()
        {
            throw new NotImplementedException();
        }

    }
}
