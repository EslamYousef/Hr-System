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
        [Required]
        [Display(Name = "Employee No.")]
        public string Employee_ProfileId { get; set; }
        [Required]
        [Display(Name = "Contact profile No.")]
        public string Code { get; set; }
        public bool Primary { get; set; }
        [Display(Name = "Contact method code")]
        public string ContactmethodsId { get; set; }
        public virtual Contact_methods Contact_methods { get; set; }
        [Display(Name = "Contact method detail")]
        public string Contact_method_detail { get; set; }
        public string Comments { get; set; }
    }
}