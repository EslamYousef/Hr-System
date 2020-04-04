using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class items_man_power
    {
        public int id { get; set; }
        public int current_jobs { get; set; }
        public int new_jobs { get; set; }
        public int vacancy { get; set; }
        public int quarter1 { get; set; }
        public int quarter2 { get; set; }
        public int quarter3 { get; set; }
        public int quarter4 { get; set; }
        public string notes { get; set; }

        public virtual job_level_setup job_level_setup { get; set; }
        public int job_level_setupID { get; set; }
        public virtual job_title_cards job_title_cards { get; set; }
        public int job_title_cardsID { get; set; }

        public virtual Job_title_sub_class Job_title_sub_class { get; set; }
        public int Job_title_sub_classID { get; set; }
        public virtual man_power man_power{get;set;}

    }
}