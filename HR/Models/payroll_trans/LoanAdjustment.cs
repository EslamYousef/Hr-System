using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HR.Models.payroll_trans
{
    [Table("LoanAdjustment")]
    public partial class LoanAdjustment
    {
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]

        public string TransactionNumber { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]

        public string LoanRequestNumber { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]

        public Nullable<System.DateTime> TransactionDate { get; set; }
        public Nullable<int> TransactionStatus { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedComments { get; set; }
        public string ReportAsReadyBy { get; set; }
        public Nullable<System.DateTime> ReportAsReadyDate { get; set; }
        public string ReportAsReadyComments { get; set; }
        public string InstallmentNumber { get; set; }
        public Nullable<double> InstallmentAmount { get; set; }
        public Nullable<double> PaidAmount { get; set; }
        public Nullable<double> TotalPaidAmount { get; set; }
        public Nullable<double> TotalUnpaidAmount { get; set; }
        public string Company_ID { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public string Modified_By { get; set; }
        public Nullable<System.DateTime> Modified_Date { get; set; }
        public int RowIndx { get; set; }
    }
}