using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;

namespace HR.Models
{
    public class Applicant_Military_Service_Profile
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Applicant No.")]
        public string Employee_ProfileId { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Military service profile No.")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string Code { get; set; }
        [Display(Name = "Service at hire")]
        public bool Service_at_hire { get; set; }
        [Display(Name = "Trio number")]
        public string Trio_number { get; set; }
        public string Branch { get; set; }
        [Display(Name = "Military service status")]
        public Military_service_status Military_service_status { get; set; } = Military_service_status.Led_service;
        public Level Level { get; set; } = Level.Poor;
        [Display(Name = "Military service rank code")]
        [ForeignKey("Military_Service_Rank")]
        public int? Military_Service_RankId { get; set; }
        public virtual Military_Service_Rank Military_Service_Rank { get; set; }
        [Display(Name = "Certificate date ")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Certificate_date { get; set; }
        [Display(Name = "From date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime From_date { get; set; }
        [Display(Name = "To date ")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime To_date { get; set; }
        [Display(Name = "Batch reference No.")]
        public string Batch_reference_No { get; set; }
        [Display(Name = "Id number")]
        public string Id_number { get; set; }
        [Display(Name = "Exemption reason")]
        [ForeignKey("Rejection_Reasons")]
        public int? Rejection_ReasonsId { get; set; }
        public virtual Rejection_Reasons Rejection_Reasons { get; set; }
        [Display(Name = "Service period - YY ")]
        public double Service_period_YY { get; set; }
        [Display(Name = "MM ")]
        public double Service_period_MM { get; set; }
        [Display(Name = "The number of days ")]
        public double Service_period_The_number_of_days { get; set; }
        [Display(Name = "Added Service period - YY ")]
        public double Added_Service_period_YY { get; set; }
        [Display(Name = "MM ")]
        public double Added_Service_period_MM { get; set; }
        [Display(Name = "The number of days ")]
        public double Added_Service_period_The_number_of_days { get; set; }
        [Display(Name = "Total Service period - YY ")]
        public double Total_Service_period_YY { get; set; }
        [Display(Name = "MM ")]
        public double Total_Service_period_MM { get; set; }
        [Display(Name = "The number of days ")]
        public double Total_Service_period_The_number_of_days { get; set; }
        public string Comments { get; set; }
        public virtual Applicant_Profile Applicant_Profile { get; set; }


    }
}