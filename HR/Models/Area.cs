using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class Area///المناطق
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Code")]
      //  [Index(IsUnique = true)]
        public string Code { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name ="Country Name")]
        [Required]
        public string Countryid { get; set; }
        public virtual Country Country { get; set; }

        //public virtual the_states the_states { get; set; }
    }
}