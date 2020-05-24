using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models.CardPayroll
{
    public class VMs
    {
        public SalaryItemCollectionGroup_Header SalaryItemCollectionGroup_Header { get; set; }
        public List<SalaryItemCollectionGroup_Detail> SalaryItemCollectionGroup_Detail { get; set; }
    }
}