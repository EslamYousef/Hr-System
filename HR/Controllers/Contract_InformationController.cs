using HR.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HR.Models.Infra;
using HR.Models.ViewModel;
using HR.Models.Application;


namespace HR.Controllers
{
    [Authorize]
    public class Contract_InformationController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Contract_Information
        public ActionResult Create(string id)
        {
            var ID = int.Parse(id);
            var App = dbcontext.Application.FirstOrDefault(a => a.ID == ID).Applicant_ProfileId;
            var AppId = int.Parse(App);
            ViewBag.ApplicationApp = dbcontext.Applicant_Profile.FirstOrDefault(a => a.ID == AppId).Full_Name;
            ViewBag.ApplicationCode = dbcontext.Application.FirstOrDefault(a => a.ID == ID).Code;
            ViewBag.Contract_Type = dbcontext.Contract_Type.ToList().Select(m => new { Code = m.Contract_Type_Code + "------[" + m.Contract_Type_Description + ']', ID = m.ID });
            ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.idemp = id;
            DateTime statis = DateTime.Now;
            var com = dbcontext.Application.FirstOrDefault(m => m.ID == ID);
            var Contract_Information = new Contract_Information { Application = com, ID = com.ID ,Approved_Date=statis,Start_Date=statis,End_Date=statis};
            return View(Contract_Information);
        }
        [HttpPost]
        public ActionResult Create( Contract_Information model, string Command, string id2)
        {
            try
            {
                ViewBag.Contract_Type = dbcontext.Contract_Type.ToList().Select(m => new { Code = m.Contract_Type_Code + "------[" + m.Contract_Type_Description + ']', ID = m.ID });
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.idemp = id2;
                //if (ModelState.IsValid)
                //{
                    Contract_Information record = new Contract_Information();
                //record.Code = model.Code;
                record.ApplicantId = int.Parse(id2);
                record.Contract = model.Contract;
                    record.Contract_TypeId = model.Contract_TypeId;
                    record.Employment_type = model.Employment_type;
                    record.Start_Date = model.Start_Date;
                    record.End_Date = model.End_Date;
                    record.Years = model.Years;
                    record.Months = model.Months;
                    record.EmployeeProfileId = model.EmployeeProfileId;
                    record.Approved_Date = model.Approved_Date;
                    record.ApplicantNotes = model.ApplicantNotes;
                    dbcontext.Contract_Information.Add(record);
                    dbcontext.SaveChanges();
                    if (Command == "Submit")
                    {
                        return RedirectToAction("Edit", "Application", new { id = id2 });
                    }
                //}
                return RedirectToAction("Index", "Application", new { id = id2 });
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
        public ActionResult Edit(string id)
        {
            try
            {
                var ID = int.Parse(id);
                var App = dbcontext.Application.FirstOrDefault(a => a.ID == ID).Applicant_ProfileId;
                var AppId = int.Parse(App);
                ViewBag.ApplicationApp = dbcontext.Applicant_Profile.FirstOrDefault(a => a.ID == AppId).Full_Name;
                ViewBag.ApplicationCode = dbcontext.Application.FirstOrDefault(a => a.ID == ID).Code;
                ViewBag.Contract_Type = dbcontext.Contract_Type.ToList().Select(m => new { Code = m.Contract_Type_Code + "------[" + m.Contract_Type_Description + ']', ID = m.ID });
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });

                var record = dbcontext.Contract_Information.FirstOrDefault(m => m.ApplicantId == ID);

                if (record != null)
                {
                    ViewBag.idemp = ID;
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
        public ActionResult Edit( Contract_Information model, string Command, string id2)
        {
            try
            {
                ViewBag.Contract_Type = dbcontext.Contract_Type.ToList().Select(m => new { Code = m.Contract_Type_Code + "------[" + m.Contract_Type_Description + ']', ID = m.ID });
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.idemp = id2;
                var ID = int.Parse(id2);
                var record = dbcontext.Contract_Information.FirstOrDefault(m => m.ApplicantId == ID);

                record.ApplicantId = int.Parse(id2);
                record.Contract = model.Contract;
                record.Contract_TypeId = model.Contract_TypeId;
                record.Employment_type = model.Employment_type;
                record.Start_Date = model.Start_Date;
                record.End_Date = model.End_Date;
                record.Years = model.Years;
                record.Months = model.Months;
                record.EmployeeProfileId = model.EmployeeProfileId;
                record.Approved_Date = model.Approved_Date;
                record.ApplicantNotes = model.ApplicantNotes;
                dbcontext.SaveChanges();
                if (Command == "Submit")
                {
                    return RedirectToAction("Edit", "Application", new { id = id2 });
                }
                //}
                return RedirectToAction("Index", "Application", new { id = id2 });
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
    }
}