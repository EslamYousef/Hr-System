using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models.ViewModel
{
    public class organization_type_vm
    {
        public Organization_Unit_Type Educate_Title { get; set; }
        public bool selected { get; set; }
        public int job_level_setup_id { get; set; }
    }
}