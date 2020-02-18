using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HR.Models
{
    public class Employee_Profile_Groups
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Group Code")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string Group_Code { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Group Description")]
        public string Group_Description { get; set; }
        [Display(Name = "Group Alternative Description")]
        public string Group_Alternative_Description { get; set; }
    }
}