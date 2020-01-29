using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class Risks
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Code")]
     //   [Index(IsUnique = true)]
        public string Code { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Risks Type")]
        public string Risks_TypeId { get; set; }
        public virtual Risks_Type Risks_Type { get; set; } 
    }
}