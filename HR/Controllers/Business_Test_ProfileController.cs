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
    public class Business_Test_ProfileController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Business_Test_Profile
        public ActionResult Create(string id)
        {
            ViewBag.Test = dbcontext.Test.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.work_location = dbcontext.work_location.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            var ID = int.Parse(id);
            var App = dbcontext.Application.FirstOrDefault(a => a.ID == ID).Applicant_ProfileId;
            var AppId = int.Parse(App);
            ViewBag.ApplicationApp = dbcontext.Applicant_Profile.FirstOrDefault(a => a.ID == AppId).Full_Name;
            ViewBag.ApplicationCode = dbcontext.Application.FirstOrDefault(a => a.ID == ID).Code;
            ViewBag.idemp = id;
            DateTime statis = DateTime.Now;
            var com = dbcontext.Application.FirstOrDefault(m => m.ID == ID);
            var Business_Test_Profile = new Business_Test_Profile { Application = com, ID = com.ID };
            return View(Business_Test_Profile);
        }
        [HttpPost]
        public ActionResult Create(FormCollection form, Business_Test_Profile model, string Command, string id2)
        {
            try
            {
                ViewBag.Test = dbcontext.Test.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.work_location = dbcontext.work_location.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.idemp = id2;

                var TryNum = form["TryNum"].Split(char.Parse(","));
                var StartDate = form["StartDate"].Split(char.Parse(","));
                var TestCode = form["TestCode"].Split(char.Parse(","));
                var TestDescription = form["TestDescription"].Split(char.Parse(","));
                var PassMark = form["PassMark"].Split(char.Parse(","));
                var FullMark = form["FullMark"].Split(char.Parse(","));
                var QbtainMark = form["QbtainMark"].Split(char.Parse(","));
                var LocationCode = form["LocationCode"].Split(char.Parse(","));
                var LocationDes = form["LocationDes"].Split(char.Parse(","));
                var Notes = form["Notes"].Split(char.Parse(","));

                var items = new List<Business_Test_Profile>();
                for (var i = 0; i < TestCode.Count(); i++)
                {
                    if (TryNum[i] != "" && TestCode[i] != "" && TestDescription[i] != "" && PassMark[i] != "" && FullMark[i] != "" && QbtainMark[i] != "" && LocationCode[i] != "" && LocationDes[i] != "")
                    {
                        items.Add(new Business_Test_Profile {ApplicantId = int.Parse(id2), TryNumber = TryNum[i], Test_Date = DateTime.Parse(StartDate[i]), TestCode = TestCode[i], TestDescription = TestDescription[i], Pass_Mark = int.Parse(PassMark[i]), Full_Mark = int.Parse(FullMark[i]), Qbtain_Mark = int.Parse(QbtainMark[i]), Test_location_Code = LocationCode[i], Location_Description = LocationDes[i], Comments = Notes[i] });
                    }
                }
                if (items.Count() > 0)
                {
                    var add_items = dbcontext.Business_Test_Profile.AddRange(items);
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

                ViewBag.Test = dbcontext.Test.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.work_location = dbcontext.work_location.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                var record = dbcontext.Business_Test_Profile.Where(a => a.ApplicantId == ID).ToList();
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
        public ActionResult Edit(FormCollection form, Business_Test_Profile model, string Command, string id2)
        {
            try
            {
                ViewBag.Test = dbcontext.Test.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.work_location = dbcontext.work_location.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });

                ViewBag.idemp = id2;
                var ID = int.Parse(id2);
                var record = dbcontext.Business_Test_Profile.Where(m => m.ApplicantId == ID);
                dbcontext.Business_Test_Profile.RemoveRange(record);
                dbcontext.SaveChanges();

                var TryNum = form["TryNum"].Split(char.Parse(","));
                var StartDate = form["StartDate"].Split(char.Parse(","));
                var TestCode = form["TestCode"].Split(char.Parse(","));
                var TestDescription = form["TestDescription"].Split(char.Parse(","));
                var PassMark = form["PassMark"].Split(char.Parse(","));
                var FullMark = form["FullMark"].Split(char.Parse(","));
                var QbtainMark = form["QbtainMark"].Split(char.Parse(","));
                var LocationCode = form["LocationCode"].Split(char.Parse(","));
                var LocationDes = form["LocationDes"].Split(char.Parse(","));
                var Notes = form["Notes"].Split(char.Parse(","));

                var items = new List<Business_Test_Profile>();
                for (var i = 0; i < TestCode.Count(); i++)
                {
                    if (TryNum[i] != "" && TestCode[i] != "" && TestDescription[i] != "" && PassMark[i] != "" && FullMark[i] != "" && QbtainMark[i] != "" && LocationCode[i] != "" && LocationDes[i] != "")
                    {
                        items.Add(new Business_Test_Profile { ApplicantId = int.Parse(id2), TryNumber = TryNum[i], Test_Date = DateTime.Parse(StartDate[i]), TestCode = TestCode[i], TestDescription = TestDescription[i], Pass_Mark = int.Parse(PassMark[i]), Full_Mark = int.Parse(FullMark[i]), Qbtain_Mark = int.Parse(QbtainMark[i]), Test_location_Code = LocationCode[i], Location_Description = LocationDes[i], Comments = Notes[i] });
                    }
                }
                if (items.Count() > 0)
                {
                    var add_items = dbcontext.Business_Test_Profile.AddRange(items);
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