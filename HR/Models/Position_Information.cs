using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;
namespace HR.Models
{
    public class Position_Information
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Employee No.")]
        public string Employee_ProfileId { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Position profile No.")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string Code { get; set; }
        [Display(Name = "Primary Position")]
        public bool Primary_Position { get; set; }
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
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Display(Name = "End of service date")]
        public DateTime End_of_service_date { get; set; }
        [Display(Name = "Last working date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Last_working_date { get; set; }
        public string Commnets { get; set; }
        [Display(Name = "Working system  ")]
        public working_system working_system { get; set; } = working_system.Day;
        [Display(Name = "Position status  ")]
        public Position_status Position_status { get; set; } = Position_status.Active;
        [Display(Name = "EOS reasons ")]
        public EOS_type EOS_reasons { get; set; } = EOS_type.On_poinsor;
        public virtual Position_Transaction_Information Position_Transaction_Information { get; set; }
        public virtual Employee_Profile Employee_Profile { get; set; }

    }

}