using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;

namespace HR.Models.Application
{
    public class Basic_Salary_Calculation_Result
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Start Basic Salary")]
        public int Start_Basic_Salary { get; set; }
        [Display(Name = "Other Allowance")]
        public int Other_Allowance { get; set; }
        [Display(Name = "Basic Salary A")]
        public int Basic_Salary_A { get; set; }
        [Display(Name = "Total Included Allowances")]
        public int Total_Included_Allowances { get; set; }
        [Display(Name = "Total Execluded Allowances")]
        public int Total_Execluded_Allowances { get; set; }
        [Display(Name = "Total Basic Salary")]
        public int Total_Basic_Salary { get; set; }
        [Display(Name = "Total Remuneration")]
        public int Total_Remuneration { get; set; }
        [Display(Name = "Insurance Basic Salary")]
        public int Insurance_Basic_Salary { get; set; }
        [Display(Name = "Insurance Variable Salary")]
        public int Insurance_Variable_Salary { get; set; }
        [Display(Name = "Applicant Id")]
        public int ApplicantId { get; set; }
        public virtual Application Application { get; set; }
    }
}