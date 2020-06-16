using HR.Models.payroll_trans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models.ViewModel
{
    public class loan_installmentVM
    {
        public LoanInstallment LoanInstallment { get; set; }
        public bool freez { get; set; } = false;
        
    }
}