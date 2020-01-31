using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.Models.Infra
{
    public class enums
    {

    }
    public enum Type_of_contect_method
    {
        Email=1,
        phone=2,
        mobile=3,
        fax=4

    }
    public enum reject_purpose
    {
        Job_experience=1,
        Military_service=2
    }
    public enum check_status
    {
        created=1,
        Report_as_ready=2,
        Approved=3,
        Rejected=4,
        Canceled=5,
        Recervied=6,
        Closed=7
            
    }
    public class months
    {
        public int id { get; set; }
        public string Name { get; set; }
        public float value { get; set; }
        
    }
    public enum ClassType
    {
        Economy=1,
        Premium_economy=2,
        Business=3,
        First=4
    }
    public enum Company_type
    {
        Public_Sector=1,
        join_Venture_Sector=2,
        Investment=3,
        None=4
    }
    public enum typenumstreet
    {
        Even=1,
        Odd=2,
        Both=3
    }
    public enum Test_type
    {
        Interview=1,
        Test=2,
        Both=3
    }
    public enum gender
    {
        male=1,
        female=2,
        Both=3
    }
    public enum working_system
    {
        Day = 1,
        shift_8_Hours = 2,
        shift_12_Hours =3
    }
    public enum validity
    {
        valid = 1,
        invalid = 2,
       
    }
    public enum parment
    {
        permanent = 1,
        part_time = 2,

    }
    public enum slot_type
    {
        Active = 1,
        in_active = 2,

    }
    public enum type_allowance
    {
        job_level_class=1,
        job_levle_grade=2,
        job_level_card=3

    }
    public enum ChModels
    {
        Basic = 1,
        Organization = 2,
        Recuirtment = 3,
        Personnel = 4,
        Medical = 5,
        Talent_Development = 6,
        Compensation =7,
        Payroll=8

    }
    public enum The_Purpose
    {
        Vacation = 1,
        Recruitment = 2,
        EOS = 3

    }
    public enum budget_type
    {
        Operating = 1,
        Captial = 2
    }
    public enum Deduction_Period
    {
        Monthly = 1,
        Quartely = 2,
        Yearly=3
    }
    public enum Type
    {
        Subscription = 1,
        Syndicate = 2,
        budget=3,
        employee_record=4
    }
    public enum Gender
    {
        male = 1,
        female = 2,
            other=3
    }
    public enum Marital_Status
    {
        Single = 1,
        Married = 2,
        Married_reliable = 3,
        Divorced = 4,
        Divorced_reliable = 5,
        Widower = 6,
        Widower_reliable = 7
    }
    public enum ID_type
    {
        National_ID = 1,
        Passport = 2,
        Dire = 3
    }
    public enum Blood_group
    {
        None = 1,
        O_negative = 2,
        A_positive = 3,
        A_negative = 4,
        B_positive = 5,
        B_negative = 6,
        AB_positive = 7,
        AB_negative = 8
    }
    public enum  Health_Status
    {
        Ability =1,
        In_Ability=2,
        In_Ability_military_operations=3
    }
    public enum Main_Status
    {
 
        On_Job = 1,
        Out_Of_Job = 2 
    }
    public enum Sub_Status
    {
        Hire = 1,
        Internal_Transafer = 2,
        Horizontal_Delegation = 3,
        Vertical_Delegation = 4,
        Secondment_In = 5,
        Assignment = 6,
        Re_Hire = 7,
        On_vacation = 8,
        Retired = 9,
        Resigned = 10,
        Terminated = 11,
        Dead = 12,
        Secondment_Out = 13,
        End_Of_Contract = 14
    }
    public enum Boarding_membership
    {
        None = 1,
        Board_member = 2,
        Head_member = 3
    }
    public enum Transportation_method
    {
        None =1,
        Company_bus =2,
        Given_car=3
    }
    //public enum Transaction_type
    //{
    //    internal_transfer_permanent=1,
    //    internal_transfer_temporary = 2,
    //    Horizontal_delegation=3,
    //    vertical_delegation = 4,
    //    assignment=5,
    //    external_assignment = 6,
    //    promotion=7,
    //    Re_hire=8,
    //    contract_extension=9


    //}
    public enum EOS_type
    {
        On_poinsor=1,
        Resignation=2,
        Termination=3,
        Death=4,
        External_loande_out=5,
        End_of_cotract=6,
        Early_EOS=7,
        Early_EOS_for_medical=8
    }
    public enum Notice_period_type
    {
        Working_date_only = 1,
        Days_work = 2

    }
    public enum Position_status
    {
        Active = 1,
        Hold = 2,
        History=3

    }
    public enum EOS_reasons
    {
        Retired = 1,
        Resigned = 2,
        Terminate = 3,
        Dead = 4,
        Transfered = 5,
        Secondment_out = 6,
        Early_EOS = 7,
        Early_EOS_for_medical = 8
    }
    public enum Transaction_Type
    {
        Hire = 1,
        Internal_Transafer = 2,
        Horizontal_Delegation = 3,
        Vertical_Delegation = 4,
        Secondment_In = 5,
        Secondment_Out = 6,
        Assignment = 7,
        External_Assignment = 8,
        Promotion = 9,
        Occupation = 10, 
        Re_Hire = 11,
        End_Of_Service = 12
    }
    public enum Fixed_basic_salary_by
    {
        In_House = 1,
        Out_House = 2,

    }
    public enum Family_member_type
    {
        Spouse = 1,
        Parents = 2,
        Siblings=3,
        Othe_family=4,
        Other=5,
        Partner=6,
        Child=7
    }
    public enum Degree_of_kinship
    {
          
        First_grade = 1,
        Second_grade = 2,
        Third_grade = 3,
  
    }
    public enum Status
    {    
        Live = 1,
        Dead = 2,
    }
    public enum Id_type
    {
          
        National_id = 1,
        Passport = 2,
        Other = 3
    }
    public enum Health_Status2
    {
        Ability = 1,
        In_Ability = 2
    }
    public enum Working_status
    {
        Working = 1,
        Retired = 2,
        Not_working=3
    }
    
    public enum Emergency_level
    {      
        Primary = 1,
        Secondary = 2
    }
    public enum transaction_type
    {
        [Display(Name = "Internal transfer permanant")]
        internal_transfer_permanant=1,
        [Display(Name = "Internal transfer teporary")]
        internal_transfer_teporary =2,
        [Display(Name = "Horizontal delegation")]
        horizontal_delegation =3,
        [Display(Name = "vertical delegtation")]
        vertical_delegtation =4,
        assignment=5,
        [Display(Name = "External assignment")]
        external_assignment =6,
        promotion=7,
        [Display(Name = "Re-Hire")]
        re_hire =8,
        [Display(Name = "Contract extenstion")]
        contract_extenstion =9
    }
    public enum Employment_type
    {
        [Display(Name = "Full time")]
        Fulltime = 1,
        [Display(Name = "Part time")]
        Parttime = 2
    }
    public enum Medical_commite_recomindation
    {
        
        Fit = 1,
        [Display(Name = "Not fit")]
        Not_Fit = 2
    }
    

          public enum Tickets_Class_Tpye
    {

        Economy = 1,
        [Display(Name = "Premium Economy")]
        Premium_Economy = 2,
        Business = 3,
         First=4
    }











}