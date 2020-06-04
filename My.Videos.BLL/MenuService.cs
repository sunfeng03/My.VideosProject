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
    public partial class MenuService : BaseService<TblMenu>, IMenuService
    {
        /// <summary>
        /// 获取菜单信息
        /// </summary>
        /// <returns></returns>
        public List<MenuResponse> GetALLMenuList()
        {
            var res = new List<MenuResponse>();
            var whereLambda = PredicateBuilder.True<TblMenu>();
            var menuList = LoadEntities(whereLambda);
            if (menuList != null && menuList.Count() > 0)
            {
                //父结点
                var parentMenu = menuList.Where(p => p.ParentId == 0);
                MenuResponse MenuResponse = null;
                parentMenu.ToList().ForEach(p =>
                {
                    MenuResponse = new MenuResponse();
                    MenuResponse.id = p.Id;
                    MenuResponse.title = p.Name;
                    //子结点
                    var childrenMenu = menuList.Where(u => u.ParentId == p.Id);
                    if (childrenMenu != null && childrenMenu.Count() > 0)
                    {
                        var childrenList = new List<ChildernMenu>();
                        childrenMenu.ToList().ForEach(u =>
                        {
                            childrenList.Add(new ChildernMenu
                            {
                                id = u.Id,
                                title = u.Name
                            });
                        });
                        MenuResponse.children = childrenList;
                    }
                    res.Add(MenuResponse);
                });
            }
            return res;
        }
    }
}
