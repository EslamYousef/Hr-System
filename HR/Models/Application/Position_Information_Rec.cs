using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;

namespace HR.Models.Application
{
    public class Position_Information_Rec
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Applicant Id")]
        public int ApplicantId { get; set; }

        public virtual Application Application { get; set; }
        [Display(Name = "Job desc")]
        public string job_descId { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Slot desc")]
        public string SlotdescId { get; set; }
        public virtual job_title_cards job_title_cards { get; set; }
        [Display(Name = "Default location desc")]
        public string Default_location_descId { get; set; }
        [Display(Name = "Location desc")]
        public string Location_descId { get; set; }
        [Display(Name = "Work Location")]
        public work_location work_location { get; set; }

        [Display(Name = "Job level desc")]
        public string Job_level_gradeId { get; set; }
        public virtual Job_level_grade Job_level_grade { get; set; }
        public virtual job_level_setup job_level_setup { get; set; }

        [Display(Name = "Unit desc")]
        public string Organization_ChartId { get; set; }
        public virtual Organization_Chart Organization_Chart { get; set; }

        [Display(Name = "From date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime From_date { get; set; }
        [Display(Name = "To date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime To_date { get; set; }
        public int Years { get; set; }
        public int Months { get; set; }
        public Working_System Working_System { get; set; } = Working_System.Day;
        public Position_Status Position_Status { get; set; } = Position_Status.Active;

    }
}