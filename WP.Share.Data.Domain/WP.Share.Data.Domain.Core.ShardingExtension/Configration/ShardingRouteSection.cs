using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.Share.Data.Domain.Core.ShardingExtension.Configration
{
    public class ShardingRouteSection : ConfigurationSection
    {
        public override bool IsReadOnly()
        {
            return true;
        }

        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return (string)base["name"]; }
            set { base["name"] = value; }
        }

        [ConfigurationProperty("shardingRules", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(ShardingRule), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap, RemoveItemName = "remove")]
        public ShardingRules ShardingRules
        {
            get { return (ShardingRules)base["shardingRules"]; }
            set { base["shardingRules"] = value; }
        }

        public List<ShardingExtension.ShardingRule> GetShardingRules()
        {
            ShardingRules rules = ShardingRules;
            if (rules == null || rules.Count<=0)
            {
                throw new ArgumentNullException("配置的路由规则为空");
            }

            List<ShardingExtension.ShardingRule> shardingRules = new List<ShardingExtension.ShardingRule>();
            foreach (ShardingRule rule in rules)
            { 
                ShardingExtension.ShardingRule sRule = new ShardingExtension.ShardingRule();
                sRule.EffectiveTime = Convert.ToDateTime(rule.EffectiveTime);
                sRule.FailureTime = Convert.ToDateTime(rule.FailureTime);
                sRule.DbNormal = rule.DbNormal;
                sRule.TableNormal = rule.TableNormal;
                sRule.TableTimeSharding = (TimeShardingEnum)rule.TableTimeSharding;
                sRule.Server = rule.Server;
                sRule.UserId = rule.UserId;
                sRule.Password = rule.Password;
                shardingRules.Add(sRule);
            }
            return shardingRules;
        }
    }

    public class ShardingRules : ConfigurationElementCollection
    {
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ShardingRule)element).EffectiveTime;
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ShardingRule();
        }

        public ShardingRule this[int i]
        {
            get { return (ShardingRule)base.BaseGet(i); }
        }

        public ShardingRule this[string key]
        {
            get { return (ShardingRule)base.BaseGet(key); }
        }
    }

    public class ShardingRule : ConfigurationElement
    {
        [ConfigurationProperty("server", IsRequired = true)]
        public string Server
        {
            get { return (string)base["server"]; }
            set { base["server"] = value; }
        }
        [ConfigurationProperty("userId", IsRequired = true)]
        public string UserId
        {
            get { return (string)base["userId"]; }
            set { base["userId"] = value; }
        }
        [ConfigurationProperty("password", IsRequired = true)]
        public string Password
        {
            get { return (string)base["password"]; }
            set { base["password"] = value; }
        }
        [ConfigurationProperty("effectiveTime", IsRequired = true, IsKey = true)]
        public string EffectiveTime
        {
            get { return (string)base["effectiveTime"]; }
            set { base["effectiveTime"] = value; }
        }
        [ConfigurationProperty("failureTime", IsRequired = true)]
        public string FailureTime
        {
            get { return (string)base["failureTime"]; }
            set { base["failureTime"] = value; }
        }
        [ConfigurationProperty("dbNormal", IsRequired = true)]
        public string DbNormal
        {
            get { return (string)base["dbNormal"]; }
            set { base["dbNormal"] = value; }
        }
        [ConfigurationProperty("tableNormal", IsRequired = true)]
        public string TableNormal
        {
            get { return (string)base["tableNormal"]; }
            set { base["tableNormal"] = value; }
        }
        [ConfigurationProperty("tableTimeSharding",
            DefaultValue = (int)1,
            IsRequired = false)]
        [IntegerValidator(MinValue = 1,
            MaxValue = 12, ExcludeRange = false)]
        public int TableTimeSharding
        {
            get { return (int)base["tableTimeSharding"]; }
            set { base["tableTimeSharding"] = value; }
        }
    }
}
