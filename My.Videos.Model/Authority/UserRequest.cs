using My.Videos.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Videos.Model.Authority
{
    public class UserRequest : BaseRequest
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

    }
}
