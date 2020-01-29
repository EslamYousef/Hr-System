using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HR.Models;
using System.Data.Entity.Infrastructure;
using HR.Models.Infra;
using HR.Models.ViewModel;

namespace HR.Controllers
{
    public class Employee_family_profileController : Controller
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Employee_family_profile
        public ActionResult Index()
        {
            var employee = dbcontext.Employee_Profile.ToList();
            var family = dbcontext.Employee_family_profile.ToList();
            var model = from a in employee
                        join b in family on a.Employee_family_profile.ID equals b.ID
                        select new Employeefamily_VM
                        {
                            fullname = a.Full_Name,
                            code = a.Code,
                            EmployeeId = a.ID,
                            Employee_family_profile = b
                        };
            return View(model);
        }
    }
}