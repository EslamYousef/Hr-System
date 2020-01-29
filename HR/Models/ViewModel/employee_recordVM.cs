using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models.ViewModel
{
    public class employee_recordVM
    {
        public Employee_records record { get; set; }
        public int selectedEmpoyee { get; set; }
        public int selectedgroup { get; set; }
    }
}