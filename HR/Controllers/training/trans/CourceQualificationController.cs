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
    public class CourceQualificationController : BaseController
    {
        // GET: CourceQualification
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
             
                ViewBag.quli_name = dbcontext.Name_of_educational_qualification.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.course = dbcontext.Cours.ToList().Select(m => new { Code = m.Course_Code + "-[" + m.Course_Desc + ']', ID = m.ID });
                ViewBag.spe1 = dbcontext.Qualification_Major.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });

                var new_model = new CourceQualification();
                if (ID_course != null)
                {
                    var cou_q= dbcontext.CourceQualification.Where(m => m.Course_Code == ID_course.ToString()).ToList();
                    if(cou_q.Count()>0)
                    {
                        return RedirectToAction("edit", new { id = ID_course });
                    }
                    var course = dbcontext.Cours.FirstOrDefault(m => m.ID == ID_course);
                    new_model.Course_Code=course.ID.ToString();
                  
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
        public ActionResult create(FormCollection form,CourceQualification model)
        {
            try
            {
                ViewBag.quli_name = dbcontext.Name_of_educational_qualification.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.course = dbcontext.Cours.ToList().Select(m => new { Code = m.Course_Code + "-[" + m.Course_Desc + ']', ID = m.ID });
                ViewBag.spe1 = dbcontext.Qualification_Major.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });

                //================================
                var quli_name = form["quli_id"].Split(',');
                var spec = form["spec_id"].Split(',');
                for (var i = 0; i < quli_name.Length; i++)
                {
                    if (quli_name[i] != "")
                    {
                        var ID = int.Parse(quli_name[i]);
                        var quli = dbcontext.Name_of_educational_qualification.FirstOrDefault(m => m.ID == ID);
                        var ID_spe = int.Parse(spec[i]);
                        var spe = dbcontext.Qualification_Major.FirstOrDefault(m => m.ID == ID_spe);
                        var Cource_Qualification = new CourceQualification { quli_name_des = quli.Code + "-" + quli.Name,QualificationName_Code=quli.ID.ToString(), quli_spec_des = spe.Code + "-" + quli.Name, QualificationSpecialty_Code = spe.ID.ToString(), Course_Code= model.Course_Code, Created_By = User.Identity.Name, Created_Date = DateTime.Now.Date };
                        dbcontext.CourceQualification.Add(Cource_Qualification);
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
                ViewBag.quli_name = dbcontext.Name_of_educational_qualification.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.course = dbcontext.Cours.ToList().Select(m => new { Code = m.Course_Code + "-[" + m.Course_Desc + ']', ID = m.ID });
                ViewBag.spe1 = dbcontext.Qualification_Major.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });

                var cours = dbcontext.Cours.FirstOrDefault(m => m.ID == id);
                var edit_model= new CourceQualification { Course_Code = cours.ID.ToString() };
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
        public ActionResult edit(CourceQualification model,FormCollection form, string course_code99)
        {
            try
            {
                ViewBag.quli_name = dbcontext.Name_of_educational_qualification.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.course = dbcontext.Cours.ToList().Select(m => new { Code = m.Course_Code + "-[" + m.Course_Desc + ']', ID = m.ID });
                ViewBag.spe1 = dbcontext.Qualification_Major.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                var old_course = course_code99;
                if (model.Course_Code==null ||model.Course_Code=="0" ||model.Course_Code=="")
                {

                    ViewBag.course_code5 = course_code99;
                    return View();
                }
                else
                {
                    ViewBag.course_code5 = model.Course_Code;
                }
                //================================
                var old_course_quli = dbcontext.CourceQualification.Where(m => m.Course_Code == old_course);///=======////////////
                if(old_course_quli!=null)
                {
                    dbcontext.CourceQualification.RemoveRange(old_course_quli);
                    dbcontext.SaveChanges();
                }
                //================================
                var quli_name = form["quli_id"].Split(',');
                var spec = form["spec_id"].Split(',');
                for (var i = 0; i < quli_name.Length; i++)
                {
                    if (quli_name[i] != "")
                    {
                        var ID = int.Parse(quli_name[i]);
                        var quli = dbcontext.Name_of_educational_qualification.FirstOrDefault(m => m.ID == ID);
                        var ID_spe = int.Parse(spec[i]);
                        var spe = dbcontext.Qualification_Major.FirstOrDefault(m => m.ID == ID_spe);
                        var Cource_Qualification = new CourceQualification { quli_name_des = quli.Code + "-" + quli.Name, QualificationName_Code = quli.ID.ToString(), quli_spec_des = spe.Code + "-" + quli.Name, QualificationSpecialty_Code = spe.ID.ToString(), Course_Code = model.Course_Code, Created_By = User.Identity.Name, Created_Date = DateTime.Now.Date };
                        dbcontext.CourceQualification.Add(Cource_Qualification);
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
            var quli = dbcontext.CourceQualification.Where(m => m.Course_Code ==id.ToString()).ToList();
            try
            {
                dbcontext.CourceQualification.RemoveRange(quli);
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
        public JsonResult get_spec(int id_quli)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            var spec = dbcontext.Qualification_Major.Where(m => m.Name_of_educational_qualificationid == id_quli).ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID }).ToList();
            return Json(spec);
        }
        public JsonResult getallcontent(string coures_code)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            var content = dbcontext.CourceQualification.Where(m => m.Course_Code == coures_code).ToList();
            return Json(content);
        }
    }
}