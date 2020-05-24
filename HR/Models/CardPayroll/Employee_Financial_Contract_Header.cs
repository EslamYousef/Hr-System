using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;

namespace HR.Models.CardPayroll
{
    [Table("Employee_Financial_Contract_Header")]
    public class Employee_Financial_Contract_Header
    {

        [Key]
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string Contract_Number { get; set; }
        public string Employee_Code { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> From_Date { get; set; }
        public Nullable<System.DateTime> To_Date { get; set; }
        public string Company_ID { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public string Modified_By { get; set; }
        public Nullable<System.DateTime> Modified_Date { get; set; }
        public int RowIndx { get; set; }
        public virtual Employee_Profile Employee_Profile { get; set; }
        public virtual List<FinancialContract_ExtendedFieldsDetails> FinancialContract_ExtendedFieldsDetails { get; set; }

    }
}