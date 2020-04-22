using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;

namespace HR.Models.Application
{
    public class Business_Test_Profile
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Try Number")]
        public string TryNumber { get; set; }
        [Display(Name = "Test Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Test_Date { get; set; }
        [Display(Name = "Test Code")]
        public string TestCode { get; set; }
        [Display(Name = "Test Description")]
        public string TestDescription { get; set; }
        [Display(Name = "Pass Mark")]
        public int Pass_Mark { get; set; }
        [Display(Name = "Full Mark")]
        public int Full_Mark { get; set; }
        [Display(Name = "Qbtain Mark")]
        public int Qbtain_Mark { get; set; }
        [Display(Name = "Test location Code")]
        public string Test_location_Code { get; set; }
        [Display(Name = "Location Description")]
        public string Location_Description { get; set; }
        [Display(Name = "Work Location")]
        public work_location work_location { get; set; }
        public string Comments { get; set; }
        [Display(Name = "Applicant Id")]
        public int ApplicantId { get; set; }
        public virtual Application Application { get; set; }
    }
}