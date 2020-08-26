using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;
namespace HR.Models.Vacations
{
    [Table("LeavesTransactionBalance")]
    public class LeavesTransactionBalance
    {
        [Key]
        public int ID { get; set; }
        public long SerialLTB { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string Serial_LB { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public int EmployeeID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public int VacCode { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
        public Nullable<double> Value { get; set; }
        public string Notes { get; set; }
        public Nullable<double> Balance { get; set; }
        public Nullable<double> Remain { get; set; }
        public string SerialNo { get; set; }
        public string Company_ID { get; set; }
        public string Created_By { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> Created_Date { get; set; }
        public string Modified_By { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> Modified_Date { get; set; }
        public Nullable<int> RowIndx { get; set; }

        public int Year { get; set; }
        public bool Check { get; set; }

    }
}