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
    [Authorize(Roles = "Admin,Organization,OrganizationSetup,Work location")]
    public class work_locationController : BaseController
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
            ViewBag.Shiftdaystatus = dbcontext.Shiftdaystatus.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
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
                ViewBag.Shiftdaystatus = dbcontext.Shiftdaystatus.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                if (ModelState.IsValid)
                {
                    work_location record = new work_location();
                   
                    record.Name = model.Name;
                    record.Description = model.Description;
                    record.Code = model.Code;
                    record.Location_name = model.Location_name;
                    record.Defaultdaystatuscode = model.Defaultdaystatuscode;
                    record.Absencedaystatus = model.Absencedaystatus;
                    var location = dbcontext.work_location.Add(record);
                    dbcontext.SaveChanges();
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
                ViewBag.Shiftdaystatus = dbcontext.Shiftdaystatus.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                var record = dbcontext.work_location.FirstOrDefault(m => m.ID == id);
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
        public ActionResult Edit(work_location model)
        {
            try
            {
                ViewBag.Shiftdaystatus = dbcontext.Shiftdaystatus.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                var record = dbcontext.work_location.FirstOrDefault(m => m.ID == model.ID);
                record.Name = model.Name;
                record.Description = model.Description;
                record.Code = model.Code;
                record.Location_name = model.Location_name;
                record.Defaultdaystatuscode = model.Defaultdaystatuscode;
                record.Absencedaystatus = model.Absencedaystatus;
                dbcontext.SaveChanges();
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
                var record = dbcontext.work_location.FirstOrDefault(m => m.ID == id);
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
            var record = dbcontext.work_location.FirstOrDefault(m => m.ID == id);

            try
            {
                dbcontext.work_location.Remove(record);
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
    }
}