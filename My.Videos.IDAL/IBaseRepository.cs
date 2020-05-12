using My.Videos.EntityFramework.Authority;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace My.Videos.IDAL
{
    public interface IBaseRepository<T> where T : class, new()
    {
        IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLambda);
        IQueryable<T> LoadPageEntities<model>(int pageIdex, int pageSize, out int toalCount, Expression<Func<T, bool>> whereLambda, string orderby, bool? isAsc);
        bool DeleteEntity(T entity);
        bool EditEntity(T entity);
        T AddEntity(T entity);

        IQueryable<T> LoadEntitiesAll(string entity);
    }

    #region 扩展
    public partial interface ILogDRepository : IBaseRepository<TblLog>
    {
    }
    public partial interface IMenuRepository : IBaseRepository<TblMenu>
    {
    }
    public partial interface IMenuRelationRoleRepository : IBaseRepository<TblMenuRelationRole>
    {
    }
    public partial interface IRoleRepository : IBaseRepository<TblRole>
    {
    }
    public partial interface IUserRepository : IBaseRepository<TblUser>
    {
    }
    public partial interface IUserRelationMenuRepository : IBaseRepository<TblUserRelationMenu>
    {
    }

    #endregion
}
