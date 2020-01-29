using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HR.Models
{
    public class Employee_Profile_Groups
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Group Code")]
        public string Group_Code { get; set; }
        [Required]
        [Display(Name = "Group Description")]
        public string Group_Description { get; set; }
        [Display(Name = "Group Alternative Description")]
        public string Group_Alternative_Description { get; set; }
    }
}