//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HR
{
    using System;
    using System.Collections.Generic;
    
    public partial class Employee_subscription_syndicate_profile
    {
        public int ID { get; set; }
        public int Employee_ProfileId { get; set; }
        public string Code { get; set; }
        public int Subscription_SyndicateId { get; set; }
        public string Subscription_type { get; set; }
        public System.DateTime Subscription_date { get; set; }
        public double Employee_pay { get; set; }
        public double Company_pay { get; set; }
        public int Start_year_of_deduction { get; set; }
        public int End_year_of_deduction { get; set; }
        public int Start_month_of_deduction { get; set; }
        public int End_month_of_deduction { get; set; }
        public double Beneficiary_period { get; set; }
        public string Referance_number { get; set; }
        public System.DateTime Last_date_paid { get; set; }
        public double Balance { get; set; }
        public string Pay_to_entity { get; set; }
        public string Pay_to_entity_type { get; set; }
        public string Membership { get; set; }
        public Nullable<double> Subscription_fees { get; set; }
        public bool Subscription_in_house { get; set; }
        public bool Is_family_subscribed { get; set; }
        public double Family_subscription_fees { get; set; }
        public System.DateTime Boarder_election_date { get; set; }
        public System.DateTime Head_election_date { get; set; }
        public string Contact_detail { get; set; }
        public int Subscription_value_type { get; set; }
        public int Membership_type { get; set; }
    
        public virtual Employee_Profile Employee_Profile { get; set; }
        public virtual Subscription_Syndicate Subscription_Syndicate { get; set; }
    }
}