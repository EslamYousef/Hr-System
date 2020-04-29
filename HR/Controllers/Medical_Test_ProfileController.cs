using HR.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HR.Models.Infra;
using HR.Models.Application;

namespace HR.Controllers
{
    [Authorize]
    public class Medical_Test_ProfileController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Medical_Test_Profile
        public ActionResult Create(string id)
        {
            var ID = int.Parse(id);
            var App = dbcontext.Application.FirstOrDefault(a => a.ID == ID).Applicant_ProfileId;
            var AppId = int.Parse(App);
            ViewBag.ApplicationApp = dbcontext.Applicant_Profile.FirstOrDefault(a => a.ID == AppId).Full_Name;
            ViewBag.ApplicationCode = dbcontext.Application.FirstOrDefault(a => a.ID == ID).Code;
            ViewBag.idemp = id;

            //var ID = int.Parse(id);
            var com = dbcontext.Application.FirstOrDefault(m => m.ID == ID);
            var Medical_Test_Profile = new Medical_Test_Profile { Application = com, ID = com.ID };
            return View(Medical_Test_Profile);

        }
        [HttpPost]
        public ActionResult Create(FormCollection form, Medical_Test_Profile model, string command, string id2)
        {
            try
            {

                ViewBag.idemp = id2;

                var SubjectCode = form["SubjectCode"].Split(char.Parse(","));
                var StartDate = form["StartDate"].Split(char.Parse(","));
                var MedicalEntity = form["MedicalEntity"].Split(char.Parse(","));
                var TestResult = form["TestResult"].Split(char.Parse(","));
                var NotFitReason = form["NotFitReason"].Split(char.Parse(","));
                var Comments = form["Comments"].Split(char.Parse(","));
                var FinalTestResult = form["FinalTestResult"].Split(char.Parse(","));

                var items = new List<Medical_Test_Profile>();
                for (var i = 0; i < SubjectCode.Count(); i++)
                {
                    if (SubjectCode[i] != "" && StartDate[i] != "")
                    {
                        items.Add(new Medical_Test_Profile { ApplicantId = int.Parse(id2), TryNumber = SubjectCode[i], Medical_Entity = MedicalEntity[i], Test_Date = DateTime.Parse(StartDate[i]), Test_Result = TestResult[i], Not_Fit_Reason = NotFitReason[i], Comments = Comments[i], Final_Test_Result = FinalTestResult[i] });
                    }
                }
                if (items.Count() > 0)
                {
                    var add_items = dbcontext.Medical_Test_Profile.AddRange(items);
                    dbcontext.SaveChanges();
                    /////////////////////////////////////
                    if (command == "Submit")
                    {
                        return RedirectToAction("Edit", "Application", new { id = id2 });
                    }
                }
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

                var record = dbcontext.Medical_Test_Profile.Where(m => m.ApplicantId == ID).ToList();
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
        public ActionResult Edit(FormCollection form, Medical_Test_Profile model, string command, string id2)
        {
            try
            {
                ViewBag.idemp = id2;
                var ID = int.Parse(id2);
                var record = dbcontext.Medical_Test_Profile.Where(m => m.ApplicantId == ID);
                dbcontext.Medical_Test_Profile.RemoveRange(record);
                dbcontext.SaveChanges();

                var SubjectCode = form["SubjectCode"].Split(char.Parse(","));
                var StartDate = form["StartDate"].Split(char.Parse(","));
                var MedicalEntity = form["MedicalEntity"].Split(char.Parse(","));
                var TestResult = form["TestResult"].Split(char.Parse(","));
                var NotFitReason = form["NotFitReason"].Split(char.Parse(","));
                var Comments = form["Comments"].Split(char.Parse(","));
                var FinalTestResult = form["FinalTestResult"].Split(char.Parse(","));

                var items = new List<Medical_Test_Profile>();
                for (var i = 0; i < SubjectCode.Count(); i++)
                {
                    if (SubjectCode[i] != "" && StartDate[i] != "")
                    {
                        items.Add(new Medical_Test_Profile { ApplicantId = int.Parse(id2), TryNumber = SubjectCode[i], Medical_Entity = MedicalEntity[i], Test_Date = DateTime.Parse(StartDate[i]), Test_Result = TestResult[i], Not_Fit_Reason = NotFitReason[i], Comments = Comments[i], Final_Test_Result = FinalTestResult[i] });
                    }
                }
                if (items.Count() > 0)
                {
                    var add_items = dbcontext.Medical_Test_Profile.AddRange(items);
                    dbcontext.SaveChanges();
                    /////////////////////////////////////
                    if (command == "Submit")
                    {
                        return RedirectToAction("Edit", "Application", new { id = id2 });
                    }
                }
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