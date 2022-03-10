using Mini.Dinner.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace System
{
    public static class GlobalUtility
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T As<T>(this Object obj) where T : class
        {
            return obj as T;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static Boolean IsIn<T>(this T obj, IEnumerable<T> collection)
        {
            return collection.Contains(obj);
        }

        /// <summary>
        /// 判断该类型是否实现了指定接口
        /// </summary>
        /// <param name="type"></param>
        /// <param name="tinterface"></param>
        /// <returns></returns>
        public static Boolean IsImplemented(this Type type, Type tinterface)
        {
            if (tinterface.IsInterface)
            {
                return type.GetInterfaces().Contains(tinterface);
            }

            return false;
        }

        public static T GetModelValue<T>(this object obj, string fieldName)
        {
            try
            {
                Type ts = obj.GetType();
                var o = ts.GetProperty(fieldName).GetValue(obj, null);
                return (T)o;
            }
            catch
            {
                return (T)typeof(T).GetDefaultValue();
            }
        }

        /// <summary>
        /// 创建对象的深度克隆，对象类型必须标记为可序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T DeepClone<T>(this T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }

        /// <summary>
        /// 尝试把字符串转换成枚举形式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"> </param>
        /// <returns></returns>
        public static T ConvertToEnum<T>(this String str) where T : struct, IConvertible
        {
            return Enum.TryParse(str, true, out T t) ? t : default(T);
        }

        /// <summary>
        /// 将 String 转换为 Guid
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Guid? ToGuid(this String str)
        {
            if (String.IsNullOrEmpty(str))
            {
                return Guid.Empty;
            }

            if (Guid.TryParse(str.Trim(), out Guid obj))
            {
                return obj;
            }

            return null;
        }

        public static Guid ToGuid(this String str, Guid defaultValue)
        {
            var res = str.ToGuid();
            return res == null ? defaultValue : res.Value;
        }
        public static Uuid? ToUuid(this String str)
        {
            if (String.IsNullOrEmpty(str))
            {
                return Uuid.Empty;
            }

            if (Uuid.TryParse(str.Trim(), out Uuid obj))
            {
                return obj;
            }

            return null;
        }
        public static Uuid ToUuid(this String str, Uuid defaultValue)
        {
            var res = str.ToUuid();
            return res == null ? defaultValue : res.Value;
        }

        /// <summary>
        /// 将 String 转换为 Int32
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Int32? ToInt32(this String str)
        {
            if (String.IsNullOrEmpty(str))
            {
                return 0;
            }

            if (Int32.TryParse(str.Trim(), out int obj))
            {
                return obj;
            }

            return null;
        }

        public static Int32 ToInt32(this String str, Int32 defaultValue)
        {
            if (Int32.TryParse(str?.Trim(), out int obj))
            {
                return obj;
            }
            return defaultValue;
        }

        /// <summary>
        /// 将 String 转换为 Decimal
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Decimal? ToDecimal(this String str)
        {
            if (String.IsNullOrEmpty(str))
            {
                return 0;
            }

            if (Decimal.TryParse(str.Trim(), out decimal obj))
            {
                return obj;
            }

            return null;
        }

        public static Decimal ToDecimal(this String str, Decimal defaultValue)
        {
            var res = str.ToDecimal();
            return res == null ? defaultValue : res.Value;
        }

        /// <summary>
        /// 将 String 转换为 Float
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Single? ToFloat(this String str)
        {
            if (String.IsNullOrEmpty(str))
            {
                return 0;
            }

            if (Single.TryParse(str.Trim(), out float obj))
            {
                return obj;
            }

            return null;
        }

        public static Single ToFloat(this String str, Single defaultValue)
        {
            var res = str.ToFloat();
            return res == null ? defaultValue : res.Value;
        }

        /// <summary>
        /// 将 String 转换为 Double
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Double? ToDouble(this String str)
        {
            if (String.IsNullOrEmpty(str))
            {
                return 0;
            }

            if (Double.TryParse(str.Trim(), out double obj))
            {
                return obj;
            }

            return null;
        }

        public static Double ToDouble(this String str, Double defaultValue)
        {
            var res = str.ToDouble();
            return res == null ? defaultValue : res.Value;
        }

        /// <summary>
        /// 将 String 转换为 DateTime
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime? ToDateTime(this String str)
        {
            if (String.IsNullOrEmpty(str))
            {
                return null;
            }

            if (!DateTime.TryParse(str.Trim(), out DateTime obj))
            {
                return null;
            }

            return obj;
        }

        public static DateTime ToDateTime(this String str, DateTime defaultValue)
        {
            var res = str.ToDateTime();

            return res == null ? defaultValue : res.Value;
        }

        /// <summary>
        /// 将 String 转换为 Boolean
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Boolean? ToBoolean(this String str)
        {
            if (String.IsNullOrEmpty(str))
            {
                return null;
            }

            if (!Boolean.TryParse(str.Trim(), out bool obj))
            {
                return null;
            }

            return obj;
        }

        /// <summary>
        /// 获取类型的默认值，等同于default(type)的返回值
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Object GetDefaultValue(this Type type)
        {
            return Assembly.GetAssembly(type).CreateInstance(type.FullName);
        }

        /// <summary>
        /// 使用MD5加密
        /// </summary>
        /// <param name="original">密文</param>
        /// <returns></returns>
        public static string EncryptMD5(String original)
        {
            string md5String = string.Empty;
            try
            {
                byte[] byteCode = System.Text.Encoding.GetEncoding("GB2312").GetBytes(original.Trim());
                byteCode = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(byteCode);

                for (int i = 0; i < byteCode.Length; i++)
                {
                    md5String += byteCode[i].ToString("x").PadLeft(2, '0');
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return md5String;
        }

        public static string GetChineseSpell(this String strText)
        {
            int len = strText.Length;
            string myStr = "";
            for (int i = 0; i < len; i++)
            {
                myStr += getSpell(strText.Substring(i, 1));
            }
            return myStr;
        }
        private static string getSpell(string cnChar)
        {
            byte[] arrCN = System.Text.Encoding.Default.GetBytes(cnChar);
            if (arrCN.Length > 1)
            {
                int area = arrCN[0];
                int pos = arrCN[1];
                int code = (area << 8) + pos;
                int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
                for (int i = 0; i < 26; i++)
                {
                    int max = 55290;
                    if (i != 25)
                    {
                        max = areacode[i + 1];
                    }

                    if (areacode[i] <= code && code < max)
                    {
                        return System.Text.Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
                    }
                }
                return "*";
            }
            else
            {
                return cnChar;
            }

        }

        /// <summary>
        /// 将JavaScript时间戳转换为DateTime类型时间
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static DateTime ConvertJavaScriptDateTime(this long d)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)); // 当地时区
            DateTime time = startTime.AddMilliseconds(d);
            return time;
        }
        /// <summary>
        /// 将DateTime转换为JavaScript时间戳
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long ConvertDateTimeJavaScript(this DateTime time)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long JavaScriptResult = (long)(time - startTime).TotalMilliseconds;
            return JavaScriptResult;
        }
        /// <summary> 
        /// 将Unix时间戳转换为DateTime类型时间
        /// </summary> 
        /// <param name="d"> double 型数字 </param> 
        /// <returns> DateTime </returns> 
        public static DateTime ConvertIntDateTime(this int d)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            DateTime time = startTime.AddSeconds(d);

            return time;
        }

        /// <summary> 
        /// 将c# DateTime时间格式转换为Unix时间戳格式
        /// </summary> 
        /// <param name="time"> 时间 </param> 
        /// <returns> double </returns> 
        public static int ConvertDateTimeInt(this DateTime time)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            var intResult = (int)(time - startTime).TotalSeconds;

            return intResult;
        }

        /// <summary>
        /// 将sourceObj对象属性值赋给TResult对象
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="sourceObj"></param>
        /// <returns>TResult对象</returns>
        public static TResult CopyToObject<TResult>(this object sourceObj)
        {
            if (sourceObj == null)
            {
                return default(TResult);
            }

            PropertyInfo[] sourceProperties = sourceObj.GetType().GetProperties();
            var targetProperties = typeof(TResult).GetProperties().ToList();
            TResult result = (TResult)Activator.CreateInstance(typeof(TResult));
            foreach (PropertyInfo itemp in sourceProperties)
            {
                PropertyInfo targetp = targetProperties.Find(p => p.Name.ToLower().Equals(itemp.Name.ToLower()));
                if (targetp != null)
                {
                    try
                    {
                        var value = itemp.GetValue(sourceObj);
                        targetp.SetValue(result, value);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 将sourceObjs对象列表值赋给TResult对象列表
        /// </summary>
        /// <param name="bakDbCusts"></param>
        /// <returns></returns>
        public static IEnumerable<TResult> CopyToObjectList<TResult>(this IEnumerable<object> sourceObjs)
        {
            if (sourceObjs == null)
            {
                yield break;
            }

            foreach (var bakDbCust in sourceObjs)
            {
                yield return bakDbCust.CopyToObject<TResult>();
            }
            yield break;
        }
    }
}
