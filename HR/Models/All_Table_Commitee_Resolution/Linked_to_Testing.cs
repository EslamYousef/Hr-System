using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;

namespace HR.Models.All_Table_Commitee_Resolution
{
    public class Linked_to_Testing
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Test Code")]
        public string TestCode { get; set; }
        [Display(Name = "Test Description")]
        public string TestDescription { get; set; }
        [Display(Name = "Expected Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Expected_Start_Date { get; set; }
        [Display(Name = "Expected End Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Expected_End_Date { get; set; }
        [Display(Name = "Pass Mark")]
        public int Pass_Mark { get; set; }
        [Display(Name = "Full Mark")]
        public int Full_Mark { get; set; }
       
        [Display(Name = "Committe Resolution RecuirtmentId")]
        public int Committe_Resolution_RecuirtmentId { get; set; }
        public virtual Committe_Resolution_Recuirtment Committe_Resolution_Recuirtment { get; set; }

    }
}