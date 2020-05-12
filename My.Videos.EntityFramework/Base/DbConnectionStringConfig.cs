using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Videos.EntityFramework.Base
{
    public class DbConnectionStringConfig
    {
        private static DbConnectionStringConfig _default;

        /// <summary>
        /// 默认配置对象
        /// </summary>
        public static DbConnectionStringConfig Default { get { return _default; } }

        /// <summary>
        /// 权限中心链接字符串
        /// </summary>
        public string RolePermissionDbConnectionString { set; get; }


        /// <summary>
        /// 初始化配置对象
        /// </summary>
        public static void InitDefault(DbConnectionStringConfig defaultConfig)
        {
            _default = defaultConfig;
        }
    }
}
