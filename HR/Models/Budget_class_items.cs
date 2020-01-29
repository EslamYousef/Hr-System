using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class Budget_class_items
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Code")]
      //  [Index(IsUnique = true)]
        public string Code { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Required]
        public string Budget_classId { get; set; }
        public virtual Budget_class Budget_class { get; set; }
    }
}