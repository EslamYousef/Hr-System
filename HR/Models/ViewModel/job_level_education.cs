using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HR.Models.ViewModel
{
    public class job_level_education
    {
       
        public int ID { get; set; }
        public virtual Educate_Title Educate_Title { get; set; }
        [Display(Name = "Number Of Years Required")]
        public double number_of_years_requires { get; set; }
    }
}