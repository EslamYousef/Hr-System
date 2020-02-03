using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;
namespace HR.Models
{
    public class Employee_contract_profile
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Employee No.")]
        public string Employee_ProfileId { get; set; }
        [Required]
        [Display(Name = "Contract No.")]
        public string Code { get; set; }
        //[Display(Name = "Contract No.")]
        //public string Contract { get; set; }
        [Display(Name = "Contract Type")]
        public string ContractTypeId { get; set; }
        [Display(Name = "Approved by")]
        public string ApprovedbyId { get; set; }
        public virtual Contract_Type Contract_Type { get; set; }
        [Display(Name = "Employment type")]
        public Employment_type Employment_type { get; set; } = Employment_type.Fulltime;
        [Display(Name = "Contract start date ")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public DateTime Contract_start_date { get; set; }
        [Display(Name = "Contract end date ")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public DateTime Contract_end_date { get; set; }
        public int Years { get; set; }
        public int Months { get; set; }
        [Display(Name = "Approved date ")]
        public DateTime Approved_date { get; set; }
        public string Notes { get; set; }

        [Display(Name = "Medical date ")]
        public DateTime Medical_date { get; set; }
        [Display(Name = "Medical entity name")]
        public string Medical_entity_name { get; set; }

        [Display(Name = "Not fit reason")]
        public string Not_fit_reason { get; set; }
        [Display(Name = "Medical commite comments")]
        public string Medical_commite_comments { get; set; }
        [Display(Name = "Medical commite recomindation")]
        public Medical_commite_recomindation Medical_commite_recomindation { get; set; } = Medical_commite_recomindation.Fit;
        public Medical_commite_recomindation Result { get; set; } = Medical_commite_recomindation.Fit;

        [Display(Name = "Days_Balance")]
        public int Days_Balance { get; set; }
        [Display(Name = "Days Per Period")]
        public int Days_Per_Period { get; set; }
        [Display(Name = "Tickets No.")]
        public int Tickets_No { get; set; }
        [Display(Name = "Tickets Per Period")]
        public int Tickets_Per_Period { get; set; }
        [Display(Name = "Tickets Class Tpye")]
        public Tickets_Class_Tpye Tickets_Class_Tpyeemp { get; set; } = Tickets_Class_Tpye.Economy;
        [Display(Name = "From Airpot")]
        public string FromAirpotId { get; set; }
        [Display(Name = "To Airpot")]
        public string ToAirpotId { get; set; }
        public virtual Air_ports Air_ports { get; set; }
        [Display(Name = "Adult Tickets No.")]
        public int Adult_Tickets_No { get; set; }
        [Display(Name = "Child Tickets No.")]
        public int Child_Tickets_No { get; set; }
        [Display(Name = "Tickets Class Tpye")]
        public Tickets_Class_Tpye Tickets_Class_Tpyefam { get; set; } = Tickets_Class_Tpye.Economy;
        public virtual Employee_Profile Employee_Profile { get; set; }

    }
}