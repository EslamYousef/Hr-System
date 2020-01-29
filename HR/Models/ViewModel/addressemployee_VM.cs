using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models.ViewModel
{
    public class addressemployee_VM
    {
        public string fullname { get; set; }
        public string code { get; set; }
        public int EmployeeId { get; set; }

        public Employee_Address_Profile Employee_Address_Profile { get; set; }
    }
}