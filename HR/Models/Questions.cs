using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class Questions
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Question { get; set; }
        public string Standart_Question { get; set; }
        public virtual Files Files { get; set; }
        public string Filesid { get; set; }
    }
}