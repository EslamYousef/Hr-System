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
    public class Hiring_InformationController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Hiring_Information
        public ActionResult Create(string id)
        {
            var ID = int.Parse(id);
            var App = dbcontext.Application.FirstOrDefault(a => a.ID == ID).Applicant_ProfileId;
            var AppId = int.Parse(App);
            ViewBag.ApplicationApp = dbcontext.Applicant_Profile.FirstOrDefault(a => a.ID == AppId).Full_Name;
            ViewBag.ApplicationCode = dbcontext.Application.FirstOrDefault(a => a.ID == ID).Code;
            ViewBag.Weekendsetup = dbcontext.Weekend_setup.ToList().Select(m => new { Code = m.Code + "------[" + m.Description + ']', ID = m.ID });
            ViewBag.idemp = id;
            DateTime statis = DateTime.Now;
            var com = dbcontext.Application.FirstOrDefault(m => m.ID == ID);
            var Hiring_Information = new Hiring_Information { Application = com, ID = com.ID, HireDate = statis, JoinDate = statis, OilSectorJoinDate = statis, SocialInsuranceDate = statis , Service_Period_Ins_M = 0 , Service_Period_Ins_Y = 0};
            return View(Hiring_Information);
        }
        [HttpPost]
        public ActionResult Create(Hiring_Information model, string Command, string id2)
        {
            try
            {
                ViewBag.Weekendsetup = dbcontext.Weekend_setup.ToList().Select(m => new { Code = m.Code + "------[" + m.Description + ']', ID = m.ID });
                ViewBag.idemp = id2;
                //if (ModelState.IsValid)
                //{
                Hiring_Information record = new Hiring_Information();
                //record.Code = model.Code;
                record.ApplicantId = int.Parse(id2);
                record.Committee_Recommendation = model.Committee_Recommendation;
                record.Medical_commite_recomindation = model.Medical_commite_recomindation;
                record.Employee_ID = model.Employee_ID;
                record.HireDate = model.HireDate;
                record.JoinDate = model.JoinDate;
                record.OilSectorJoinDate = model.OilSectorJoinDate;
                record.SocialInsuranceDate = model.SocialInsuranceDate;
                record.SocialInsuranceNum = model.SocialInsuranceNum;
                record.Service_Period_Ins_Y = model.Service_Period_Ins_Y;
                record.Service_Period_Ins_M = model.Service_Period_Ins_M;
                record.WeekendCodeId = model.WeekendCodeId;
                dbcontext.Hiring_Information.Add(record);
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
                ViewBag.Weekendsetup = dbcontext.Weekend_setup.ToList().Select(m => new { Code = m.Code + "------[" + m.Description + ']', ID = m.ID });
                var record = dbcontext.Hiring_Information.FirstOrDefault(m => m.ApplicantId == ID);

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
        public ActionResult Edit(Hiring_Information model, string Command, string id2)
        {
            try
            {
                ViewBag.Weekendsetup = dbcontext.Weekend_setup.ToList().Select(m => new { Code = m.Code + "------[" + m.Description + ']', ID = m.ID });
                ViewBag.idemp = id2;
                var ID = int.Parse(id2);
                var record = dbcontext.Hiring_Information.FirstOrDefault(m => m.ApplicantId == ID);

                record.ApplicantId = int.Parse(id2);
                record.Committee_Recommendation = model.Committee_Recommendation;
                record.Medical_commite_recomindation = model.Medical_commite_recomindation;
                record.Employee_ID = model.Employee_ID;
                record.HireDate = model.HireDate;
                record.JoinDate = model.JoinDate;
                record.OilSectorJoinDate = model.OilSectorJoinDate;
                record.SocialInsuranceDate = model.SocialInsuranceDate;
                record.SocialInsuranceNum = model.SocialInsuranceNum;
                record.Service_Period_Ins_Y = model.Service_Period_Ins_Y;
                record.Service_Period_Ins_M = model.Service_Period_Ins_M;
                record.WeekendCodeId = model.WeekendCodeId;
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