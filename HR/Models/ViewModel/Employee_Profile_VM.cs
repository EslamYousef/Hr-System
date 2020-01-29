using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models.ViewModel
{
    public class Employee_Profile_VM
    {
        public Employee_Profile Employee_Profile { get; set; }
        public Ability Ability { get; set; }
        public Personnel_Information Personnel_Information { get; set; }
        public Service_Information Service_Information { get; set; }

    }
}