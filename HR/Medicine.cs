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
    
    public partial class Medicine
    {
        public int ID { get; set; }
        public bool Is_Foreign { get; set; }
        public double price { get; set; }
        public string Indication { get; set; }
        public string Usual_Dosage { get; set; }
        public string Contraindication { get; set; }
        public string Precaution_Warnings { get; set; }
        public string Note { get; set; }
        public string Medical_Medicine_ClassficationId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<int> Medical_Medicine_Classfication_ID { get; set; }
    
        public virtual Medical_Medicine_Classfication Medical_Medicine_Classfication { get; set; }
    }
}