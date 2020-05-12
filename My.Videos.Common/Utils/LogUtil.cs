using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Videos.Common.Utils
{

    /// <summary>
    /// log4net工具类
    /// </summary>
    public static class LogUtil
    {
        /// <summary>
        /// The _ip
        /// </summary>
        private static readonly string Ip = string.Empty;

        /// <summary>
        /// Initializes the <see cref="LogUtil"/> class.
        /// </summary>
        static LogUtil()
        {
            if (string.IsNullOrEmpty(Ip))
            {
                var listIp = new List<string>();
                // ReSharper disable once LoopCanBeConvertedToQuery
                foreach (var ip in EnvironmentUtil.EnvironmentIp.Split('|'))
                {
                    var ds = ip.Split('.').Select(o => o.ToInt(0).ToString("d3"));
                    var temp = string.Join("", ds).Reverse();
                    listIp.Add(string.Join("", temp));
                }
                Ip = string.Join("", listIp);
            }
        }

        /// <summary>
        /// 根据log4net.config中配置的logName获取Ilog
        /// </summary>
        /// <param name="logName">log4net.config中配置的logName</param>
        /// <returns></returns>
        public static ILog GetLogger(string logName)
        {
            return LogManager.GetLogger(logName);
        }

        /// <summary>
        /// CreateLogger By T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ILog GetLogger<T>()
        {
            return LogManager.GetLogger(typeof(T));
        }

        /// <summary>
        /// CreateLogger By Type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ILog GetLogger(Type type)
        {
            return LogManager.GetLogger(type);
        }

        /// <summary>
        /// 创建一个logId
        /// </summary>
        /// <returns></returns>
        public static string CreateLogId()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssffff") + "I" + Ip + "I" + new Random().Next(0, int.MaxValue).ToString("d10");
        }

        /// <summary>
        /// LogMessage
        /// </summary>
        public class LogMessage
        {
            /// <summary>
            /// json 配置,在静态构造方法中初始化
            /// </summary>
            private readonly static JsonSerializerSettings JsonSettings;


            private readonly string _logId = LogUtil.CreateLogId();


            /// <summary>
            /// logId
            /// </summary>
            public string LogId { get { return _logId; } }

            /// <summary>
            /// 关键字
            /// </summary>
            public string MessageKey { set; get; }

            /// <summary>
            ///  messageBody
            /// </summary>
            public object MessageBody { set; get; }

            /// <summary>
            /// 异常
            /// </summary>
            public Exception Exception { set; get; }

            static LogMessage()
            {

                JsonSettings = new JsonSerializerSettings
                {
                    //防止循环引用
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
#if DEBUG
                    Formatting = Formatting.Indented
#endif
                };
                //设置枚举类型的json为string
                JsonSettings.Converters.Add(new StringEnumConverter());
                //设置json日期格式
                JsonSettings.Converters.Add(new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss:fff" });
            }

            /// <summary>
            /// 重写 tostring 方法,返回的是json string
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return JsonConvert.SerializeObject(this, JsonSettings);
            }
        }
    }
}
