using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;

namespace HR.Models
{
    public class Employee_military_service_calling
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Employee No.")]
        public string Employee_ProfileId { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Contract No.")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string Code { get; set; }
        [Display(Name = "Start date ")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Start_date { get; set; }
        [Display(Name = "End date ")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime End_date { get; set; }
        public double Years { get; set; }
        public double Months { get; set; }
        public double Days { get; set; }
        public string Comments { get; set; }
        public virtual Employee_Profile Employee_Profile { get; set; }

    }
}