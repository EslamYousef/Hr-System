using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.Models
{
    public class Medical_Service_Classification
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        [Display(Name = "Classification Code")]
        public string Classification_Code { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public string Description { get; set; }
        public string TDescription { get; set; }

        [Display(Name = "Group Code")]
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public string Group_Medical_Service_GroupId { get; set; }
        public virtual Medical_Service_Group Medical_Service_Group { get; set; }

   
    }
}