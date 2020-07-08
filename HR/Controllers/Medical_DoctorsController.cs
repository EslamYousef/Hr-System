using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HR.Models;
using System.Data.Entity.Infrastructure;
using HR.Models.Infra;

namespace HR.Controllers
{
    [Authorize]
    public class Medical_DoctorsController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Medical_Doctors
        public ActionResult Index()
        {
            var model = dbcontext.Medical_Doctors.ToList();
            return View(model);
        }
        public ActionResult Create(string id)
        {
            ViewBag.Medical_Doctors_Level = dbcontext.Medical_Doctors_Level.ToList().Select(m => new { Code = m.Level_Code + "-----[" + m.Level_Name + ']', ID = m.ID });
            ViewBag.Employee_Profile = dbcontext.Employee_Profile.Where(a=>a.Active == true).ToList().Select(m => new { Code = m.Code + "-----[" + m.Name + ']', ID = m.ID });

            //if (id != null)
            //{
            //    var ID = int.Parse(id);
            //    var Medical_Doctors_Level = dbcontext.Medical_Doctors_Level.FirstOrDefault(m => m.ID == ID);
            //    var models = new Medical_Doctors { Medical_Doctors_Level = Medical_Doctors_Level, Medical_Doctors_LevelId = Medical_Doctors_Level.ID.ToString() };
            //    return View(models);
            //}
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Medical);
            var model = dbcontext.Medical_Doctors.ToList();
            var count = 0;
            if (model.Count() == 0)
            {
                count = 1;
            }
            else
            {
                var te = model.LastOrDefault().ID;
                count = te + 1;
            }

            var model_ = new Medical_Doctors { Code = stru.Structure_Code + count };
            return View(model_);
        }
        [HttpPost]
        public ActionResult Create(Medical_Doctors model, string command)
        {
            try
            {
                ViewBag.Medical_Doctors_Level = dbcontext.Medical_Doctors_Level.ToList().Select(m => new { Code = m.Level_Code + "-----[" + m.Level_Name + ']', ID = m.ID });
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true).ToList().Select(m => new { Code = m.Code + "-----[" + m.Name + ']', ID = m.ID });

                if (ModelState.IsValid)
                {
                    if (model.Medical_Doctors_LevelId == "0" || model.Medical_Doctors_LevelId == null)
                    {
                        ModelState.AddModelError("", "Medical Doctors Level Code must enter");
                        return View(model);
                    }
                    Medical_Doctors record = new Medical_Doctors();
                    record.Code = model.Code;
                    record.Doctor_Name = model.Doctor_Name;
                    record.Doctor_TName = model.Doctor_TName;
                    record.Notes = model.Notes;
                    record.EmpCode = model.EmpCode;
                    record.Medical_Doctors_LevelId = model.Medical_Doctors_LevelId;
                    var Medical_Doctors_LevelId = int.Parse(model.Medical_Doctors_LevelId);
                    record.Medical_Doctors_Level = dbcontext.Medical_Doctors_Level.FirstOrDefault(m => m.ID == Medical_Doctors_LevelId);
                    dbcontext.Medical_Doctors.Add(record);
                    dbcontext.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(model);
                }
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = "this code Is already exists";
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
                ViewBag.Medical_Doctors_Level = dbcontext.Medical_Doctors_Level.ToList().Select(m => new { Code = m.Level_Code + "-----[" + m.Level_Name + ']', ID = m.ID });
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true).ToList().Select(m => new { Code = m.Code + "-----[" + m.Name + ']', ID = m.ID });

                var record = dbcontext.Medical_Doctors.FirstOrDefault(m => m.ID == id);
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = "There is no Medical Doctors Level";
                    return View();
                }
            }
            catch
            (Exception e)
            { return View(); }
        }
        [HttpPost]
        public ActionResult Edit(Medical_Doctors model)
        {
            try
            {
                ViewBag.Medical_Doctors_Level = dbcontext.Medical_Doctors_Level.ToList().Select(m => new { Code = m.Level_Code + "-----[" + m.Level_Name + ']', ID = m.ID });
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true).ToList().Select(m => new { Code = m.Code + "-----[" + m.Name + ']', ID = m.ID });

                if (model.Medical_Doctors_LevelId == "0" || model.Medical_Doctors_LevelId == null)
                {
                    ModelState.AddModelError("", "Medical Doctors Level Code must enter");
                    return View(model);
                }
                var record = dbcontext.Medical_Doctors.FirstOrDefault(m => m.ID == model.ID);
                record.Code = model.Code;
                record.Doctor_Name = model.Doctor_Name;
                record.Doctor_TName = model.Doctor_TName;
                record.EmpCode = model.EmpCode;

                record.Notes = model.Notes;
                record.Medical_Doctors_LevelId = model.Medical_Doctors_LevelId;
                var Medical_Doctors_LevelId = int.Parse(model.Medical_Doctors_LevelId);
                record.Medical_Doctors_Level = dbcontext.Medical_Doctors_Level.FirstOrDefault(m => m.ID == Medical_Doctors_LevelId);

                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = "This code Is already exists";
                return View(model);
            }
            catch (Exception e)
            { return View(model); }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                var record = dbcontext.Medical_Doctors.FirstOrDefault(m => m.ID == id);
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = "There is no Medical Doctors";
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
            var record = dbcontext.Medical_Doctors.FirstOrDefault(m => m.ID == id);

            try
            {
                dbcontext.Medical_Doctors.Remove(record);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = "You can't delete beacause it have child";
                return View(record);
            }
            catch (Exception e)
            {
                return View();
            }
        }


    }
}