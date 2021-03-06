﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;
namespace HR.Models
{
    public class Employee_experience_profile
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Employee No.")]
        public string Employee_ProfileId { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Experience profile No.")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string Code { get; set; }
       
        [Display(Name = "Company code")]
        public string External_compainesId { get; set; }
        public virtual External_compaines External_compaines { get; set; }     
      
        [Display(Name = "Reason of leave")]
        public string Rejection_ReasonsId { get; set; }
        public virtual Rejection_Reasons Rejection_Reasons { get; set; }
        [Display(Name = "Company type")]
       
        public string Company_type { get; set; }
        [Display(Name = "Job title")]
        public string Job_title { get; set; }        
        [Display(Name = "Start date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Start_date { get; set; }
        [Display(Name = "End date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime End_date { get; set; }
        public int Years { get; set; }
        public int Days { get; set; }
        public int Months { get; set; }
        [Display(Name = "Total salary")]
        public double Total_salary { get; set; }
        [Display(Name = "Added months")]
        public int Added_months { get; set; }
        [Display(Name = "Added years")]
        public int Added_years { get; set; }
        [Display(Name = "Added days")]
        public int Added_days { get; set; }
        [Display(Name = "Considered period")]
        public int Considered_period { get; set; }
        [Display(Name = "Approval_date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Approval_date { get; set; }
        public virtual Employee_Profile Employee_Profile { get; set; }

    }
}