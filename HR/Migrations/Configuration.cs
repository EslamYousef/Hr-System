namespace HR.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HR.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HR.Models.ApplicationDbContext context)
        {
            var permissions = new List<HR.Models.user.permissions>();
            ApplicationDbContext dbcontext = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(dbcontext));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var role = new IdentityRole
            {
                Name = "Admin"
            };
            roleManager.Create(role);
            var user = new ApplicationUser
            {
                UserName = "S_admin",
                Email = "S_admin@admin.com",
                employee_name = "no empoyee",
                active = true
            };
            var userPWD = "Admin@123";
            var chkUser = userManager.Create(user, userPWD);
            var admin = userManager.FindByEmail(user.Email);
            //Add default User to Role Admin   
            if (chkUser.Succeeded)
            {
                var result1 = userManager.AddToRole(admin.Id, "Admin");
            }
            
            //=====================================================modules===================================================================
            permissions.Add(new Models.user.permissions { Permission_Name = "Basic", type_permission = Models.user.type_permission.module, module = 1, sub_module = 0 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Organization", type_permission = Models.user.type_permission.module, module = 2, sub_module = 0 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Recuirtment", type_permission = Models.user.type_permission.module, module = 3, sub_module = 0 });
            permissions.Add(new Models.user.permissions { Permission_Name = "personnel", type_permission = Models.user.type_permission.module, module = 4, sub_module = 0 });
            permissions.Add(new Models.user.permissions { Permission_Name = "talent", type_permission = Models.user.type_permission.module, module = 5, sub_module = 0 });
            permissions.Add(new Models.user.permissions { Permission_Name = "payroll", type_permission = Models.user.type_permission.module, module = 6, sub_module = 0 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Vacations", type_permission = Models.user.type_permission.module, module = 7, sub_module = 0 });
            permissions.Add(new Models.user.permissions { Permission_Name = "TM", type_permission = Models.user.type_permission.module, module = 8, sub_module = 0 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Penalties", type_permission = Models.user.type_permission.module, module = 9, sub_module = 0 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Evalution", type_permission = Models.user.type_permission.module, module = 10, sub_module = 0 });
            roleManager.Create(new IdentityRole { Name = "Basic" }); roleManager.Create(new IdentityRole { Name = "Organization" }); roleManager.Create(new IdentityRole { Name = "Recuirtment" });
            roleManager.Create(new IdentityRole { Name = "personnel" }); roleManager.Create(new IdentityRole { Name = "talent" }); roleManager.Create(new IdentityRole { Name = "payroll" });
            roleManager.Create(new IdentityRole { Name = "Vacations" }); roleManager.Create(new IdentityRole { Name = "TM" }); roleManager.Create(new IdentityRole { Name = "Penalties" });
            roleManager.Create(new IdentityRole { Name = "Evalution" });
            //=====================================================submodules==========================================================================
            permissions.Add(new Models.user.permissions { Permission_Name = "BasicSetup", type_permission = Models.user.type_permission.sub_module, module = 1, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "BasicCards", type_permission = Models.user.type_permission.sub_module, module = 1, sub_module = 2 });
            permissions.Add(new Models.user.permissions { Permission_Name = "BasicTransaction", type_permission = Models.user.type_permission.sub_module, module = 1, sub_module = 3 });
            permissions.Add(new Models.user.permissions { Permission_Name = "BasicProcess", type_permission = Models.user.type_permission.sub_module, module = 1, sub_module = 4 });
            permissions.Add(new Models.user.permissions { Permission_Name = "BasicInquiry", type_permission = Models.user.type_permission.sub_module, module = 1, sub_module = 5 });
            permissions.Add(new Models.user.permissions { Permission_Name = "BasicReport", type_permission = Models.user.type_permission.sub_module, module = 1, sub_module = 6 });
            roleManager.Create(new IdentityRole { Name = "BasicSetup" }); roleManager.Create(new IdentityRole { Name = "BasicTransaction" }); roleManager.Create(new IdentityRole { Name = "BasicInquiry" });
            roleManager.Create(new IdentityRole { Name = "BasicCards" }); roleManager.Create(new IdentityRole { Name = "BasicProcess" }); roleManager.Create(new IdentityRole { Name = "BasicReport" });


            permissions.Add(new Models.user.permissions { Permission_Name = "OrganizationSetup", type_permission = Models.user.type_permission.sub_module, module = 2, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "OrganizationCards", type_permission = Models.user.type_permission.sub_module, module = 2, sub_module = 2 });
            permissions.Add(new Models.user.permissions { Permission_Name = "OrganizationTransaction", type_permission = Models.user.type_permission.sub_module, module = 2, sub_module = 3 });
            permissions.Add(new Models.user.permissions { Permission_Name = "OrganizationProcess", type_permission = Models.user.type_permission.sub_module, module = 2, sub_module = 4 });
            permissions.Add(new Models.user.permissions { Permission_Name = "OrganizationInquiry", type_permission = Models.user.type_permission.sub_module, module = 2, sub_module = 5 });
            permissions.Add(new Models.user.permissions { Permission_Name = "OrganizationReport", type_permission = Models.user.type_permission.sub_module, module = 2, sub_module = 6 });
            roleManager.Create(new IdentityRole { Name = "OrganizationSetup" }); roleManager.Create(new IdentityRole { Name = "OrganizationTransaction" }); roleManager.Create(new IdentityRole { Name = "OrganizationInquiry" });
            roleManager.Create(new IdentityRole { Name = "OrganizationCards" }); roleManager.Create(new IdentityRole { Name = "OrganizationProcess" }); roleManager.Create(new IdentityRole { Name = "OrganizationReport" });


            permissions.Add(new Models.user.permissions { Permission_Name = "RecuirtmentSetup", type_permission = Models.user.type_permission.sub_module, module = 3, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "RecuirtmentCards", type_permission = Models.user.type_permission.sub_module, module = 3, sub_module = 2 });
            permissions.Add(new Models.user.permissions { Permission_Name = "RecuirtmentTransaction", type_permission = Models.user.type_permission.sub_module, module = 3, sub_module = 3 });
            permissions.Add(new Models.user.permissions { Permission_Name = "RecuirtmentProcess", type_permission = Models.user.type_permission.sub_module, module = 3, sub_module = 4 });
            permissions.Add(new Models.user.permissions { Permission_Name = "RecuirtmentInquiry", type_permission = Models.user.type_permission.sub_module, module = 3, sub_module = 5 });
            permissions.Add(new Models.user.permissions { Permission_Name = "RecuirtmentReport", type_permission = Models.user.type_permission.sub_module, module = 3, sub_module = 6 });
            roleManager.Create(new IdentityRole { Name = "RecuirtmentSetup" }); roleManager.Create(new IdentityRole { Name = "RecuirtmentTransaction" }); roleManager.Create(new IdentityRole { Name = "RecuirtmentInquiry" });
            roleManager.Create(new IdentityRole { Name = "RecuirtmentCards" }); roleManager.Create(new IdentityRole { Name = "RecuirtmentProcess" }); roleManager.Create(new IdentityRole { Name = "RecuirtmentReport" });


            permissions.Add(new Models.user.permissions { Permission_Name = "personnelSetup", type_permission = Models.user.type_permission.sub_module, module = 4, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "personnelCards", type_permission = Models.user.type_permission.sub_module, module = 4, sub_module = 2 });
            permissions.Add(new Models.user.permissions { Permission_Name = "personnelTransaction", type_permission = Models.user.type_permission.sub_module, module = 4, sub_module = 3 });
            permissions.Add(new Models.user.permissions { Permission_Name = "personnelProcess", type_permission = Models.user.type_permission.sub_module, module = 4, sub_module = 4 });
            permissions.Add(new Models.user.permissions { Permission_Name = "personnelInquiry", type_permission = Models.user.type_permission.sub_module, module = 4, sub_module = 5 });
            permissions.Add(new Models.user.permissions { Permission_Name = "personnelReport", type_permission = Models.user.type_permission.sub_module, module = 4, sub_module = 6 });
            roleManager.Create(new IdentityRole { Name = "personnelSetup" }); roleManager.Create(new IdentityRole { Name = "personnelTransaction" }); roleManager.Create(new IdentityRole { Name = "personnelInquiry" });
            roleManager.Create(new IdentityRole { Name = "personnelCards" }); roleManager.Create(new IdentityRole { Name = "personnelProcess" }); roleManager.Create(new IdentityRole { Name = "personnelReport" });


            permissions.Add(new Models.user.permissions { Permission_Name = "talentSetup", type_permission = Models.user.type_permission.sub_module, module = 5, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "talentCards", type_permission = Models.user.type_permission.sub_module, module = 5, sub_module = 2 });
            permissions.Add(new Models.user.permissions { Permission_Name = "talentTransaction", type_permission = Models.user.type_permission.sub_module, module = 5, sub_module = 3 });
            permissions.Add(new Models.user.permissions { Permission_Name = "talentProcess", type_permission = Models.user.type_permission.sub_module, module = 5, sub_module = 4 });
            permissions.Add(new Models.user.permissions { Permission_Name = "talentInquiry", type_permission = Models.user.type_permission.sub_module, module = 5, sub_module = 5 });
            permissions.Add(new Models.user.permissions { Permission_Name = "talentReport", type_permission = Models.user.type_permission.sub_module, module = 5, sub_module = 6 });
            roleManager.Create(new IdentityRole { Name = "talentSetup" }); roleManager.Create(new IdentityRole { Name = "talentTransaction" }); roleManager.Create(new IdentityRole { Name = "talentInquiry" });
            roleManager.Create(new IdentityRole { Name = "talentCards" }); roleManager.Create(new IdentityRole { Name = "talentProcess" }); roleManager.Create(new IdentityRole { Name = "talentReport" });


            permissions.Add(new Models.user.permissions { Permission_Name = "payrollSetup", type_permission = Models.user.type_permission.sub_module, module = 6, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "payrollCards", type_permission = Models.user.type_permission.sub_module, module = 6, sub_module = 2 });
            permissions.Add(new Models.user.permissions { Permission_Name = "payrollTransaction", type_permission = Models.user.type_permission.sub_module, module = 6, sub_module = 3 });
            permissions.Add(new Models.user.permissions { Permission_Name = "payrollProcess", type_permission = Models.user.type_permission.sub_module, module = 6, sub_module = 4 });
            permissions.Add(new Models.user.permissions { Permission_Name = "payrollInquiry", type_permission = Models.user.type_permission.sub_module, module = 6, sub_module = 5 });
            permissions.Add(new Models.user.permissions { Permission_Name = "payrollReport", type_permission = Models.user.type_permission.sub_module, module = 6, sub_module = 6 });
            roleManager.Create(new IdentityRole { Name = "payrollSetup" }); roleManager.Create(new IdentityRole { Name = "payrollTransaction" }); roleManager.Create(new IdentityRole { Name = "payrollInquiry" });
            roleManager.Create(new IdentityRole { Name = "payrollCards" }); roleManager.Create(new IdentityRole { Name = "payrollProcess" }); roleManager.Create(new IdentityRole { Name = "payrollReport" });

            permissions.Add(new Models.user.permissions { Permission_Name = "VacationsSetup", type_permission = Models.user.type_permission.sub_module, module = 7, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "VacationsCards", type_permission = Models.user.type_permission.sub_module, module = 7, sub_module = 2 });
            permissions.Add(new Models.user.permissions { Permission_Name = "VacationsTransaction", type_permission = Models.user.type_permission.sub_module, module = 7, sub_module = 3 });
            permissions.Add(new Models.user.permissions { Permission_Name = "VacationsProcess", type_permission = Models.user.type_permission.sub_module, module = 7, sub_module = 4 });
            permissions.Add(new Models.user.permissions { Permission_Name = "VacationsReport", type_permission = Models.user.type_permission.sub_module, module = 7, sub_module = 6 });
            roleManager.Create(new IdentityRole { Name = "VacationsSetup" }); roleManager.Create(new IdentityRole { Name = "VacationsTransaction" }); roleManager.Create(new IdentityRole { Name = "VacationsReport" });
            roleManager.Create(new IdentityRole { Name = "VacationsCards" }); roleManager.Create(new IdentityRole { Name = "VacationsProcess" });




            permissions.Add(new Models.user.permissions { Permission_Name = "TMSetup", type_permission = Models.user.type_permission.sub_module, module = 8, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "TMTransaction", type_permission = Models.user.type_permission.sub_module, module = 8, sub_module = 3 });
            permissions.Add(new Models.user.permissions { Permission_Name = "TMProcess", type_permission = Models.user.type_permission.sub_module, module = 8, sub_module = 4 });
            permissions.Add(new Models.user.permissions { Permission_Name = "TMReport", type_permission = Models.user.type_permission.sub_module, module = 8, sub_module = 6 });
            roleManager.Create(new IdentityRole { Name = "TMSetup" }); roleManager.Create(new IdentityRole { Name = "TMProcess" });
            roleManager.Create(new IdentityRole { Name = "TMTransaction" }); roleManager.Create(new IdentityRole { Name = "TMReport" });


            permissions.Add(new Models.user.permissions { Permission_Name = "PenaltiesSetup", type_permission = Models.user.type_permission.sub_module, module = 9, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "PenaltiesTransaction", type_permission = Models.user.type_permission.sub_module, module = 9, sub_module = 3 });
            permissions.Add(new Models.user.permissions { Permission_Name = "PenaltiesProcess", type_permission = Models.user.type_permission.sub_module, module = 9, sub_module = 4 });
            roleManager.Create(new IdentityRole { Name = "PenaltiesSetup" }); roleManager.Create(new IdentityRole { Name = "PenaltiesTransaction" });
            roleManager.Create(new IdentityRole { Name = "PenaltiesProcess" });


            permissions.Add(new Models.user.permissions { Permission_Name = "EvalutionSetup", type_permission = Models.user.type_permission.sub_module, module = 10, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "EvalutionCards", type_permission = Models.user.type_permission.sub_module, module = 10, sub_module = 2 });
            permissions.Add(new Models.user.permissions { Permission_Name = "EvalutionTransaction", type_permission = Models.user.type_permission.sub_module, module = 10, sub_module = 3 });
            permissions.Add(new Models.user.permissions { Permission_Name = "EvalutionProcess", type_permission = Models.user.type_permission.sub_module, module = 10, sub_module = 4 });
            roleManager.Create(new IdentityRole { Name = "EvalutionSetup" }); roleManager.Create(new IdentityRole { Name = "EvalutionTransaction" });
            roleManager.Create(new IdentityRole { Name = "EvalutionCards" }); roleManager.Create(new IdentityRole { Name = "EvalutionProcess" });



            //====================================================Forms===========================================================================
            //====basic===========================================================================================================================
            permissions.Add(new Models.user.permissions { Permission_Name = "Address", type_permission = Models.user.type_permission.forms, module = 1, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Qulifications", type_permission = Models.user.type_permission.forms, module = 1, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Documents", type_permission = Models.user.type_permission.forms, module = 1, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Currency", type_permission = Models.user.type_permission.forms, module = 1, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "check requestion", type_permission = Models.user.type_permission.forms, module = 1, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Nationality", type_permission = Models.user.type_permission.forms, module = 1, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Religion", type_permission = Models.user.type_permission.forms, module = 1, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Military", type_permission = Models.user.type_permission.forms, module = 1, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "contact method", type_permission = Models.user.type_permission.forms, module = 1, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Rejection reasons", type_permission = Models.user.type_permission.forms, module = 1, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "External companies", type_permission = Models.user.type_permission.forms, module = 1, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Air ports", type_permission = Models.user.type_permission.forms, module = 1, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Tickets prices", type_permission = Models.user.type_permission.forms, module = 1, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "sponsors", type_permission = Models.user.type_permission.forms, module = 1, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "check requestion_transa", type_permission = Models.user.type_permission.forms, module = 1, sub_module = 3 });
            permissions.Add(new Models.user.permissions { Permission_Name = "check requestion_proces", type_permission = Models.user.type_permission.forms, module = 1, sub_module = 4 });
            roleManager.Create(new IdentityRole { Name = "Address" }); roleManager.Create(new IdentityRole { Name = "check requestion" }); roleManager.Create(new IdentityRole { Name = "contact method" });
            roleManager.Create(new IdentityRole { Name = "Qulifications" }); roleManager.Create(new IdentityRole { Name = "Nationality" }); roleManager.Create(new IdentityRole { Name = "Rejection reasons" });
            roleManager.Create(new IdentityRole { Name = "Documents" }); roleManager.Create(new IdentityRole { Name = "Religion" }); roleManager.Create(new IdentityRole { Name = "External companies" });
            roleManager.Create(new IdentityRole { Name = "Currency" }); roleManager.Create(new IdentityRole { Name = "Military" }); roleManager.Create(new IdentityRole { Name = "check requestion_transa" });
            roleManager.Create(new IdentityRole { Name = "Air ports" }); roleManager.Create(new IdentityRole { Name = "sponsors" }); roleManager.Create(new IdentityRole { Name = "check requestion_proces" });
            roleManager.Create(new IdentityRole { Name = "Tickets prices" });

            //=====organization
            permissions.Add(new Models.user.permissions { Permission_Name = "Authority", type_permission = Models.user.type_permission.forms, module = 2, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Skills", type_permission = Models.user.type_permission.forms, module = 2, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Risks", type_permission = Models.user.type_permission.forms, module = 2, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "job level", type_permission = Models.user.type_permission.forms, module = 2, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "job title", type_permission = Models.user.type_permission.forms, module = 2, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Organization chart", type_permission = Models.user.type_permission.forms, module = 2, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Budget", type_permission = Models.user.type_permission.forms, module = 2, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Requirments", type_permission = Models.user.type_permission.forms, module = 2, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Experience groups", type_permission = Models.user.type_permission.forms, module = 2, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Responsibilities", type_permission = Models.user.type_permission.forms, module = 2, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Work location", type_permission = Models.user.type_permission.forms, module = 2, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "job level_card", type_permission = Models.user.type_permission.forms, module = 2, sub_module = 2 });
            permissions.Add(new Models.user.permissions { Permission_Name = "job title_card", type_permission = Models.user.type_permission.forms, module = 2, sub_module = 2 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Organization chart_card", type_permission = Models.user.type_permission.forms, module = 2, sub_module = 2 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Budget_card", type_permission = Models.user.type_permission.forms, module = 2, sub_module = 2 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Manpower plan_card", type_permission = Models.user.type_permission.forms, module = 2, sub_module = 2 });
            permissions.Add(new Models.user.permissions { Permission_Name = "view jobs", type_permission = Models.user.type_permission.forms, module = 2, sub_module = 5 });
            permissions.Add(new Models.user.permissions { Permission_Name = "organization reloated to jobs", type_permission = Models.user.type_permission.forms, module = 2, sub_module = 5 });
            permissions.Add(new Models.user.permissions { Permission_Name = "jobs title_report", type_permission = Models.user.type_permission.forms, module = 2, sub_module = 6 });
            permissions.Add(new Models.user.permissions { Permission_Name = "organization chart_report", type_permission = Models.user.type_permission.forms, module = 2, sub_module = 6 });
            roleManager.Create(new IdentityRole { Name = "Authority" }); roleManager.Create(new IdentityRole { Name = "Budget" }); roleManager.Create(new IdentityRole { Name = "job level" });
            roleManager.Create(new IdentityRole { Name = "Skills" }); roleManager.Create(new IdentityRole { Name = "Requirments" }); roleManager.Create(new IdentityRole { Name = "job title" });
            roleManager.Create(new IdentityRole { Name = "Risks" }); roleManager.Create(new IdentityRole { Name = "Experience groups" }); roleManager.Create(new IdentityRole { Name = "Organization chart" });
            roleManager.Create(new IdentityRole { Name = "job level_card" }); roleManager.Create(new IdentityRole { Name = "Responsibilities" }); roleManager.Create(new IdentityRole { Name = "Budget_card" });
            roleManager.Create(new IdentityRole { Name = "job title_card" }); roleManager.Create(new IdentityRole { Name = "Work location" }); roleManager.Create(new IdentityRole { Name = "Manpower plan_card" });
            roleManager.Create(new IdentityRole { Name = "Organization chart_card" });
            roleManager.Create(new IdentityRole { Name = "view jobs" }); roleManager.Create(new IdentityRole { Name = "jobs title_report" }); roleManager.Create(new IdentityRole { Name = "organization chart_report" });
            roleManager.Create(new IdentityRole { Name = "organization reloated to jobs" });

            //=====Recuirtment
            permissions.Add(new Models.user.permissions { Permission_Name = "Check list item Rec", type_permission = Models.user.type_permission.forms, module = 3, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Committle Subjects", type_permission = Models.user.type_permission.forms, module = 3, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Tests", type_permission = Models.user.type_permission.forms, module = 3, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Allowance years", type_permission = Models.user.type_permission.forms, module = 3, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Total Applicants", type_permission = Models.user.type_permission.forms, module = 3, sub_module = 6 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Applicant Profile", type_permission = Models.user.type_permission.forms, module = 3, sub_module = 2 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Committe Resolution Rec", type_permission = Models.user.type_permission.forms, module = 3, sub_module = 2 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Applications Rec", type_permission = Models.user.type_permission.forms, module = 3, sub_module = 2 });

            roleManager.Create(new IdentityRole { Name = "Check list item Rec" }); roleManager.Create(new IdentityRole { Name = "Check list item Rec" }); 
            roleManager.Create(new IdentityRole { Name = "Tests" }); roleManager.Create(new IdentityRole { Name = "Allowance years" });
            roleManager.Create(new IdentityRole { Name = "Total Applicants" });
            roleManager.Create(new IdentityRole { Name = "Applicant Profile" }); roleManager.Create(new IdentityRole { Name = "Applications Rec" });
            roleManager.Create(new IdentityRole { Name = "Committe Resolution Rec" });

            //==personnel
            permissions.Add(new Models.user.permissions { Permission_Name = "Contract Types", type_permission = Models.user.type_permission.forms, module = 4, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Subscription", type_permission = Models.user.type_permission.forms, module = 4, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Employee Record Type", type_permission = Models.user.type_permission.forms, module = 4, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Retirement Levels Setup", type_permission = Models.user.type_permission.forms, module = 4, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Employee Profile Groups", type_permission = Models.user.type_permission.forms, module = 4, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Public Holidays Events", type_permission = Models.user.type_permission.forms, module = 7, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Public Holidays Dates", type_permission = Models.user.type_permission.forms, module = 7, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Weekends", type_permission = Models.user.type_permission.forms, module = 7, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Vacations Setup", type_permission = Models.user.type_permission.forms, module = 7, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Transportation method", type_permission = Models.user.type_permission.forms, module = 8, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Time managment action setup", type_permission = Models.user.type_permission.forms, module = 8, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Business trip mission type setup", type_permission = Models.user.type_permission.forms, module = 8, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Time managemt conditionl formulas setup", type_permission = Models.user.type_permission.forms, module = 8, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Shift day status setup", type_permission = Models.user.type_permission.forms, module = 8, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Shift setup", type_permission = Models.user.type_permission.forms, module = 8, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Employee shift schedule", type_permission = Models.user.type_permission.forms, module = 8, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Exit Permission Types", type_permission = Models.user.type_permission.forms, module = 8, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Exit Permission Reasons", type_permission = Models.user.type_permission.forms, module = 8, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Check List Item Groups", type_permission = Models.user.type_permission.forms, module = 4, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Check List Items", type_permission = Models.user.type_permission.forms, module = 4, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Eos Questions groups", type_permission = Models.user.type_permission.forms, module = 4, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Eos Questions", type_permission = Models.user.type_permission.forms, module = 4, sub_module = 1 });

            permissions.Add(new Models.user.permissions { Permission_Name = "Employee record transaction", type_permission = Models.user.type_permission.forms, module = 4, sub_module = 3 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Employee record process", type_permission = Models.user.type_permission.forms, module = 4, sub_module = 4 });
            permissions.Add(new Models.user.permissions { Permission_Name = "personnel transaction transaction", type_permission = Models.user.type_permission.forms, module = 4, sub_module = 3 });
            permissions.Add(new Models.user.permissions { Permission_Name = "personnel  transaction process", type_permission = Models.user.type_permission.forms, module = 4, sub_module = 4 });
            roleManager.Create(new IdentityRole { Name = "Employee record transaction" }); roleManager.Create(new IdentityRole { Name = "personnel transaction transaction" });
            roleManager.Create(new IdentityRole { Name = "Employee record process" }); roleManager.Create(new IdentityRole { Name = "personnel  transaction process" });
           
            roleManager.Create(new IdentityRole { Name = "Weekends" }); roleManager.Create(new IdentityRole { Name = "Public Holidays Dates" });
            roleManager.Create(new IdentityRole { Name = "Public Holidays Events" }); roleManager.Create(new IdentityRole { Name = "Vacations Setup" });
            roleManager.Create(new IdentityRole { Name = "Employee Profile Groups" }); roleManager.Create(new IdentityRole { Name = "Employee Record Type" });
            roleManager.Create(new IdentityRole { Name = "Subscription" }); roleManager.Create(new IdentityRole { Name = "Retirement Levels Setup" });
            roleManager.Create(new IdentityRole { Name = "Contract Types" }); roleManager.Create(new IdentityRole { Name = "Transportation method" });
            roleManager.Create(new IdentityRole { Name = "Time managment action setup" }); roleManager.Create(new IdentityRole { Name = "Time managment action setup" });
            roleManager.Create(new IdentityRole { Name = "Business trip mission type setup" }); roleManager.Create(new IdentityRole { Name = "Time managemt conditionl formulas setup" });
            roleManager.Create(new IdentityRole { Name = "Shift day status setup" }); roleManager.Create(new IdentityRole { Name = "Shift setup" });
            roleManager.Create(new IdentityRole { Name = "Employee shift schedule" }); roleManager.Create(new IdentityRole { Name = "Exit Permission Types" });
            roleManager.Create(new IdentityRole { Name = "Exit Permission Reasons" }); roleManager.Create(new IdentityRole { Name = "Check List Item Groups" });
            roleManager.Create(new IdentityRole { Name = "Check List Items" }); roleManager.Create(new IdentityRole { Name = "Eos Questions groups" });
            roleManager.Create(new IdentityRole { Name = "Eos Questions" });

            permissions.Add(new Models.user.permissions { Permission_Name = "Employee Profile", type_permission = Models.user.type_permission.forms, module = 4, sub_module = 2 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Create Vacation Balance", type_permission = Models.user.type_permission.forms, module = 7, sub_module = 2 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Vacation Balance", type_permission = Models.user.type_permission.forms, module = 7, sub_module = 2 });
            roleManager.Create(new IdentityRole { Name = "Employee Profile" }); roleManager.Create(new IdentityRole { Name = "Create Vacation Balance" });
            roleManager.Create(new IdentityRole { Name = "Vacation Balance" });

            permissions.Add(new Models.user.permissions { Permission_Name = "Vacations Request", type_permission = Models.user.type_permission.forms, module = 7, sub_module = 3 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Vacation Mass", type_permission = Models.user.type_permission.forms, module = 7, sub_module = 3 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Vacation Balance Adjustement", type_permission = Models.user.type_permission.forms, module = 7, sub_module = 3 });
            roleManager.Create(new IdentityRole { Name = "Vacations Request" }); roleManager.Create(new IdentityRole { Name = "Vacation Mass" });
            roleManager.Create(new IdentityRole { Name = "Vacation Balance Adjustement" });

            permissions.Add(new Models.user.permissions { Permission_Name = "Vacation Approve", type_permission = Models.user.type_permission.forms, module = 7, sub_module = 4 });
            roleManager.Create(new IdentityRole { Name = "Vacations Approve" });

            permissions.Add(new Models.user.permissions { Permission_Name = "Work permission request", type_permission = Models.user.type_permission.forms, module = 8, sub_module = 3});
            permissions.Add(new Models.user.permissions { Permission_Name = "Business trip Mission type request", type_permission = Models.user.type_permission.forms, module = 8, sub_module = 3 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Exit Permission Request", type_permission = Models.user.type_permission.forms, module = 8, sub_module = 3 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Employee Time Attendances", type_permission = Models.user.type_permission.forms, module = 8, sub_module = 3 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Import From Exel Time Attendance", type_permission = Models.user.type_permission.forms, module = 4, sub_module = 3 });

            roleManager.Create(new IdentityRole { Name = "Work permission request" }); roleManager.Create(new IdentityRole { Name = "Business trip Mission type request" }); 
            roleManager.Create(new IdentityRole { Name = "Exit Permission Request" }); roleManager.Create(new IdentityRole { Name = "Employee Time Attendances" });
            roleManager.Create(new IdentityRole { Name = "Import From Exel Time Attendance" });

            permissions.Add(new Models.user.permissions { Permission_Name = "Work permission Approve", type_permission = Models.user.type_permission.forms, module = 8, sub_module = 4 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Business trip Mission type Approve", type_permission = Models.user.type_permission.forms, module = 8, sub_module = 4 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Exit Permission Approve", type_permission = Models.user.type_permission.forms, module = 8, sub_module = 4 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Employee Time Approve", type_permission = Models.user.type_permission.forms, module = 8, sub_module = 4 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Assign schedule template to multi employee", type_permission = Models.user.type_permission.forms, module = 8, sub_module = 4 });

            roleManager.Create(new IdentityRole { Name = "Business trip Mission type Approve" }); roleManager.Create(new IdentityRole { Name = "Work permission Approve" });
            roleManager.Create(new IdentityRole { Name = "Exit Permission Approve" }); roleManager.Create(new IdentityRole { Name = "Employee Time Approve" });
            roleManager.Create(new IdentityRole { Name = "Assign schedule template to multi employee" });

            //======talent
            permissions.Add(new Models.user.permissions { Permission_Name = "Training centers evalution elements", type_permission = Models.user.type_permission.forms, module = 5, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Training centers", type_permission = Models.user.type_permission.forms, module = 5, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Class rooms", type_permission = Models.user.type_permission.forms, module = 5, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Courses_setup", type_permission = Models.user.type_permission.forms, module = 5, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Training plan_setup", type_permission = Models.user.type_permission.forms, module = 5, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Training ploicy", type_permission = Models.user.type_permission.forms, module = 5, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "courses_card", type_permission = Models.user.type_permission.forms, module = 5, sub_module = 2 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Training opportunity_trans", type_permission = Models.user.type_permission.forms, module = 5, sub_module = 3 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Training plan_card_trans", type_permission = Models.user.type_permission.forms, module = 5, sub_module = 3 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Training opportunity_process", type_permission = Models.user.type_permission.forms, module = 5, sub_module = 4 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Training plan_card_process", type_permission = Models.user.type_permission.forms, module = 5, sub_module = 4 });

            roleManager.Create(new IdentityRole { Name = "Training opportunity_process" }); roleManager.Create(new IdentityRole { Name = "Training plan_card_process" });
            roleManager.Create(new IdentityRole { Name = "Training centers" }); roleManager.Create(new IdentityRole { Name = "Training plan_setup" }); roleManager.Create(new IdentityRole { Name = "Training opportunity_trans" });
            roleManager.Create(new IdentityRole { Name = "Class rooms" }); roleManager.Create(new IdentityRole { Name = "Training ploicy" }); roleManager.Create(new IdentityRole { Name = "Training plan_card_trans" });
            roleManager.Create(new IdentityRole { Name = "Courses_setup" }); roleManager.Create(new IdentityRole { Name = "courses_card" }); roleManager.Create(new IdentityRole { Name = "Training centers evalution elements" });


            //=====payroll
            permissions.Add(new Models.user.permissions { Permission_Name = "payroll general setup", type_permission = Models.user.type_permission.forms, module = 6, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "salary item", type_permission = Models.user.type_permission.forms, module = 6, sub_module = 2 });
            permissions.Add(new Models.user.permissions { Permission_Name = "salary items collection groups", type_permission = Models.user.type_permission.forms, module = 6, sub_module = 2 });
            permissions.Add(new Models.user.permissions { Permission_Name = "loan transaction", type_permission = Models.user.type_permission.forms, module = 6, sub_module = 3 });
            permissions.Add(new Models.user.permissions { Permission_Name = "loan process", type_permission = Models.user.type_permission.forms, module = 6, sub_module = 4 });
            roleManager.Create(new IdentityRole { Name = "payroll general setup" }); roleManager.Create(new IdentityRole { Name = "salary items collection groups" });
            roleManager.Create(new IdentityRole { Name = "salary item" }); roleManager.Create(new IdentityRole { Name = "loan transaction" }); roleManager.Create(new IdentityRole { Name = "loan process" });

            permissions.Add(new Models.user.permissions { Permission_Name = "payroll Periodic", type_permission = Models.user.type_permission.forms, module = 6, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Transaction journal", type_permission = Models.user.type_permission.forms, module = 6, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Cost Center Cetegory", type_permission = Models.user.type_permission.forms, module = 6, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Cost Center", type_permission = Models.user.type_permission.forms, module = 6, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Gl Account", type_permission = Models.user.type_permission.forms, module = 6, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Bank", type_permission = Models.user.type_permission.forms, module = 6, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Branches", type_permission = Models.user.type_permission.forms, module = 6, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Locker ", type_permission = Models.user.type_permission.forms, module = 6, sub_module = 1});
            permissions.Add(new Models.user.permissions { Permission_Name = "Extended Fields", type_permission = Models.user.type_permission.forms, module = 6, sub_module = 1 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Extended Fields Details", type_permission = Models.user.type_permission.forms, module = 6, sub_module = 1 });

            roleManager.Create(new IdentityRole { Name = "payroll Periodic" }); roleManager.Create(new IdentityRole { Name = "Cost Center Cetegory" });
            roleManager.Create(new IdentityRole { Name = "Transaction journal" }); roleManager.Create(new IdentityRole { Name = "Cost Center" });
            roleManager.Create(new IdentityRole { Name = "Gl Account" }); roleManager.Create(new IdentityRole { Name = "Branches" });
            roleManager.Create(new IdentityRole { Name = "Bank" }); roleManager.Create(new IdentityRole { Name = "Cost Center" });
            roleManager.Create(new IdentityRole { Name = "Locker" }); roleManager.Create(new IdentityRole { Name = "Extended Fields" });
            roleManager.Create(new IdentityRole { Name = "Extended Fields Details" });

            permissions.Add(new Models.user.permissions { Permission_Name = "Manual Payment Settlement", type_permission = Models.user.type_permission.forms, module = 6, sub_module = 2 });
            roleManager.Create(new IdentityRole { Name = "Manual Payment Settlement" });

            permissions.Add(new Models.user.permissions { Permission_Name = "Transaction Entry", type_permission = Models.user.type_permission.forms, module = 6, sub_module = 3 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Import Transaction Entry From Execl", type_permission = Models.user.type_permission.forms, module = 6, sub_module = 3 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Batch Transactions Vertical", type_permission = Models.user.type_permission.forms, module = 6, sub_module = 3 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Batch Transactions Horizntal", type_permission = Models.user.type_permission.forms, module = 6, sub_module = 3 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Payment Settlement Transaction Entry", type_permission = Models.user.type_permission.forms, module = 6, sub_module = 3 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Payment Settlement Mass Transaction", type_permission = Models.user.type_permission.forms, module = 6, sub_module = 3 });

            roleManager.Create(new IdentityRole { Name = "Transaction Entry" }); roleManager.Create(new IdentityRole { Name = "Batch Transactions Vertical" });
            roleManager.Create(new IdentityRole { Name = "Import Transaction Entry From Execl" }); roleManager.Create(new IdentityRole { Name = "Batch Transactions Horizntal" });
            roleManager.Create(new IdentityRole { Name = "Payment Settlement Transaction Entry" }); roleManager.Create(new IdentityRole { Name = "Payment Settlement Mass Transaction" });

            permissions.Add(new Models.user.permissions { Permission_Name = "Transactions Approval", type_permission = Models.user.type_permission.forms, module = 6, sub_module = 4 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Manual Payment Settlement Action Process", type_permission = Models.user.type_permission.forms, module = 6, sub_module = 4 });

            roleManager.Create(new IdentityRole { Name = "Transactions Approval" }); roleManager.Create(new IdentityRole { Name = "Manual Payment Settlement Action Process" });

            permissions.Add(new Models.user.permissions { Permission_Name = "Ledger Transactions", type_permission = Models.user.type_permission.forms, module = 6, sub_module = 4 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Employee Annual and Special Allowance", type_permission = Models.user.type_permission.forms, module = 6, sub_module = 4 });
            permissions.Add(new Models.user.permissions { Permission_Name = "Specail allwances increase for job cadres", type_permission = Models.user.type_permission.forms, module = 6, sub_module = 4 });

            roleManager.Create(new IdentityRole { Name = "Ledger Transactions" }); roleManager.Create(new IdentityRole { Name = "Specail allwances increase for job cadres" });
            roleManager.Create(new IdentityRole { Name = "Employee Annual and Special Allowance" }); 

            //====================================================================================================================================
            dbcontext.permissions.AddRange(permissions);
            dbcontext.SaveChanges();
            //===================================================
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
