using HR.Models;
using HR.Models.Infra;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers
{
    [Authorize]
    public class Allowance_yearsController : BaseController
    {
        // GET: Allowance_years
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Country
        public ActionResult Index()
        {
            var model = dbcontext.Allowance_years.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Allowance_years model )
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Allowance_years record = new Allowance_years();
                    record.Allowance_percentage = model.Allowance_percentage;
                    record.Allowance_year = model.Allowance_year;
                    record.max_Allowance_amount = model.max_Allowance_amount;
                    record.min_Allowance_amount = model.min_Allowance_amount;
                    dbcontext.Allowance_years.Add(record);
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
                var record = dbcontext.Allowance_years.FirstOrDefault(m => m.ID == id);
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = "there is no country";
                    return View();
                }
            }
            catch
            (Exception e)
            { return View(); }
        }
        [HttpPost]
        public ActionResult Edit(Allowance_years model)
        {
            try
            {
                var record = dbcontext.Allowance_years.FirstOrDefault(m => m.ID == model.ID);
                record.Allowance_percentage = model.Allowance_percentage;
                record.Allowance_year = model.Allowance_year;
                record.max_Allowance_amount = model.max_Allowance_amount;
                record.min_Allowance_amount = model.min_Allowance_amount;
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
                var record = dbcontext.Allowance_years.FirstOrDefault(m => m.ID == id);
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = "there is no country";
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
            var record = dbcontext.Allowance_years.FirstOrDefault(m => m.ID == id);

            try
            {
                dbcontext.Allowance_years.Remove(record);
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