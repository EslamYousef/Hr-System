using HR.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HR.Models.Infra;
using HR.Models.ViewModel;

namespace HR.Controllers
{
    public class Weekend_setupController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Weekend_setup
        public ActionResult Index()
        {
            var model = db.Weekend_setup.ToList();
            return View(model);
        }
        public ActionResult Create()
        {
            ViewBag.Shift_day_status_setup = db.Shift_day_status_setup.ToList().Select(m => new { Code= "" + m.Code + "-----[" + m.Description + ']' ,ID=m.ID});
            var stru = db.StructureModels.FirstOrDefault(a => a.All_Models == ChModels.Personnel);
            var model = db.Weekend_setup.ToList();
            var count = 0;
            if (model.Count()==0)
            {
                count = 1;
            }
            else
            {
              var te = model.LastOrDefault().ID;
                count = count + te;
            }
            var modell = new Weekend_setup { Code = stru.Structure_Code + count };
            return View(modell);
        }
        [HttpPost]
        public ActionResult Create(Weekend_setup model)
        {
            try
            {
                ViewBag.Shift_day_status_setup = db.Shift_day_status_setup.ToList().Select(m => new { Code = "" + m.Code + "-----[" + m.Description + ']', ID = m.ID });
                if (ModelState.IsValid)
                {
                    Weekend_setup recode = new Weekend_setup();
                    recode.Code = model.Code;
                    recode.Description = model.Description;
                    recode.Alternative_Description = model.Alternative_Description;
                    recode.ShiftdaystatussetupId = model.ShiftdaystatussetupId;
                    recode.Saturday = model.Saturday;
                    recode.Friday = model.Friday;
                    recode.Sunday = model.Sunday;
                    recode.Monday = model.Monday;
                    recode.Tuesday = model.Tuesday;
                    recode.Wednesday = model.Wednesday;
                    recode.Thursday = model.Thursday;
                    db.Weekend_setup.Add(recode);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(model);
                }
            }
            catch(DbUpdateException e)
            {
                TempData["Message"] = HR.Resource.Basic.thiscodeIsalreadyexists;
                return View(model);
            }
            catch(Exception e)
            {
                return View(model);
            }
        }
        public ActionResult Edit(int Id)
        {
            try
            {
                ViewBag.Shift_day_status_setup = db.Shift_day_status_setup.ToList().Select(m => new { Code = "" + m.Code + "-----[" + m.Description + ']', ID = m.ID });
                var recode = db.Weekend_setup.FirstOrDefault(a => a.ID == Id);
                if (recode != null)
                {
                    return View(recode);
                }
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
        [HttpPost]
        public ActionResult Edit(Weekend_setup model)
        {
            ViewBag.Shift_day_status_setup = db.Shift_day_status_setup.ToList().Select(m => new { Code = "" + m.Code + "-----[" + m.Description + ']', ID = m.ID });
            try
            {
                var recode = db.Weekend_setup.FirstOrDefault(a => a.ID == model.ID);
                recode.Code = model.Code;
                recode.Description = model.Description;
                recode.Alternative_Description = model.Alternative_Description;
                recode.ShiftdaystatussetupId = model.ShiftdaystatussetupId;
                recode.Saturday = model.Saturday;
                recode.Friday = model.Friday;
                recode.Sunday = model.Sunday;
                recode.Monday = model.Monday;
                recode.Tuesday = model.Tuesday;
                recode.Wednesday = model.Wednesday;
                recode.Thursday = model.Thursday;
                db.SaveChanges();
                return RedirectToAction("index");
            }
            catch (DbUpdateException e)
            {
                TempData["Message"] = HR.Resource.Basic.thiscodeIsalreadyexists;
                return View(model);
            }
            catch (Exception e)
            { return View(model); }
        }
        public ActionResult Delete(int id)
        {
            var old = db.Weekend_setup.FirstOrDefault(a => a.ID == id);
            try
            {
                if (old != null)
                {
                    return View(old);
                }
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
        public ActionResult Deletemethos(int id)
        {
            try
            {
                var old = db.Weekend_setup.FirstOrDefault(a => a.ID == id);
                db.Weekend_setup.Remove(old);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }

        }

    }
}