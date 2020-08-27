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
    [Authorize(Roles = "Admin,Recuirtment,RecuirtmentCards,Applicant Profile")]
    public class Applicant_Military_Service_ProfileController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Applicant_Military_Service_Profile
        public ActionResult Index(string id)
        {
            var ID = int.Parse(id);
            var new_model = dbcontext.Applicant_Military_Service_Profile.Where(m => m.Applicant_Profile.ID == ID).ToList();
            ViewBag.idemp = id;
            return View(new_model);
        }
        public ActionResult Create(string id)
        {

            ViewBag.Military_Service_Rank = dbcontext.Military_Service_Rank.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.Rejection_Reasons = dbcontext.Rejection_Reasons.Where(a => a.purpose == reject_purpose.Military_service).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.Employee_Profile = dbcontext.Applicant_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.idemp = id;

            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Recuirtment);
            var model = dbcontext.Applicant_Military_Service_Profile.ToList();
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

            var EmployeeMilitary = new Applicant_Military_Service_Profile { Applicant_Profile = emp, Employee_ProfileId = emp.ID.ToString(), Code = stru.Structure_Code + count.ToString(), Certificate_date = statis, From_date = statis, To_date = statis };
            return View(EmployeeMilitary);

        }
        [HttpPost]
        public ActionResult Create(Applicant_Military_Service_Profile model, string command)
        {
            try
            {
                if (model.Military_Service_RankId == 0) { model.Military_Service_RankId = null; }
                if (model.Rejection_ReasonsId == 0) { model.Rejection_ReasonsId = null; }

                ViewBag.Military_Service_Rank = dbcontext.Military_Service_Rank.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Rejection_Reasons = dbcontext.Rejection_Reasons.Where(a => a.purpose == reject_purpose.Military_service).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Employee_Profile = dbcontext.Applicant_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });



                //if (ModelState.IsValid)
                //{
                var emp = int.Parse(model.Employee_ProfileId);
                var EmpObj = dbcontext.Applicant_Profile.FirstOrDefault(a => a.ID == emp);

                //var record = dbcontext.Employee_military_service_profile.FirstOrDefault(m => m.ID == emp.Employee_military_service_profile.ID);
                Applicant_Military_Service_Profile record = new Applicant_Military_Service_Profile();
                var empid = EmpObj.Code + "------" + EmpObj.Name;
                record.Employee_ProfileId = model.Employee_ProfileId == null ? model.Employee_ProfileId = EmpObj.ID.ToString() : model.Employee_ProfileId;
                ViewBag.idemp = model.Employee_ProfileId;
                record.Applicant_Profile = EmpObj;

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
                    TempData["Message"] = HR.Resource.Personnel.FromdatebiggerTodate;
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
                dbcontext.Applicant_Military_Service_Profile.Add(record);
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


                ViewBag.Military_Service_Rank = dbcontext.Military_Service_Rank.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Rejection_Reasons = dbcontext.Rejection_Reasons.Where(a => a.purpose == reject_purpose.Military_service).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Employee_Profile = dbcontext.Applicant_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                var record = dbcontext.Applicant_Military_Service_Profile.FirstOrDefault(m => m.ID == id);
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
        public ActionResult Edit(Applicant_Military_Service_Profile model, string command)
        {
            try
            {
                if (model.Military_Service_RankId == 0) { model.Military_Service_RankId = null; }
                if (model.Rejection_ReasonsId == 0) { model.Rejection_ReasonsId = null; }

                ViewBag.Military_Service_Rank = dbcontext.Military_Service_Rank.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Rejection_Reasons = dbcontext.Rejection_Reasons.Where(a => a.purpose == reject_purpose.Military_service).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Employee_Profile = dbcontext.Applicant_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });

                var record = dbcontext.Applicant_Military_Service_Profile.FirstOrDefault(a => a.ID == model.ID);

                var EmpObj = dbcontext.Applicant_Profile.FirstOrDefault(a => a.ID == model.Applicant_Profile.ID);
                var empid = EmpObj.Code + "------" + EmpObj.Name;
                record.Employee_ProfileId = model.Employee_ProfileId == null ? model.Employee_ProfileId = EmpObj.ID.ToString() : model.Employee_ProfileId;
                ViewBag.idemp = model.Employee_ProfileId;
                record.Applicant_Profile = EmpObj;

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
                    TempData["Message"] = HR.Resource.Personnel.FromdatebiggerTodate;
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
                    return RedirectToAction("edit", "Applicant_Profile", new { id = EmpObj.ID });
                }
                return RedirectToAction("index", new { id = EmpObj.ID });
            }
            catch (DbUpdateException)
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
                var record = dbcontext.Applicant_Military_Service_Profile.FirstOrDefault(m => m.ID == id);
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
            var record = dbcontext.Applicant_Military_Service_Profile.FirstOrDefault(m => m.ID == id);
            ViewBag.idemp = record.Applicant_Profile.ID.ToString();

            try
            {
                dbcontext.Applicant_Military_Service_Profile.Remove(record);
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