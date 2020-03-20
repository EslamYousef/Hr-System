using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class special
    {
        public int ID { get; set; }
        public double Year { get; set; } = 0.0;
        public double Month { get; set; } = 0.0;
        public double Percentage { get; set; } = 0.0;
        [Display(Name = "Allowance amount")]
        public double Allowance_amount { get; set; } = 0.0;
        [Display(Name = "pervious basic sallary")]
        public double pervious_basic { get; set; } = 0.0;
        [Display(Name = "new basic sallary")]
        public double new_basic_sallary { get; set; } = 0.0;
        [Display(Name = " type allowance")]


        public virtual job_level_setup job_level_setup { get; set; }
        public int job_level_setupID { get; set; }
    }
}