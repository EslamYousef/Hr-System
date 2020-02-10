using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HR.Models.Infra;
using HR.Models.ViewModel;
using HR.Models;
using System.Data.Entity.Infrastructure;

namespace HR.Controllers
{
    [Authorize]
    public class Employee_military_service_profileController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Employee_military_service_profile
        public ActionResult Index()
        {
            var military = dbcontext.Employee_military_service_profile.ToList();
            var employeeprofile = dbcontext.Employee_Profile.Where(a=>a.Gender==Gender.male).Select(a=>a).ToList();
            var model = from a in employeeprofile
                        join b in military on a.Employee_military_service_profile.ID equals b.ID
                        select new EmployeeMilitary_VM
                        {
                            fullname = a.Full_Name,
                            code = a.Code,
                            EmployeeId = a.ID,
                            Employee_military_service_profile = b
                        };
            return View(model);

        }
        public ActionResult Create(string id)
        {

            ViewBag.Military_Service_Rank = dbcontext.Military_Service_Rank.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.Rejection_Reasons = dbcontext.Rejection_Reasons.Where(a => a.purpose == reject_purpose.Military_service).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });

            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel);
            var model = dbcontext.Employee_military_service_profile.ToList();
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
                var x = emp.Employee_military_service_profile;
                return View(x);
            }

            var EmployeeMilitary = new Employee_military_service_profile();
            return View(EmployeeMilitary);

        }
        [HttpPost]
        public ActionResult Create(Employee_military_service_profile model, string command)
        {
            try
            {
                if (model.Military_Service_RankId == null) { model.Military_Service_RankId = "0"; }
                if (model.Rejection_ReasonsId == null) { model.Rejection_ReasonsId = "0"; }

                ViewBag.Military_Service_Rank = dbcontext.Military_Service_Rank.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Rejection_Reasons = dbcontext.Rejection_Reasons.Where(a=>a.purpose==reject_purpose.Military_service).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
             


                if (ModelState.IsValid)
                {
                    var prof = int.Parse(model.Employee_ProfileId);
                    var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == prof);
                 //   var EmpObj = dbcontext.Employee_Profile.FirstOrDefault(a => a.ID == model.ID);

                    var record = dbcontext.Employee_military_service_profile.FirstOrDefault(m => m.ID == emp.Employee_military_service_profile.ID);

                    record.Service_at_hire = model.Service_at_hire;
                    record.Trio_number = model.Trio_number;
                    record.Branch = model.Branch;
                    record.Level = model.Level;
                    record.Military_service_status = model.Military_service_status;
                    record.Military_Service_RankId = model.Military_Service_RankId;
                    record.Certificate_date = model.Certificate_date;
                    record.From_date = model.From_date;
                    record.To_date = model.To_date;
                    if (model.From_date > model.To_date)
                    {
                        TempData["Message"] = "From date bigger To date";
                        return View(model);
                    }
                    record.Batch_reference_No = model.Batch_reference_No;
                    record.Id_number = model.Id_number;                 
                    record.Rejection_ReasonsId = model.Rejection_ReasonsId;
                    record.Service_period_MM = model.Service_period_MM;
                    record.Service_period_YY = model.Service_period_YY;
                    record.Service_period_The_number_of_days = model.Service_period_The_number_of_days;
                    record.Added_Service_period_YY = model.Added_Service_period_YY;
                    record.Added_Service_period_MM = model.Added_Service_period_MM;
                    record.Added_Service_period_The_number_of_days = model.Added_Service_period_The_number_of_days;
                    record.Total_Service_period_YY = model.Total_Service_period_YY;
                    record.Total_Service_period_MM = model.Total_Service_period_MM;
                    record.Total_Service_period_The_number_of_days = model.Total_Service_period_The_number_of_days;
                    record.Comments = model.Comments;

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
              

                ViewBag.Military_Service_Rank = dbcontext.Military_Service_Rank.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Rejection_Reasons = dbcontext.Rejection_Reasons.Where(a => a.purpose == reject_purpose.Military_service).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                var record = dbcontext.Employee_military_service_profile.FirstOrDefault(m => m.ID == id);
                if (record != null)
                {
                    return View(record);
                }
                else
                {
                    TempData["Message"] = "There is no Employee Military Service Profile";
                    return View();
                }
            }
            catch
            (Exception e)
            { return View(); }
        }
        [HttpPost]
        public ActionResult Edit(Employee_military_service_profile model, string command)
        {
            try
            {
                if (model.Military_Service_RankId == null) { model.Military_Service_RankId = "0"; }
                if (model.Rejection_ReasonsId == null) { model.Rejection_ReasonsId = "0"; }

                ViewBag.Military_Service_Rank = dbcontext.Military_Service_Rank.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Rejection_Reasons = dbcontext.Rejection_Reasons.Where(a => a.purpose == reject_purpose.Military_service).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
             
              var EmpObj = dbcontext.Employee_Profile.FirstOrDefault(a => a.ID == model.ID);

                var record = dbcontext.Employee_military_service_profile.FirstOrDefault(m => m.ID == model.ID);
               var empid = EmpObj.Code + "------" + EmpObj.Name;
               var empl= record.Employee_ProfileId = model.Employee_ProfileId == null ? model.Employee_ProfileId = EmpObj.ID.ToString() : model.Employee_ProfileId;
         //       record.Employee_ProfileId = empl;
    //            var trt = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == int.Parse(empl));
                record.Code = model.Code;

                record.Service_at_hire = model.Service_at_hire;
                record.Trio_number = model.Trio_number;
                record.Branch = model.Branch;
                record.Level = model.Level;
                record.Military_service_status = model.Military_service_status;
                record.Military_Service_RankId = model.Military_Service_RankId;
                record.Certificate_date = model.Certificate_date;
                record.From_date = model.From_date;
                record.To_date = model.To_date;
                if (model.From_date > model.To_date)
                {
                    TempData["Message"] = "From date bigger To date";
                    return View(model);
                }
                record.Batch_reference_No = model.Batch_reference_No;
                record.Id_number = model.Id_number;
                record.Rejection_ReasonsId = model.Rejection_ReasonsId;
                record.Service_period_MM = model.Service_period_MM;
                record.Service_period_YY = model.Service_period_YY;
                record.Service_period_The_number_of_days = model.Service_period_The_number_of_days;
                record.Added_Service_period_YY = model.Added_Service_period_YY;
                record.Added_Service_period_MM = model.Added_Service_period_MM;
                record.Added_Service_period_The_number_of_days = model.Added_Service_period_The_number_of_days;
                record.Total_Service_period_YY = model.Total_Service_period_YY;
                record.Total_Service_period_MM = model.Total_Service_period_MM;
                record.Total_Service_period_The_number_of_days = model.Total_Service_period_The_number_of_days;
                record.Comments = model.Comments;

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
        public ActionResult Delete(int id)
        {
            try
            {
                var record = dbcontext.Employee_military_service_profile.FirstOrDefault(m => m.ID == id);
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = "There is no Military Service Profile";
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
            var record = dbcontext.Employee_military_service_profile.FirstOrDefault(m => m.ID == id);

            try
            {
                dbcontext.Employee_military_service_profile.Remove(record);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = "You can't delete beacause it have child";
                return View(record);
            }
            catch (Exception e)
            {
                return View();
            }
        }
    }
}