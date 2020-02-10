using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;
namespace HR.Models
{
    public class Employee_family_profile
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Employee No.")]
        public string Employee_ProfileId { get; set; }
        [Required]
        [Display(Name = "Family profile No.")]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Is Reliable")]
        public bool Is_Reliable { get; set; }
        public Gender Gender { get; set; } = Gender.other;
        [Display(Name = "Family member type")]
        public Family_member_type Family_member_type { get; set; } = Family_member_type.Spouse;
        [Display(Name = "Degree of kinship")]
        public Degree_of_kinship Degree_of_kinship { get; set; } = Degree_of_kinship.First_grade;
        [Display(Name = "Marital Status")]
        public Marital_Status Marital_Status { get; set; } = Marital_Status.Married;
        public Status Status { get; set; } = Status.Live;
        [Display(Name = "Id type")]
        public Id_type Id_type { get; set; } = Id_type.National_id;
        [Display(Name = "Health Status2")]
        public Health_Status2 Health_Status2 { get; set; } = Health_Status2.Ability;
        [Display(Name = "Working status")]
        public Working_status Working_status { get; set; } = Working_status.Working;
        [Display(Name = "Emergency level")]
        public Emergency_level Emergency_level { get; set; } = Emergency_level.Primary;
        [Display(Name = "Nationality")]
        public string NationalityId { get; set; }
        public virtual Nationality Nationality { get; set; }
        [Display(Name = "Educate level")]
        public string Educate_TitleId { get; set; }
        public virtual Educate_Title Educate_Title { get; set; }
        [Display(Name = "Qualification name")]
        public string Name_of_educational_qualificationId { get; set; }
        public virtual Name_of_educational_qualification Name_of_educational_qualification { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Display(Name = "Start relation date")]
        public DateTime Start_relation_date { get; set; }
        [Display(Name = "End relation date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime End_relation_date { get; set; }
        [Display(Name = "Birth date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Birth_date { get; set; }
        [Display(Name = "Death date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Death_date { get; set; }
        [Display(Name = "Id number")]
        public string Id_number { get; set; }
        [Display(Name = "Father name")]
        public string Father_name { get; set; }
        [Display(Name = "Mother name ")]
        public string Mother_name { get; set; }
        public bool Subscribed { get; set; }
        [Display(Name = "Working in oil sector")]
        public bool Working_in_oil_sector { get; set; }

        [Display(Name = "Position title")]
        public string Position_title { get; set; }
        [Display(Name = "Company name")]
        public string Company_name { get; set; }
        [Display(Name = "Company address")]
        public string Company_address { get; set; }
        [Display(Name = "Working in company")]
        public bool Working_in_company { get; set; }
        [Display(Name = "Employee No.")]
        public string Employee_Profile_WorkId { get; set; }
        public bool Is_emergency_contact_person { get; set; }
        [Display(Name = "Home phone number")]
        public string Home_phone_number { get; set; }
        [Display(Name = "Mobil phone number")]
        public string Mobil_phone_number { get; set; }
        public string Address { get; set; }
        public virtual Employee_Profile Employee_Profile { get; set; }



    }
}