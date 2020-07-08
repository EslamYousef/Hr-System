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
    public class Medical_Doctors_LevelController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Medical_Doctors_Level
        public ActionResult Index()
        {
            var model = dbcontext.Medical_Doctors_Level.ToList();
            return View(model);
        }
        public ActionResult Create()
        {
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Medical);
            var model = dbcontext.Medical_Doctors_Level.ToList();
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

            var model_ = new Medical_Doctors_Level { Level_Code = stru.Structure_Code + count };
            return View(model_);
        }
        [HttpPost]
        public ActionResult Create(Medical_Doctors_Level model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Medical_Doctors_Level record = new Medical_Doctors_Level();
                    record.Level_Name = model.Level_Name;
                    record.Level_Code = model.Level_Code;
                    record.Notes = model.Notes;
                    dbcontext.Medical_Doctors_Level.Add(record);
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
                var record = dbcontext.Medical_Doctors_Level.FirstOrDefault(m => m.ID == id);
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
        public ActionResult Edit(Medical_Doctors_Level model)
        {
            try
            {
                var record = dbcontext.Medical_Doctors_Level.FirstOrDefault(m => m.ID == model.ID);
                record.Level_Name = model.Level_Name;
                record.Level_Code = model.Level_Code;
                record.Notes = model.Notes;
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = "this code Is already exists";
                return View(model);
            }
            catch (Exception e)
            { return View(model); }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                var record = dbcontext.Medical_Doctors_Level.FirstOrDefault(m => m.ID == id);
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = "There is no Medical Doctors Level"; ;
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
            var record = dbcontext.Medical_Doctors_Level.FirstOrDefault(m => m.ID == id);
            try
            {
                dbcontext.Medical_Doctors_Level.Remove(record);
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