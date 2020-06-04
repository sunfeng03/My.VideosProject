using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Videos.Model.Authority
{
    public class MenuResponse
    {
        public string title { get; set; }

        public int id { get; set; }

        public string field { get; set; }

        public List<ChildernMenu> children { get; set; }

    }

    public class ChildernMenu
    {
        public string title { get; set; }

        public int id { get; set; }

        public string field { get; set; }
    }
}
