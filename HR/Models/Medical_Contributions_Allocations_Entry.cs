using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;

namespace HR.Models
{
    public class Medical_Contributions_Allocations_Entry
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        [Display(Name ="Allo.Con. Code")]
        public string Code { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public string Name { get; set; }
        public string TName { get; set; }
        [Display(Name = "Is Contribution")]
        public bool Is_Contribution { get; set; }
        [Display(Name = "Allowed To")]
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public gender Allowed_To { get; set; }
    }
}