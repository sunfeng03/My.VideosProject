using My.Videos.Model.Authority;
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
    }
}
