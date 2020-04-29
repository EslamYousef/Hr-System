using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;
using HR.Models.All_Table_Commitee_Resolution;

namespace HR.Models.Application
{
    public class Personnel_Committee_Profile
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Committee Resolution No.")]
        public string Committe_Resolution_RecuirtmentId { get; set; }
        public virtual Committe_Resolution_Recuirtment Committe_Resolution_Recuirtment { get; set; }
        public string Subject { get; set; }
        [Display(Name = "Activity Serial No.")]
        public string Activity_Serial_No { get; set; }
        public virtual Committe_Activities Committe_Activities { get; set; }
        //[Display(Name = "Planned Date")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        //public DateTime Planned_Date { get; set; }
        //[Display(Name = "Actual Date")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        //public DateTime Actual_Date { get; set; }
        [Display(Name = "Committee Approval Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")] 
        public DateTime Committee_Approval_Date { get; set; }
        [Display(Name = "Applicant Id")]
        public int ApplicantId { get; set; }
        public virtual Application Application { get; set; }
    }
}