using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models.ViewModel
{
    public class EmployeeMilitary_VM
    {
        public string fullname { get; set; }
        public string code { get; set; }
        public int EmployeeId { get; set; }

        public Employee_military_service_profile Employee_military_service_profile { get; set; }
    }
}