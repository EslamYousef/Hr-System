using HR.Models.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class External_compaines:BaseModel
    {
        public bool oil_sector { get; set; } 
        public string address { get; set; }
        public Company_type? Company_type { get; set; }

    }
}