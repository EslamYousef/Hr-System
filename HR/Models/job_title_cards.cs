using HR.Models.Infra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class job_title_cards
    {
        public int ID { get; set; }
       // [Index(IsUnique = true)]
        [Required]
        public string Code { get; set; }
        [Required]
        public string name { get; set; }
        public string Description { get; set; }
        public string job_Summery { get; set; }
        public string Job_alt_summery { get; set; }
        //////////////////////////////////////////
      
        public int from_age { get; set; }
        public int to_age { get; set; }
        [Required]
        public int num_slots { get; set; }
        //////////////////////////////////////////
        public string parent_job { get; set; }
        public virtual job_level_setup job_level_setup { get; set; }
        public virtual List<Job_title_sub_class> Job_title_sub_class { get; set; }
        public  List<string> Job_title_sub_classid { get; set; }
        public virtual Nationality Nationality { get; set; }
        [Required]
        public string joblevelsetupID { get; set; }
        [Required]
        public string Default_job_title_sub_classID { get; set; }
        [Required]
        public string nationalityID { get; set; }
        //public virtual List<Organization_Unit_Type> Organization_Unit_Type { get; set; }
        //public List<string> Organization_Unit_TypeID { get; set; }
        /////////////////////////////////////////////enum/////////////////
        [Required]
        public gender gender { get; set; }
        [Required]
        public working_system working_system { get; set; }
        [Required]
        public check_status check_status { get; set; }
        [Required]
        public validity validity { get; set; }
        [Required]
        public parment parment { get; set; }
        //////////////////////////////////////////////////////////////

        public virtual Job_Details Job_Details { get; set; }
        public string Job_DetailsID { get; set; }

        //////////////////
        public virtual List<Slots> Slots { get; set; }
        public int number_hired { get; set; }
        public int number_vacant { get; set; }

        //////////////////////////
        public virtual Organization_Chart Organization_Chart { get; set; }
        [Required]
        [Display(Name = "Organization unit code")]
        public string Organization_unit_codeID { get; set; }

    }
}