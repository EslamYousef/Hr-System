using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HR.Models
{
    public class Weekend_setup
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public string Code { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [StringLength(50)]
        [Index(IsUnique =true)]
        public string Description { get; set; }
        [Display(Name = "Alternative Description")]
        public string Alternative_Description { get; set; }
        [Display(Name = "TM Day status code")]
        [ForeignKey("Shift_day_status_setup")]
        public string ShiftdaystatussetupId { get; set; }
        public virtual Shift_day_status_setup Shift_day_status_setup { get; set; }

    }
}