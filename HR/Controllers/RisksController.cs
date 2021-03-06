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
    [Authorize(Roles = "Admin,Organization,OrganizationSetup,Risks")]
    public class RisksController : BaseController
    {
        // GET: Risks
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        public ActionResult Index()
        {
            var model = dbcontext.Risks.ToList();
            return View(model);
        }

        public ActionResult Create(string id)
        {
            var modell = new Risks();
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Organization).Structure_Code;
            var modelll = dbcontext.Risks.ToList();
            var Code = "";
            if (modelll.Count() == 0)
            {
                Code = stru + "1";
            }
            else
            {
                Code = stru + (modelll.LastOrDefault().ID + 1).ToString();
            }



            ViewBag.Risks_Type = dbcontext.Risks_Type.ToList().Select(m => new { ID = m.ID, Code = m.Code + "->" + m.Name });
            if (id != null)
            {
                var ID = int.Parse(id);
                var Risks_Type = dbcontext.Risks_Type.FirstOrDefault(m => m.ID == ID);
                var model = new Risks {Code=Code,  Risks_Type=Risks_Type,Risks_TypeId=Risks_Type.ID.ToString()};
                return View(model);
            }
            var mm = new Risks();
            mm.Code = Code;
            return View(mm);
        }
        [HttpPost]
        public ActionResult Create(Risks model)
        {
            try
            {
                ViewBag.Risks_Type = dbcontext.Risks_Type.ToList().Select(m => new { ID = m.ID, Code = m.Code + "->" + m.Name });
                if (ModelState.IsValid)
                {
                    Risks record = new Risks();
                    record.Name = model.Name;
                    record.Description = model.Description;
                    record.Code = model.Code;
                    record.Risks_TypeId = model.Risks_TypeId;
                    var ID = int.Parse(model.Risks_TypeId);
                    record.Risks_Type = dbcontext.Risks_Type.FirstOrDefault(m => m.ID == ID);
                    dbcontext.Risks.Add(record);
                    dbcontext.SaveChanges();
                    //=================================check for alert==================================
                    var get_result_check = HR.Controllers.check.check_alert("risk", HR.Models.user.Action.Create, HR.Models.user.type_field.form);
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
                ViewBag.Risks_Type = dbcontext.Risks_Type.ToList().Select(m => new { ID = m.ID, Code = m.Code + "->" + m.Name });
                var record = dbcontext.Risks.FirstOrDefault(m => m.ID == id);
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
        public ActionResult Edit(Risks model)
        {
            try
            {
                ViewBag.Risks_Type = dbcontext.Risks_Type.ToList().Select(m => new { ID = m.ID, Code = m.Code + "->" + m.Name });
                var record = dbcontext.Risks.FirstOrDefault(m => m.ID == model.ID);
                record.Name = model.Name;
                record.Description = model.Description;
                record.Code = model.Code;
                record.Risks_TypeId = model.Risks_TypeId;
                var ID = int.Parse(model.Risks_TypeId);
                record.Risks_Type = dbcontext.Risks_Type.FirstOrDefault(m => m.ID == ID);
                dbcontext.SaveChanges();
                //=================================check for alert==================================
                var get_result_check = HR.Controllers.check.check_alert("risk", HR.Models.user.Action.edit, HR.Models.user.type_field.form);
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
                var record = dbcontext.Risks.FirstOrDefault(m => m.ID == id);
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
                var record = dbcontext.Risks.FirstOrDefault(m => m.ID == id);
                dbcontext.Risks.Remove(record);
                dbcontext.SaveChanges();
                //=================================check for alert==================================
                var get_result_check = HR.Controllers.check.check_alert("risk", HR.Models.user.Action.delete, HR.Models.user.type_field.form);
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