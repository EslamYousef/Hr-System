using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;

namespace HR.Models.TransactionsPayroll
{
    [Table("ManualPaymentTransactionEntry")]
    public class ManualPaymentTransactionEntry
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string TransactionNumber { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public string Employee_Code { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public string ManualPaymentType { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public Nullable<System.DateTime> TransactionDate { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public Nullable<System.DateTime> EffectiveDate { get; set; }
        public Nullable<double> NetAmount { get; set; }
        public Nullable<int> TransactionStatus { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ReportAsReadyBy { get; set; }
        public Nullable<System.DateTime> ReportAsReadyDate { get; set; }
        public string ApprovedBy { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public string RejectedBy { get; set; }
        public Nullable<System.DateTime> RejectedDate { get; set; }
        public string CanceledBy { get; set; }
        public Nullable<System.DateTime> CanceledDate { get; set; }
        public string CompletedBy { get; set; }
        public Nullable<System.DateTime> CompletedDate { get; set; }
        public Nullable<System.DateTime> PaidDate { get; set; }
        public string PaidReferenceNumber { get; set; }
        public Nullable<int> DocumentType { get; set; }
        public string ReferenceNumber { get; set; }
        public string ReferenceDescription { get; set; }
        public string ReferenceNote { get; set; }
        public string TransactionNote { get; set; }
        public string ExtendedFields_Code { get; set; }
        public string Company_ID { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public string Modified_By { get; set; }
        public Nullable<System.DateTime> Modified_Date { get; set; }
        public int RowIndx { get; set; }
        public Nullable<int> CurrentYear { get; set; }
        public Nullable<int> PreviousYear { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
    }
}