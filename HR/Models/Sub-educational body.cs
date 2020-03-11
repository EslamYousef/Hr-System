using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class Sub_educational_body
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string Code { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Display(Name = "Main Educate body")]
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public int Main_Educate_bodyid { get; set; }
        public virtual Main_Educate_body Main_Educate_body { get; set; }
        public virtual Name_of_educational_qualification Name_of_educational_qualification { get; set; }

        public string Name_of_educational_qualification_IDD { get; set; }

    }
}