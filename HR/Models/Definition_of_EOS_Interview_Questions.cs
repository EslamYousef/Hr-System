using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HR.Models
{
    public class Definition_of_EOS_Interview_Questions
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Question Code")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string Question_Code { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Question Description")]
        public string Question_Description { get; set; }
        public string Description { get; set; }
        [Display(Name = "Question Group")]
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public string Question_GroupId { get; set; }
        public virtual EOS_Interview_Questions_Groups EOS_Interview_Questions_Groups { get; set; }
    }
}