using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;
namespace HR.Models
{
    public class Employee_Profile
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Employee No.")]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Full Name")]
        public string Full_Name { get; set; }
        public string Arabic { get; set; }
        public string Full { get; set; }
        public string Surname { get; set; }
        public string Sur_Name { get; set; }
        public Gender Gender { get; set; } = Gender.male;
        [Display(Name = "Marital Status")]
        public Marital_Status Marital_Status { get; set; } = Marital_Status.Single;

        [Display(Name = "Religion")]
        public string ReligionId { get; set; }
        public virtual Religion Religion { get; set; }
         
        [Display(Name = "Nationality")]
        public string NationalityId { get; set; }
        public virtual Nationality Nationality { get; set; }
        public bool Citizen { get; set; }
        public Blood_group Blood_group { get; set; } = Blood_group.None;
        [Display(Name = "ID type")]
        public ID_type ID_type { get; set; } = ID_type.National_ID;
        public Health_Status Health_Status { get; set; } = Health_Status.Ability;
        [Display(Name = "Birth date")]
       
        public DateTime Birth_date { get; set; }
        [Display(Name = "ID number")]
        public string ID_number { get; set; }
        [Display(Name = "ID number in attendance machine")]
        public string ID_number_in_attendance_machine { get; set; }
        [Display(Name = "Issue date")]
        public DateTime Issue_date { get; set; }
        [Display(Name = "Expire date")]
        public DateTime Expire_date { get; set; }
 
         
        [Display(Name = "Country Code")]
        public string Countryid { get; set; }
         
        [Display(Name = "Country Code")]
        public string Countryaddressid { get; set; }
        public virtual Country Country { get; set; }
         
        [Display(Name = "Area Code")]
        public string Areaid { get; set; }
         
        [Display(Name = "Area Code")]
        public string Areaaddressid { get; set; }
        public virtual Area Area { get; set; }
         
        [Display(Name = "State / Governorate")]
        public string the_statesid { get; set; }
         
        [Display(Name = "State / Governorate")]
        public string the_statesaddressid { get; set; }
        public virtual the_states the_states { get; set; }
         
        [Display(Name = "County Code")]
        public string Territoriesid { get; set; }
         
        [Display(Name = "County Code")]
        public string Territoriesaddressid { get; set; }
        public virtual Territories Territories { get; set; }
         
        [Display(Name = "Cite Code")]
        public string citiesid { get; set; }
         
        [Display(Name = "Cite Code")]
        public string citiesaddressid { get; set; }
        public virtual cities cities { get; set; }
        public virtual Employee_Address_Profile Employee_Address_Profile { get; set; }
        public virtual Ability Ability { get; set; }
        public virtual Personnel_Information Personnel_Information { get; set; }
        public virtual Service_Information Service_Information { get; set; }
        public virtual Employee_Qualification_Profile Employee_Qualification_Profile { get; set; }
        public virtual Position_Information Employee_Positions_Profile { get; set; }
        public virtual Position_Transaction_Information Position_Transaction_Information { get; set; }
        public virtual Employee_family_profile Employee_family_profile { get; set; }
        public virtual Employee_experience_profile Employee_experience_profile { get; set; }


        //public string tab { get; set; }

        //[DataType(DataType.DateTime)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{yyyy/MM/dd HH:mm:ss tt}")]
    }
}