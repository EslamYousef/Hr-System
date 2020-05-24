using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models.CardPayroll
{
    public class VMFin
    {
        public Employee_Financial_Contract_Header Employee_Financial_Contract_Header { get; set; }
        public List<Employee_Financial_Contract_Detail> Employee_Financial_Contract_Detail { get; set; }
    }
}