using HR.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
 
[assembly: OwinStartupAttribute(typeof(HR.Startup))]
namespace HR
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);
            CreateRolesandUsers();
        }
        private static void CreateRolesandUsers()
        {
                var context = new ApplicationDbContext();
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
               var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // In Startup iam creating first Admin Role and creating a default Admin User
            
                // first we create Admin rool
                //Here we create a Admin super user who will maintain the website
                var user = new ApplicationUser
                {
                    UserName = "S_admin",
                    Email = "S_admin@admin.com",
                    employee_name="no empoyee",
                    active=true
                };
                var userPWD = "Admin@123";
                var chkUser = userManager.Create(user, userPWD);
                var admin = userManager.FindByEmail(user.Email);
                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(admin.Id, "Admin");
                }
                
                
            
        }
    }
}
