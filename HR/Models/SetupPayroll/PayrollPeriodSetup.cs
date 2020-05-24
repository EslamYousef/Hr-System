using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;

namespace HR.Models.SetupPayroll
{
    [Table("PayrollPeriodSetup")]
    public class PayrollPeriodSetup
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        [Display(Name = "Period Code")]
        public string PeriodCode { get; set; }
        [Display(Name = "Period Description")]
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public string PeriodDesc { get; set; }
        [Display(Name = "Period Alternative Description")]
        public string PeriodAltDesc { get; set; }
        [Display(Name = "Period Type")]
        public Nullable<int> PeriodType { get; set; }
        [Display(Name = "Number Of Days")]
        public Nullable<int> NumberOfDays { get; set; }
        [Display(Name = "Start Pay Month From Day")]
        public Nullable<int> StartPayMonthFromDay { get; set; }
        [Display(Name = "End Pay Month To Day")]
        public Nullable<int> EndPayMonthToDay { get; set; }
        public string Company_ID { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public string Modified_By { get; set; }
        public Nullable<System.DateTime> Modified_Date { get; set; }
        public int RowIndx { get; set; }
    }
}