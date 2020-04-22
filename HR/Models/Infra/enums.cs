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
        Email = 1,
        phone = 2,
        mobile = 3,
        fax = 4

    }
    public enum reject_purpose
    {
        [Display(Name = "Job experience")]
        Job_experience = 1,
        [Display(Name = "Military service")]
        Military_service = 2
    }
    public enum check_status
    {
        created = 1,
        Return_To_Review = 2,
        Approved = 3,
        Rejected = 4,
        Canceled = 5,
        Recervied = 6,
        Closed = 7

    }
    public class months
    {
        public int id { get; set; }
        public string Name { get; set; }
        public float value { get; set; }

    }
    public enum ClassType
    {
        Economy = 1,
        [Display(Name = "Premium economy")]
        Premium_economy = 2,
        Business = 3,
        First = 4
    }
    public enum Company_type
    {
        [Display(Name = "Public Sector")]
        Public_Sector = 1,
        [Display(Name = "Join Venture Sector")]
        join_Venture_Sector = 2,
        Investment = 3,
        None = 4
    }
    public enum typenumstreet
    {
        Even = 1,
        Odd = 2,
        Both = 3
    }
    public enum Test_type
    {
        Interview = 1,
        Test = 2,
        Both = 3
    }
    public enum gender
    {
        male = 1,
        female = 2,
        Both = 3
    }
    public enum working_system
    {
        Day = 1,
        [Display(Name = "shift -8 Hours")]
        shift_8_Hours = 2,
        [Display(Name = "shift -12 Hours")]
        shift_12_Hours = 3
    }
    public enum validity
    {
        valid = 1,
        invalid = 2,

    }
    public enum parment
    {
        permanent = 1,
        [Display(Name = "Part time")]
        part_time = 2,

    }
    public enum slot_type
    {
        Active = 1,
        [Display(Name = "In Active")]
        in_active = 2,

    }
    public enum type_allowance
    {
        [Display(Name = "Job level class")]
        job_level_class = 1,
        [Display(Name = "Job levle grade ")]
        job_levle_grade = 2,
        [Display(Name = "Job level card")]
        job_level_card = 3

    }
    public enum ChModels
    {
        Basic = 1,
        Organization = 2,
        Recuirtment = 3,
        Personnel = 4,
        Medical = 5,
        [Display(Name = "Talent Development")]
        Talent_Development = 6,
        Compensation = 7,
        Payroll = 8

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
        Yearly = 3
    }
    public enum Type
    {
        Subscription = 1,
        Syndicate = 2,
        Budget = 3,
        [Display(Name = "Employee Record")]
        employee_record = 4,
        [Display(Name = "Committe Resolution Recuirtment")]
        Committe_Resolution_Recuirtment = 5
    }
    public enum Gender
    {
        male = 1,
        female = 2,
        other = 3
    }
    public enum Marital_Status
    {
        Single = 1,
        Married = 2,
        [Display(Name = "Married reliable")]
        Married_reliable = 3,
        Divorced = 4,
        [Display(Name = "Divorced reliable")]
        Divorced_reliable = 5,
        Widower = 6,
        [Display(Name = "Widower reliable")]
        Widower_reliable = 7
    }
    public enum ID_type
    {
        [Display(Name = "National ID")]
        National_ID = 1,
        Passport = 2,
        Dire = 3
    }
    public enum Blood_group
    {
        None = 1,
        [Display(Name = "O (negative)")]
        O_negative = 2,
        [Display(Name = "A (positive)")]
        A_positive = 3,
        [Display(Name = "A (negative)")]
        A_negative = 4,
        [Display(Name = "B (positive)")]
        B_positive = 5,
        [Display(Name = "B (negative)")]
        B_negative = 6,
        [Display(Name = "AB (positive)")]
        AB_positive = 7,
        [Display(Name = "AB (negative)")]
        AB_negative = 8
    }
    public enum Health_Status
    {
        Ability = 1,
        [Display(Name = "In Ability")]
        In_Ability = 2,
        [Display(Name = "In Ability military operations")]
        In_Ability_military_operations = 3
    }
    public enum Main_Status
    {
        [Display(Name = "On Job")]
        On_Job = 1,
        [Display(Name = "Out Of Job")]
        Out_Of_Job = 2
    }
    public enum Sub_Status
    {
        Hire = 1,
        [Display(Name = "Internal Transafer")]
        Internal_Transafer = 2,
        [Display(Name = "Horizontal Delegation")]
        Horizontal_Delegation = 3,
        [Display(Name = "Vertical Delegation")]
        Vertical_Delegation = 4,
        [Display(Name = "Secondment In")]
        Secondment_In = 5,
        Assignment = 6,
        [Display(Name = "Re-Hire")]
        Re_Hire = 7,
        [Display(Name = "On vacation")]
        On_vacation = 8,
        Retired = 9,
        Resigned = 10,
        Terminated = 11,
        Dead = 12,
        [Display(Name = "Secondment Out")]
        Secondment_Out = 13,
        [Display(Name = "End Of Contract")]
        End_Of_Contract = 14
    }
    public enum Boarding_membership
    {
        None = 1,
        [Display(Name = "Board member")]
        Board_member = 2,
        [Display(Name = "Head member")]
        Head_member = 3
    }
    public enum Transportation_method
    {
        None = 1,
        [Display(Name = "Company bus")]
        Company_bus = 2,
        [Display(Name = "Given car")]
        Given_car = 3
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
        [Display(Name = "On poinsor")]
        On_poinsor = 1,
        Resignation = 2,
        Termination = 3,
        Death = 4,
        [Display(Name = "External loande out")]
        External_loande_out = 5,
        [Display(Name = "End of cotract")]
        End_of_cotract = 6,
        [Display(Name = "Early EOS")]
        Early_EOS = 7,
        [Display(Name = "Early EOS for medical")]
        Early_EOS_for_medical = 8
    }
    public enum Notice_period_type
    {
        [Display(Name = "Working date only")]
        Working_date_only = 1,
        [Display(Name = "Days work/official holiday")]
        Days_work = 2

    }
    public enum Position_status
    {
        Active = 1,
        Hold = 2,
        History = 3

    }
    public enum EOS_reasons
    {
        Retired = 1,
        Resigned = 2,
        Terminate = 3,
        Dead = 4,
        Transfered = 5,
        [Display(Name = "Secondment out")]
        Secondment_out = 6,
        [Display(Name = "Early EOS")]
        Early_EOS = 7,
        [Display(Name = "Early EOS for medical")]
        Early_EOS_for_medical = 8
    }
    public enum Transaction_Type
    {
        Hire = 1,
        [Display(Name = "Internal Transafer")]
        Internal_Transafer = 2,
        [Display(Name = "Horizontal Delegation")]
        Horizontal_Delegation = 3,
        [Display(Name = "Vertical Delegation")]
        Vertical_Delegation = 4,
        [Display(Name = "Secondment In")]
        Secondment_In = 5,
        [Display(Name = "Secondment out")]
        Secondment_Out = 6,
        Assignment = 7,
        [Display(Name = "External Assignment")]
        External_Assignment = 8,
        Promotion = 9,
        Occupation = 10,
        [Display(Name = "Re-Hire")]
        Re_Hire = 11,
        [Display(Name = "End Of Service")]
        End_Of_Service = 12
    }
    public enum Fixed_basic_salary_by
    {
        [Display(Name = "In House")]
        In_House = 1,
        [Display(Name = "Out House")]
        Out_House = 2,

    }
    public enum Family_member_type
    {
        Spouse = 1,
        Parents = 2,
        Siblings = 3,
        [Display(Name = "Othe family")]
        Othe_family = 4,
        Other = 5,
        Partner = 6,
        Child = 7
    }
    public enum Degree_of_kinship
    {
        [Display(Name = "First grade")]
        First_grade = 1,
        [Display(Name = "Second grade")]
        Second_grade = 2,
        [Display(Name = "Third grade")]
        Third_grade = 3,

    }
    public enum Status
    {
        Live = 1,
        Dead = 2,
    }
    public enum Id_type
    {
        [Display(Name = "National id")]
        National_id = 1,
        Passport = 2,
        Other = 3
    }
    public enum Health_Status2
    {
        Ability = 1,
        [Display(Name = "In Ability")]
        In_Ability = 2
    }
    public enum Working_status
    {
        Working = 1,
        Retired = 2,
        [Display(Name = "Not working")]
        Not_working = 3
    }

    public enum Emergency_level
    {
        Primary = 1,
        Secondary = 2
    }
    public enum transaction_type
    {
        [Display(Name = "Internal transfer permanant")]
        internal_transfer_permanant = 1,
        [Display(Name = "Internal transfer teporary")]
        internal_transfer_teporary = 2,
        [Display(Name = "Horizontal delegation")]
        horizontal_delegation = 3,
        [Display(Name = "vertical delegtation")]
        vertical_delegtation = 4,
        assignment = 5,
        [Display(Name = "External assignment")]
        external_assignment = 6,
        promotion = 7,
        [Display(Name = "Re-Hire")]
        re_hire = 8,
        [Display(Name = "Contract extenstion")]
        contract_extenstion = 9
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
        First = 4
    }

    public enum Level
    {
        Poor = 1,
        Good = 2,
        [Display(Name = "Very Good ")]
        very_good = 3,
        [Display(Name = "a Good Idol")]
        a_good_idol = 4
    }

    public enum Military_service_status
    {
        [Display(Name = "Led service")]
        Led_service = 1,
        [Display(Name = "Under Request")]
        Under_Request = 2,
        [Display(Name = "Permanently recovered ")]
        Permanently_recovered = 3,
        hale = 4,
        Delayed = 5,
        [Display(Name = "Never asked for service ")]
        Never_asked_for_service = 6,
        Excluded = 7,
        [Display(Name = "In Service ")]
        in_service = 8
    }
    public enum Subscription_value_type
    {
        Fixed = 1,
        Variable = 2
    }
    public enum Membership_type
    {
        Member = 1,
        [Display(Name = "Board member")]
        Board_member = 2,
        [Display(Name = "Head member")]
        Head_member = 3
    }

    public enum Related_to
    {
        Employee = 1,
        Family = 2
    }
    public enum Document_status
    {
        [Display(Name = "Not delivered")]
        Not_delivered = 1,
        Delivered = 2,
        [Display(Name = "Received by employee")]
        Received_by_employee = 3
    }
    public enum Decisiontype
    {
       
        Positive =1,
       
        Negative =2
    }
    public enum Periods
    {
        Monyhely=1,
        Quarter=2,
        [Display(Name = "Half year")]
        Half_year =3,
        Yearly=4

    }
    public enum Type_Holiday
    {
        [Display(Name = "PublicHoliday")]
        Public_Holiday = 1,
        Event = 2
    }
           
    public enum LeavesType
    {
        Annual = 1,
        [Display(Name = "Annual (Casual)")]
        AnnualCasual = 2,
        [Display(Name = "Leave without pay")]
        Leavewithoutpay = 3,
        [Display(Name = "Sick Leave")]
        SickLeave = 4,
        Others = 5,
        Unlimited = 6,
        [Display(Name = "Instead Of Overtime")]
        InsteadOfOvertime = 7,
        [Display(Name = "Lateness After Leave")]
        LatenessAfterLeave = 8
    }
    public enum Committe_Usage
    {
        Personnel = 1,
        Test = 2
    }
    public enum Committe_Resolution_Status
    {
            
        Created = 1,
        Approved = 2,
        Rejected=3,
        Canceled=4,
        [Display(Name = "Report As Ready")]
        Report_As_Ready=5
    }
    public enum Committe_Type
    {
        Official = 1,
        [Display(Name = "Non-Official")]
        Non_Official = 2
    }
    public enum Position_Status
    {
        Active = 1,
        [Display(Name = "Not Active")]
        Not_Active = 2,
    }
    public enum Working_System
    {
        Day = 1,
        Night = 2,
    }
    public enum ApplicationStatus
    {
        [Display(Name = "Interview/Test")]
        Interview_Test = 1,
        Confirmed = 2,
        Rejected = 3,
        Canceled = 4
    }
}
