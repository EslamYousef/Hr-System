using HR.Models.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class EvaluationType:BaseModel
    {
        public Periods Periods { get; set; }
    }
}