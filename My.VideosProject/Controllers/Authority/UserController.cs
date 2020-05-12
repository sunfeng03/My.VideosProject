using My.Videos.IBLL;
using My.Videos.Ioc;
using My.Videos.Model.Authority;
using My.Videos.Model.Base;
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
        private readonly IUserService _userService;
        public UserController(IUserService articleRepository)
        {
            _userService = articleRepository;
        }

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
            var userId = !string.IsNullOrEmpty(Request["UserId"]) ? Request["UserId"] : "";
            var userName = !string.IsNullOrEmpty(Request["UserName"]) ? Request["UserName"] : "";

            var entity = _userService.GetUserPage(new UserRequest
            {
                PageIndex = page,
                PageSize = limit,
                UserId = userId,
                UserName = userName,
                Order = "CreateTime",
                Sort = "desc"
            });
            if (entity != null && entity.count > 0)
            {
                entity.msg = "SUCCESS";
                entity.code = 0;
            }
            return Json(entity);
        }

        /// <summary>
        /// 新增用户|修改用户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult AddUser()
        {
            var param = string.IsNullOrEmpty(Request["param"]) ? "" : Request["param"];
            if (param == "")
                return Json(new BaseResponse { isSuccess = false, msg = "参数不能为空" });

            var userObj = JsonConvert.DeserializeObject<User>(param);
            var userInfo = _userService.AddUser(userObj);
            return Json(userInfo);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult BathDeleteUser()
        {
            var param = string.IsNullOrEmpty(Request["ids"]) ? "" : Request["ids"];
            if (param == "")
                return Json(new BaseResponse { isSuccess = false, msg = "参数不能为空" });

            var userInfo = _userService.BathDeleteUser(param.Split(',').ToList());
            return Json(userInfo);
        }
    }
}