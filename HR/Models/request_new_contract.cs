﻿using HR.Models.Infra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class request_new_contract
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Employee No.")]
        public string Employee_ProfileId { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Contract No.")]
        public string Code { get; set; }
        [Display(Name = "Contract Type")]
        public string ContractTypeId { get; set; }
        [Display(Name = "Approved by")]
        public string ApprovedbyId { get; set; }
        public virtual Contract_Type Contract_Type { get; set; }
        [Display(Name = "Employment type")]
        public Employment_type Employment_type { get; set; } = Employment_type.Fulltime;
        [Display(Name = "Contract start date ")]
        public DateTime Contract_start_date { get; set; }
        [Display(Name = "Contract end date ")]
        public DateTime Contract_end_date { get; set; }
        public int Years { get; set; }
        public int Months { get; set; }
        [Display(Name = "Approved date ")]
        public DateTime Approved_date { get; set; }
        public string Notes { get; set; }

        [Display(Name = "Medical date ")]
        public DateTime Medical_date { get; set; }
        [Display(Name = "Medical entity name")]
        public string Medical_entity_name { get; set; }

        [Display(Name = "Not fit reason")]
        public string Not_fit_reason { get; set; }
        [Display(Name = "Medical commite comments")]
        public string Medical_commite_comments { get; set; }
        [Display(Name = "Medical commite recomindation")]
        public Medical_commite_recomindation Medical_commite_recomindation { get; set; } = Medical_commite_recomindation.Fit;
        public Medical_commite_recomindation Result { get; set; } = Medical_commite_recomindation.Fit;
        public virtual Employee_Profile Employee_Profile { get; set; }
        public virtual status status { get; set; }
    }
}