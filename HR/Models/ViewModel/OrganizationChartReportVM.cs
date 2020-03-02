using HR.Models.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models.ViewModel
{
    public class OrganizationChartReportVM
    {
        public check_status check_status { get; set; }
        public int numberDirectPosition { get; set; }
        public string userUnit { get; set; }
        public string unitDescription { get; set; }
        public string parent { get; set; }
        public string unitType { get; set; }
        public string unitLevel { get; set; }
        public string unitSchema {get;set;}
        public string loaction { get; set; }
        public string List_Display { get; set; }
    }
    public class reportVM
    {
        public List<Organization_Chart> Organization_Chart { get; set; }
        public Boolean[] listDisplay { get; set; } 
    }

}