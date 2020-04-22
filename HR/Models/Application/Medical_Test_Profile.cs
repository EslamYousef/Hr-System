using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;

namespace HR.Models.Application
{
    public class Medical_Test_Profile
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Try Number")]
        public string TryNumber { get; set; }
        [Display(Name = "Test Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Test_Date { get; set; }
        [Display(Name = "Medical Entity")]   
        public string Medical_Entity { get; set; }
        [Display(Name = "Test Result")]
        public Medical_commite_recomindation Test_Result { get; set; } = Medical_commite_recomindation.Fit;
        [Display(Name = "Not Fit Reason")]
        public string Not_Fit_Reason { get; set; }
        public string Comments { get; set; }
        [Display(Name = "Final Test Result")]
        public Medical_commite_recomindation Final_Test_Result { get; set; } = Medical_commite_recomindation.Fit;
        [Display(Name = "Applicant Id")]
        public int ApplicantId { get; set; }
        public virtual Application Application { get; set; }
    }
}