using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.Models
{
    public class Medical_Service
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Index(IsUnique = true)]
        [Display(Name = "Service Code")]
        public double Service_Code { get; set; }
        [Required]
        public string Name { get; set; }
        public string TName { get; set; }
        [Required]
        [Display(Name = "Classification Code")]
        public string Medical_Service_ClassificationId { get; set; }
        public virtual Medical_Service_Classification Medical_Service_Classification { get; set; }
    }
}