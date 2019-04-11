using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WP.Share.Data.Domain.Core.Extensions
{
    public static class EnumExtensions
    {
        public static string ToDescription(this Enum tEnum)
        {
            Type type = tEnum.GetType();
            MemberInfo[] memInfo = type.GetMember(tEnum.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }
            return tEnum.ToString();
        }
    }
}
