using HR.Controllers;
using HR.Models;
using HR.Models.NOtification;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;

[assembly: OwinStartupAttribute(typeof(HR.Startup))]
namespace HR
{
    public partial class Startup
    {
        string con2 ="";

        public void Configuration(IAppBuilder app)
        {
            //Enter_dbController f = new Enter_dbController();
            //f.check();

            //==================================================

            //object[] array1 = new object[4] { db_.server_name, db_.data_base, db_.user_id, db_.pass_ };
            //con2 = new SqlConnection(string.Format(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, array1)).ConnectionString;
            //db_.con = con2;
            ////==================================================
            db_.fun();

            
            ConfigureAuth(app);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);
            CreateRolesandUsers();
            
            //SqlDependency.Start(db_.con);
        

        }
        private static void CreateRolesandUsers()
        {
                var context = new ApplicationDbContext();
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
               var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // In Startup iam creating first Admin Role and creating a default Admin User

            // first we create Admin rool
            //Here we create a Admin super user who will maintain the website

           
        }
        //protected void Session_Start(object sender, EventArgs e)
        //{
        //    NotificationComponent NC = new NotificationComponent();
        //    var currentTime = DateTime.Now;
        //    HttpContext.Current.Session["LastUpdated"] = currentTime;
        //    NC.RegisterNotification(currentTime);
        //}
        protected void Application_End()
        {
            //here we will stop Sql Dependency
            SqlDependency.Stop(db_.con);
        }
    }
}
