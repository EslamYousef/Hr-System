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
    
    public partial class Authority
    {
        public int ID { get; set; }
        public string Skill_Description { get; set; }
        public string Authority_TypeId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<int> Authority_Type_ID { get; set; }
    
        public virtual Authority_Type Authority_Type { get; set; }
    }
}