using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models.ViewModel
{
    public class SalaryCode_ExtendedFields_VM
    {
        public int ID { get; set; }

        public string SalaryCodeID { get; set; }
        public string SalaryCodeDesc { get; set; }
        public Nullable<int> CodeGroupType { get; set; }
        public Nullable<int> CodeValueType { get; set; }
        public Nullable<int> DefaultValue { get; set; }
        public string ExtendedFields_Code { get; set; }
        public string ExtendedFields_Description { get; set; }

    }
}