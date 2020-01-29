using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class Air_ports
    {
        [Key]
        public int ID { get; set; }
        [Required]
        //[Index(IsUnique = true)]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Display(Name = "State Name")]
        [Required]
        public string the_statesid { get; set; }
        public virtual the_states the_states { get; set; }
        public int areaid { get; set; }
        public int countryid { get; set; }
        //public virtual List<cities> cities { get; set; }
    }
}