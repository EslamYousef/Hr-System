using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class items_man_power
    {
        public int id { get; set; }
        public virtual job_level_setup cadre_code { get; set; }
        public int current_jobs { get; set; }
        public int new_jobs { get; set; }
        public int vacancy { get; set; }
        public int quarter1 { get; set; }
        public int quarter2 { get; set; }
        public int quarter3 { get; set; }
        public int quarter4 { get; set; }
        public string notes { get; set; }
        public virtual man_power man_power{get;set;}

    }
}