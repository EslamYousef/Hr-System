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
        [Required]
        [Display(Name = "Employee No.")]
        public string Employee_ProfileId { get; set; }
        [Required]
        [Display(Name = "Beneficiary Profile No.")]
        public string Code { get; set; }
        public bool Legatee { get; set; }
        [Required]
        [Display(Name = "Benefit type code")]
        public string Benefit_type_codeId { get; set; }
        public virtual Subscription_Syndicate Subscription_Syndicate { get; set; }
        public virtual Employee_Profile Employee_Profile { get; set; }  
        [Display(Name = "Family profile No")]
        public string Family_profile_No { get; set; }
        [Display(Name = "Family profile No")]
        public string Family_profile_No2 { get; set; }
        [Display(Name = "Family name")]
        public string Family_name { get; set; }
        public int Percentage { get; set; }
        

    }
}