using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;
namespace HR.Models
{
    public class Service_Information
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Pension #")]
        public string Pension { get; set; }
       [Display(Name = "Pension source")]
        public string Pension_source { get; set; }
        [Display(Name = "EOS date")]
        public DateTime EOS_date { get; set; }
        [Display(Name = "Have pension")]
        public bool Have_pension { get; set; }
        [Display(Name = "Retired Expected EOS")]
        public DateTime Retired_expected_EOS { get; set; }
        [Display(Name = "Last working date")]
        public DateTime Last_working_date { get; set; }
        [Display(Name = "Currency")]
        public string CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }
        [Display(Name = "Is merits The date of death")]
        public bool Is_merits_The_date_of_death { get; set; }

    }
}