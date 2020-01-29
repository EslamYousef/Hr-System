using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models.ViewModel
{
    public class Employee_Qualification_VM
    {
        public string fullname { get; set; }
        public string code { get; set; }
        public int EmployeeId { get; set; }
        public Employee_Qualification_Profile Employee_Qualification_Profile { get; set; }
    }
}