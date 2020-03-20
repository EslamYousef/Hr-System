using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class Experience_group:BaseModel
    {

        public virtual List<exper_jobdetails> exper_jobdetails { get; set; }
    }
}