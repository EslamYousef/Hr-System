using HR.Models.Infra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class Slots
    {
        public int ID { get; set; }
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string slot_code { get; set; }
        public string slot_description { get; set; }
        public virtual job_level_setup job_level_setup { get; set; }
       public int joblevelsetupID { get; set; }
        public check_status check_status { get; set; }
        public slot_type slot_type { get; set; }
        public string EmployeeID {get;set;}
        public string EmployeeName { get; set; }

        public virtual job_title_cards job_title_cards { get; set; }
        public int? job_title_cardsID { get; set; }
        public virtual Organization_Chart Organization_Chart__ { get; set; }
        public int Organization_Chart__ID { get; set; }
        public virtual Employee_Profile Employee_Profile { get; set; }
        //public virtual job_title_cards job_title_cards { get; set; }
        //public string  job_title_cardsID { get; set; }

    }
}