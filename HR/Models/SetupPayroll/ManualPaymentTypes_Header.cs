using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HR.Models.SetupPayroll
{
    public class ManualPaymentTypes_Header
    { 
          public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Code")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string PaymentTypeCode { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]

        public string PaymentTypeDesc { get; set; }
        public string PaymentTypeAltDesc { get; set; }
        public Nullable<int> PaymentTypeSourceDocument { get; set; }
        public Nullable<short> Transaction_Type { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]

        public string DebitAccountNumber { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]

        public string CreditAccountNumber { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]

        public string JournalName_BatchCode { get; set; }
        public Nullable<int> Frequency { get; set; }
        public Nullable<short> FrequencyPeriodType { get; set; }
        public string ExtendedFields_Code { get; set; }
        public string Company_ID { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public string Modified_By { get; set; }
        public Nullable<System.DateTime> Modified_Date { get; set; }
        public int RowIndx { get; set; }
        public string Type_Code { get; set; }
    }
}