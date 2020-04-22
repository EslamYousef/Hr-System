using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;

namespace HR.Models.Application
{
    public class Application_Status
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Application Status")]
        public ApplicationStatus ApplicationStatus { get; set; } = ApplicationStatus.Interview_Test;
        [Display(Name = "Status Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime StatusDate { get; set; }
        public string Comments { get; set; }
        [Display(Name = "Applicant Id")]
        public int ApplicantId { get; set; }
        public virtual Application Application { get; set; }
    }
}