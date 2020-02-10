using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class Job_level_class
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        // [Index(IsUnique = true)]
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
        /////////////////////////////////////////////////////////



    }
}