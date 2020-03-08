using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;
namespace HR.Models
{
    public class Public_Holiday_Events
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string Code { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public string Description { get; set; }
        public string AlternativeDescription { get; set; }
        [Display(Name = "Type")]
        public Type_Holiday Type_Holiday { get; set; }
        [Display(Name = "TM Day status code")]
        [ForeignKey("Shift_day_status_setup")]
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public int ShiftdaystatussetupId { get; set; }
        public virtual Shift_day_status_setup Shift_day_status_setup { get; set; }
        public virtual List<Append_Public_Holidays_Dates> Append_Public_Holidays_Dates { get; set; }

    }
}