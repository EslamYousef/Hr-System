using HR.Models;
using HR.Models.Infra;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers
{
    [Authorize(Roles = "Admin,Organization,OrganizationSetup,Authority")]
    public class AuthoritiesController : BaseController
    {
        // GET: Authorities
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Country
        public ActionResult Index()
        {
            var model = dbcontext.Authorities.ToList();
            return View(model);
        }

        public ActionResult Create(string id)
        {
            //////
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Organization).Structure_Code;
            var modell = dbcontext.Authorities.ToList();
            string Code;
            if (modell.Count() == 0)
            {
                Code = stru + "1";
            }
            else
            {
                Code = stru + (modell.LastOrDefault().ID + 1).ToString();
            }
            /////
            ViewBag.author = dbcontext.Authority_Type.ToList().Select(m => new { ID = m.ID, Code = m.Code + "->" + m.Name });
            if (id != null)
            {
                var ID = int.Parse(id);
                var Authority_Type = dbcontext.Authority_Type.FirstOrDefault(m => m.ID == ID);
                var model = new Authorities {Code=Code, Authority_TypeId=Authority_Type.ID.ToString(),Authority_Type=Authority_Type  };
                return View(model);
            }
            var mm = new Authorities();
            mm.Code = Code;
            return View(mm);
        }
        [HttpPost]
        public ActionResult Create(Authorities model)
        {
            try
            {
                ViewBag.author = dbcontext.Authority_Type.ToList().Select(m => new { ID = m.ID, Code = m.Code + "->" + m.Name });
                if (ModelState.IsValid)
                {
                    Authorities record = new Authorities();
                    record.Name = model.Name;
                    record.Description = model.Description;
                    record.Code = model.Code;
                    record.Authority_TypeId = model.Authority_TypeId;
                    var ID = int.Parse(model.Authority_TypeId);
                    record.Authority_Type = dbcontext.Authority_Type.FirstOrDefault(m => m.ID == ID);
                    record.Skill_Description = model.Skill_Description;
                   var Authority_Type=   dbcontext.Authorities.Add(record);
                    dbcontext.SaveChanges();
                    //=================================check for alert==================================
                    var get_result_check = HR.Controllers.check.check_alert("athourities", HR.Models.user.Action.Create, HR.Models.user.type_field.form);
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
                ViewBag.author = dbcontext.Authority_Type.ToList().Select(m => new { ID = m.ID, Code = m.Code + "->" + m.Name });
                var record = dbcontext.Authorities.FirstOrDefault(m => m.ID == id);
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
        public ActionResult Edit(Authorities model)
        {
            try
            {
                ViewBag.author = dbcontext.Authority_Type.ToList().Select(m => new { ID = m.ID, Code = m.Code + "->" + m.Name });
                var record = dbcontext.Authorities.FirstOrDefault(m => m.ID == model.ID);
                record.Name = model.Name;
                record.Description = model.Description;
                record.Code = model.Code;
                record.Authority_TypeId = model.Authority_TypeId;
                var ID = int.Parse(model.Authority_TypeId);
                record.Authority_Type = dbcontext.Authority_Type.FirstOrDefault(m => m.ID == ID);
                record.Skill_Description = model.Skill_Description;
                dbcontext.SaveChanges();
                //=================================check for alert==================================
                var get_result_check = HR.Controllers.check.check_alert("athourities", HR.Models.user.Action.edit, HR.Models.user.type_field.form);
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
                var record = dbcontext.Authorities.FirstOrDefault(m => m.ID == id);
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
                var record = dbcontext.Authorities.FirstOrDefault(m => m.ID == id);
                dbcontext.Authorities.Remove(record);
                dbcontext.SaveChanges();
                //=================================check for alert==================================
                var get_result_check = HR.Controllers.check.check_alert("athourities", HR.Models.user.Action.delete, HR.Models.user.type_field.form);
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