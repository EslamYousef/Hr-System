using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class check_list_EOS
    {
        public int id { get; set; }
        public bool interpolation { get; set; }
     
        public virtual EOS_Request EOS { get; set; }

        public virtual Check_Lists_Items item { get; set; }

    }
}