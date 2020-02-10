using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.Models
{
    public class Allergy_Type
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Index(IsUnique = true)]
        [Display(Name = "Allergy Code")]
        public double Allergy_Code { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Allergy Name")]
        public string Allergy_Name { get; set; }
        [Display(Name = "Allergy TName")]
        public string Allergy_TName { get; set; }
        public string Notes { get; set; }
    }
}