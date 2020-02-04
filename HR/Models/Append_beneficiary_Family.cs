using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;
namespace HR.Models
{
    public class Append_beneficiary_Family
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Family profile No")]
        public string Family_profile { get; set; }
        [Display(Name = "Family name")]
        public string Family_name { get; set; }
        public int Percentage { get; set; }
        public virtual Employee_beneficiary_profile Employee_beneficiary_profile { get; set; }

    }
}