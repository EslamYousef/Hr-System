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
    public class Application_StatusController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Application_Status
        public ActionResult Create(string id)
        {
            var ID = int.Parse(id);
            var App = dbcontext.Application.FirstOrDefault(a => a.ID == ID).Applicant_ProfileId;
            var AppId = int.Parse(App);
            ViewBag.ApplicationApp = dbcontext.Applicant_Profile.FirstOrDefault(a => a.ID == AppId).Full_Name;
            ViewBag.ApplicationCode = dbcontext.Application.FirstOrDefault(a => a.ID == ID).Code;
            ViewBag.idemp = id;
            DateTime statis = DateTime.Now;
            var com = dbcontext.Application.FirstOrDefault(m => m.ID == ID);
            var Application_Status = new Application_Status { Application = com, ID = com.ID, StatusDate = statis };
            return View(Application_Status);
        }
        [HttpPost]
        public ActionResult Create(Application_Status model, string Command, string id2)
        {
            try
            {
                 ViewBag.idemp = id2;
                //if (ModelState.IsValid)
                //{
                Application_Status record = new Application_Status();
                //record.Code = model.Code;
                record.ApplicantId = int.Parse(id2);
                record.StatusDate = model.StatusDate;
                record.Comments = model.Comments;
                record.ApplicationStatus = model.ApplicationStatus;
                dbcontext.Application_Status.Add(record);
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
              
                var record = dbcontext.Application_Status.FirstOrDefault(m => m.ApplicantId == ID);

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
        public ActionResult Edit(Application_Status model, string Command, string id2)
        {
            try
            {
                ViewBag.idemp = id2;
                var ID = int.Parse(id2);
                var record = dbcontext.Application_Status.FirstOrDefault(m => m.ApplicantId == ID);

                record.ApplicantId = int.Parse(id2);
                record.StatusDate = model.StatusDate;
                record.Comments = model.Comments;
                record.ApplicationStatus = model.ApplicationStatus;
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