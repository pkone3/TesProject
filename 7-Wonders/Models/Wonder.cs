using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SevenWonders.Models
{
    public class Wonder
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class WonderList
    {
        public List<Wonder> Wonders { get; set; }
    }
}
