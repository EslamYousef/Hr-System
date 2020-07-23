using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;

namespace HR.Models.Vacations
{
    [Table("LeavesBalance")]
    public class LeavesBalance
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string Serial_LB { get; set; }
        public int? EmployeeID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public int VacCode { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> BalanceStartDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> BalanceEndDate { get; set; }
        public Nullable<double> Balance { get; set; }
        public Nullable<double> Used { get; set; }
        public Nullable<double> UsedBySys { get; set; }
        public Nullable<double> Settled { get; set; }
        public Nullable<double> CashedDays { get; set; }
        public Nullable<double> EOSProportional { get; set; }
        public string Company_ID { get; set; }
        public string Created_By { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> Created_Date { get; set; }
        public string Modified_By { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> Modified_Date { get; set; }
        public int RowIndx { get; set; }
    }
}