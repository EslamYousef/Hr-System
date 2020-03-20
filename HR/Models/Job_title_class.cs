using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class Job_title_class
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Code")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string Code { get; set; }

        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
        
        public virtual List<special_allowance_job_level_class> special_allowance_job_level_class { get; set; }
    }
}