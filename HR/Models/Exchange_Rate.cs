using HR.Models.Infra;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Models
{
    public class Exchange_Rate
    {
        public int ID { get; set; }
        public int Year { get; set; }

        public virtual IList<months> months { get; set; }

        public virtual int CurrencyID { get; set; }
        public virtual Currency Currency { get; set; }
        

    }
}