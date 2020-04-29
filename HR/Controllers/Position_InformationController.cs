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
    public class Position_InformationController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Position_Information

        public ActionResult Create(string id)
        {
            var ID = int.Parse(id);
            var App = dbcontext.Application.FirstOrDefault(a => a.ID == ID).Applicant_ProfileId;
            var AppId = int.Parse(App);
            ViewBag.ApplicationApp = dbcontext.Applicant_Profile.FirstOrDefault(a => a.ID == AppId).Full_Name;
            ViewBag.ApplicationCode = dbcontext.Application.FirstOrDefault(a => a.ID == ID).Code;
            ViewBag.job_desc = dbcontext.job_title_cards.ToList().Select(m => new { Code = m.Code + "------[" + m.name + ']', ID = m.ID });
            ViewBag.job_slot_desc = dbcontext.job_title_cards.ToList().Select(m => new { Code = m.num_slots + "------[" + m.name + ']', ID = m.ID });
            ViewBag.Default_location = dbcontext.work_location.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.location_desc = dbcontext.work_location.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.Job_level_grade = dbcontext.job_level_setup.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.Organization_Chart = dbcontext.Organization_Chart.ToList().Select(m => new { Code = m.Code + "------[" + m.unit_Description + ']', ID = m.ID });
            ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.idemp = id;
            
            DateTime statis2 = Convert.ToDateTime("1/1/1900");
            var com = dbcontext.Application.FirstOrDefault(m => m.ID == ID);
            var Application = new Position_Information_Rec { Application = com, ID = com.ID, From_date = statis2, To_date = statis2 };
            return View(Application);

        }
        [HttpPost]
        public ActionResult Create(Position_Information_Rec model, string Command, string id2)
        {
            try
            {

                if (model.job_descId == null) { model.job_descId = "0"; }
                if (model.Default_location_descId == null) { model.Default_location_descId = "0"; }
                if (model.Location_descId == null) { model.Location_descId = "0"; }
                if (model.Job_level_gradeId == null) { model.Job_level_gradeId = "0"; }
                if (model.SlotdescId == null) { model.SlotdescId = "0"; }
                if (model.Organization_ChartId == null) { model.Organization_ChartId = "0"; }

                var App = dbcontext.Application.FirstOrDefault(a => a.ID == model.ID).Applicant_ProfileId;
                var AppId = int.Parse(App);
                ViewBag.ApplicationApp = dbcontext.Applicant_Profile.FirstOrDefault(a => a.ID == AppId).Full_Name;
                ViewBag.ApplicationCode = dbcontext.Application.FirstOrDefault(a => a.ID == model.ID).Code;

                ViewBag.job_desc = dbcontext.job_title_cards.ToList().Select(m => new { Code = m.Code + "------[" + m.name + ']', ID = m.ID });
                ViewBag.job_slot_desc = dbcontext.job_title_cards.ToList().Select(m => new { Code = m.num_slots + "------[" + m.name + ']', ID = m.ID });
                ViewBag.Default_location = dbcontext.work_location.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.location_desc = dbcontext.work_location.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Job_level_grade = dbcontext.job_level_setup.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Organization_Chart = dbcontext.Organization_Chart.ToList().Select(m => new { Code = m.Code + "------[" + m.unit_Description + ']', ID = m.ID });
                ViewBag.idemp = id2;

                if (ModelState.IsValid)
                {
                   
                    Position_Information_Rec record = new Position_Information_Rec();
                    record.ApplicantId = int.Parse(id2);

                    record.From_date = model.From_date;
                    record.To_date = model.To_date;
                    if (model.From_date > model.To_date)
                    {
                        TempData["Message"] = HR.Resource.Personnel.FromdatebiggerTodate;
                        return View(model);
                    }
                    record.Years = model.Years;
                    record.Months = model.Months;                   
                    record.Working_System = model.Working_System;
                    record.Position_Status = model.Position_Status;

                    //if (model.SlotdescId != "0")
                    //{
                    //    var SlotdescId = int.Parse(model.SlotdescId);
                    //    var my_slot = dbcontext.Slots.FirstOrDefault(m => m.ID == SlotdescId);
                    //    my_slot.Employee_Profile = emp;
                    //    my_slot.EmployeeName = emp.Full_Name;
                    //    my_slot.EmployeeID = emp.ID.ToString();
                    //    //my_slot.check_status = check_status.Canceled;
                    //    var job = dbcontext.job_title_cards.FirstOrDefault(m => m.ID == my_slot.job_title_cards.ID);
                    //    job.number_hired = job.number_hired + 1;
                    //    job.number_vacant = job.number_vacant - 1;
                    //    dbcontext.SaveChanges();
                    //}
                    record.job_descId = model.job_descId;
                    var job_descId = int.Parse(model.job_descId);
                    record.job_title_cards = dbcontext.job_title_cards.FirstOrDefault(m => m.ID == job_descId);
                    record.SlotdescId = model.SlotdescId;
                    var SlotdescId = int.Parse(model.SlotdescId);
                    record.job_title_cards = dbcontext.job_title_cards.FirstOrDefault(m => m.ID == SlotdescId);
                    record.Default_location_descId = model.Default_location_descId;
                    var Default_location_descId = int.Parse(model.Default_location_descId);
                    record.work_location = dbcontext.work_location.FirstOrDefault(m => m.ID == Default_location_descId);
                    record.Location_descId = model.Location_descId;
                    var Location_descId = int.Parse(model.Location_descId);
                    record.work_location = dbcontext.work_location.FirstOrDefault(m => m.ID == Location_descId);
                    record.Job_level_gradeId = model.Job_level_gradeId;
                    var Job_level_gradeId = int.Parse(model.Job_level_gradeId);
                    record.Job_level_grade = dbcontext.Job_level_gradee.FirstOrDefault(m => m.ID == Job_level_gradeId);
                    record.Organization_ChartId = model.Organization_ChartId;
                    var Organization_ChartId = int.Parse(model.Organization_ChartId);
                    record.Organization_Chart = dbcontext.Organization_Chart.FirstOrDefault(m => m.ID == Organization_ChartId);
                    var pos = dbcontext.Position_Information_Rec.Add(record);
                    dbcontext.SaveChanges();



                    if (Command == "Submit")
                    {
                        return RedirectToAction("Edit", "Application", new { id = id2 });
                    }
                    return RedirectToAction("Index", "Application", new { id = id2 });
                }
                else
                {
                    return View(model);
                }
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

                ViewBag.job_desc = dbcontext.job_title_cards.ToList().Select(m => new { Code = m.Code + "------[" + m.name + ']', ID = m.ID });
                ViewBag.job_slot_desc = dbcontext.job_title_cards.ToList().Select(m => new { Code = m.num_slots + "------[" + m.name + ']', ID = m.ID });
                ViewBag.Default_location = dbcontext.work_location.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.location_desc = dbcontext.work_location.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Job_level_grade = dbcontext.job_level_setup.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Organization_Chart = dbcontext.Organization_Chart.ToList().Select(m => new { Code = m.Code + "------[" + m.unit_Description + ']', ID = m.ID });
             
                var record = dbcontext.Position_Information_Rec.FirstOrDefault(m => m.ApplicantId == ID);
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
        public ActionResult Edit(Position_Information_Rec model, string Command, string id2)
        {
            try
            {
                if (model.job_descId == null) { model.job_descId = "0"; }
                if (model.Default_location_descId == null) { model.Default_location_descId = "0"; }
                if (model.Location_descId == null) { model.Location_descId = "0"; }
                if (model.Job_level_gradeId == null) { model.Job_level_gradeId = "0"; }
                if (model.SlotdescId == null) { model.SlotdescId = "0"; }
                if (model.Organization_ChartId == null) { model.Organization_ChartId = "0"; }

                ViewBag.job_desc = dbcontext.job_title_cards.ToList().Select(m => new { Code = m.Code + "------[" + m.name + ']', ID = m.ID });
                ViewBag.job_slot_desc = dbcontext.job_title_cards.ToList().Select(m => new { Code = m.num_slots + "------[" + m.name + ']', ID = m.ID });
                ViewBag.Default_location = dbcontext.work_location.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.location_desc = dbcontext.work_location.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Job_level_grade = dbcontext.job_level_setup.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Organization_Chart = dbcontext.Organization_Chart.ToList().Select(m => new { Code = m.Code + "------[" + m.unit_Description + ']', ID = m.ID });

                ViewBag.idemp = id2;
                var ID = int.Parse(id2);
                var record = dbcontext.Position_Information_Rec.FirstOrDefault(m => m.ApplicantId == ID);
                record.ApplicantId = int.Parse(id2);
                
                record.From_date = model.From_date;
                record.To_date = model.To_date;
                if (model.From_date > model.To_date)
                {
                    TempData["Message"] = HR.Resource.Personnel.FromdatebiggerTodate;
                    return View(model);
                }
                record.Years = model.Years;
                record.Months = model.Months;
                record.Working_System = model.Working_System;
                record.Position_Status = model.Position_Status;

                // var SlotdescId = int.Parse(model.SlotdescId);
                //   var t = dbcontext.Select(a => a.SlotdescId);
                //if (model.SlotdescId == "0")
                //{
                //    ////remove 
                //    if (record.SlotdescId != "0")
                //    {
                //        var IDslot = int.Parse(record.SlotdescId);
                //        var old_slot = dbcontext.Slots.FirstOrDefault(m => m.ID == IDslot);
                //        old_slot.Employee_Profile = null;
                //        old_slot.EmployeeID = null;
                //        old_slot.EmployeeName = null;
                //        dbcontext.SaveChanges();
                //        var job = dbcontext.job_title_cards.FirstOrDefault(m => m.ID == old_slot.job_title_cards.ID);
                //        job.number_hired = job.number_hired - 1;
                //        job.number_vacant = job.number_vacant + 1;
                //        dbcontext.SaveChanges();
                //    }
                //}
                //else
                //{
                //    var IDslot = int.Parse(model.SlotdescId);
                //    var new_slot = dbcontext.Slots.FirstOrDefault(m => m.ID == IDslot);
                //if (new_slot.EmployeeID == null)
                //{
                //    ///remove from old
                //if (record.SlotdescId != "0")
                //{
                //    var old_IDslot = int.Parse(record.SlotdescId);
                //    var old_slot = dbcontext.Slots.FirstOrDefault(m => m.ID == old_IDslot);
                //    old_slot.Employee_Profile = null;
                //    old_slot.EmployeeID = null;
                //    old_slot.EmployeeName = null;
                //    dbcontext.SaveChanges();
                //    var jobb = dbcontext.job_title_cards.FirstOrDefault(m => m.ID == old_slot.job_title_cards.ID);
                //    jobb.number_hired = jobb.number_hired - 1;
                //    jobb.number_vacant = jobb.number_vacant + 1;
                //    dbcontext.SaveChanges();
                //}

                /////add to new

                //new_slot.Employee_Profile = emp;
                //new_slot.EmployeeName = emp.Full_Name;
                //new_slot.EmployeeID = emp.ID.ToString();
                //var job = dbcontext.job_title_cards.FirstOrDefault(m => m.ID == new_slot.job_title_cards.ID);
                //job.number_hired = job.number_hired + 1;
                //job.number_vacant = job.number_vacant - 1;
                //dbcontext.SaveChanges();
                //}
                //}
                record.job_descId = model.job_descId;
                var job_descId = int.Parse(model.job_descId);
                record.job_title_cards = dbcontext.job_title_cards.FirstOrDefault(m => m.ID == job_descId);
                record.SlotdescId = model.SlotdescId;
                var SlotdescId = int.Parse(model.SlotdescId);
                record.job_title_cards = dbcontext.job_title_cards.FirstOrDefault(m => m.ID == SlotdescId);
                record.Default_location_descId = model.Default_location_descId;
                var Default_location_descId = int.Parse(model.Default_location_descId);
                record.work_location = dbcontext.work_location.FirstOrDefault(m => m.ID == Default_location_descId);
                record.Location_descId = model.Location_descId;
                var Location_descId = int.Parse(model.Location_descId);
                record.work_location = dbcontext.work_location.FirstOrDefault(m => m.ID == Location_descId);
                record.Job_level_gradeId = model.Job_level_gradeId;
                var Job_level_gradeId = int.Parse(model.Job_level_gradeId);
                record.Job_level_grade = dbcontext.Job_level_gradee.FirstOrDefault(m => m.ID == Job_level_gradeId);
                record.Organization_ChartId = model.Organization_ChartId;
                var Organization_ChartId = int.Parse(model.Organization_ChartId);
                record.Organization_Chart = dbcontext.Organization_Chart.FirstOrDefault(m => m.ID == Organization_ChartId);
                dbcontext.SaveChanges();
                if (Command == "Submit")
                {
                    return RedirectToAction("Edit", "Application", new { id = id2 });
                }
                return RedirectToAction("Index", "Application", new { id = id2 });
            }
            catch (DbUpdateException e)
            {
                TempData["Message"] = HR.Resource.Basic.thiscodeIsalreadyexists;
                return View(model);
            }
            catch (Exception e)
            { return View(model); }
        }
    }
}