using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class Authorities:BaseModel
    {
        [Display(Name = "Description")]
        public string Skill_Description { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Authority Type")]
        public string Authority_TypeId { get; set; }
        public virtual Authority_Type Authority_Type { get; set; }
    }
}