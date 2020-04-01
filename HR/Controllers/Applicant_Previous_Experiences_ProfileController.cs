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
    public class Applicant_Previous_Experiences_ProfileController : Controller
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Applicant_Previous_Experiences_Profile
        public ActionResult Index(string id)
        {
            var ID = int.Parse(id);
            var new_model = dbcontext.Applicant_Previous_Experiences_Profile.Where(m => m.Applicant_Profile.ID == ID).ToList();
            ViewBag.idemp = id;
            return View(new_model);
        }
        public ActionResult Create(string id)
        {

            ViewBag.Employee_Profile = dbcontext.Applicant_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.External_compaines = dbcontext.External_compaines.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.Rejection_Reasons = dbcontext.Rejection_Reasons.Where(m => m.purpose == reject_purpose.Job_experience).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.idemp = id;
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Recuirtment);
            var model = dbcontext.Applicant_Previous_Experiences_Profile.ToList();
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
            var emp = dbcontext.Applicant_Profile.FirstOrDefault(m => m.ID == ID);

            var EmployeeExperience = new Applicant_Previous_Experiences_Profile { Applicant_Profile = emp, Employee_ProfileId = emp.ID.ToString(), Code = stru.Structure_Code + count.ToString(), Approval_date = statis, Start_date = statis, End_date = statis/*, Rejection_ReasonsId = 0, External_compainesId = 0 */};
            return View(EmployeeExperience);

        }
        [HttpPost]
        public ActionResult Create(Applicant_Previous_Experiences_Profile model, string command)
        {
            try
            {

                ViewBag.Employee_Profile = dbcontext.Applicant_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.External_compaines = dbcontext.External_compaines.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Rejection_Reasons = dbcontext.Rejection_Reasons.Where(m => m.purpose == reject_purpose.Job_experience).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                //if (ModelState.IsValid)
                //{
                var emp = int.Parse(model.Employee_ProfileId);
                var EmpObj = dbcontext.Applicant_Profile.FirstOrDefault(a => a.ID == emp);

                Applicant_Previous_Experiences_Profile record = new Applicant_Previous_Experiences_Profile();
                var empid = EmpObj.Code + "------" + EmpObj.Name;
                record.Employee_ProfileId = model.Employee_ProfileId == null ? model.Employee_ProfileId = EmpObj.ID.ToString() : model.Employee_ProfileId;
                ViewBag.idemp = model.Employee_ProfileId;
                record.Applicant_Profile = EmpObj;
                //if (model.External_compainesId == 0 || model.External_compainesId == null)
                //{
                //    ModelState.AddModelError("", HR.Resource.Personnel.CompanyCodemustenter);
                //    return View(model);
                //}
                //if (model.Rejection_ReasonsId == 0 || model.Rejection_ReasonsId == null)
                //{
                //    ModelState.AddModelError("", HR.Resource.Personnel.ReasonofleaveCodemustenter);
                //    return View(model);
                //}
                if (model.External_compainesId == 0) { model.External_compainesId = null; }
                if (model.Rejection_ReasonsId == 0) { model.Rejection_ReasonsId = null; }
               
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

                dbcontext.Applicant_Previous_Experiences_Profile.Add(record);
                dbcontext.SaveChanges();
                if (command == "Submit")
                {
                    return RedirectToAction("edit", "Applicant_Profile", new { id = EmpObj.ID });
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
                ViewBag.Employee_Profile = dbcontext.Applicant_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.External_compaines = dbcontext.External_compaines.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Rejection_Reasons = dbcontext.Rejection_Reasons.Where(m => m.purpose == reject_purpose.Job_experience).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                var record = dbcontext.Applicant_Previous_Experiences_Profile.FirstOrDefault(m => m.ID == id);
                ViewBag.idemp = record.Applicant_Profile.ID.ToString();

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
        public ActionResult Edit(Applicant_Previous_Experiences_Profile model, string command)
        {
            try
            {
                ViewBag.Employee_Profile = dbcontext.Applicant_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.External_compaines = dbcontext.External_compaines.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Rejection_Reasons = dbcontext.Rejection_Reasons.Where(m => m.purpose == reject_purpose.Job_experience).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                var record = dbcontext.Applicant_Previous_Experiences_Profile.FirstOrDefault(a => a.ID == model.ID);
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
                var EmpObj = dbcontext.Applicant_Profile.FirstOrDefault(a => a.ID == model.Applicant_Profile.ID);
                var empid = EmpObj.Code + "------" + EmpObj.Name;
                record.Employee_ProfileId = model.Employee_ProfileId == null ? model.Employee_ProfileId = EmpObj.ID.ToString() : model.Employee_ProfileId;
                ViewBag.idemp = model.Employee_ProfileId;
                record.Applicant_Profile = EmpObj;

                record.Code = model.Code;
                record.Rejection_ReasonsId = model.Rejection_ReasonsId;
                
                record.External_compainesId = model.External_compainesId;
               

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
                    return RedirectToAction("edit", "Applicant_Profile", new { id = EmpObj.ID });
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
                var record = dbcontext.Applicant_Previous_Experiences_Profile.FirstOrDefault(m => m.ID == id);
                ViewBag.idemp = record.Applicant_Profile.ID.ToString();

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
            var record = dbcontext.Applicant_Previous_Experiences_Profile.FirstOrDefault(m => m.ID == id);
            ViewBag.idemp = record.Applicant_Profile.ID.ToString();

            try
            {
                dbcontext.Applicant_Previous_Experiences_Profile.Remove(record);
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