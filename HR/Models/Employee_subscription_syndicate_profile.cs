using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;
namespace HR.Models
{
    public class Employee_subscription_syndicate_profile
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Employee No.")]
        public int Employee_ProfileId { get; set; }
        public virtual Employee_Profile Employee_Profile { get; set; }
        [Required]
        [Display(Name = "Subscription profile No.")]
        public string Code { get; set; }
        [Required]
        [Display(Name = "Subscription No.")]
        public int Subscription_SyndicateId { get; set; }
        public virtual Subscription_Syndicate Subscription_Syndicate { get; set; }
        [Display(Name = "Subscription type")]
        public string Subscription_type { get; set; }
   //     public Infra.Type yyy { get; set; } = .
        [Display(Name = "Subscription date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Subscription_date { get; set; }
        [Display(Name = "Employee pay % ")]
        //[DisplayFormat(DataFormatString = "{0:#,##0.00#}", ApplyFormatInEditMode = true)]
        public double Employee_pay { get; set; }
        [Display(Name = "Company pay % ")]
        public double Company_pay { get; set; }
        [Display(Name = "Start year of deduction")]
        public int Start_year_of_deduction { get; set; }
        [Display(Name = "End year of deduction")]
        public int End_year_of_deduction { get; set; }
        [Display(Name = "Start month of deduction")]
        public int Start_month_of_deduction { get; set; }
        [Display(Name = "End month of deduction")]
        public int End_month_of_deduction { get; set; }
        //[DisplayFormat(DataFormatString = "{0:#,##0.000#}", ApplyFormatInEditMode = true)]
        [Display(Name = "Beneficiary period")]
        public double Beneficiary_period { get; set; }
        [Display(Name = "Referance number")]
        public string Referance_number { get; set; }
        [Display(Name = "Last date paid")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Last_date_paid { get; set; }
        public double Balance { get; set; }
        [Display(Name = "Pay to entity")]
        public string Pay_to_entity { get; set; }
        [Display(Name = "Pay to entity type")]
        public string Pay_to_entity_type { get; set; }
        public string Membership { get; set; }
        [Display(Name = "Subscription fees")]
        public double? Subscription_fees { get; set; }
        [Display(Name = "Subscription in house")]
        public bool Subscription_in_house { get; set; }
        [Display(Name = "Is family subscribed")]
        public bool Is_family_subscribed { get; set; }
        [Display(Name = "Family subscription fees")]
        public double Family_subscription_fees { get; set; }
        [Display(Name = "Boarder election date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Boarder_election_date { get; set; }
        [Display(Name = "Head election date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Head_election_date { get; set; }
        [Display(Name = "Contact detail")]
        public string Contact_detail { get; set; }
        [Display(Name = "Subscription value type")]
        public Subscription_value_type Subscription_value_type { get; set; } = Subscription_value_type.Fixed;
        [Display(Name = "Membership type")]
        public Membership_type Membership_type { get; set; } = Membership_type.Member;


    }
}