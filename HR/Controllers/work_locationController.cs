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
    public class work_locationController : Controller
    {
        // GET: work_location
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Country
        public ActionResult Index()
        {
            var model = dbcontext.work_location.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            //////
            var modell = new work_location();

            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Organization).Structure_Code;
            var model = dbcontext.work_location.ToList();
            if (model.Count() == 0)
            {
                modell.Code = stru + "1";
            }
            else
            {
                modell.Code = stru + (model.LastOrDefault().ID + 1).ToString();
            }
            /////
            return View(modell);
        }
        [HttpPost]
        public ActionResult Create(work_location model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    work_location record = new work_location();
                    record.Name = model.Name;
                    record.Description = model.Description;
                    record.Code = model.Code;
                    record.Location_name = model.Location_name;
                    var location = dbcontext.work_location.Add(record);
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
                var record = dbcontext.work_location.FirstOrDefault(m => m.ID == id);
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = "there is no Data";
                    return View();
                }
            }
            catch
            (Exception e)
            { return View(); }
        }
        [HttpPost]
        public ActionResult Edit(work_location model)
        {
            try
            {
                var record = dbcontext.work_location.FirstOrDefault(m => m.ID == model.ID);
                record.Name = model.Name;
                record.Description = model.Description;
                record.Code = model.Code;
                record.Location_name = model.Location_name;
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
                var record = dbcontext.work_location.FirstOrDefault(m => m.ID == id);
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = "there is no Data";
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
            var record = dbcontext.work_location.FirstOrDefault(m => m.ID == id);

            try
            {
                dbcontext.work_location.Remove(record);
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
                return View(record);
            }
        }
    }
}