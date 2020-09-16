using HR.Models;
using HR.Models.Infra;
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
    public class CoursController : BaseController
    {
        // GET: Cours
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        public ActionResult Index()
        {
            var model = dbcontext.Cours.ToList();
            return View(model);
        }
        public ActionResult Create()
        {
           
                //////
                var modell = new Cours();
                var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Talent_Development).Structure_Code;
                var model = dbcontext.Cours.ToList();
                var code = "";
                if (model.Count() == 0)
                {
                    modell.Course_Code = stru + "1";

                }
                else
                {
                    modell.Course_Code = stru + (model.LastOrDefault().ID + 1).ToString();

                }
                ViewBag.sub = dbcontext.Course_SubGroups.ToList().Select(m => new { Code = m.SubGroup_Code + "-[" + m.SubGroup_Desc + ']', ID = m.ID });
                ViewBag.classification = dbcontext.CourseClassification.ToList().Select(m => new { Code = m.CourseClassification_Code + "-[" + m.CourseClassification_Desc + ']', ID = m.ID });
             
              
                /////
                return View(modell);
            
            
        }
        [HttpPost]
        public ActionResult create(Cours model,FormCollection form,string command)
        {
            try
            {
                ViewBag.sub = dbcontext.Course_SubGroups.ToList().Select(m => new { Code = m.SubGroup_Code + "-[" + m.SubGroup_Desc + ']', ID = m.ID });
                ViewBag.classification = dbcontext.CourseClassification.ToList().Select(m => new { Code = m.CourseClassification_Code + "-[" + m.CourseClassification_Desc + ']', ID = m.ID });
                var state = form["check_A"].Split(',');
                if (state.Length == 1)
                {
                    model.IsActive = false;
                }
                else
                {

                    model.IsActive = true;

                }
                if (ModelState.IsValid)
                {
                    model.Created_By = User.Identity.Name;
                    model.Created_Date = DateTime.Now.Date;
                    var instr = dbcontext.Cours.Add(model);
                    dbcontext.SaveChanges();
                    //=================================check for alert==================================

                    var get_result_check = HR.Controllers.check.check_alert("course carde", HR.Models.user.Action.Create, HR.Models.user.type_field.form);
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
                    if (command == "Submit")
                    {
                        return RedirectToAction("Create", "CourceQualification", new { ID_course = instr.ID });
                    }
                    else if (command == "Submit_center")
                    {
                        return RedirectToAction("Create", "Course_Centers", new { ID_course = instr.ID });
                    }
                    else if (command == "Submit_skill")
                    {
                        return RedirectToAction("Create", "Course_skill", new { ID_course = instr.ID });
                    }
                    else if (command == "Submit_type")
                    {
                        return RedirectToAction("Create", "Course_training_type", new { ID_course = instr.ID });
                    }
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
        public ActionResult edit(int id)
        {
            try
            {
                ViewBag.sub = dbcontext.Course_SubGroups.ToList().Select(m => new { Code = m.SubGroup_Code + "-[" + m.SubGroup_Desc + ']', ID = m.ID });
                ViewBag.classification = dbcontext.CourseClassification.ToList().Select(m => new { Code = m.CourseClassification_Code + "-[" + m.CourseClassification_Desc + ']', ID = m.ID });
                var model = dbcontext.Cours.FirstOrDefault(m => m.ID == id);
                return View(model);

            }
            catch (Exception e)
            {

                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult edit(Cours model,FormCollection form,string command)
        {
            try
            {
                ViewBag.sub = dbcontext.Course_SubGroups.ToList().Select(m => new { Code = m.SubGroup_Code + "-[" + m.SubGroup_Desc + ']', ID = m.ID });
                ViewBag.classification = dbcontext.CourseClassification.ToList().Select(m => new { Code = m.CourseClassification_Code + "-[" + m.CourseClassification_Desc + ']', ID = m.ID });
                var state = form["check_A"].Split(',');
                if (state.Length == 1)
                {
                    model.IsActive = false;
                }
                else
                {

                    model.IsActive = true;

                }
                if (ModelState.IsValid)
                {
                 
                    var course = dbcontext.Cours.FirstOrDefault(m => m.ID == model.ID);
                    //=============================
                    course.SubGroup_Code = model.SubGroup_Code;
                    course.Course_Code = model.Course_Code;
                    course.Course_Desc = model.Course_Desc;
                    course.Course_AltDesc = model.Course_AltDesc;
                    course.CourseClassification_Code = model.CourseClassification_Code;
                    course.LocalAveragePrice_Course = model.LocalAveragePrice_Course;
                    course.LocalAveragePrice_Trainee = model.LocalAveragePrice_Trainee;
                    course.Pass_Grade = model.Pass_Grade;
                    course.Reword_Degree = model.Reword_Degree;
                    course.RewordAmount = model.RewordAmount;
                    course.Modified_By = User.Identity.Name;
                    course.Modified_Date = DateTime.Now.Date;
                    course.IsActive = model.IsActive;
                    dbcontext.SaveChanges();
                    //=================================check for alert==================================

                    var get_result_check = HR.Controllers.check.check_alert("course carde", HR.Models.user.Action.edit, HR.Models.user.type_field.form);
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
                    //=============================
                    if (command == "Submit")
                    {
                        return RedirectToAction("Create", "CourceQualification", new { ID_course = course.ID });
                    }
                    else if (command == "Submit_center")
                    {
                        return RedirectToAction("Create", "Course_Centers", new { ID_course = course.ID });
                    }
                    else if (command == "Submit_skill")
                    {
                        return RedirectToAction("Create", "Course_skill", new { ID_course = course.ID });
                    }
                    else if (command == "Submit_type")
                    {
                        return RedirectToAction("Create", "Course_training_type", new { ID_course = course.ID });
                    }
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
            var quli = dbcontext.CourceQualification.Where(m => m.Course_Code == record.ID.ToString()).ToList();
            var center = dbcontext.Course_Centers.Where(m => m.Course_Code == record.ID.ToString()).ToList();
            var skill = dbcontext.Course_Skills.Where(m => m.Course_Code == record.ID.ToString()).ToList();
            var type = dbcontext.Course_TrainingType.Where(m => m.Course_Code == record.ID.ToString()).ToList();

            try
            {
                dbcontext.Cours.Remove(record);
                if(quli.Count>0)
                    dbcontext.CourceQualification.RemoveRange(quli);
                if (center.Count > 0)
                    dbcontext.Course_Centers.RemoveRange(center);
                if (skill.Count > 0)
                    dbcontext.Course_Skills.RemoveRange(skill);
                if (type.Count > 0)
                    dbcontext.Course_TrainingType.RemoveRange(type);
                dbcontext.SaveChanges();
                //=================================check for alert==================================

                var get_result_check = HR.Controllers.check.check_alert("course carde", HR.Models.user.Action.delete, HR.Models.user.type_field.form);
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