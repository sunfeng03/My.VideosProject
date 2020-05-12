using AutoMapper;
using My.Videos.Common.Utils;
using My.Videos.EntityFramework.Authority;
using My.Videos.IBLL;
using My.Videos.Model.Authority;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace My.Videos.BLL
{
    public partial class UserService : BaseService<TblUser>, IUserService
    {
        /// <summary>
        /// 获取用户分页数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public UserResponse GetUserPage(UserRequest request)
        {
            var total = 0;
            IQueryable<TblUser> queryData = null;
            Expression<Func<TblUser, bool>> whereLambda = PredicateBuilder.True<TblUser>();
            whereLambda = whereLambda.And(u => u.IsDelete == 0);
            if (!string.IsNullOrEmpty(request.UserId))
                whereLambda = whereLambda.And(u => u.UserId == request.UserId);
            if (!string.IsNullOrEmpty(request.UserName))
                whereLambda = whereLambda.And(u => u.UserName == request.UserName);
            bool? isAsc = null;
            string orderByFile = string.Empty;
            if (!string.IsNullOrEmpty(request.Order) && !string.IsNullOrEmpty(request.Sort))
            {
                orderByFile = request.Order;
                isAsc = request.Sort == "asc" ? true : false;
            }
            queryData = LoadPageEntities<TblUser>(request.PageIndex, request.PageSize, out total, whereLambda, orderByFile, isAsc);

            List<User> queryDataView = new List<User>();
            if (queryData != null && queryData.Count() > 0)
            {
                queryDataView = Mapper.Map<List<User>>(queryData.ToList());
            }
            return new UserResponse
            {
                count = total,
                data = queryDataView
            };
        }
    }
}
