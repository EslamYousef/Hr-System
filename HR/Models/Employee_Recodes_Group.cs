using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HR.Models
{
    public class Employee_Recodes_Group
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Record Group Code")]
        public string Record_Group_Code { get; set; }
        [Required]
        [Display(Name = "Record Group Description")]
        public string Record_Group_Description { get; set; }
        [Display(Name = "Record Group Alternative")]
        public string Record_Group_Alternative { get; set; }
    }
}