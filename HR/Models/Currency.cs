using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class Currency:BaseModel
    {
        public bool Native { get; set; }
        public string symbol { get; set; }
        public int Nmmber_of_decimal_places { get; set; }
    }
}