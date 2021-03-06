//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HR.Models.Training.transaction
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("TrainingOpportunity_Detail")]
    public  class TrainingOpportunity_Detail
    {
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public int Year { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public string TrainingType_Code { get; set; }
        public string CourseClassification_Code { get; set; }
        public string Cadre_Code { get; set; }
        public Nullable<int> Total_Num_Of_Employee { get; set; }
        public Nullable<int> Total_Num_Of_Opportunity { get; set; }
        public Nullable<double> Cost_Per_Opportunity { get; set; }
        public Nullable<double> Total_Cost { get; set; }
        public Nullable<double> Total_Cost_for { get; set; }
        public string Company_ID { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public string Modified_By { get; set; }
        public Nullable<System.DateTime> Modified_Date { get; set; }
        public int RowIndx { get; set; }
        public int headerID { get; set; }

        public string classifiaction_des { get; set; }
       
        public string card_des { get; set; }
        
    }
}
