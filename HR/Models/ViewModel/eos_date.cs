using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models.ViewModel
{
    public class eos_date
    {
        public List<EOS_Request> eos { get; set; }
        public List<string> req_date{get;set;}
        public List<string> eos_Date { get; set; }
    }
}