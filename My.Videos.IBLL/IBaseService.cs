using My.Videos.EntityFramework.Authority;
using My.Videos.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace My.Videos.IBLL
{
    public interface IBaseService<T> where T : class, new()
    {
        IDBSession GetCurrentDbSession { get; }
        IBaseRepository<T> CurrentRepository { get; set; }
        IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLambda);
        IQueryable<T> LoadPageEntities<s>(int pageIdex, int pageSize, out int toalCount, Expression<Func<T, bool>> whereLambda, string orderby, bool? isAsc);
        bool DeleteEntity(T entity);
        bool EditEntity(T entity);
        T AddEntity(T entity);
    }

    #region 扩展
    public partial interface ILogDService : IBaseService<TblLog>
    {
    }
    public partial interface IMenuService : IBaseService<TblMenu>
    {
    }
    public partial interface IMenuRelationRoleService : IBaseService<TblMenuRelationRole>
    {
    }
    public partial interface IRoleService : IBaseService<TblRole>
    {
    }
    public partial interface IUserService : IBaseService<TblUser>
    {
    }
    public partial interface IUserRelationMenuService : IBaseService<TblUserRelationMenu>
    {
    }
    #endregion
}
