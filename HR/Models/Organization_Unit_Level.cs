using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class Organization_Unit_Level:BaseModel
    {
        public double Level_Number { get; set; }
      public virtual List<Organization_Unit_Type> Organization_Unit_Type { get; set; }


    }
}