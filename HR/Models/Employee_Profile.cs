﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;
using HR.Models.Time_management;
using HR.Models.CardPayroll;

namespace HR.Models
{
    public class Employee_Profile
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Employee No.")]
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
        public string ReligionId { get; set; }
        public virtual Religion Religion { get; set; }
        [Display(Name = "Nationality")]
        public string NationalityId { get; set; }
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
        [Display(Name = "ID number in attendance machine")]
        public string ID_number_in_attendance_machine { get; set; }
        [Display(Name = "Issue date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Issue_date { get; set; }
        [Display(Name = "Expire date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
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
        public virtual List<Employee_Address_Profile> Employee_Address_Profile { get; set; }
        public virtual Ability Ability { get; set; }
        public virtual Personnel_Information Personnel_Information { get; set; }
        public virtual Service_Information Service_Information { get; set; }
        public virtual List<Employee_Qualification_Profile> Employee_Qualification_Profile { get; set; }
        public virtual List<Position_Information>  Employee_Positions_Profile { get; set; }
        public virtual Position_Transaction_Information Position_Transaction_Information { get; set; }
        public virtual List<Employee_family_profile> Employee_family_profile { get; set; }
        public bool Active { get; set; }
        public string EmpProfileIMG { get; set; }
        public Nullable<int> PayrollPeriodcode { get; set; }
        public Nullable<int> Weekendcode { get; set; }
        public Nullable<int> Groupcode { get; set; }
        public double Startbasicsalary { get; set; }
        public double BasicSalary_A { get; set; }
        public double Otherallowances { get; set; }
        public double Totalincludedallowances { get; set; }
        public double Totalexecludedallowances { get; set; }
        public double Totalbasicsalary { get; set; }
        public double Totalremuneration { get; set; }
        public double Insurancebasicsalary { get; set; }
        public double Insurancevariablesalary { get; set; }

        public virtual List<Employee_contact_profile> Employee_contact_profile { get; set; }
        public virtual List<Employee_military_service_profile> Employee_military_service_profile { get; set; }

        public virtual List<Employee_contract_profile> Employee_contract_profile { get; set; }
        public virtual List<Employee_military_service_calling> Employee_military_service_calling { get; set; }
        public virtual List<Employee_beneficiary_profile> Employee_beneficiary_profile { get; set; }
        public virtual List<Employee_subscription_syndicate_profile> Employee_subscription_syndicate_profile { get; set; }
        public virtual List<Employee_attachment_profile> Employee_attachment_profile { get; set; }
        public virtual List<per_emp> per_emp { get; set; }
        public virtual List<Employee_experience_profile> Employee_experience_profile { get; set; }
        public virtual List<EvaluationTransaction> EvaluationTransaction { get; set; }   
        public virtual List<Employee_sponsor_profile> Employee_sponsor_profile { get; set; }
        public virtual List<Employee_vehicle_profile> Employee_vehicle_profile { get; set; }

        public virtual List<Employee_Shift_schedule> Employee_Shift_schedule { get; set; }
        public virtual List<Exit_permission_request> Exit_permission_request { get; set; }
        public virtual List<workpermissionrequest> workpermissionrequest { get; set; }
        public virtual List<business_trip_request> business_trip_request { get; set; }
        public virtual List<Employee_Financial_Contract_Header> Employee_Financial_Contract { get; set; }

        //public string tab { get; set; }

        //[DataType(DataType.DateTime)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{yyyy/MM/dd HH:mm:ss tt}")]
    }
}