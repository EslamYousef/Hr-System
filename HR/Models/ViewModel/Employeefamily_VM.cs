using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models.ViewModel
{
    public class Employeefamily_VM
    {
        public string fullname { get; set; }
        public string code { get; set; }
        public int EmployeeId { get; set; }
        public Employee_family_profile Employee_family_profile { get; set; }
    }
}