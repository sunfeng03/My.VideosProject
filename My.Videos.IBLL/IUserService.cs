using My.Videos.Model.Authority;
using My.Videos.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Videos.IBLL
{
    public partial interface IUserService
    {
        /// <summary>
        /// 获取用户分页数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        UserResponse GetUserPage(UserRequest request);

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        BaseResponse AddUser(User request);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        BaseResponse BathDeleteUser(List<string> ids);
    }
}
