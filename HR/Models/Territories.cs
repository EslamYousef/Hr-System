using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class Territories////الأقاليم
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name = "  Code")]
    //    [Index(IsUnique = true)]
        public string Code { get; set; }
        [Required]
        [Display(Name = "  Name")]
        public string Name { get; set; }
        [Display(Name = "Description  ")]
        public string Description { get; set; }
        [Display(Name ="State Name")]
        [Required]
        public string the_statesid { get; set; }
        public virtual the_states the_states { get; set; }
        [Display(Name = "Area Name")]
        public int areaid { get; set; }
        [Display(Name = "Country Name")]
        public int countryid { get; set;}
        //public virtual List<cities> cities { get; set; }
    }
}