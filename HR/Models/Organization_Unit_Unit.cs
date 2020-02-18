using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class Organization_Unit_Type : BaseModel
    {
        [Display(Name = "unit Level code")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string unitLevelcode { get; set; }
        public virtual Organization_Unit_Level Organization_Unit_Level { get;set;}
        public virtual Organization_Unit_Schema Organization_Unit_Schema { get; set; }
        [Display(Name = "unit schema code")]
        public string unitschemacode { get; set; }
    } 
}