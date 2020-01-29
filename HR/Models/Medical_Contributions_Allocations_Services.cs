using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HR.Models
{
    public class Medical_Contributions_Allocations_Services
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Con./Alloc. Code")]
        public string Medical_Contributions_Allocations_EntryId { get; set; }
        public virtual Medical_Contributions_Allocations_Entry Medical_Contributions_Allocations_Entry { get; set; }
        [Required]
        [Display(Name = "Classification Code")]
        public string Classification_CodeId { get; set; }
        public virtual Medical_Service_Classification Medical_Service_Classification { get; set; }
        [Required]
        [Display(Name = "Service Code")]
        public string Service_CodeId { get; set; }
        public virtual Medical_Service Medical_Service { get; set; }
     
    }
}