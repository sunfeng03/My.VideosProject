using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Videos.Common.Utils
{
    public static class ConventUtils
    {
        #region  string to decimal

        /// <summary>
        /// string to decimal
        /// </summary>
        /// <param name="str"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string str, decimal def = 0.00M)
        {
            if (string.IsNullOrEmpty(str))
            {
                return def;
            }

            try
            {
                return decimal.Parse(str);
            }
            catch
            {
                return def;
            }
        }
        #endregion

        #region string to Int
        /// <summary>
        /// string To the int.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="def">The definition.</param>
        /// <returns></returns>
        public static int ToInt(this string str, int def)
        {
            if (string.IsNullOrEmpty(str))
            {
                return def;
            }

            try
            {
                return int.Parse(str);
            }
            catch
            {
                return def;
            }
        }

        #endregion

        #region string to DateTime

        /// <summary>
        /// string to datetime
        /// </summary>
        /// <param name="source"></param>
        /// <param name="defaultValue"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string source, DateTime defaultValue, string format = "yyyy-MM-dd HH:mm:ss")
        {
            try
            {
                return string.IsNullOrEmpty(source) ? defaultValue : DateTime.ParseExact(source, format, CultureInfo.CurrentCulture);
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// string to datetime 如果转换失败 返回null
        /// </summary>
        /// <param name="source"></param>
        /// <param name="format"></param>
        /// <returns></returns>

        public static DateTime? ToDateTime(this string source, string format = "yyyy-MM-dd HH:mm:ss")
        {
            try
            {
                if (string.IsNullOrEmpty(source)) return null;
                return DateTime.ParseExact(source, format, CultureInfo.CurrentCulture);
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region string to Int
        /// <summary>
        /// string To the str.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="def">The definition.</param>
        /// <returns></returns>
        public static string ToStr(this string str, string def)
        {
            if (string.IsNullOrEmpty(str))
            {
                return def;
            }
            return str;

        }

        #endregion

        #region string bool
        /// <summary>
        /// string To the bool.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="defalutValue">if set to <c>true</c> [defalut value].</param>
        /// <returns></returns>
        public static bool ToBool(this string source, bool defalutValue = false)
        {
            bool result;
            return bool.TryParse(source, out result) ? result : defalutValue;
        }

        #endregion

        #region string ext  method

        /// <summary>
        /// Determines whether [is null or empty].
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string source)
        {
            return string.IsNullOrEmpty(source);
        }

        /// <summary>
        /// Trims the specified default value.
        /// If source is null , return default value.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="defaultVal">The default value.If source is null , return default value.</param>
        /// <returns></returns>
        public static string ToTrim(this string source, string defaultVal = "")
        {
            return source == null ? defaultVal : source.Trim();
        }

        #endregion

        #region JSON序列化

        /// <summary>
        /// 转换Json字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJSON(this object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj, new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());
        }
        /// <summary>
        /// 转换Json字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJSONDate(this object obj)
        {
            Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
            timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj, timeConverter);
        }

        #endregion

        /// <summary>
        /// 从DataRow中读取字符串并除前后空白字符后
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetTrimedString(this DataRow dr, string key)
        {
            return dr[key].ToString().Trim();
        }

        /// <summary>
        /// 从DataRow中读取字符串并除前后空白字符后
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string GetTrimedString(this DataRow dr, int index)
        {
            return dr[index].ToString().Trim();
        }

        /// <summary>
        /// 从DataRow中读取可空对象
        /// </summary>
        /// <typeparam name="T"><peparam>
        /// <param name="dr"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Nullable<T> GetNullable<T>(this DataRow dr, string key)
            where T : struct
        {
            return dr[key] == null || dr[key] == DBNull.Value ? (Nullable<T>)null : (T)dr[key];
        }

        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="Message"></param>
        /// <returns></returns>
        public static string Base64Code(string Message)
        {
            byte[] bytes = Encoding.Default.GetBytes(Message);
            return Convert.ToBase64String(bytes);
        }
        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="Message"></param>
        /// <returns></returns>
        public static string Base64Decode(string Message)
        {
            byte[] bytes = Convert.FromBase64String(Message);
            return Encoding.Default.GetString(bytes);
        }
    }
}
