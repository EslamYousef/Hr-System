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
    
    public partial class Employee_military_service_calling
    {
        public int ID { get; set; }
        public string Employee_ProfileId { get; set; }
        public string Code { get; set; }
        public System.DateTime Start_date { get; set; }
        public System.DateTime End_date { get; set; }
        public double Years { get; set; }
        public double Months { get; set; }
        public double Days { get; set; }
        public string Comments { get; set; }
        public Nullable<int> Employee_Profile_ID { get; set; }
    
        public virtual Employee_Profile Employee_Profile { get; set; }
    }
}