using AutoMapper;
using My.Videos.Common.Utils;
using My.Videos.EntityFramework.Authority;
using My.Videos.IBLL;
using My.Videos.Model.Authority;
using My.Videos.Model.Base;
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

        /// <summary>
        /// 新增用户|更新用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public BaseResponse AddUser(User request)
        {
            var tblUser = Mapper.Map<TblUser>(request);
            if (request.Status == "insert")
            {
                var userEntity = LoadEntities(x => x.UserId == request.UserId).FirstOrDefault();
                if (userEntity != null)
                {
                    return new BaseResponse { isSuccess = false, msg = $"{request.UserId},已存在" };
                }
                else
                {
                    tblUser.CreateTime = DateTime.Now;
                    tblUser.UpdateTime = DateTime.Now;
                    tblUser.CreateName = "";
                    tblUser.UpdateName = "";
                    var entity = AddEntity(tblUser);
                    return new BaseResponse { isSuccess = entity != null, msg = (entity != null) ? "添加成功" : "添加失败" };
                }
            }
            else
            {
                var userEntity = LoadEntities(x => x.UserId == request.UserId).FirstOrDefault();
                userEntity.UserName = request.UserName;
                userEntity.NickName = request.NickName;
                userEntity.Email = request.Email;
                userEntity.MobilePhone = request.MobilePhone;
                userEntity.UpdateName = "";
                userEntity.UpdateTime = DateTime.Now;
                var result = EditEntity(userEntity);
                return new BaseResponse { isSuccess = result, msg = result ? "修改成功" : "修改失败" };
            }
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <returns></returns>
        public BaseResponse BathDeleteUser(List<string> ids)
        {
            for (var i = 0; i < ids.Count; i++)
            {
                var id = Convert.ToInt32(ids[i]);
                var entity = this.GetCurrentDbSession.IUserRepository.LoadEntities(x => x.Id == id).FirstOrDefault();
                this.GetCurrentDbSession.IUserRepository.DeleteEntity(entity);
            }
            var result = this.GetCurrentDbSession.SaveChanges();

            return new BaseResponse { isSuccess = result, msg = result ? "删除成功" : "删除失败" };
        }
    }
}
