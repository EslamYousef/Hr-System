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
    public class Employee_experience_profileController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Employee_experience_profile
        public ActionResult Index(string id)
        {
            var ID = int.Parse(id);
            var new_model = dbcontext.Employee_experience_profile.Where(m => m.Employee_Profile.ID == ID).ToList();
            ViewBag.idemp = id;
            return View(new_model);
        }
        public ActionResult Create(string id)
        {

            ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.External_compaines = dbcontext.External_compaines.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.Rejection_Reasons = dbcontext.Rejection_Reasons.Where(m=>m.purpose==reject_purpose.Job_experience).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.idemp = id;
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
            DateTime statis = Convert.ToDateTime("1/1/1900");
            var ID = int.Parse(id);
                var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == ID);
             
              
          

            var EmployeeExperience = new Employee_experience_profile { Employee_Profile=emp,Employee_ProfileId=emp.ID.ToString(),Code=stru.Structure_Code +count.ToString(), Approval_date = statis, Start_date = statis, End_date = statis, Rejection_ReasonsId = "0", External_compainesId = "0" };
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
                //if (ModelState.IsValid)
                //{
                var emp = int.Parse(model.Employee_ProfileId);
                var EmpObj = dbcontext.Employee_Profile.FirstOrDefault(a => a.ID == emp);

                Employee_experience_profile record = new Employee_experience_profile();
                var empid = EmpObj.Code + "------" + EmpObj.Name;
                record.Employee_ProfileId = model.Employee_ProfileId == null ? model.Employee_ProfileId = EmpObj.ID.ToString() : model.Employee_ProfileId;
                ViewBag.idemp = model.Employee_ProfileId;
                record.Employee_Profile = EmpObj;
                if (model.External_compainesId == "0" || model.External_compainesId == null)
                    {
                        ModelState.AddModelError("", HR.Resource.Personnel.CompanyCodemustenter);
                        return View(model);
                    }
                    if (model.Rejection_ReasonsId == "0" || model.Rejection_ReasonsId == null)
                    {
                        ModelState.AddModelError("", HR.Resource.Personnel.ReasonofleaveCodemustenter);
                        return View(model);
                    }
            
                //var experience = int.Parse(model.Employee_ProfileId);
                //var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == experience);
                    record.Code = model.Code;
                    record.External_compainesId = model.External_compainesId;
                    record.Rejection_ReasonsId = model.Rejection_ReasonsId;
                
                
                    record.Company_type = model.Company_type;
                    record.Job_title = model.Job_title;
                    record.Start_date = model.Start_date;
                    record.End_date = model.End_date;
                    if (model.Start_date > model.End_date)
                    {
                        TempData["Message"] = HR.Resource.Personnel.FromdatebiggerTodate;
                        return View(model);
                    }
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

                dbcontext.Employee_experience_profile.Add(record);
                    dbcontext.SaveChanges();
                    if (command == "Submit")
                    {
                        return RedirectToAction("edit", "Employee_Profile", new { id = EmpObj.ID });
                    }
                    return RedirectToAction("Index", new { id = EmpObj.ID });
                //}
                //else
                //{
                    return View(model);
                //}
            }
            catch (DbUpdateException e)
            {
                TempData["Message"] = HR.Resource.Basic.thiscodeIsalreadyexists;
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
                ViewBag.idemp = record.Employee_Profile.ID.ToString();

                if (record != null)
                {
                    return View(record);
                }
                else
                {
                    TempData["Message"] = HR.Resource.Basic.thereisnodata;
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
                var record = dbcontext.Employee_experience_profile.FirstOrDefault(a => a.ID == model.ID);
                //if (model.External_compainesId == "0" || model.External_compainesId == null)
                //{
                //    ModelState.AddModelError("", "Company Code must enter");
                //    return View(model);
                //}
                //if (model.Rejection_ReasonsId == "0" || model.Rejection_ReasonsId == null)
                //{
                //    ModelState.AddModelError("", "Reason of leave Code must enter");
                //    return View(model);
                //}
                var EmpObj = dbcontext.Employee_Profile.FirstOrDefault(a => a.ID == model.Employee_Profile.ID);
                var empid = EmpObj.Code + "------" + EmpObj.Name;
                record.Employee_ProfileId = model.Employee_ProfileId == null ? model.Employee_ProfileId = EmpObj.ID.ToString() : model.Employee_ProfileId;
                ViewBag.idemp = model.Employee_ProfileId;
                record.Employee_Profile = EmpObj;

                record.Code = model.Code;
                record.Rejection_ReasonsId = model.Rejection_ReasonsId;
                if (model.Rejection_ReasonsId == null)
                {
                    record.Rejection_Reasons = null;
                }
                else
                {
                    var Rejection_ReasonsId = int.Parse(model.Rejection_ReasonsId);
                    record.Rejection_Reasons = dbcontext.Rejection_Reasons.FirstOrDefault(m => m.ID == Rejection_ReasonsId);
                }
                record.External_compainesId = model.External_compainesId;
                if (model.External_compainesId == null)
                {
                    record.External_compaines = null;
                }
                else
                {
                    var External_compainesId = int.Parse(model.External_compainesId);
                    record.External_compaines = dbcontext.External_compaines.FirstOrDefault(m => m.ID == External_compainesId);
                }

                record.Code = model.Code;
                record.Company_type = model.Company_type;
                record.Job_title = model.Job_title;
                record.Start_date = model.Start_date;
                record.End_date = model.End_date;
                if (model.Start_date > model.End_date)
                {
                    TempData["Message"] = HR.Resource.Personnel.FromdatebiggerTodate;
                    return View(model);
                }
           
                record.Years = model.Years;
                record.Months = model.Months;
                record.Days = model.Days;
                record.Total_salary = model.Total_salary;
        
                record.Added_days = model.Added_days;
                record.Added_months = model.Added_months;
                record.Added_years = model.Added_years;
                record.Approval_date = model.Approval_date;
                record.Considered_period = model.Considered_period;

                dbcontext.SaveChanges();

                if (command == "Submit")
                {
                    return RedirectToAction("edit", "Employee_Profile", new { id = EmpObj.ID });
                }
                return RedirectToAction("index", new { id = EmpObj.ID });
            }
            catch (DbUpdateException e)
            {
                TempData["Message"] = HR.Resource.Basic.thiscodeIsalreadyexists;
                return View(model);
            }
            catch (Exception e)
            { return View(model); }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                var record = dbcontext.Employee_experience_profile.FirstOrDefault(m => m.ID == id);
                ViewBag.idemp = record.Employee_Profile.ID.ToString();

                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = HR.Resource.Basic.thereisnodata;
                    return View();
                }

            }
            catch (Exception e)
            {
                return View();
            }

        }
        [ActionName("Delete")]
        [HttpPost]
        public ActionResult Deletemethod(int id)
        {
            var record = dbcontext.Employee_experience_profile.FirstOrDefault(m => m.ID == id);
            ViewBag.idemp = record.Employee_Profile.ID.ToString();

            try
            {
                dbcontext.Employee_experience_profile.Remove(record);
                dbcontext.SaveChanges();
                return RedirectToAction("index", new { id = record.Employee_ProfileId });
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = HR.Resource.Basic.youcannotdeletethisRow;
                return View(record);
            }
            catch (Exception e)
            {
                return View();
            }
        }
    }
}