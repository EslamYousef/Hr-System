using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;

namespace HR.Models.TransactionsPayroll
{
    [Table("LedgerTransaction")]
    public class LedgerTransaction
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string JournalNumber { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public string JournalDescription { get; set; }
        public int JournalType { get; set; }
        public string JournalName { get; set; }
        public string Currency_Code { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> TransactionDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public System.DateTime PostedDate { get; set; }
        public double TotalAmountDebit { get; set; }
        public double TotalAmountCredit { get; set; }
        public bool Posted { get; set; }
        public string ERP_JournalNumber { get; set; }
        public string PaymentJournalNumber { get; set; }
        public string ERP_PaymentJournalNumber { get; set; }
        public string Company_ID { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public string Modified_By { get; set; }
        public Nullable<System.DateTime> Modified_Date { get; set; }
        public int RowIndx { get; set; }
    }
}