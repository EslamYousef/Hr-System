using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;

namespace HR.Models
{
    public class Applicant_Profile
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Applicant No.")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string Code { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public string Name { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Full Name")]
        public string Full_Name { get; set; }
        public string Arabic { get; set; }
        [Display(Name = "Full Name")]
        public string Full { get; set; }
        [Display(Name = "SurName")]
        public string Surname { get; set; }
        [Display(Name = "SurName")]
        public string Sur_Name { get; set; }
        public Gender Gender { get; set; } = Gender.male;
        [Display(Name = "Marital Status")]
        public Marital_Status Marital_Status { get; set; } = Marital_Status.Single;

        [Display(Name = "Religion")]
        [ForeignKey("Religion")]
        public int ReligionId { get; set; }
        public virtual Religion Religion { get; set; }

        [Display(Name = "Nationality")]
        [ForeignKey("Nationality")]
        public int NationalityId { get; set; }
        public virtual Nationality Nationality { get; set; }
        public bool Citizen { get; set; }
        [Display(Name = "Blood group")]
        public Blood_group Blood_group { get; set; } = Blood_group.None;
        [Display(Name = "ID type")]
        public ID_type ID_type { get; set; } = ID_type.National_ID;
        [Display(Name = "Health Status")]
        public Health_Status Health_Status { get; set; } = Health_Status.Ability;
        [Display(Name = "Birth date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Birth_date { get; set; }
        [Display(Name = "ID number")]
        public string ID_number { get; set; }
     
        [Display(Name = "Issue date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Issue_date { get; set; }
        [Display(Name = "Expire date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Expire_date { get; set; }
        [Display(Name = "Country Code")]
        //[ForeignKey("Country")]
        public int? Countryid { get; set; }

        [Display(Name = "Country Code")]
        //[ForeignKey("Country")]
        public int? Countryaddressid { get; set; }
        public virtual Country Country { get; set; }

        [Display(Name = "Area Code")]
        //[ForeignKey("Area")]
        public int? Areaid { get; set; }
        public virtual Area Area { get; set; }

        [Display(Name = "Area Code")]
        //[ForeignKey("Area")]
        public int? Areaaddressid { get; set; }

        [Display(Name = "State / Governorate")]
        //[ForeignKey("the_states")]
        public int? the_statesid { get; set; }

        [Display(Name = "State / Governorate")]
        //[ForeignKey("the_states")]
        public int? the_statesaddressid { get; set; }
        public virtual the_states the_states { get; set; }

        [Display(Name = "County Code")]
        //[ForeignKey("Territories")]
        public int? Territoriesid { get; set; }

        [Display(Name = "County Code")]
        //[ForeignKey("Territories")]
        public int? Territoriesaddressid { get; set; }
        public virtual Territories Territories { get; set; }

        [Display(Name = "Cite Code")]
        //[ForeignKey("cities")]
        public int? citiesid { get; set; }

        [Display(Name = "Cite Code")]
        //[ForeignKey("cities")]
        public int? citiesaddressid { get; set; }
        public virtual cities cities { get; set; }
        public string EmpProfileIMG { get; set; }
        [Display(Name = " Inability reason ")]
        public string Inability_reason { get; set; }
        [Display(Name = " Inability  description ")]
        public string Inability_description { get; set; }
        [Display(Name = "registration number")]
        public string registration_number { get; set; }
        [Display(Name = "registration date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime registration_date { get; set; }
        [Display(Name = "Pension #")]
        public string Pension { get; set; }
        [Display(Name = "Pension source")]
        public string Pension_source { get; set; }
        [Display(Name = "Have pension")]
        public bool Have_pension { get; set; }
        [Display(Name = "Job Code")]
        [ForeignKey("job_title_cards")]
        public int? job_title_cardsId { get; set; }
        public virtual job_title_cards job_title_cards { get; set; }
        public virtual List<Applicant_Address_Profile> Applicant_Address_Profile { get; set; }
        public virtual List<Applicant_Attachment_Profile> Applicant_Attachment_Profile { get; set; }
        public virtual List<Applicant_Qualification_Profile> Applicant_Qualification_Profile { get; set; }
        public virtual List<Applicant_Family_Profile> Applicant_Family_Profile { get; set; }
        public virtual List<Applicant_Previous_Experiences_Profile> Applicant_Previous_Experiences_Profile { get; set; }
        public virtual List<Applicant_Contact_Profile> Applicant_Contact_Profile { get; set; }
        public virtual List<Applicant_Military_Service_Profile> Applicant_Military_Service_Profile { get; set; }
        public virtual List<Applicant_Subscription_Syndicate_Profile> Applicant_Subscription_Syndicate_Profile { get; set; }
        //public virtual Application.Application Application { get; set; }


    }
}