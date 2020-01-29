using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HR.Models;
using System.Data.Entity.Infrastructure;

namespace HR.Controllers
{
    public class DiseaseController : Controller
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Disease
        public ActionResult Index()
        {
            var model = dbcontext.Disease.ToList();
            return View(model);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Disease model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Disease record = new Disease();
                    record.Disease_Code = model.Disease_Code;
                    record.Disease_Name = model.Disease_Name;
                    record.Disease_TName = model.Disease_TName;
                    record.Notes = model.Notes;
                    dbcontext.Disease.Add(record);
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
                var record = dbcontext.Disease.FirstOrDefault(m => m.ID == id);
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = "There is no Disease";
                    return View();
                }
            }
            catch
            (Exception e)
            { return View(); }
        }
        [HttpPost]
        public ActionResult Edit(Disease model)
        {
            try
            {
                var record = dbcontext.Disease.FirstOrDefault(m => m.ID == model.ID);
                record.Disease_Code = model.Disease_Code;
                record.Disease_Name = model.Disease_Name;
                record.Disease_TName = model.Disease_TName;
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
                var record = dbcontext.Disease.FirstOrDefault(m => m.ID == id);
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = "There is no Disease"; ;
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
            var record = dbcontext.Disease.FirstOrDefault(m => m.ID == id);
            try
            {
                dbcontext.Disease.Remove(record);
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