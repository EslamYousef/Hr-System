using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;

namespace HR.Models
{
    public class Ability
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = " Inability reason ")]
        public string Inability_reason { get; set; }
        [Display(Name = " Inability  description ")]
        public string Inability_description { get; set; }
        [Display(Name = "registration number")]
        public string registration_number { get; set; }
        [Display(Name = "registration date")]
        public DateTime registration_date { get; set; }
    }
}