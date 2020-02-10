using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;
namespace HR.Models
{
    public class Personnel_Information
    { 
        [Key]
        public int ID { get; set; }
        [Display(Name = "Main Status")]
        public Main_Status Main_Status { get; set; } = Main_Status.On_Job;
        [Display(Name = "Sub Status")]
        public Sub_Status Sub_Status { get; set; } = Sub_Status.Hire;
        [Display(Name = "Hire Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Hire_Date { get; set; }
        [Display(Name = "Sector Join Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Sector_Join_Date { get; set; }
        [Display(Name = "Join Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Join_Date { get; set; }
        [Display(Name = "Pratice profession out of company")]
        public bool Pratice_profession_Out_Of_Company { get; set; }
        [Display(Name = "Social Insurance")]
        public string Social_Insurance { get; set; }
        [Display(Name = "Social Insurance Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Social_Insurance_Date { get; set; }
        [Display(Name = "Service period ins (Y)")]
        public int Service_period_ins_Y { get; set; }
        [Display(Name = "Service period ins (M)")]
        public int Service_period_ins_M { get; set; }
        [Display(Name = "Boarding membership")]
        public Boarding_membership Boarding_membership { get; set; } = Boarding_membership.None;
        [Display(Name = "Boarding Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Boarding_Date { get; set; }

    }
}