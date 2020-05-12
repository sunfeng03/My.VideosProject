using log4net;
using My.Videos.Common.AutoMap;
using My.Videos.Common.Utils;
using My.Videos.EntityFramework;
using My.Videos.EntityFramework.Base;
using My.Videos.Ioc;
using My.VideosProject.App_Start;
using My.VideosProject.Engines;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace My.VideosProject
{
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// The _app version
        /// </summary>
        public static readonly string Version = DateTime.Now.ToString("yyyyMMddHHmm");
      
        protected void Application_Start()
        {
            //注册autofac
            DependencyInjector.Initialise();
            HttpContext.Current.Application["Version"] = Version;
            InitLog4net();
            InitJsonNet();
            InitDbConnectionStringConfig();

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            MapperProfile.InitMapper();
        }

        /// <summary>
        /// Initializes the log4net.
        /// </summary>
        private static void InitLog4net()
        {
            var configPath = AppDomain.CurrentDomain.BaseDirectory + "Config\\log4net.config";
            if (File.Exists(configPath))
            {
                Trace.Write("log4net.config 配置文件路径-->" + configPath, "InitLog4net");
                log4net.Config.XmlConfigurator.Configure(new FileInfo(configPath));
            }
            else
                Trace.Write("未能找到log4net.config-->" + configPath, "InitLog4net");
        }

        /// <summary>
        /// Initializes the json net.
        /// </summary>
        private static void InitJsonNet()
        {
            JsonConvert.DefaultSettings = () =>
            {
                var defaultSettings = new JsonSerializerSettings
                {
                    //防止循环引用
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                };
                //设置枚举类型的json为string
                defaultSettings.Converters.Add(new StringEnumConverter());
                //设置json日期格式
                defaultSettings.Converters.Add(new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss:fff" });
                return defaultSettings;
            };
        }

        /// <summary>
        /// 初始化数据库连接
        /// </summary>
        private static void InitDbConnectionStringConfig()
        {
            var encryptKey = AppSettingUtil.EncryptKey;
            var dbConnectionStringConfig = new DbConnectionStringConfig();
            var encryptConStr = ConfigurationManager.ConnectionStrings["VideoDBConnectionString"].ToString();

            dbConnectionStringConfig.RolePermissionDbConnectionString = ManagePass.Decrypt(encryptConStr, encryptKey);

            DbConnectionStringConfig.InitDefault(dbConnectionStringConfig);
            Database.SetInitializer<VideoDbContext>(null);
            //是否使用with(nolock)
            DbInterception.Add(new NoLockInterceptor());
            NoLockInterceptor.IsEnableNoLock = true;
        }
    }
}
