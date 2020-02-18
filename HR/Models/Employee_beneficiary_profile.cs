using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;
namespace HR.Models
{
    public class Employee_beneficiary_profile
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Beneficiary Profile No.")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string Code { get; set; }
        public bool Legatee { get; set; }

        public virtual Employee_Profile Employee_Profile { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Employee No.")]
        public string Employee_ProfileId { get; set; }

        public virtual List<Append_beneficiary_Family> Append_beneficiary_Family { get; set; }


    }
}