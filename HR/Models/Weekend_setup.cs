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
        public int ShiftdaystatussetupId { get; set; }
        public virtual Shift_day_status_setup Shift_day_status_setup { get; set; }
          
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }

    }
}