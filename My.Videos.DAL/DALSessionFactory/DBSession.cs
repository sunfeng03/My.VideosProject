using My.Videos.DAL.DALFactory;
using My.Videos.IDAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Videos.DAL.DALSessionFactory
{
    public partial class DBSession : IDBSession
    {
        public DbContext Db
        {
            get { return new DbContextFactory().CreateDbContext(); }
        }

        /// <summary>
        /// 执行EF上下文的SaveChanges方法
        /// </summary>
        /// <returns></returns>
        public bool SaveChanges()
        {
            Db.Configuration.ValidateOnSaveEnabled = false;
            return Db.SaveChanges() > 0;
        }
        public int ExecuteSql(string sql, params System.Data.SqlClient.SqlParameter[] pars)
        {
            return pars == null ? Db.Database.ExecuteSqlCommand(sql) : Db.Database.ExecuteSqlCommand(sql, pars);
        }
        public List<T> ExecuteSelectQuery<T>(string sql, params System.Data.SqlClient.SqlParameter[] pars)
        {
            return Db.Database.SqlQuery<T>(sql, pars).ToList();
        }
    }

    public partial class DBSession : IDBSession
    {
        private ILogDRepository _LogDRepository;
        public ILogDRepository ILogDRepository
        {
            get
            {
                if (_LogDRepository == null)
                {
                    _LogDRepository = DalFactory.GetSMFIELDRepository;
                }
                return _LogDRepository;
            }
            set { _LogDRepository = value; }
        }

        private IMenuRepository _MenuRepository;
        public IMenuRepository IMenuRepository
        {
            get
            {
                if (_MenuRepository == null)
                {
                    // _SMFUNCTBRepository = new SMFUNCTBRepository();
                    _MenuRepository = DalFactory.GetSMFUNCTBRepository;
                }
                return _MenuRepository;
            }
            set { _MenuRepository = value; }
        }

        private IMenuRelationRoleRepository _MenuRelationRoleRepository;
        public IMenuRelationRoleRepository IMenuRelationRoleRepository
        {
            get
            {
                if (_MenuRelationRoleRepository == null)
                {
                    // _SMLOGRepository = new SMLOGRepository();
                    _MenuRelationRoleRepository = DalFactory.GetSMLOGRepository;
                }
                return _MenuRelationRoleRepository;
            }
            set { _MenuRelationRoleRepository = value; }
        }

        private IRoleRepository _RoleRepository;
        public IRoleRepository IRoleRepository
        {
            get
            {
                if (_RoleRepository == null)
                {
                    // _SMMENUROLEFUNCTBRepository = new SMMENUROLEFUNCTBRepository();
                    _RoleRepository = DalFactory.GetSMMENUROLEFUNCTBRepository;
                }
                return _RoleRepository;
            }
            set { _RoleRepository = value; }
        }

        private IUserRepository _UserRepository;
        public IUserRepository IUserRepository
        {
            get
            {
                if (_UserRepository == null)
                {
                    // _SMMENUTBRepository = new SMMENUTBRepository();
                    _UserRepository = DalFactory.GetSMMENUTBRepository;
                }
                return _UserRepository;
            }
            set { _UserRepository = value; }
        }

        private IUserRelationMenuRepository _UserRelationMenuRepository;
        public IUserRelationMenuRepository IUserRelationMenuRepository
        {
            get
            {
                if (_UserRelationMenuRepository == null)
                {
                    // _SMROLETBRepository = new SMROLETBRepository();
                    _UserRelationMenuRepository = DalFactory.GetSMROLETBRepository;
                }
                return _UserRelationMenuRepository;
            }
            set { _UserRelationMenuRepository = value; }
        }
    }
}
