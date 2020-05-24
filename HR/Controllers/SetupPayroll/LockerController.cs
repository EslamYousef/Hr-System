using HR.Models;
using HR.Models.Infra;
using HR.Models.SetupPayroll;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers.SetupPayroll
{
    [Authorize]
    public class LockerController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Locker
        public ActionResult Index()
        {
            var model = dbcontext.Locker.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            //////
            var modell = new Locker ();

            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Payroll).Structure_Code;
            var model = dbcontext.Locker.ToList();
            if (model.Count() == 0)
            {
                modell.Locker_Code = stru + "1";
            }
            else
            {
                modell.Locker_Code = stru + (model.LastOrDefault().ID + 1).ToString();
            }
            /////
            return View(modell);
        }
        [HttpPost]
        public ActionResult Create(Locker model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Locker record = new Locker();
                    record.Locker_Code = model.Locker_Code;
                    record.Locker_AltDesc = model.Locker_AltDesc;
                    record.Locker_Desc = model.Locker_Desc;
                    dbcontext.Locker.Add(record);
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
                var record = dbcontext.Locker.FirstOrDefault(m => m.ID == id);
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
        public ActionResult Edit(Locker model)
        {
            try
            {
                var record = dbcontext.Locker.FirstOrDefault(m => m.ID == model.ID);
                record.Locker_Code = model.Locker_Code;
                record.Locker_AltDesc = model.Locker_AltDesc;
                record.Locker_Desc = model.Locker_Desc;
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
                var record = dbcontext.Locker.FirstOrDefault(m => m.ID == id);
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
            try
            {
                var record = dbcontext.Locker.FirstOrDefault(m => m.ID == id);
                dbcontext.Locker.Remove(record);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (Exception e)
            {
                return View();
            }
        }
    }
}