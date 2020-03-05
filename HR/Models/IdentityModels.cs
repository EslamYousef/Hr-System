using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using HR.Models.Infra;
using HR.Models.ViewModel;
using HR.Reposatory;
using HR.Areas.suberAdmin.Models;

namespace HR.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string ImagePath { get; internal set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
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
        public DbSet<Special_Allwonce_History> Special_Allwonce_History{get;set;}
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

        
    }
}