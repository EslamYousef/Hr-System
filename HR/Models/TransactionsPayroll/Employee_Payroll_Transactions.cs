using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;

namespace HR.Models.TransactionsPayroll
{
    [Table("Employee_Payroll_Transactions")]
    public class Employee_Payroll_Transactions
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
        public string SalaryCodeID { get; set; }
        public string JournalName_BatchCode { get; set; }
        public string Contract_Number { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
        public Nullable<System.DateTime> EffectiveDate { get; set; }
        public Nullable<int> TransactionMonth { get; set; }
        public Nullable<int> TransactionYear { get; set; }
        public Nullable<int> EffectiveMonth { get; set; }
        public Nullable<int> EffectiveYear { get; set; }
        public Nullable<double> TransactionValue { get; set; }
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
        public Nullable<int> SourceDocumentType { get; set; }
        public string SourceDocumentRefrence { get; set; }
        public string SourceDocumentDescription { get; set; }
        public string SourceDocumentNotes { get; set; }
        public string TransactionNotes { get; set; }
        public string CostCenterCode { get; set; }
        public string ExtendedFields_Code { get; set; }
        public string Company_ID { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public string Modified_By { get; set; }
        public Nullable<System.DateTime> Modified_Date { get; set; }
        public int RowIndx { get; set; }
        public virtual status status { get; set; }
        public check_status check_status { get; set; }

        public int statID { get; set; }
        public string name_state { get; set; }
    }
}