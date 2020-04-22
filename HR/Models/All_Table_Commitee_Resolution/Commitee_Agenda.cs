using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;

namespace HR.Models.All_Table_Commitee_Resolution
{
    public class Commitee_Agenda
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]   
        [Display(Name = "Subject Code")]
        public int SubjectCode { get; set; }
        [Display(Name = "Subject Description")]
        public string SubjectDescription { get; set; }
        [Display(Name = "Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? Start_Date { get; set; }
        [Display(Name = "End Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? End_Date { get; set; }
        public string Notes { get; set; }
        [Display(Name = "Committe Resolution RecuirtmentId")]
        public int Committe_Resolution_RecuirtmentId { get; set; }
        public virtual Committe_Resolution_Recuirtment Committe_Resolution_Recuirtment { get; set; }

    }
}