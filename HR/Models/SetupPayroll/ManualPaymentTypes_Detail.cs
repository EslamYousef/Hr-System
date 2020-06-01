using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HR.Models.SetupPayroll
{
    [Table("ManualPaymentTypes_Detail")]
    public class ManualPaymentTypes_Detail
    {
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public string PaymentTypeCode { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public string SalaryCodeID { get; set; }
        public Nullable<int> DefaultValue { get; set; }
        public string Company_ID { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public string Modified_By { get; set; }
        public Nullable<System.DateTime> Modified_Date { get; set; }
        public int RowIndx { get; set; }
        public string Type_Code { get; set; }
        public string Salarycodedescription { get; set; }
        public string Type { get; set; }
        public string ValueType { get; set; }
        public virtual List<LinkLoanDeductionsWithOtherManualPayment> LinkLoanDeductionsWithOtherManualPayment { get; set; }

    }
}