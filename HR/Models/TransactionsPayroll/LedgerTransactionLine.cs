using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;

namespace HR.Models.TransactionsPayroll
{
    [Table("LedgerTransactionLine")]
    public class LedgerTransactionLine
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string JournalNumber { get; set; }
        public int LineNumber { get; set; }
        public string AccountNumber { get; set; }
        public string CostCenterCode { get; set; }
        public string CostCenterMask { get; set; }
        public double AmountDebit { get; set; }
        public double AmountCredit { get; set; }
        public string Currency_Code { get; set; }
        public string Company_ID { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public string Modified_By { get; set; }
        public Nullable<System.DateTime> Modified_Date { get; set; }
        public int RowIndx { get; set; }
    }
}