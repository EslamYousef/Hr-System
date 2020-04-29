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
    public class Personnel_Committee_ProfileController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Personnel_Committee_Profile
        public ActionResult Create(string id)
        {
            ViewBag.Committe_Resolution_Recuirtment = dbcontext.Committe_Resolution_Recuirtment.Where(a=>a.Committe_Usage==Committe_Usage.Personnel).ToList().Select(m => new { Code = m.Code , ID = m.ID });
            ViewBag.Committe_Activities = dbcontext.Committe_Activities.ToList().Select(m => new { Code = m.ActivitySerialNumber, ID = m.ID });
            var ID = int.Parse(id);
            var App = dbcontext.Application.FirstOrDefault(a => a.ID == ID).Applicant_ProfileId;
            var AppId = int.Parse(App);
            ViewBag.ApplicationApp = dbcontext.Applicant_Profile.FirstOrDefault(a => a.ID == AppId).Full_Name;
            ViewBag.ApplicationCode = dbcontext.Application.FirstOrDefault(a => a.ID == ID).Code;
            ViewBag.idemp = id;
            DateTime statis = DateTime.Now;
            var com = dbcontext.Application.FirstOrDefault(m => m.ID == ID);
            var Personnel_Committee_Profile = new Personnel_Committee_Profile { Application = com, ID = com.ID };
            return View(Personnel_Committee_Profile);
        }
        [HttpPost]
        public ActionResult Create(FormCollection form, Personnel_Committee_Profile model, string Command, string id2)
        {
            try
            {
                ViewBag.Committe_Resolution_Recuirtment = dbcontext.Committe_Resolution_Recuirtment.Where(a => a.Committe_Usage == Committe_Usage.Personnel).ToList().Select(m => new { Code = m.Code, ID = m.ID });
                ViewBag.Committe_Activities = dbcontext.Committe_Activities.ToList().Select(m => new { Code = m.ActivitySerialNumber, ID = m.ID });
                ViewBag.idemp = id2;

                var CommitteResolution = form["CommitteResolution"].Split(char.Parse(","));
                var Subject = form["Subject"].Split(char.Parse(","));
                var ActivitySerialNo = form["ActivitySerialNo"].Split(char.Parse(","));
                var CommitteeApprovalDate = form["CommitteeApprovalDate"].Split(char.Parse(","));
                
                var items = new List<Personnel_Committee_Profile>();
                for (var i = 0; i < CommitteResolution.Count(); i++)
                {
                    if (CommitteResolution[i] != "" && ActivitySerialNo[i] != "" )
                    {
                        items.Add(new Personnel_Committee_Profile { ApplicantId = int.Parse(id2), Committe_Resolution_RecuirtmentId =CommitteResolution[i],Subject= Subject[i], Activity_Serial_No =ActivitySerialNo[i] , Committee_Approval_Date = DateTime.Parse(CommitteeApprovalDate[i]) });
                    }
                }
                if (items.Count() > 0)
                {
                    var add_items = dbcontext.Personnel_Committee_Profile.AddRange(items);
                    dbcontext.SaveChanges();
                    /////////////////////////////////////
                    if (Command == "Submit")
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

                ViewBag.Committe_Resolution_Recuirtment = dbcontext.Committe_Resolution_Recuirtment.Where(a => a.Committe_Usage == Committe_Usage.Personnel).ToList().Select(m => new { Code = m.Code, ID = m.ID });
                ViewBag.Committe_Activities = dbcontext.Committe_Activities.ToList().Select(m => new { Code = m.ActivitySerialNumber, ID = m.ID });
                var record = dbcontext.Personnel_Committee_Profile.Where(a => a.ApplicantId == ID).ToList();
                //if (record.ApplicantId != 0)
                //{
                //    record.Application = dbcontext.Application.FirstOrDefault(m => m.ID == record.ApplicantId);
                //}
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
        public ActionResult Edit(FormCollection form, Personnel_Committee_Profile model, string Command, string id2)
        {
            try
            {
                ViewBag.Committe_Resolution_Recuirtment = dbcontext.Committe_Resolution_Recuirtment.Where(a => a.Committe_Usage == Committe_Usage.Personnel).ToList().Select(m => new { Code = m.Code, ID = m.ID });
                ViewBag.Committe_Activities = dbcontext.Committe_Activities.ToList().Select(m => new { Code = m.ActivitySerialNumber, ID = m.ID });

                ViewBag.idemp = id2;
                var ID = int.Parse(id2);
                var record = dbcontext.Personnel_Committee_Profile.Where(m => m.ApplicantId == ID);
                dbcontext.Personnel_Committee_Profile.RemoveRange(record);
                dbcontext.SaveChanges();

                var CommitteResolution = form["CommitteResolution"].Split(char.Parse(","));
                var Subject = form["Subject"].Split(char.Parse(","));
                var ActivitySerialNo = form["ActivitySerialNo"].Split(char.Parse(","));
                var CommitteeApprovalDate = form["CommitteeApprovalDate"].Split(char.Parse(","));

                var items = new List<Personnel_Committee_Profile>();
                for (var i = 0; i < CommitteResolution.Count(); i++)
                {
                    if (CommitteResolution[i] != "" && ActivitySerialNo[i] != "")
                    {
                        items.Add(new Personnel_Committee_Profile { ApplicantId = int.Parse(id2), Committe_Resolution_RecuirtmentId = CommitteResolution[i], Subject = Subject[i], Activity_Serial_No = ActivitySerialNo[i], Committee_Approval_Date = DateTime.Parse(CommitteeApprovalDate[i]) });
                    }
                }
                if (items.Count() > 0)
                {
                    var add_items = dbcontext.Personnel_Committee_Profile.AddRange(items);
                    dbcontext.SaveChanges();
                    /////////////////////////////////////
                    if (Command == "Submit")
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