using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HR.Models
{
    public class Medical_Service_Group
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Index(IsUnique = true)]
        [Display(Name = "Group Code")]
        public double Group_Code { get; set; }
        [Required]
        public string Name { get; set; }
        public string TName { get; set; }
    }
}