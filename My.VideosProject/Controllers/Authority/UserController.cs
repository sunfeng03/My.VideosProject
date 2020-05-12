using My.Videos.IBLL;
using My.Videos.Ioc;
using My.Videos.Model.Authority;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace My.VideosProject.Controllers.Authority
{
    public class UserController : Controller
    {
        // GET: Authority
        public ActionResult UserList()
        {
            return View("~/Views/Authority/User/UserList.cshtml");
        }

        /// <summary>
        /// 获取用户分页信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetUserPage()
        {
            int page = !string.IsNullOrEmpty(Request["page"]) ? Convert.ToInt32(Request["page"]) : 1;
            int limit = !string.IsNullOrEmpty(Request["limit"]) ? Convert.ToInt32(Request["limit"]) : 10;
            var userId = !string.IsNullOrEmpty(Request["userId"]) ? Request["userId"] : "";
            var userName = !string.IsNullOrEmpty(Request["userName"]) ? Request["userName"] : "";

            var _userService = DependencyInjector.GetObject<IUserService>();
            var entity = _userService.GetUserPage(new UserRequest
            {
                PageIndex = page,
                PageSize = limit,
                UserId = userId,
                UserName = userName
            });
            if (entity != null && entity.count > 0)
            {
                entity.msg = "SUCCESS";
                entity.code = 0;
            }
            return Json(entity);
        }
    }
}