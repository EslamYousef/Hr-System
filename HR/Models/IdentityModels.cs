﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using HR.Models.Infra;
using HR.Models.ViewModel;
using HR.Reposatory;
using HR.Areas.suberAdmin.Models;
using HR.Models.Time_management;
using HR.Models.All_Table_Commitee_Resolution;
using HR.Models.Application;
using HR.Models.SetupPayroll;
using HR.Models.CardPayroll;
using HR.Models.payroll_trans;
using HR.Models.TransactionsPayroll;
using HR.Models.Training.setup;
using HR.Models.Training.trans;
using HR.Models.Training.transaction;
using HR.Models.Vacations;
using HR.Models.penalities.setup;
using HR.Models.user;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Configuration;
using HR.Controllers;
using System;

namespace HR.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string ImagePath { get; internal set; }
        public string company_name { get; set; }
        public int? employee_o { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public string employee_name { get; set; }
        public bool active { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
   static public class db_
    {
        public static string server_name { get; set; }
        public static string data_base { get; set; }
        public static string user_id { get; set; }
        public static string pass_ { get; set; }
        public  static string con { get; set; }
         public static void fun()
        {
            var text = System.IO.File.ReadAllText(@"C:\conc.txt").Split('/');
            db_.server_name = text[0];
            db_.data_base = text[1];
            db_.user_id = text[2];
            db_.pass_ = text[3];
            object[] array1 = new object[4] { db_.server_name, db_.data_base, db_.user_id, db_.pass_ };
            var con2 = new SqlConnection(string.Format(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, array1)).ConnectionString;
            db_.con = con2;
            using (SqlConnection conn = new SqlConnection(db_.con))
            {
                try
                {
                    conn.Open();
                }
                catch(Exception)
                {

                }
                // throws if invalid
            }
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false)
        {
           if(db_.con==null)
            {
                db_.fun();
            }
            this.Database.Connection.ConnectionString = db_.con;
        }

        public static ApplicationDbContext Create()
        {
                return new ApplicationDbContext();
        }
        public DbSet<Country> Country { get; set; }
        public DbSet<Area> Area { get; set; }
        public DbSet<the_states> the_states { get; set; }
        public DbSet<Territories> Territories { get; set; }
        public DbSet<cities> cities { get; set; }
        public DbSet<postcode> postcode { get; set; }
        public DbSet<Nationality> Nationality { get; set; }
        public DbSet<Religion> Religion { get; set; }
        public DbSet<Main_Educate_body> Main_Educate_body { get; set; }
        public DbSet<Educate_category> Educate_category { get; set; }
        public DbSet<Educate_Title> Educate_Title { get; set; }
        public DbSet<Sub_educational_body> Sub_educational_body { get; set; }
        public DbSet<GradeEducate> GradeEducate { get; set; }
        public DbSet<Name_of_educational_qualification> Name_of_educational_qualification { get; set; }
        public DbSet<Qualification_Major> Qualification_Major { get; set; }
        public DbSet<Military_Service_Status> Military_Service_Status { get; set; }
        public DbSet<Military_Service_Rank> Military_Service_Rank { get; set; }
        public DbSet<Document_Group> Document_Group { get; set; }
        public DbSet<Documents> Documents { get; set; }
        public DbSet<Contact_methods> Contact_methods { get; set; }
        public DbSet<Rejection_Reasons> Rejection_Reasons { get; set; }
        public DbSet<Job_level_class> Job_level_class { get; set; }

        public DbSet<Currency> Currency { get; set; }
        public DbSet<Exchange_Rate> Exchange_Rate { get; set; }
        public DbSet<check_request_status> check_request_status { get; set; }
        public DbSet<Checktype> Checktype { get; set; }
        public DbSet<Air_ports> Air_ports { get; set; }
        public DbSet<Sponsor> Sponsor { get; set; }
        public DbSet<months> months { get; set; }
        public DbSet<TicketPrice> TicketPrice { get; set; }
        public DbSet<External_compaines> External_compaines { get; set; }
        public DbSet<Authority_Type> Authority_Type { get; set; }
        public DbSet<Authorities> Authorities { get; set; }
        public DbSet<Skill_group> Skill_group { get; set; }
        public DbSet<Skill> Skill { get; set; }
        public DbSet<Risks_Type> Risks_Type { get; set; }
        public DbSet<Risks> Risks { get; set; }
        public DbSet<Job_title_class> Job_title_class { get; set; }
        public DbSet<Job_title_sub_class> Job_title_sub_class { get; set; }
        public DbSet<Budget_class_items> Budget_class_items { get; set; }
        public DbSet<Budget_class> Budget_class { get; set; }
        public DbSet<Organization_Unit_Type> Organization_Unit_Type { get; set; }
        public DbSet<Organization_Unit_Schema> Organization_Unit_Schema { get; set; }
        public DbSet<Organization_Unit_Level> Organization_Unit_Level { get; set; }
        public DbSet<Responsibilities> Responsibilities { get; set; }
        public DbSet<Requirments> Requirments { get; set; }
        public DbSet<Experience_group> Experience_group { get; set; }
        public DbSet<Allowance_years> Allowance_years { get; set; }
        public DbSet<Committe_subjects> Committe_subjects { get; set; }
        public DbSet<Check_List_Items> Check_List_Items { get; set; }
        public DbSet<Test> Test { get; set; }
        public DbSet<Questions> Questions { get; set; }
        public DbSet<FileDetailsModel> FileDetailsModel { get; set; }

        public DbSet<Files> Files { get; set; }
        public DbSet<check_Request> check_Request { get; set; }

        public DbSet<check_request_change_status> check_request_change_status { get; set; }
        public DbSet<job_level_setup> job_level_setup { get; set; }
        public DbSet<job_level_education> job_level_education { get; set; }

        public DbSet<Job_level_grade> Job_level_gradee { get; set; }
        public DbSet<job_title_cards> job_title_cards { get; set; }
        public DbSet<Slots> Slots { get; set; }
        public DbSet<qulification_job> qulification_job { get; set; }
        public DbSet<skills_job> skills_job { get; set; }
        public DbSet<Job_Details> Job_Details { get; set; }
        public DbSet<Medical_Medicine_Classfication> Medical_Medicine_Classfication { get; set; }
        public DbSet<Medicine> Medicine { get; set; }
        public DbSet<Medical_Doctors_Level> Medical_Doctors_Level { get; set; }
        public DbSet<Medical_Doctors> Medical_Doctors { get; set; }
        public DbSet<Allergy_Type> Allergy_Type { get; set; }
        public DbSet<Disease> Disease { get; set; }
        public DbSet<Sings_Symptom> Sings_Symptom { get; set; }
        public DbSet<Special_Allwonce_History> Special_Allwonce_History { get; set; }
        public DbSet<Medical_Service_Group> Medical_Service_Group { get; set; }
        public DbSet<Medical_Service_Classification> Medical_Service_Classification { get; set; }
        public DbSet<Medical_Service> Medical_Service { get; set; }
        public DbSet<Medical_Contributions_Allocations_Entry> Medical_Contributions_Allocations_Entry { get; set; }
        public DbSet<Medical_Contributions_Allocations_Services> Medical_Contributions_Allocations_Services { get; set; }
        public DbSet<Organization_Chart> Organization_Chart { get; set; }
        public DbSet<Contract_Type> Contract_Type { get; set; }
        public DbSet<StructureModels> StructureModels { get; set; }
        public DbSet<Employee_Profile_Groups> Employee_Profile_Groups { get; set; }
        public DbSet<EOS_Interview_Questions_Groups> EOS_Interview_Questions_Groups { get; set; }
        public DbSet<Definition_of_EOS_Interview_Questions> Definition_of_EOS_Interview_Questions { get; set; }
        public DbSet<Check_List_Item_Groups> Check_List_Item_Groups { get; set; }

        public DbSet<Check_Lists_Items> Check_Lists_Items { get; set; }
        public DbSet<Budget> Budget { get; set; }
        public DbSet<Budget_details> Budget_details { get; set; }
        public DbSet<Employee_Recodes_Group> Employee_Recodes_Group { get; set; }
        public DbSet<Subscription_Syndicate> Subscription_Syndicate { get; set; }
        public DbSet<Employee_Profile> Employee_Profile { get; set; }
        public DbSet<status> status { get; set; }
        public DbSet<justification> justification { get; set; }
        public DbSet<Ability> Ability { get; set; }
        public DbSet<work_location> work_location { get; set; }
        public DbSet<Personnel_Information> Personnel_Information { get; set; }
        public DbSet<Service_Information> Service_Information { get; set; }
        public DbSet<Employee_Address_Profile> Employee_Address_Profile { get; set; }
        public DbSet<Employee_records> Employee_records { get; set; }
        public DbSet<Employee_Qualification_Profile> Employee_Qualification_Profile { get; set; }
        public DbSet<EOS_Request> EOS_Request { get; set; }
        public DbSet<Answer_EOS> Answer_EOS { get; set; }
        public DbSet<Position_Information> Position_Information { get; set; }
        public DbSet<Position_Transaction_Information> Position_Transaction_Information { get; set; }
        public DbSet<check_list_EOS> check_list_EOS { get; set; }
        public DbSet<Employee_family_profile> Employee_family_profile { get; set; }
        public DbSet<personnel_transaction> personnel_transaction { get; set; }
        public DbSet<Employee_experience_profile> Employee_experience_profile { get; set; }
        public DbSet<Employee_contact_profile> Employee_contact_profile { get; set; }
        public DbSet<Employee_contract_profile> Employee_contract_profile { get; set; }
        public DbSet<man_power> man_power { get; set; }
        public DbSet<items_man_power> items_man_power { get; set; }
        public DbSet<request_new_contract> new_contrct { get; set; }
        public DbSet<Employee_military_service_profile> Employee_military_service_profile { get; set; }
        public DbSet<Employee_military_service_calling> Employee_military_service_calling { get; set; }
        public DbSet<Employee_beneficiary_profile> Employee_beneficiary_profile { get; set; }
        public DbSet<Employee_vehicle_profile> Employee_vehicle_profile { get; set; }
        public DbSet<Employee_sponsor_profile> Employee_sponsor_profile { get; set; }
        public DbSet<Append_beneficiary_Family> Append_beneficiary_Family { get; set; }
        public DbSet<Employee_subscription_syndicate_profile> Employee_subscription_syndicate_profile { get; set; }
        public DbSet<Employee_attachment_profile> Employee_attachment_profile { get; set; }
        public DbSet<Roles> AddSpecificListOfRoles { get; set; }

        public DbSet<EvaluationElements> EvaluationElements { get; set; }
        public DbSet<EvaluationElementCompetenies> EvaluationElementCompetenies { get; set; }
        public DbSet<Evalution_and_competencies> Evalution_and_competencies { get; set; }
        public DbSet<EvaluationGrade> EvaluationGrade { get; set; }
        public DbSet<EvaluationType> EvaluationType { get; set; }
        public DbSet<EvaluationObjectives> EvaluationObjectives { get; set; }
        public DbSet<EvaluationQuestionsandanswers> EvaluationQuestionsandanswers { get; set; }
        public DbSet<Shift_day_status_setup> Shift_day_status_setup { get; set; }
        public DbSet<Public_Holiday_Events> Public_Holiday_Events { get; set; }
        public DbSet<Weekend_setup> Weekend_setup { get; set; }
        public DbSet<Vacations_Setup> Vacations_Setup { get; set; }
        public DbSet<Public_Holidays_Dates> Public_Holidays_Dates { get; set; }
        public DbSet<Append_Public_Holidays_Dates> Append_Public_Holidays_Dates { get; set; }
        public DbSet<PerformanceEvaluationGroup> PerformanceEvaluationGroup { get; set; }
        public DbSet<PerformanceEvaluationGroupEvaluationElements> PerformanceEvaluationGroupEvaluationElements { get; set; }

        public DbSet<Questions_Performance> Questions_Performance { get; set; }
        public DbSet<PlaneSchedule> PlaneSchedule { get; set; }
        public DbSet<EvaluationPlan> EvaluationPlan { get; set; }
        public DbSet<per_emp> per_emp { get; set; }

        public DbSet<EvaluationTransaction> EvaluationTransaction { get; set; }

        // public DbSet<groupevaluation_evaluation_transaction> groupevaluation_evaluation_transaction { get;set;}

        //public DbSet<QuestionsANDAnswers_EvaluationTransaction> QuestionsANDAnswers_EvaluationTransaction { get;set;}
        public DbSet<exper_jobdetails> exper_jobdetails { get; set; }
        public DbSet<mental> mental { get; set; }
        public DbSet<Required_Licenses> Required_Licenses { get; set; }

        public DbSet<special_allowance_job_level_grade> special_allowance_job_level_grade { get; set; }
        public DbSet<special_allowance_job_level_class> special_allowance_job_level_class { get; set; }
        public DbSet<special> special { get; set; }
        public DbSet<Evalu_Element_Tran> Evalu_Element_Tran { get; set; }

        public DbSet<A_Q> A_Q { get; set; }

        public DbSet<obje_eval_tran> obje_eval_tran { get; set; }
        public DbSet<skill_eval> skill_eval { get; set; }
        public DbSet<Applicant_Profile> Applicant_Profile { get; set; }
        public DbSet<Applicant_Address_Profile> Applicant_Address_Profile { get; set; }
        public DbSet<Applicant_Attachment_Profile> Applicant_Attachment_Profile { get; set; }
        public DbSet<Applicant_Qualification_Profile> Applicant_Qualification_Profile { get; set; }

        public DbSet<Applicant_Family_Profile> Applicant_Family_Profile { get; set; }
        public DbSet<Applicant_Previous_Experiences_Profile> Applicant_Previous_Experiences_Profile { get; set; }
        public DbSet<Applicant_Contact_Profile> Applicant_Contact_Profile { get; set; }
        public DbSet<Applicant_Military_Service_Profile> Applicant_Military_Service_Profile { get; set; }
        public DbSet<Applicant_Subscription_Syndicate_Profile> Applicant_Subscription_Syndicate_Profile { get; set; }
        public DbSet<Committe_Resolution_Recuirtment> Committe_Resolution_Recuirtment { get; set; }
        public DbSet<Append_Committe_Member> Append_Committe_Member { get; set; }
        public DbSet<Shift_setup> Shift_setup { get; set; }
        public DbSet<Shiftdaystatus> Shiftdaystatus { get; set; }
        public DbSet<Day_Status_Linkedto_Location> Day_Status_Linkedto_Location { get; set; }
        public DbSet<TransportationMethod> TransportationMethod { get; set; }
        public DbSet<Business_Trip> Business_Trip { get; set; }
        public DbSet<Time_management_action_setup> Time_management_action_setup { get; set; }
        public DbSet<Exit_Permission_Reason> Exit_Permission_Reason { get; set; }
        public DbSet<Exit_permission_type> Exit_permission_type { get; set; }

        public DbSet<Employee_Shift_schedule> Employee_Shift_schedule { get; set; }
        public DbSet<Schedule_Details> Schedule_Details { get; set; }
        public DbSet<Commitee_Agenda> Commitee_Agenda { get; set; }
        public DbSet<Out_Organization> Out_Organization { get; set; }
        public DbSet<In_Organization> In_Organization { get; set; }
        public DbSet<Committe_Activities> Committe_Activities { get; set; }
        public DbSet<Linked_to_Testing> Linked_to_Testing { get; set; }
        public DbSet<Application.Application> Application { get; set; }
        public DbSet<Application.Contract_Information> Contract_Information { get; set; }
        public DbSet<Application.Position_Information_Rec> Position_Information_Rec { get; set; }
        public DbSet<Application.Basic_Salary_Calculation_Result> Basic_Salary_Calculation_Result { get; set; }
        public DbSet<Application.Hiring_Information> Hiring_Information { get; set; }
        public DbSet<Application.Business_Test_Profile> Business_Test_Profile { get; set; }
        public DbSet<Application.Medical_Test_Profile> Medical_Test_Profile { get; set; }
        public DbSet<Application.Application_Status> Application_Status { get; set; }
        public DbSet<Exit_permission_request> Exit_permission_request { get; set; }
        public DbSet<CasesTM> CasesTM { get; set; }
        public DbSet<Time_management_conditional_setup> Time_management_conditional_setup { get; set; }
        public DbSet<workpermissionrequest> workpermissionrequest { get; set; }
        public DbSet<business_trip_request> business_trip_request { get; set; }
        public DbSet<Personnel_Committee_Profile> Personnel_Committee_Profile { get; set; }
        public DbSet<Hiring_Check_List> Hiring_Check_List { get; set; }
        public DbSet<Bank> Bank { get; set; }
        public DbSet<Branch> Branch { get; set; }
        public DbSet<CostCenter> CostCenter { get; set; }
        public DbSet<CostCenterCategory> CostCenterCategory { get; set; }
        public DbSet<ExtendedFields_Details> ExtendedFields_Details { get; set; }
        public DbSet<ExtendedFields_Header> ExtendedFields_Header { get; set; }
        public DbSet<GL_AccountSetup> GL_AccountSetup { get; set; }
        public DbSet<Locker> Locker { get; set; }
        public DbSet<PayrollPeriodSetup> PayrollPeriodSetup { get; set; }
        public DbSet<PayrollTransactionJournalSetup> PayrollTransactionJournalSetup { get; set; }

        public DbSet<PayrollGeneralSetup> PayrollGeneralSetup { get; set; }
        public DbSet<salary_code> salary_code { get; set; }
        public DbSet<SalaryCodeGroup_Header> SalaryCodeGroup_Header { get; set; }
        public DbSet<SalaryCodeGroup_Detail> SalaryCodeGroup_Detail { get; set; }
        public DbSet<LoanInAdvanceSetup> LoanInAdvanceSetup { get; set; }
        public DbSet<ManualPaymentTypes_Detail> ManualPaymentTypes_Detail { get; set; }
        public DbSet<ManualPaymentTypes_Header> ManualPaymentTypes_Header { get; set; }

        public DbSet<Employee_Financial_Contract_Detail> Employee_Financial_Contract_Detail { get; set; }
        public DbSet<Employee_Financial_Contract_Header> Employee_Financial_Contract_Header { get; set; }
        public DbSet<FinancialContract_ExtendedFieldsDetails> FinancialContract_ExtendedFieldsDetails { get; set; }

        public DbSet<SalaryItemCollectionGroup_Header> SalaryItemCollectionGroup_Header { get; set; }
        public DbSet<SalaryItemCollectionGroup_Detail> SalaryItemCollectionGroup_Detail { get; set; }

        public DbSet<LinkLoanDeductionsWithOtherManualPayment> LinkLoanDeductionsWithOtherManualPayment { get; set; }
        public DbSet<LoanInstallment> LoanInstallment { get; set; }
        public DbSet<LoanRequest> LoanRequest { get; set; }
        public DbSet<ManualPaymentTransactionEntry> ManualPaymentTransactionEntry { get; set; }

        public DbSet<ManualPaymentTransactionEntry_Detail> ManualPaymentTransactionEntry_Detail { get; set; }

        public DbSet<ManualPaymentTransactionEntry_ExtendedFieldsDetail> ManualPaymentTransactionEntry_ExtendedFieldsDetail { get; set; }
        public DbSet<Employee_Payroll_Transactions> Employee_Payroll_Transactions { get; set; }
        public DbSet<Employee_Payroll_Transactions_ExtendedFieldsDetail> Employee_Payroll_Transactions_ExtendedFieldsDetail { get; set; }
        public DbSet<EmployeeAnnualIncreaseHistory> EmployeeAnnualIncreaseHistory { get; set; }
        public DbSet<Employee_BasicSalary_History> Employee_BasicSalary_History { get; set; }
        public DbSet<LedgerTransaction> LedgerTransaction { get; set; }
        public DbSet<LedgerTransactionLine> LedgerTransactionLine { get; set; }
        public DbSet<LoanTransfer> LoanTransfer { get; set; }
        public DbSet<LoanAdjustment> LoanAdjustment { get; set; }
        public DbSet<TrainingCenter> TrainingCenter { get; set; }
        public DbSet<TrainingCenters_Branch> TrainingCenters_Branch { get; set; }
        public DbSet<TrainingCenters_Branch_Contact> TrainingCenters_Branch_Contact { get; set; }
        public DbSet<ClassRoom_Group> ClassRoom_Group { get; set; }
        public DbSet<ClassRoom> ClassRoom { get; set; }
        public DbSet<CourseClassification> CourseClassification { get; set; }
        public DbSet<Course_Groups> Course_Groups { get; set; }
        public DbSet<Course_SubGroups> Course_SubGroups { get; set; }
        public DbSet<CostElement> CostElement { get; set; }
        public DbSet<Provider> Provider { get; set; }
        public DbSet<Training_Facilities> Training_Facilities { get; set; }
        public DbSet<TrainingType_Detail> TrainingType_Detail { get; set; }
        public DbSet<TrainingType_Header> TrainingType_Header { get; set; }
        public DbSet<TrainingPolicy> TrainingPolicy { get; set; }

        public DbSet<Instructor> Instructor { get; set; }
        public DbSet<Instructor_Contact> Instructor_Contact { get; set; }
        public DbSet<Cours> Cours { get; set; }
        public DbSet<CourceQualification> CourceQualification { get; set; }
        public DbSet<Course_Centers> Course_Centers { get; set; }
        public DbSet<Course_Skills> Course_Skills { get; set; }
        public DbSet<Course_TrainingType> Course_TrainingType { get; set; }
        public DbSet<Distributed_Opportunity> Distributed_Opportunity { get; set; }
        public DbSet<TrainingOpportunity_Detail> TrainingOpportunity_Detail { get; set; }
        public DbSet<TrainingOpportunity_Header> TrainingOpportunity_Header { get; set; }
        public DbSet<TrainingPlan_Header> TrainingPlan_Header { get; set; }
        public DbSet<TrainingPlan_Detail> TrainingPlan_Detail { get; set; }
        public DbSet<Course_CostElement_Header> Course_CostElement_Header { get; set; }
        public DbSet<Course_CostElement_Detail> Course_CostElement_Detail { get; set; }
        public DbSet<TrainingParticipants_Header> TrainingParticipants_Header { get; set; }
        public DbSet<TrainingParticipants_Detail> TrainingParticipants_Detail { get; set; }
        public DbSet<ShiftdaystatusDetials> ShiftdaystatusDetials { get; set; }
        public DbSet<TrainingCourseResult_Detail> TrainingCourseResult_Detail { get; set; }
        public DbSet<TrainingCourseResult_Header> TrainingCourseResult_Header { get; set; }
        public DbSet<TrainingCourceEvaluationElement> TrainingCourceEvaluationElement { get; set; }
        public DbSet<TrainingCourceEvaluation_Details> TrainingCourceEvaluation_Details { get; set; }
        public DbSet<TrainingCourceEvaluation_Header> TrainingCourceEvaluation_Header { get; set; }
        public DbSet<Shiftscheduletemplate> Shiftscheduletemplate { get; set; }
        public DbSet<Template> Template { get; set; }
        public DbSet<TimeManagement_EmployeeTimeAttendanceTransaction_Header> TimeManagement_EmployeeTimeAttendanceTransaction_Header { get; set; }
        public DbSet<TimeManagement_EmployeeTimeAttendanceTransaction_Detail> TimeManagement_EmployeeTimeAttendanceTransaction_Detail { get; set; }
        public DbSet<TimeManagement_EmployeeTimeAttendanceTransaction_Clc_Results> TimeManagement_EmployeeTimeAttendanceTransaction_Clc_Results { get; set; }
        public DbSet<LeavesBalance> LeavesBalance { get; set; }
        public DbSet<LeavesRequestMaster> LeavesRequestMaster { get; set; }
        public DbSet<Discipline_PunishmentGroup> Discipline_PunishmentGroup { get; set; }
        public DbSet<Discipline_PenaltyItem_Header> Discipline_PenaltyItem_Header { get; set; }
        public DbSet<Discipline_PenaltyItem_Detail> Discipline_PenaltyItem_Detail { get; set; }
        public DbSet<Discipline_PunishmentRestOption> Discipline_PunishmentRestOption { get; set; }
        public DbSet<Discipline_Punishment> Discipline_Punishment { get; set; }
        public DbSet<Discipline_Punishment_Detail> Discipline_Punishment_Detail { get; set; }
        public DbSet<Discipline_PunishmentTransaction> Discipline_PunishmentTransaction { get; set; }
        public DbSet<Discipline_PunishmentTransaction_Detail> Discipline_PunishmentTransaction_Detail { get; set; }
        public DbSet<Formula_Header> Formula_Header { get; set; }
        public DbSet<Formula_Detail> Formula_Detail { get; set; }
        public DbSet<Payroll_Calculation_Result> Payroll_Calculation_Result { get; set; }
        public DbSet<Payroll_Calculation_Result_Header> Payroll_Calculation_Result_Header { get; set; }
        public DbSet<User_LinkTo_UserGroup> User_LinkTo_UserGroup { get;set;}
        public DbSet<User_Permissions> User_Permissions { get; set; }
        public DbSet<User_Group_Info> User_Group_Info { get; set; }
        public DbSet<permissions> permissions { get; set; }
        public DbSet<group_with_permission> group_with_permission { get; set; }
        public DbSet<LeavesTransactionBalance> LeavesTransactionBalance { get; set; }
        public DbSet<LeavesMass_Transaction> LeavesMass_Transaction { get; set; }
        public DbSet<Screen> Screen { get; set; }
        public DbSet<notifications> notifications { get; set; }
        public DbSet<Alert_inbox> Alert_inbox { get; set; }
        public DbSet<date_alert_daily> date_alert_daily { get; set; }

    }

}