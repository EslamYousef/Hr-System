using HR.Models.Infra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class Organization_Chart
    {
        public int ID { get; set; }
        [Display(Name = "unit Code")]
        public string Code { get; set; }
        [Required]
        [Display(Name = "unit Description")]
        public string unit_Description { get; set; }
        [Display(Name = "Alterunit Description")]
        public string alter_unit_Description { get; set; }
        [Display(Name = "user unit Code")]
        public string User_unit_code { get; set; }
        [Required]
        public Organization_Unit_Type unit_type_code { get; set; }
        [Required]
        [Display(Name = "Unit Type Code ")]
        public int unit_type_codeID { get; set; }
        [Display(Name = "unit type code")]
        public check_status unit_status { get; set; }
        [Required]
        [Display(Name = "number of direct positions ")]
        public int number_of_direct_positions { get; set; }
        [Display(Name = "unit mail")]
        public string unit_mail { get; set; }
        [Display(Name = "parent unit code")]
        [Required]
        public string parent { get; set; }
        public string master_node { get; set; }
        [Display(Name = "Refrence number")]
        public int refrence_number { get; set; }
        public virtual work_location work_location { get; set; }
        [Required]
        public string worklocationid { get; set; }
        public virtual ICollection<Organization_Chart> Childs { get; set; }
        public List<Slots> Slots { get; set; }


    }
}