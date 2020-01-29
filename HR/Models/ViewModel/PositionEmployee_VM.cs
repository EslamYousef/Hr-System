using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models.ViewModel
{
    public class PositionEmployee_VM
    {
        public string fullname { get; set; }
        public string code { get; set; }
        public int EmployeeId { get; set; }
        public Position_Information Position_Information { get; set; }

    }
}