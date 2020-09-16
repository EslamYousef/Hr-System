using HR.Models;
using HR.Models.Infra;
using HR.Models.Training.setup;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers.training.setup
{
    [Authorize(Roles = "Admin,talent,talentSetup,Courses_setup")]
    public class course_sub_groupController : BaseController
    {
        // GET: course_sub_group
        ApplicationDbContext dbcontext = new ApplicationDbContext();

        public ActionResult Index()
        {
            var model = dbcontext.Course_SubGroups.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            //////
            var modell = new Course_SubGroups();

            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Talent_Development).Structure_Code;
            var model = dbcontext.Course_SubGroups.ToList();
            if (model.Count() == 0)
            {
                modell.SubGroup_Code = stru + "1";
            }
            else
            {
                modell.SubGroup_Code = stru + (model.LastOrDefault().ID + 1).ToString();
            }
            /////
            ViewBag.group = dbcontext.Course_Groups.ToList().Select(m => new { Code = m.Group_Code + "-[" + m.Group_Desc + ']', ID = m.Group_Code });

            return View(modell);
        }
        [HttpPost]
        public ActionResult Create(Course_SubGroups model)
        {
            try
            {
                ViewBag.group = dbcontext.Course_Groups.ToList().Select(m => new { Code = m.Group_Code + "-[" + m.Group_Desc + ']', ID = m.Group_Code });
                if (ModelState.IsValid)
                {
                    model.Created_By = User.Identity.Name;
                    model.Created_Date = DateTime.Now.Date;
                    dbcontext.Course_SubGroups.Add(model);
                    dbcontext.SaveChanges();
                    //=================================check for alert==================================

                    var get_result_check = HR.Controllers.check.check_alert("course sub group", HR.Models.user.Action.Create, HR.Models.user.type_field.form);
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
        public ActionResult Edit(int id)
        {
            try
            {
                ViewBag.group = dbcontext.Course_Groups.ToList().Select(m => new { Code = m.Group_Code + "-[" + m.Group_Desc + ']', ID = m.Group_Code });
                var record = dbcontext.Course_SubGroups.FirstOrDefault(m => m.ID == id);
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
        public ActionResult Edit(Course_SubGroups model)
        {
            try
            {
                ViewBag.group = dbcontext.Course_Groups.ToList().Select(m => new { Code = m.Group_Code + "-[" + m.Group_Desc + ']', ID = m.Group_Code });
                var record = dbcontext.Course_SubGroups.FirstOrDefault(m => m.ID == model.ID);

                record.SubGroup_Desc = model.SubGroup_Desc;
                record.SubGroup_AltDesc = model.SubGroup_AltDesc;
                record.Group_Code = model.Group_Code;
                record.Modified_By = User.Identity.Name;
                record.Modified_Date = DateTime.Now.Date;
                dbcontext.SaveChanges();
                //=================================check for alert==================================

                var get_result_check = HR.Controllers.check.check_alert("course sub group", HR.Models.user.Action.edit, HR.Models.user.type_field.form);
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
                var record = dbcontext.Course_SubGroups.FirstOrDefault(m => m.ID == id);
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
            var record = dbcontext.Course_SubGroups.FirstOrDefault(m => m.ID == id);
            try
            {
                dbcontext.Course_SubGroups.Remove(record);
                dbcontext.SaveChanges();
                //=================================check for alert==================================

                var get_result_check = HR.Controllers.check.check_alert("course sub group", HR.Models.user.Action.delete, HR.Models.user.type_field.form);
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
                TempData["Message"] = HR.Resource.Basic.youcannotdeletethisRow;
                return View(record);
            }
            catch (Exception e)
            {
                return View(record);
            }
        }
    }
}