using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;
namespace HR.Models
{
    public class Employee_sponsor_profile
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Employee No.")]
        public int Employee_ProfileId { get; set; }
        public virtual Employee_Profile Employee_Profile { get; set; }
        [Required]
        [Display(Name = "Sponsor profile No.")]
        public int SponsorId { get; set; }
        public virtual Sponsor Sponsor { get; set; }
        [Display(Name = "Residence Id")]
        public string Residence_Id { get; set; }
        [Display(Name = "Issue place")]
        public string Issue_place { get; set; }
        public string Owner { get; set; }
        public string Job { get; set; }
        public string Nationality { get; set; }
        public string Religin { get; set; }
        [Display(Name = "Issue date")]
        public DateTime Issue_date { get; set; }
        [Display(Name = "Birth date")]
        public DateTime Birth_date { get; set; }

         


    }
}