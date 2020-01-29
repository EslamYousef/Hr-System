using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class Allowance_years
    {

        public int ID { get; set; }
        [Required]
        public double Allowance_year { get; set; }
        [Required]
        public double Allowance_percentage{get;set;}
        [Required]
        public double min_Allowance_amount { get; set; }
        [Required]
        public double max_Allowance_amount { get; set; }
    }
}