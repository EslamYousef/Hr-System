using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HR.Models.SetupPayroll
{
    public class LoanInAdvanceSetup
    {
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Code")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string LoanTypeCode { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]

        public string LoanTypeDesc { get; set; }
        public string LoanTypeAltDesc { get; set; }
        public Nullable<int> LoanEligibilityType { get; set; }
        public string EligibilityMatrixCode { get; set; }



       
        public Nullable<bool> EnableToGenerateManualPayment { get; set; }
        public string ManualPaymentCode { get; set; }
        public Nullable<int> LoanAmountType { get; set; }


        public Nullable<decimal> MinimumAmount { get; set; }
        public Nullable<decimal> MaximumAmount { get; set; }
        public Nullable<bool> EnableLoanAmountRestriction { get; set; }
       
        
        public Nullable<decimal> Percentage { get; set; }
        public string SalaryCodeAmount { get; set; }


        public Nullable<int> InstallmentPeriodType { get; set; }
        public Nullable<decimal> PeriodLength { get; set; }
        public Nullable<decimal> InstallmentAmount { get; set; }
        public Nullable<bool> EnableFreezing { get; set; }
        public Nullable<bool> EnableAutomaticPayrollDeduction { get; set; }
        public string SalaryCodeID { get; set; }
        public Nullable<int> NumberOfDeductedInstallments { get; set; }


        public Nullable<bool> EnableToCreateLoanRequestBeforeCloseTheNextTypes { get; set; }
        public Nullable<bool> AllLoanTypes { get; set; }



        public string Interval { get; set; }
        public Nullable<int> IntervalType { get; set; }
        public Nullable<decimal> IntervalPeriod { get; set; }
        public Nullable<bool> EnableToRecuiningLoanRequestAutomaticAfterCloseTheRequest { get; set; }


        public string Company_ID { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public string Modified_By { get; set; }
        public Nullable<System.DateTime> Modified_Date { get; set; }
        public int RowIndx { get; set; }
    }
}