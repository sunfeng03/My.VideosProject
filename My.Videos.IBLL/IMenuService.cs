using My.Videos.Model.Authority;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Videos.IBLL
{
    public partial interface IMenuService
    {
        List<MenuResponse> GetALLMenuList();
    }
}
