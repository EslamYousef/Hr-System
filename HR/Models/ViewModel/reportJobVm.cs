using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models.ViewModel
{
    public class reportJobVm
    {
        public List<job_title_cards> jobs { get; set; }
        public Boolean [] my_list   {get;set;}
    }
}