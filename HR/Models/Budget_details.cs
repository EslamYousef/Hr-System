using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class Budget_details
    {
        public int ID { get; set; }
        public virtual Budget_class_items Budget_class_items { get; set; }
        public double amount_native { get; set; }
        public double amount_forign { get; set; }
        public string comment { get; set; }
        public string sign_native { get; set; }
        public string sign_forgin { get; set; }
    }
}