using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class cities
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Code  ")]
      //  [Index(IsUnique = true)]
        public string  Code { get; set; }
        [Required]
        [Display(Name = "  Name")]
        public string Name { get; set; }
        [Display(Name = "  Description")]
        public string Description { get; set; }
        [Display(Name = "Territory Name")]
        [Required]

        public string Territoriesid { get; set; }
        public virtual Territories Territories { get; set; }
        [Display(Name = "State Name")]
        public int stateid { get; set; }
        [Display(Name = "Area Name")]
        public int areaid { get; set; }
        [Display(Name = "Country Name")]
        public int countryid { get; set; }

    }
}