using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;
namespace HR.Models
{
    public class Employee_Address_Profile
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Employee No.")]
        public string Employee_ProfileId { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Address profile No.")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string Code { get; set; }
        public bool Resident { get; set; }
        [Display(Name = "Country name")]
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public string countryid { get; set; }
        public virtual Country Country { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Region name")]
        public string areaid { get; set; }
        public virtual Area Area { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "State name")]
        public string stateid { get; set; }
        public virtual the_states the_states { get; set; }
        [Display(Name = "Territories name")]
        public string Territoriesid { get; set; }
        public virtual Territories Territories { get; set; }
        [Display(Name = "City name")]
        public string citiesid { get; set; }
        public virtual cities cities { get; set; }
        [Display(Name = "Postal name")]
        public string postcodeId { get; set; }
        public virtual postcode postcode { get; set; }
        [Display(Name = "Street name")]
        public string Streetname { get; set; }
        [Display(Name = "Street number")]
        public int Streetnumber { get; set; }
        [Display(Name = "Po box")]
        public string Pobox { get; set; }
        [Display(Name = "Distance to work location (km)")]
        public int Distancetoworklocationkm { get; set; }
        [Display(Name = "Transportation method")]
        public Transportation_method Transportation_method { get; set; } = Transportation_method.None;
        [Display(Name = "Distance from Meeting point to work location (km)")]
        public int DistancefromMeetingpointtoworklocationkm { get; set; }
        public virtual Employee_Profile Employee_Profile { get; set; }


    }
}