using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.Models
{
    public class Sings_Symptom
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Index(IsUnique = true)]
        [Display(Name = "Sing Code")]
        public double Sing_Code { get; set; }
        [Required]
        [Display(Name = "Sing Name")]
        public string Sing_Name { get; set; }
        public string Notes { get; set; }
    }
}