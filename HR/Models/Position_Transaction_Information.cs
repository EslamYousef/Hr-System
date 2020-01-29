using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;
namespace HR.Models
{
    public class Position_Transaction_Information
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Position transaction no.")]
        public string Position_transaction_no { get; set; }
        [Display(Name = "Position transaction")]
        public DateTime Position_transaction { get; set; }
        public Transaction_Type Transaction_Type { get; set; } = Transaction_Type.Re_Hire;
        public Fixed_basic_salary_by Fixed_basic_salary_by { get; set; } = Fixed_basic_salary_by.In_House;
        [Display(Name = "Resolution number")]
        public string Resolution_number { get; set; }
        [Display(Name = "Resolution date")]
        public DateTime Resolution_date { get; set; }
        [Display(Name = "Activity number")]
        public string Activity_number { get; set; }
        [Display(Name = "Memo number")]
        public string Memo_number { get; set; }
        [Display(Name = "Memo date")]
        public DateTime Memo_date { get; set; }
        [Display(Name = "Recommended by")]
        public string Recommended_by { get; set; }
        [Display(Name = "Approved by")]
        public string Approved_by { get; set; }
        [Display(Name = "Approved date")]
        public DateTime Approved_date { get; set; }

    }
}