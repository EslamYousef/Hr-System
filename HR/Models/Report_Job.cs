using HR.Models.Infra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class Report_JobVM
    {

    //    public string codes { get; set; }
        public string name { get; set; }
        [Display(Name ="job setup")]
        public string job_setup { get; set; }
        public string working_system { get; set; }
        public string chart { get; set; }
        public string parent { get; set; }
        public string nationality { get; set; }
        public string sub_class { get; set; }
        public string parmanet { get; set; }
        public string validity { get; set; }
        public string gender { get; set; }
        public working_system working_system_List { get; set; }
        public parment parmet_List { get; set; }
        public gender gender_list { get; set; }
        public validity validity_list { get; set; }
        public int numSlot { get; set; }
        public int fromAge { get; set; }
        public int toAge { get; set; }
              
        public string List_Display { get; set; }

    }
}