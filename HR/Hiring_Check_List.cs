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
    
    public partial class Hiring_Check_List
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Hiring_Check_List()
        {
            this.Check_List_Items = new HashSet<Check_List_Items>();
        }
    
        public int ID { get; set; }
        public bool Select { get; set; }
        public string Item_Code { get; set; }
        public string Item_Desc { get; set; }
        public int ApplicantId { get; set; }
        public Nullable<int> Application_ID { get; set; }
    
        public virtual Application Application { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Check_List_Items> Check_List_Items { get; set; }
    }
}