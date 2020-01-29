using HR.Models.Infra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class Budget
    {
        public int ID { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public int Year_From { get; set; }
        [Required]
        public int Year_To { get; set; }
        public virtual Organization_Chart organization_unit { get; set; }
        [Display(Name = "Organization unit")]
        public string Organization_unitid { get; set; }
        [Display(Name = "budget type")]
        public budget_type budget_type { get; set; }
        [Display(Name = "Currency rate")]
        public double Currency_rate { get; set; } 

        public double amount_native { get; set; }
        public double ammount_forigne { get; set; }

        public string sign_native { get; set; }

        public string sign_forign { get; set; }

        [Display(Name = "Distributed Remaining")]
        public double Remaining_native { get; set; }

        [Display(Name = "Distributed Remaining")]
        public double Remaining_forgin { get; set; }

        public virtual List<Budget_details> Budget_details { get; set; }
        public virtual List<int> Budget_detailsID { get; set; }

        public  virtual status status { get; set; }

        public virtual justification justification { get; set; }
    }
}