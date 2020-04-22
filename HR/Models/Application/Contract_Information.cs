using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;

namespace HR.Models.Application
{
    public class Contract_Information
    {
        [Key]
        public int ID { get; set; }
        //[Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        //[Display(Name = "Applicant No")]
        //public string Code { get; set; }
        //[Display(Name = "Applicant Id")]
        //public string ApplicantId { get; set; }
        public string Contract { get; set; }
        [ForeignKey("Contract_Type")]
        [Display(Name = "Contract Type")]
        public int? Contract_TypeId { get; set; }
        public virtual Contract_Type Contract_Type { get; set; }
        public Employment_type Employment_type { get; set; } = Employment_type.Fulltime;
        [Display(Name = "Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Start_Date { get; set; }
        [Display(Name = "End Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime End_Date { get; set; }
        public int Years { get; set; }
        public int Months { get; set; }
        [ForeignKey("Employee_Profile")]
        [Display(Name = "Approved By")]
        public int? EmployeeProfileId { get; set; }
        public virtual Employee_Profile Employee_Profile { get; set; }
        [Display(Name = "Approved Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Approved_Date { get; set; }
        [Display(Name = "Applicant No")]
        public string ApplicantNotes { get; set; }
        [Display(Name = "Applicant Id")]
        public int ApplicantId { get; set; }
        public virtual Application Application { get; set; }






    }
}