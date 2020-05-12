using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Videos.Model.Base
{
    public class BaseResponse
    {
        public int code { get; set; }

        public string msg { get; set; }

        public bool _success = false;
        public bool isSuccess
        {
            get { return _success; }
            set { _success = value; }
        }
    }
}
