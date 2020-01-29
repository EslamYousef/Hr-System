using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.Models
{
    public class Medical_Doctors_Level
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Index(IsUnique = true)]
        [Display(Name = "Level Code")]
        public double Level_Code { get; set; }
        [Required]
        [Display(Name = "Level Name")]
        public string Level_Name { get; set; }
        public string Notes { get; set; }
    }
}