using HR.Models;
using HR.Models.Training.trans;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers.training.trans
{
    [Authorize(Roles = "Admin,talent,talentCards,courses_card")]
    public class Course_skillController : BaseController
    {
        // GET: Course_skill
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        public ActionResult Index()
        {
            var model = dbcontext.Cours.ToList();
            return View(model);
        }
        public ActionResult create(int? ID_course)
        {
            try
            {

                ViewBag.centers = dbcontext.Skill.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.course = dbcontext.Cours.ToList().Select(m => new { Code = m.Course_Code + "-[" + m.Course_Desc + ']', ID = m.ID });

                var new_model = new Course_Skills();
                if (ID_course != null)
                {
                    var cou_q = dbcontext.Course_Skills.Where(m => m.Course_Code == ID_course.ToString()).ToList();
                    if (cou_q.Count() > 0)
                    {
                        return RedirectToAction("edit", new { id = ID_course });
                    }
                    var course = dbcontext.Cours.FirstOrDefault(m => m.ID == ID_course);
                    new_model.Course_Code = course.ID.ToString();

                }
                /////

                return View(new_model);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult create(FormCollection form, Course_Skills model)
        {
            try
            {
                ViewBag.centers = dbcontext.Skill.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.course = dbcontext.Cours.ToList().Select(m => new { Code = m.Course_Code + "-[" + m.Course_Desc + ']', ID = m.ID });

                //================================
                var center_D = form["center_id"].Split(',');
                for (var i = 0; i < center_D.Length; i++)
                {
                    if (center_D[i] != "")
                    {
                        var ID = int.Parse(center_D[i]);
                        var CENTER = dbcontext.Skill.FirstOrDefault(m => m.ID == ID);
                        var Cource_cen = new Course_Skills { Course_Code = model.Course_Code, skill_des = CENTER.Code + "-" + CENTER.Name, Skill_Code = CENTER.ID.ToString(), Created_By = User.Identity.Name, Created_Date = DateTime.Now.Date };
                        dbcontext.Course_Skills.Add(Cource_cen);
                        dbcontext.SaveChanges();
                    }
                }
                //================================
                //=================================check for alert==================================

                var get_result_check = HR.Controllers.check.check_alert("course skills carde", HR.Models.user.Action.Create, HR.Models.user.type_field.form);
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
            catch (DbUpdateException e)
            {
                TempData["Message"] = HR.Resource.Basic.thiscodeIsalreadyexists;
                return View();
            }
            catch (Exception e)
            {
                return View();
            }
        }
        public ActionResult edit(int id)
        {
            try
            {
                ViewBag.centers = dbcontext.Skill.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.course = dbcontext.Cours.ToList().Select(m => new { Code = m.Course_Code + "-[" + m.Course_Desc + ']', ID = m.ID });

                var cours = dbcontext.Cours.FirstOrDefault(m => m.ID == id);
                var edit_model = new Course_Skills { Course_Code = cours.ID.ToString() };
                ViewBag.course_code5 = cours.ID.ToString();
                return View(edit_model);
            }
            catch (DbUpdateException e)
            {
                TempData["Message"] = HR.Resource.Basic.thiscodeIsalreadyexists;
                return RedirectToAction("index");
            }
            catch (Exception e)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult edit(Course_Skills model, FormCollection form, string course_code99)
        {
            try
            {
                ViewBag.centers = dbcontext.Skill.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.course = dbcontext.Cours.ToList().Select(m => new { Code = m.Course_Code + "-[" + m.Course_Desc + ']', ID = m.ID });
                var old_course = course_code99;
                if (model.Course_Code == null || model.Course_Code == "0" || model.Course_Code == "")
                {

                    ViewBag.course_code5 = course_code99;
                    return View();
                }
                else
                {
                    ViewBag.course_code5 = model.Course_Code;
                }
                //================================
                var old_course_center = dbcontext.Course_Skills.Where(m => m.Course_Code == old_course);
                if (old_course_center != null)
                {
                    dbcontext.Course_Skills.RemoveRange(old_course_center);
                    dbcontext.SaveChanges();
                }
                //================================
                var center_D = form["center_id"].Split(',');

                for (var i = 0; i < center_D.Length; i++)
                {
                    if (center_D[i] != "")
                    {
                        var ID = int.Parse(center_D[i]);
                        var center = dbcontext.Skill.FirstOrDefault(m => m.ID == ID);
                        var Cource_cen = new Course_Skills { Course_Code = model.Course_Code, skill_des = center.Code + "-" + center.Name, Skill_Code = center.ID.ToString(), Created_By = User.Identity.Name, Created_Date = DateTime.Now.Date };
                        dbcontext.Course_Skills.Add(Cource_cen);
                        dbcontext.SaveChanges();
                    }
                }
                //================================
                //=================================check for alert==================================

                var get_result_check = HR.Controllers.check.check_alert("course skills carde", HR.Models.user.Action.edit, HR.Models.user.type_field.form);
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
            catch (DbUpdateException e)
            {
                TempData["Message"] = HR.Resource.Basic.thiscodeIsalreadyexists;
                return View();
            }
            catch (Exception e)
            {
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                var record = dbcontext.Cours.FirstOrDefault(m => m.ID == id);
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
            var record = dbcontext.Cours.FirstOrDefault(m => m.ID == id);
            var centers = dbcontext.Course_Skills.Where(m => m.Course_Code == id.ToString()).ToList();
            try
            {
                dbcontext.Course_Skills.RemoveRange(centers);
                dbcontext.SaveChanges();
                //=================================check for alert==================================

                var get_result_check = HR.Controllers.check.check_alert("course skills carde", HR.Models.user.Action.delete, HR.Models.user.type_field.form);
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
        //============ajax=============

        public JsonResult getallcontent(string coures_code)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            var content = dbcontext.Course_Skills.Where(m => m.Course_Code == coures_code).ToList();
            return Json(content);
        }
    }
}