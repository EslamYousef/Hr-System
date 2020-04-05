using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;


namespace HR.Models
{
    public class Append_Committe_Member
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Employee profile No")]
        public string Employee_profile { get; set; }
        [Display(Name = "Employee Name")]
        public string Employee_Name { get; set; }
        [Display(Name = "Is Committe Head")]
        public string Is_Committe_Head { get; set; }
        public virtual Committe_Resolution_Recuirtment Committe_Resolution_Recuirtment { get; set; }
    }
}