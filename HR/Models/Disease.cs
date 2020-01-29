using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.Models
{
    public class Disease
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Index(IsUnique = true)]
        [Display(Name = "Disease Code")]
        public double Disease_Code { get; set; }
        [Required]
        [Display(Name = "Disease Name")]
        public string Disease_Name { get; set; }
        [Display(Name = "Disease TName")]
        public string Disease_TName { get; set; }
        public string Notes { get; set; }
    }
}