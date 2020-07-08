using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.Models
{
    public class Medical_Doctors
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        [Display(Name = "Code")]
        public string Code { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Doctor Name")]
        public string Doctor_Name { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Doctor TName")]
        public string Doctor_TName { get; set; }
        public string Notes { get; set; }
        [Display(Name = "Medical Doctors Level")]
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public string Medical_Doctors_LevelId { get; set; }
        public virtual Medical_Doctors_Level Medical_Doctors_Level { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public int EmpCode { get; set; }
    }
}