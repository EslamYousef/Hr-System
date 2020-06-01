using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HR.Models.SetupPayroll
{
    public class LinkLoanDeductionsWithOtherManualPayment
    {
        [Key]
        public int ID { get; set; }
        public string LoanTypeCode { get; set; }   //loan
        public string PaymentTypeCode { get; set; } ///header
        public string SalaryCodeID { get; set; }   //details
        public Nullable<int> NumberOfInstallments { get; set; }  //num
        public string Company_ID { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public string Modified_By { get; set; }
        public Nullable<System.DateTime> Modified_Date { get; set; }
        public int RowIndx { get; set; }

        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]

        public int ManualPaymentTypes_HeaderID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]

        public int ManualPaymentTypes_DetailID { get; set; }
        public int LoanInAdvanceSetupID { get; set; }
        public virtual ManualPaymentTypes_Detail ManualPaymentTypes_Detail { get; set; }
        public virtual ManualPaymentTypes_Header ManualPaymentTypes_Header { get; set; }
        public virtual LoanInAdvanceSetup LoanInAdvanceSetup { get; set; }
    }
}