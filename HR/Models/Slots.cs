using HR.Models.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class Slots
    {
        public int ID { get; set; }
        public string slot_code { get; set; }
        public string slot_description { get; set; }
        public virtual job_level_setup job_level_setup { get; set; }
       public string joblevelsetupID { get; set; }
        public check_status check_status { get; set; }
        public slot_type slot_type { get; set; }
        public string EmployeeID {get;set;}
        public string EmployeeName { get; set; }

        public virtual job_title_cards job_title_cards { get; set; }
        public virtual Employee_Profile Employee_Profile { get; set; }
        //public virtual job_title_cards job_title_cards { get; set; }
        //public string  job_title_cardsID { get; set; }

    }
}