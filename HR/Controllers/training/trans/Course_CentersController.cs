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
    public class Course_CentersController : BaseController
    {
        // GET: Course_Centers
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

                ViewBag.centers = dbcontext.TrainingCenter.ToList().Select(m => new { Code = m.TrainingCenters_Code + "-[" + m.TrainingCenters_Desc + ']', ID = m.ID });
                ViewBag.course = dbcontext.Cours.ToList().Select(m => new { Code = m.Course_Code + "-[" + m.Course_Desc + ']', ID = m.ID });

                var new_model = new Course_Centers();
                if (ID_course != null)
                {
                    var cou_q = dbcontext.Course_Centers.Where(m => m.Course_Code == ID_course.ToString()).ToList();
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
        public ActionResult create(FormCollection form, Course_Centers model)
        {
            try
            {
                ViewBag.centers = dbcontext.TrainingCenter.ToList().Select(m => new { Code = m.TrainingCenters_Code + "-[" + m.TrainingCenters_Desc + ']', ID = m.ID });
                ViewBag.course = dbcontext.Cours.ToList().Select(m => new { Code = m.Course_Code + "-[" + m.Course_Desc + ']', ID = m.ID });

                //================================
                var center_D = form["center_id"].Split(',');
                for (var i = 0; i < center_D.Length; i++)
                {
                    if (center_D[i] != "")
                    {
                        var ID = int.Parse(center_D[i]);
                        var CENTER = dbcontext.TrainingCenter.FirstOrDefault(m => m.ID == ID);
                        var Cource_cen = new Course_Centers {Course_Code=model.Course_Code, center_des = CENTER.TrainingCenters_Code + "-" + CENTER.TrainingCenters_Desc, TrainingCenters_Code = CENTER.ID.ToString(), Created_By = User.Identity.Name, Created_Date = DateTime.Now.Date };
                        dbcontext.Course_Centers.Add(Cource_cen);
                        dbcontext.SaveChanges();
                    }
                }
                //================================

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
                ViewBag.centers = dbcontext.TrainingCenter.ToList().Select(m => new { Code = m.TrainingCenters_Code + "-[" + m.TrainingCenters_Desc + ']', ID = m.ID });
                ViewBag.course = dbcontext.Cours.ToList().Select(m => new { Code = m.Course_Code + "-[" + m.Course_Desc + ']', ID = m.ID });

                var cours = dbcontext.Cours.FirstOrDefault(m => m.ID == id);
                var edit_model = new Course_Centers { Course_Code = cours.ID.ToString() };
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
        public ActionResult edit(Course_Centers model, FormCollection form, string course_code99)
        {
            try
            {
                ViewBag.centers = dbcontext.TrainingCenter.ToList().Select(m => new { Code = m.TrainingCenters_Code + "-[" + m.TrainingCenters_Desc + ']', ID = m.ID });
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
                var old_course_center = dbcontext.Course_Centers.Where(m => m.Course_Code == old_course);
                if (old_course_center != null)
                {
                    dbcontext.Course_Centers.RemoveRange(old_course_center);
                    dbcontext.SaveChanges();
                }
                //================================
                var center_D = form["center_id"].Split(',');
                
                for (var i = 0; i < center_D.Length; i++)
                {
                    if (center_D[i] != "")
                    {
                        var ID = int.Parse(center_D[i]);
                        var center = dbcontext.TrainingCenter.FirstOrDefault(m => m.ID == ID);
                        var Cource_cen = new Course_Centers { Course_Code = model.Course_Code, center_des = center.TrainingCenters_Code + "-" + center.TrainingCenters_Desc, TrainingCenters_Code = center.ID.ToString(), Created_By = User.Identity.Name, Created_Date = DateTime.Now.Date };
                        dbcontext.Course_Centers.Add(Cource_cen);
                        dbcontext.SaveChanges();
                    }
                }
                //================================

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
            var centers = dbcontext.Course_Centers.Where(m => m.Course_Code == id.ToString()).ToList();
            try
            {
                dbcontext.Course_Centers.RemoveRange(centers);
                dbcontext.SaveChanges();
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
            var content = dbcontext.Course_Centers.Where(m => m.Course_Code == coures_code).ToList();
            return Json(content);
        }
    }
}