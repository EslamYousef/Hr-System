using HR.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class job_level_setup
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string Code { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public string Name { get; set; }
        public string Description { get; set; }
        ///////////////////////////////////////////////////////
        public double max_monthly_allowance { get; set; } = 0.0;
        public double min_basic_salary { get; set; } = 0.0;
        public double mid_basic_salary { get; set; } = 0.0;
        public double max_basic_salary { get; set; } = 0.0;
        public double min_working_years { get; set; } = 0.0;
        public double max_incentive_amount { get; set; } = 0.0;
        public double max_incentive_percentage { get; set; } = 0.0;
        public double dedicated_allowence { get; set; } = 0.0;
        public double max_annual_increase_percentage { get; set; } = 0.0;
        public double representation_allowance_value { get; set; } = 0.0;
        [Display(Name = "Sequence number")]
        public double Sequence_number { get; set; }
        [Display(Name = "Calculate added military years")]
        public bool Calculate_added_military_years { get; set; } = false;
        [Display(Name = "Calculate extra qualification years")]
        public bool Calculate_extra_qualification_years { get; set; } = false;
        [Display(Name = "Calculate added experience years")]
        public bool Calculate_added_experience_years { get; set; } = false;

        [Display(Name = "Notes")]
        public string Notes { get; set; }
        
        ////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////
      
        public virtual Job_level_class Job_level_class { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public string  Job_level_class__ID { get; set; }
      
        public virtual Job_level_grade Job_level_grade { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public string Job_level_gradeI__D { get; set; }
     
        //public virtual Job_level_grade report_job_level { get; set; }
      
        public string report_job_levelID { get; set; }
        public virtual List<job_level_education> job_level_education { get; set; }
        public List<string> job_level_educationID { get; set; }

        ///////////////////////
        /// //////////////////
        
        public virtual List<Organization_Unit_Type> Organization_Unit_Type { get; set; }
        public List<string> Organization_Unit_TypeID { get; set; } 

    }
}