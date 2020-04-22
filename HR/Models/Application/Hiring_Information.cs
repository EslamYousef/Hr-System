using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;

namespace HR.Models.Application
{
    public class Hiring_Information
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Committee Recommendation")]
        public string Committee_Recommendation { get; set; }
        [Display(Name = "Final Medical Test")]
        public Medical_commite_recomindation Medical_commite_recomindation { get; set; } = Medical_commite_recomindation.Fit;
        [Display(Name = "Applicant Id")]
        public string Employee_ID { get; set; }
        [Display(Name = "Hire Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime HireDate { get; set; }
        [Display(Name = "Join Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime JoinDate { get; set; }
        [Display(Name = "Oil Sector Join Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime OilSectorJoinDate { get; set; }
        [Display(Name = "Social Insurance Num")]
        public string SocialInsuranceNum { get; set; }
        [Display(Name = "Social Insurance Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime SocialInsuranceDate { get; set; }
        [Display(Name = "Service Period Ins (Y)")]
        public int Service_Period_Ins_Y { get; set; }
        [Display(Name = "Service Period Ins (M)")]
        public int Service_Period_Ins_M { get; set; }
        [Display(Name = "Weekend Code")]
        public int? WeekendCodeId { get; set; }
        [Display(Name = "Applicant Id")]
        public int ApplicantId { get; set; }

        public virtual Application Application { get; set; }
    }
}