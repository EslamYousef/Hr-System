using HR.Models.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models.ViewModel
{
    public class employeestate
    {
        public int empid { get; set; }
        public status status { get; set; }
        public check_status check_status { get; set; }
    }
}