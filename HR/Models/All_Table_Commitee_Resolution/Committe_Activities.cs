using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;

namespace HR.Models.All_Table_Commitee_Resolution
{
    public class Committe_Activities
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Activity Serial Number")]
        public string ActivitySerialNumber { get; set; }
        [Display(Name = "Planned Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Planned_Date { get; set; }
        [Display(Name = "Actual Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Actual_Date { get; set; }
        [Display(Name = "Activity Status")]
        public string Committe_Resolution_Status { get; set; }
        [Display(Name = "Committe Resolution RecuirtmentId")]
        public int Committe_Resolution_RecuirtmentId { get; set; }
        public virtual Committe_Resolution_Recuirtment Committe_Resolution_Recuirtment { get; set; }

    }
}