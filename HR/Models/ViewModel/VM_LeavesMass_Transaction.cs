using HR.Models.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models.ViewModel
{
    public class VM_LeavesMass_Transaction
    {
        public int ID { get; set; }
        public string CodeEmp { get; set; }
        public string NameEmp { get; set; }
        public string CodeLeave { get; set; }
        public string NameLeave { get; set; }
        public double? YearlyBalance { get; set; }
        public double? ActualBalance { get; set; }
        public DateTime HiringDate { get; set; }
        public double TotalDays { get; set; }

    }
}