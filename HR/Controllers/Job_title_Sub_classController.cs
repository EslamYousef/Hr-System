﻿using HR.Models;
using HR.Models.Infra;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers
{
    [Authorize(Roles = "Admin,Organization,OrganizationSetup,job title")]
    public class Job_title_Sub_classController : BaseController
    {
        // GET: Job_title_Sub_class
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        public ActionResult Index()
        {
            var model = dbcontext.Job_title_sub_class.ToList();
            return View(model);
        }

        public ActionResult Create(string id)
        {

            var modell = new Job_title_sub_class();
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Organization).Structure_Code;
            var modelll = dbcontext.Job_title_sub_class.ToList();
            var Code = "";
            if (modelll.Count() == 0)
            {
                Code = stru + "1";
            }
            else
            {
                Code = stru + (modelll.LastOrDefault().ID + 1).ToString();
            }




            ViewBag.Job_title_class = dbcontext.Job_title_class.ToList().Select(m => new { ID = m.ID, Code = m.Code + "->" + m.Name });
            if (id != null)
            {
                var ID = int.Parse(id);
                var Job_title_class = dbcontext.Job_title_class.FirstOrDefault(m => m.ID == ID);
                var model = new Job_title_sub_class {Code=Code, Job_title_class= Job_title_class, Job_title_classId= Job_title_class.ID.ToString() };
                return View(model);
            }
            var mm = new Job_title_sub_class();
            mm.Code = Code;
            return View(mm);
        }
        [HttpPost]
        public ActionResult Create(Job_title_sub_class model)
        {
            try
            {
                ViewBag.Job_title_class = dbcontext.Job_title_class.ToList().Select(m => new { ID = m.ID, Code = m.Code + "->" + m.Name });
                if (ModelState.IsValid)
                {
                    Job_title_sub_class record = new Job_title_sub_class();
                    record.Name = model.Name;
                    record.Description = model.Description;
                    record.Code = model.Code;
                    record.Job_title_classId = model.Job_title_classId;
                    var ID = int.Parse(model.Job_title_classId);
                    record.JOB_TYPE_ALLWANCE_PERCENTAGE = model.JOB_TYPE_ALLWANCE_PERCENTAGE;
                    record.Dedicated_ALLWANCE_VALUE = model.Dedicated_ALLWANCE_VALUE;
                    record.Exchanging_ALLWANCE_VALUE = model.Exchanging_ALLWANCE_VALUE;
                    record.Job_title_class = dbcontext.Job_title_class.FirstOrDefault(m => m.ID == ID);
                    dbcontext.Job_title_sub_class.Add(record);
                    dbcontext.SaveChanges();
                    //=================================check for alert==================================

                    var get_result_check = HR.Controllers.check.check_alert("job title sub class", HR.Models.user.Action.Create, HR.Models.user.type_field.form);
                    if (get_result_check != null)
                    {
                        var inbox = new Models.user.Alert_inbox { send_from_user_id = User.Identity.Name, send_to_user_id = get_result_check.send_to_ID_user, title = get_result_check.Subject, Subject = get_result_check.Message };
                        if (get_result_check.until != null)
                        {
                            if (get_result_check.until.Value.Year != 0001)
                            {
                                inbox.until = get_result_check.until;
                            }
                        }
                        ApplicationDbContext dbcontext = new ApplicationDbContext();
                        dbcontext.Alert_inbox.Add(inbox);
                        dbcontext.SaveChanges();
                    }
                    //===================================================================================
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(model);
                }
            }
            catch (DbUpdateException)
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
                ViewBag.Job_title_class = dbcontext.Job_title_class.ToList().Select(m => new { ID = m.ID, Code = m.Code + "->" + m.Name });
                var record = dbcontext.Job_title_sub_class.FirstOrDefault(m => m.ID == id);
                if (record != null)
                { return View(record); }
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
        public ActionResult Edit(Job_title_sub_class model)
        {
            try
            {
                ViewBag.Job_title_class = dbcontext.Job_title_class.ToList().Select(m => new { ID = m.ID, Code = m.Code + "->" + m.Name });
                var record = dbcontext.Job_title_sub_class.FirstOrDefault(m => m.ID == model.ID); 
                record.Name = model.Name;
                record.Description = model.Description;
                record.Code = model.Code;
                record.Job_title_classId = model.Job_title_classId;
                var ID = int.Parse(model.Job_title_classId);
                record.Job_title_class = dbcontext.Job_title_class.FirstOrDefault(m => m.ID == ID);
                record.JOB_TYPE_ALLWANCE_PERCENTAGE = model.JOB_TYPE_ALLWANCE_PERCENTAGE;
                record.Dedicated_ALLWANCE_VALUE = model.Dedicated_ALLWANCE_VALUE;
                record.Exchanging_ALLWANCE_VALUE = model.Exchanging_ALLWANCE_VALUE;

                dbcontext.SaveChanges();
                //=================================check for alert==================================

                var get_result_check = HR.Controllers.check.check_alert("job title sub class", HR.Models.user.Action.edit, HR.Models.user.type_field.form);
                if (get_result_check != null)
                {
                    var inbox = new Models.user.Alert_inbox { send_from_user_id = User.Identity.Name, send_to_user_id = get_result_check.send_to_ID_user, title = get_result_check.Subject, Subject = get_result_check.Message };
                    if (get_result_check.until != null)
                    {
                        if (get_result_check.until.Value.Year != 0001)
                        {
                            inbox.until = get_result_check.until;
                        }
                    }
                    ApplicationDbContext dbcontext = new ApplicationDbContext();
                    dbcontext.Alert_inbox.Add(inbox);
                    dbcontext.SaveChanges();
                }
                //===================================================================================
                return RedirectToAction("index");
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
                var record = dbcontext.Job_title_sub_class.FirstOrDefault(m => m.ID == id);
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
            try
            {
                var record = dbcontext.Job_title_sub_class.FirstOrDefault(m => m.ID == id);
                dbcontext.Job_title_sub_class.Remove(record);
                dbcontext.SaveChanges();
                //=================================check for alert==================================

                var get_result_check = HR.Controllers.check.check_alert("job title sub class", HR.Models.user.Action.delete, HR.Models.user.type_field.form);
                if (get_result_check != null)
                {
                    var inbox = new Models.user.Alert_inbox { send_from_user_id = User.Identity.Name, send_to_user_id = get_result_check.send_to_ID_user, title = get_result_check.Subject, Subject = get_result_check.Message };
                    if (get_result_check.until != null)
                    {
                        if (get_result_check.until.Value.Year != 0001)
                        {
                            inbox.until = get_result_check.until;
                        }
                    }
                    ApplicationDbContext dbcontext = new ApplicationDbContext();
                    dbcontext.Alert_inbox.Add(inbox);
                    dbcontext.SaveChanges();
                }
                //===================================================================================
                return RedirectToAction("index");
            }
            catch (Exception e)
            {
                return View();
            }
        }
    }
}