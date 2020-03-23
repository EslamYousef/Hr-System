using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;
namespace HR.Models
{
    public class Employee_contact_profile
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Employee No.")]
        public string Employee_ProfileId { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Contact profile No.")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string Code { get; set; }
        public bool Primary { get; set; }
   
        [Display(Name = "Contact method code")]
        public string ContactmethodsId { get; set; }
        public virtual Contact_methods Contact_methods { get; set; }
        [Display(Name = "Contact method detail")]
        public string Contact_method_detail { get; set; }
        public string Comments { get; set; }
        public virtual Employee_Profile Employee_Profile { get; set; }

    }
}