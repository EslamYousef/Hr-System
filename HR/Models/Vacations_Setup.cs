using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;

namespace HR.Models
{
    public class Vacations_Setup
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Leave Type Code")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string LeaveTypeCode { get; set; }
        [Display(Name = "Leave Type Name(English)")]
        public string LeaveTypeNameEnglish { get; set; }
        [Display(Name = "Leave Type Name (Arabic)")]
        public string LeaveTypeNameArabic { get; set; }
    }
}