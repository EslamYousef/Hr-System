using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class Sponsor:BaseModel
    {
        public string corporation { get; set; }
        public string sponsorid { get; set; }
        public string IBAN { get; set; }
        public string Others1 { get; set; }
        public string Others2 { get; set; }
        public string Others3 { get; set; }
    }
}