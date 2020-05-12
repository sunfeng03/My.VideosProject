using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Videos.Common.Utils
{
    public class AppSettingUtil
    {
        /// <summary>
        /// 根据key获取string值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetVal(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        /// <summary>
        /// 根据key获取string值,再转换为指定的泛型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetVal<T>(string key)
        {
            var tempVal = GetVal(key);
            return (T)Convert.ChangeType(tempVal, typeof(T), CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// 根据key获取string值,再转换为指定的泛型,且值为null时候,返回默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="defaultVal"></param>
        /// <returns></returns>
        public static T GetVal<T>(string key, T defaultVal)
        {
            var tempVal = GetVal(key);
            if (tempVal == null) return defaultVal;
            return (T)Convert.ChangeType(tempVal, typeof(T), CultureInfo.InvariantCulture);
        }


        #region 扩展获取配置项

        /// <summary>
        /// 应用程序ID
        /// </summary>
        public static int AppID
        {
            get { return GetVal("appid", "110909").ToInt(110909); }
        }
        /// <summary>
        /// 平台DB加密解密key
        /// </summary>
        public static string EncryptKey
        {
            get { return GetVal("EncryptKey", "BeiJing#2008"); }
        }

        /// <summary>
        /// 加密的数据库连接字符串
        /// </summary>
        public static string VideoDBConnectionString
        {
            get { return GetVal("VideoDBConnectionString"); }
        }

        #endregion
    }
}
