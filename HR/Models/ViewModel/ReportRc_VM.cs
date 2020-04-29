using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Hosting;
using System.Web;
using HR.Models.Application;

namespace HR.Models.ViewModel
{
    public class ReportRc_VM
    {
        public List<Applicant_Profile> jobs { get; set; }
        public Boolean[] my_list { get; set; }
    }
}