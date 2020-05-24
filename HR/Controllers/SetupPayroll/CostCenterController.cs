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
    public class CostCenterController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: CostCenter
        public ActionResult Index()
        {
            var model = dbcontext.CostCenter.ToList();
            return View(model);
        }

        public ActionResult Create(string id)

        {
            ViewBag.CostCenterCategory = dbcontext.CostCenterCategory.ToList().Select(m => new { Code = m.CategoryCode + "------[" + m.CategoryDesc + ']', ID = m.ID });
            var modell = new CostCenter();
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Payroll).Structure_Code;
            var model = dbcontext.CostCenter.ToList();
            if (model.Count() == 0)
            {
                modell.CostCenterCode = stru + "1";
            }
            else
            {
                modell.CostCenterCode = stru + (model.LastOrDefault().ID + 1).ToString();
            }

            if (id != null)
            {
                var ID = int.Parse(id);
                var country = dbcontext.CostCenterCategory.FirstOrDefault(m => m.ID == ID);
                var modells = new CostCenter {CostCenterCode = modell.CostCenterCode ,CategoryCode = country.ID.ToString() };
                return View(modells);
            }
            return View(modell);
        }
        [HttpPost]
        public ActionResult Create(CostCenter model)
        {
            try
            {
                ViewBag.CostCenterCategory = dbcontext.CostCenterCategory.ToList().Select(m => new { Code = m.CategoryCode + "------[" + m.CategoryDesc + ']', ID = m.ID });
                if (ModelState.IsValid)
                {
                    if (model.CategoryCode == "0" || model.CategoryCode == null)
                    {
                        ModelState.AddModelError("", HR.Resource.Payroll.CategoryCodemustenter);
                        return View(model);
                    }
                    CostCenter record = new CostCenter();
                    record.CategoryCode = model.CategoryCode;
                    record.CostCenterCode = model.CostCenterCode;
                    record.CostCenterDesc = model.CostCenterDesc;
                    record.CostCenterAltDesc = model.CostCenterAltDesc;
                    record.CostCenterMask = model.CostCenterMask;
                    record.CostCenterMaskDesc = model.CostCenterMaskDesc;
                    var cost = dbcontext.CostCenter.Add(record);
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
                ViewBag.CostCenterCategory = dbcontext.CostCenterCategory.ToList().Select(m => new { Code = m.CategoryCode + "------[" + m.CategoryDesc + ']', ID = m.ID });
                var record = dbcontext.CostCenter.FirstOrDefault(m => m.ID == id);
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
        public ActionResult Edit(CostCenter model)
        {
            try
            {
                ViewBag.CostCenterCategory = dbcontext.CostCenterCategory.ToList().Select(m => new { Code = m.CategoryCode + "------[" + m.CategoryDesc + ']', ID = m.ID });
                if (model.CategoryCode == "0" || model.CategoryCode == null)
                {
                    ModelState.AddModelError("", HR.Resource.Payroll.CategoryCodemustenter);
                    return View(model);
                }
                var record = dbcontext.CostCenter.FirstOrDefault(m => m.ID == model.ID);
                record.CategoryCode = model.CategoryCode;
                record.CostCenterCode = model.CostCenterCode;
                record.CostCenterDesc = model.CostCenterDesc;
                record.CostCenterAltDesc = model.CostCenterAltDesc;
                record.CostCenterMask = model.CostCenterMask;
                record.CostCenterMaskDesc = model.CostCenterMaskDesc;
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
                var record = dbcontext.CostCenter.FirstOrDefault(m => m.ID == id);
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
                var record = dbcontext.CostCenter.FirstOrDefault(m => m.ID == id);
                dbcontext.CostCenter.Remove(record);
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