using log4net;
using My.Videos.EntityFramework.Authority;
using My.Videos.EntityFramework.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Videos.EntityFramework
{
    public class VideoDbContext : BaseContext
    {
        static VideoDbContext()
        {
            //1.自动创建数据库,表有修改,自动删除数据库,创建单的数据库
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<VideoDbContext>());
            //2.取消自动删除数据库
            //Database.SetInitializer<VideoDbContext>(null);
            //生成SQL
            //Update-Database -Script -SourceMigration:"202005120144172_InitialCreate.cs" -TargetMigration:"202005120230127_InitialCreate1.cs" 
        }

        private static readonly ILog Log = LogManager.GetLogger("VideoDbContext");

        public VideoDbContext() : base(DbConnectionStringConfig.Default.RolePermissionDbConnectionString)
        {
            //DbConnectionStringConfig.Default.RolePermissionDbConnectionString
            //Data Source=127.0.0.1;Initial Catalog=MyVideo;User ID=sa;Password=SQLConnection365
        }

        /// <summary>
        /// 菜单信息
        /// </summary>
        public IDbSet<TblMenu> Menu { set; get; }
        /// <summary>
        /// 菜单角色关联信息
        /// </summary>
        public IDbSet<TblMenuRelationRole> MenuRelationRole { set; get; }
        /// <summary>
        /// 角色信息
        /// </summary>
        public IDbSet<TblRole> Role { set; get; }
        /// <summary>
        /// 用户信息
        /// </summary>
        public IDbSet<TblUser> User { set; get; }
        /// <summary>
        /// 用户角色关联信息
        /// </summary>
        public IDbSet<TblUserRelationMenu> UserRelationMenu { set; get; }
        /// <summary>
        /// 日志表
        /// </summary>
        public IDbSet<TblLog> LogMsg { get; set; }
    }
}
