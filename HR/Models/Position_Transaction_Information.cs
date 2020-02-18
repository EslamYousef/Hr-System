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
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string Position_transaction_no { get; set; }
        [Display(Name = "Position transaction")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Position_transaction { get; set; }
        [Display(Name = "Transaction Type ")]
        public Transaction_Type Transaction_Type { get; set; } = Transaction_Type.Re_Hire;
        [Display(Name = "Fixed basic salary by")]
        public Fixed_basic_salary_by Fixed_basic_salary_by { get; set; } = Fixed_basic_salary_by.In_House;
        [Display(Name = "Resolution number")]
        public string Resolution_number { get; set; }
        [Display(Name = "Resolution date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Resolution_date { get; set; }
        [Display(Name = "Activity number")]
        public string Activity_number { get; set; }
        [Display(Name = "Memo number")]
        public string Memo_number { get; set; }
        [Display(Name = "Memo date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Memo_date { get; set; }
        [Display(Name = "Recommended by")]
        public string Recommended_by { get; set; }
        [Display(Name = "Approved by")]
        public string Approved_by { get; set; }
        [Display(Name = "Approved date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Approved_date { get; set; }

    }
}