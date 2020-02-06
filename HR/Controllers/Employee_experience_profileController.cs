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
    [Authorize]
    public class Employee_experience_profileController : Controller
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Employee_experience_profile
        public ActionResult Index()
        {
            var employee = dbcontext.Employee_Profile.ToList();
            var empexperience = dbcontext.Employee_experience_profile.ToList();
            var model = from a in employee
                        join b in empexperience on a.Employee_experience_profile.ID equals b.ID
                        select new Employee_Experience_VM
                        {
                            fullname = a.Full_Name,
                            code = a.Code,
                            EmployeeId = a.ID,
                            Employee_experience_profile = b
                        };
            return View(model);
        }
        public ActionResult Create(string id)
        {

            ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.External_compaines = dbcontext.External_compaines.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.Rejection_Reasons = dbcontext.Rejection_Reasons.Where(m=>m.purpose==reject_purpose.Job_experience).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel);
            var model = dbcontext.Employee_experience_profile.ToList();
            var count = 0;
            if (model.Count() == 0)
            {
                count = 1;
            }
            else
            {
                var te = model.LastOrDefault().ID;
                count = te + 1;
            }
            if (id != null)
            {
                var ID = int.Parse(id);
                var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == ID);
                var x = emp.Employee_experience_profile;
                return View(x);
            }

            var EmployeeExperience = new Employee_experience_profile();
            return View(EmployeeExperience);

        }
        [HttpPost]
        public ActionResult Create(Employee_experience_profile model, string command)
        {
            try
            {

                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.External_compaines = dbcontext.External_compaines.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Rejection_Reasons = dbcontext.Rejection_Reasons.Where(m => m.purpose == reject_purpose.Job_experience).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                if (ModelState.IsValid)
                {
                    if (model.External_compainesId == "0" || model.External_compainesId == null)
                    {
                        ModelState.AddModelError("", "Company Code must enter");
                        return View(model);
                    }
                    if (model.Rejection_ReasonsId == "0" || model.Rejection_ReasonsId == null)
                    {
                        ModelState.AddModelError("", "Reason of leave Code must enter");
                        return View(model);
                    }
                    var experience = int.Parse(model.Employee_ProfileId);
                    var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == experience);
                    var record = dbcontext.Employee_experience_profile.FirstOrDefault(m => m.ID == emp.Employee_experience_profile.ID);

                    record.External_compainesId = model.External_compainesId;
                    record.Rejection_ReasonsId = model.Rejection_ReasonsId;
                    record.Company_type = model.Company_type;
                    record.Job_title = model.Job_title;
                    record.Start_date = model.Start_date;
                    record.End_date = model.End_date;
                    record.Years = model.Years;
                    record.Months = model.Months;
                    record.Days = model.Days;
                    record.Total_salary = model.Total_salary;
                    record.Employee_ProfileId = model.Employee_ProfileId;
                    record.Added_days = model.Added_days;
                    record.Added_months = model.Added_months;
                    record.Added_years = model.Added_years;
                    record.Approval_date = model.Approval_date;
                    record.Considered_period = model.Considered_period;
                   

                    dbcontext.SaveChanges();
                    if (command == "Submit")
                    {
                        return RedirectToAction("edit", "Employee_Profile", new { id = int.Parse(record.Employee_ProfileId) });
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(model);
                }
            }
            catch (DbUpdateException e)
            {
                TempData["Message"] = "this code Is already exists";
                return View(model);
            }
            catch (Exception e)
            {
                return View(model);
            }

        }
        public ActionResult Edit(int id)
        {
            try
            {
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.External_compaines = dbcontext.External_compaines.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Rejection_Reasons = dbcontext.Rejection_Reasons.Where(m => m.purpose == reject_purpose.Job_experience).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });

                var record = dbcontext.Employee_experience_profile.FirstOrDefault(m => m.ID == id);
                if (record != null)
                {
                    return View(record);
                }
                else
                {
                    TempData["Message"] = "There is no Employee Experience Profile";
                    return View();
                }
            }
            catch
            (Exception e)
            { return View(); }
        }
        [HttpPost]
        public ActionResult Edit(Employee_experience_profile model, string command)
        {
            try
            {
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.External_compaines = dbcontext.External_compaines.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Rejection_Reasons = dbcontext.Rejection_Reasons.Where(m => m.purpose == reject_purpose.Job_experience).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                if (model.External_compainesId == "0" || model.External_compainesId == null)
                {
                    ModelState.AddModelError("", "Company Code must enter");
                    return View(model);
                }
                if (model.Rejection_ReasonsId == "0" || model.Rejection_ReasonsId == null)
                {
                    ModelState.AddModelError("", "Reason of leave Code must enter");
                    return View(model);
                }
                var record = dbcontext.Employee_experience_profile.FirstOrDefault(m => m.ID == model.ID);
                record.Code = model.Code;
                record.Rejection_ReasonsId = model.Rejection_ReasonsId;
                record.External_compainesId = model.External_compainesId;
                record.Company_type = model.Company_type;
                record.Job_title = model.Job_title;
                record.Start_date = model.Start_date;
                record.End_date = model.End_date;
                record.Years = model.Years;
                record.Months = model.Months;
                record.Days = model.Days;
                record.Total_salary = model.Total_salary;
                record.Employee_ProfileId = model.Employee_ProfileId;
                record.Added_days = model.Added_days;
                record.Added_months = model.Added_months;
                record.Added_years = model.Added_years;
                record.Approval_date = model.Approval_date;
                record.Considered_period = model.Considered_period;

                dbcontext.SaveChanges();

                if (command == "Submit")
                {
                    return RedirectToAction("edit", "Employee_Profile", new { id = int.Parse(record.Employee_ProfileId) });
                }
                return RedirectToAction("index");
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = "This code Is already exists";
                return View(model);
            }
            catch (Exception e)
            { return View(model); }
        }
    }
}