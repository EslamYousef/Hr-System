using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;
namespace HR.Models
{
    public class StructureModels
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Structure Name")]
        public string Structure_Code { get; set; }

        [Required]
        [Index(IsUnique = true)]
        public ChModels All_Models { get; set; }

    }
}