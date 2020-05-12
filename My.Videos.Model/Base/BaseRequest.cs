using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Videos.Model.Base
{
    public class BaseRequest
    {
        public int PageSize { get; set; }

        public int PageIndex { get; set; }
        /// <summary>
        /// 升序或降序(asc|desc)
        /// </summary>
        public string Sort { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public string Order { get; set; }
        public int TotalCount { get; set; }
    }
}
