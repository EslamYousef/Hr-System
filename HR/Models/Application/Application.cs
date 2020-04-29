using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;

namespace HR.Models.Application
{
    public class Application
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Applicant No.")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string Code { get; set; }
        [Display(Name = "Applicant Id")]
        //[ForeignKey("Applicant_Profile")]
        public string Applicant_ProfileId { get; set; }
        public virtual Applicant_Profile Applicant_Profile { get; set; }
        [Display(Name = "Commitee Test No")]
        public string CommiteeTestNo { get; set; }
        public virtual List<Business_Test_Profile> Business_Test_Profile { get; set; }
        public virtual List<Medical_Test_Profile> Medical_Test_Profile { get; set; }
        public virtual List<Personnel_Committee_Profile> Personnel_Committee_Profile { get; set; }

    }
}