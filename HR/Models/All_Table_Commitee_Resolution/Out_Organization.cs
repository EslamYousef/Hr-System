using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;
namespace HR.Models.All_Table_Commitee_Resolution
{
    public class Out_Organization
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Job Code")]
        public string Job_Code { get; set; }
        [Display(Name = "Job Description")]
        public string Job_Description { get; set; }
        [Display(Name = "Cadre Code")]
        public string Cadre_Code { get; set; }
        public string Cadre { get; set; }
        [Display(Name = "Org. Unit Code")]
        public string Org_Unit_Code { get; set; }
        [Display(Name = "Org. Unit Description")]
        public string Org_Unit_Description { get; set; }
        [Display(Name = "Required Persons Num")]
        public int Required_Persons_Num { get; set; }
        [Display(Name = "Committe Resolution RecuirtmentId")]
        public int Committe_Resolution_RecuirtmentId { get; set; }
        public virtual Committe_Resolution_Recuirtment Committe_Resolution_Recuirtment { get; set; }

    }
}