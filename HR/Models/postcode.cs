using HR.Models.Infra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class postcode
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        [Display(Name = "Code")]
        public string  Code { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Street Name")]
        public string streetname { get; set; }
        [Required]
        [Display(Name = "Num Street From")]
        public int numstreetfrom { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Num Street To")]
        public int numstreetto { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Street Num Type")]
        public typenumstreet typenumstreet { get; set; }
        [Display(Name = "Territories Name")]
        public string Territoriesid { get; set; }
        [Display(Name = "City Name")]
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public  string citiesid { get; set; }
        public virtual cities cities { get; set; }
        [Display(Name = "State Name")]
        public int stateid { get; set; }
        [Display(Name = "Area Name")]
        public int areaid { get; set; }
        [Display(Name = "Country Name")]
        public int countryid { get; set; }
    }
}