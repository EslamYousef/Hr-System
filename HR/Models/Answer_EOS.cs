using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class Answer_EOS
    {
        public int ID { get; set; }
        public string answer { get; set;}
        public string Notes { get; set; }
        public virtual Definition_of_EOS_Interview_Questions question { get; set; }
        public virtual EOS_Request EOS { get; set; }
    }
}