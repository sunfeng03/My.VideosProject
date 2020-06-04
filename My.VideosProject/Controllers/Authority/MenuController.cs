using My.Videos.Common.Utils;
using My.Videos.EntityFramework.Authority;
using My.Videos.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace My.VideosProject.Controllers.Authority
{
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService;
        public MenuController(IMenuService menuRepsoitory)
        {
            _menuService = menuRepsoitory;
        }

        // GET: Menu
        public ActionResult MenuList()
        {
            return View("~/Views/Authority/Menu/MenuList.cshtml");
        }

        /// <summary>
        /// 获取所有菜单信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetAllMenu()
        {
            var menulist = _menuService.GetALLMenuList();
            return Json(menulist);
        }

        public JsonResult GetMenuById()
        {
            var id = string.IsNullOrEmpty(Request["Id"]) ? 0 : Convert.ToInt32(Request["Id"]);
            var whereLambda = PredicateBuilder.True<TblMenu>();
            whereLambda = whereLambda.And(u => u.Id == id);
            var menuEntity = _menuService.LoadEntities(whereLambda).ToList();
            return Json(menuEntity);
        }
    }
}