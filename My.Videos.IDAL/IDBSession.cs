using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Videos.IDAL
{
    public partial interface IDBSession
    {
        DbContext Db { get; }
        bool SaveChanges();
        int ExecuteSql(string sql, params System.Data.SqlClient.SqlParameter[] pars);
        List<T> ExecuteSelectQuery<T>(string sql, params System.Data.SqlClient.SqlParameter[] pars);
    }

    public partial interface IDBSession
    {
        ILogDRepository ILogDRepository { get; set; }
        IMenuRepository IMenuRepository { get; set; }
        IMenuRelationRoleRepository IMenuRelationRoleRepository { get; set; }
        IRoleRepository IRoleRepository { get; set; }
        IUserRepository IUserRepository { get; set; }
        IUserRelationMenuRepository IUserRelationMenuRepository { get; set; }
    }
}
