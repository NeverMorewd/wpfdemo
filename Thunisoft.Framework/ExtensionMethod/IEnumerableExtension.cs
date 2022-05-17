using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Thunisoft.Framework.ExtensionMethod
{
    public static class IEnumerableExtension
    {
        public static async Task ForEachAsync<T>(this IEnumerable<T> list, Func<T, Task> func)
        {
            foreach (T value in list)
            {
                await func(value);
            }
        }
        /// <summary>
        /// 默认T必须含有Key属性,或者传入aKeyPropertyName
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="aKey"></param>
        /// <returns></returns>
        public static T GetDataForKey<T>(this IEnumerable<T> list, string aKeyPropertyName = "Key", [CallerMemberName]string aKey = "")
        {
            var typeT = typeof(T);
            var keyProperty = typeT.GetProperty(aKeyPropertyName);
            if (keyProperty != null)
            {
                return list.FirstOrDefault(x =>
                {
                    return keyProperty.GetValue(x).ToString() == aKey;
                });
            }
            else
            {
                throw new FrameworkException($"在 {typeT} 中找不到属性为 {aKeyPropertyName} 的属性");
            }
        }
    }
}
