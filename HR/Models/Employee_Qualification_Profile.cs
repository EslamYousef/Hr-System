using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;
namespace HR.Models
{
    public class Employee_Qualification_Profile
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Employee No.")]
        public string Employee_ProfileId { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Qualification profile No.")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string Code { get; set; }
        [Display(Name = "Related to job")]
        public bool Related_to_job { get; set; }
        [Display(Name = "Qualification start date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Qualification_start_date { get; set; }
        [Display(Name = "Qualification end date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Qualification_end_date { get; set; }
        public int Years { get; set; }
        public int Months { get; set; }
        [Display(Name = "Qualification category code")]
        public string Educate_categoryId { get; set; }
        public virtual Educate_category Educate_category { get; set; }
        [Display(Name = "Educate level code")]
        public string Educate_TitleId { get; set; }
        public virtual Educate_Title Educate_Title { get; set; }
        [Display(Name = "Qualification main provider code")]
        public string Main_Educate_bodyId { get; set; }
        public virtual Main_Educate_body Main_Educate_body { get; set; }
        [Display(Name = "Qualification sub provider code")]
        public string Sub_educational_bodyId { get; set; }
        public virtual Sub_educational_body Sub_educational_body { get; set; }
        [Display(Name = "Qualification name code")]
        public string Name_of_educational_qualificationId { get; set; }
        public virtual Name_of_educational_qualification Name_of_educational_qualification { get; set; }
        [Display(Name = "Qualification specialty code")]
        public string Qualification_MajorId { get; set; }
        public virtual Qualification_Major Qualification_Major { get; set; }
        [Display(Name = "Qualification degree code ")]
        public string GradeEducateId { get; set; }
        public virtual GradeEducate GradeEducate { get; set; }
        public int Extra_education_years { get; set; }
        public double Allowance_value { get; set; }

    }
}