using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.Dapper
{
    public static partial class ValidationHelper
    {
        #region Null判断系列
        /// <summary>
        /// 判断是否为空或Null
        /// </summary>
        /// <param name="objStr"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string objStr)
        {
            if (string.IsNullOrWhiteSpace(objStr))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 判断类型是否为可空类型
        /// </summary>
        /// <param name="theType"></param>
        /// <returns></returns>
        public static bool IsNullableType(Type theType)
        {
            return (theType.IsGenericType && theType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)));
        }
        #endregion
    }
}
