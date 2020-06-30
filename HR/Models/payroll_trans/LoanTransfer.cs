using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HR.Models.payroll_trans
{
    [Table("LoanTransfer")]
    public partial class LoanTransfer
    {
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]

        public string TransferTransactionNumber { get; set; }
        public Nullable<int> TransactionStatus { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]

        public string LoanRequestNumber { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]

        public string NewLoanRequestNumber { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]

        public string LoanTypeCode { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]

        public string ToEmplpyee_Code { get; set; }
        public Nullable<double> LoanAmount { get; set; }
        public Nullable<double> LoanInstallmentAmount { get; set; }
        public Nullable<double> NumberOfInstallment { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<int> NumberOfDeductedInstallments { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedComments { get; set; }
        public string ReportAsReadyBy { get; set; }
        public Nullable<System.DateTime> ReportAsReadyDate { get; set; }
        public string ReportAsReadyComments { get; set; }
        public string Notes { get; set; }
        public string Company_ID { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public string Modified_By { get; set; }
        public Nullable<System.DateTime> Modified_Date { get; set; }
        public int RowIndx { get; set; }
        public string from_emp { get; set; }
        public string to_emp { get; set; }
    }
}