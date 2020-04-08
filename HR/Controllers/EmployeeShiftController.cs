using HR.Models;
using HR.Models.Time_management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers
{
    public class EmployeeShiftController : Controller
    {
        // GET: EmployeeShift
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        public ActionResult Index()
        {
            try
            {
                var empl = dbcontext.Employee_Shift_schedule.ToList();
                return View(empl);
            }
            catch(Exception)
            {
                return View();
            }
        }
        public ActionResult Create()
        {
            try
            {
                ViewBag.emp = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.shift = dbcontext.Shift_setup.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });

                ViewBag.daystate = dbcontext.Shiftdaystatus.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });

                var model = new Employee_Shift_schedule();
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }

        public JsonResult getcode(int id)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            var num_of_sch = dbcontext.Employee_Shift_schedule.Where(m => m.Employee_ProfileID == id).ToList();
            var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == id);
            var code = "";
            if(num_of_sch.Count==0)
            {
                code = emp.Code + "_SCH_1";
            }
            else
            {
                code = emp.Code + "_SCH_"+ (num_of_sch.Count+1)+ToString();
            }
            return Json(code);
        }
    }
}