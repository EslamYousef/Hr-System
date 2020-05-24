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
    public class CostCenterCategoryController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: CostCenterCategory
        public ActionResult Index()
        {
            var model = dbcontext.CostCenterCategory.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            //////
            var modell = new CostCenterCategory();

            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Payroll).Structure_Code;
            var model = dbcontext.CostCenterCategory.ToList();
            if (model.Count() == 0)
            {
                modell.CategoryCode = stru + "1";
            }
            else
            {
                modell.CategoryCode = stru + (model.LastOrDefault().ID + 1).ToString();
            }
            /////
            return View(modell);
        }
        [HttpPost]
        public ActionResult Create(CostCenterCategory model,string command)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CostCenterCategory record = new CostCenterCategory();
                    record.CategoryCode = model.CategoryCode;
                    record.CategoryDesc = model.CategoryDesc;
                    record.CategoryAltDesc = model.CategoryAltDesc;
                var cost=    dbcontext.CostCenterCategory.Add(record);
                    dbcontext.SaveChanges();
                    if (command == "Submit")
                    {
                        return RedirectToAction("Create", "CostCenter", new { id = cost.ID });
                    }
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
                var record = dbcontext.CostCenterCategory.FirstOrDefault(m => m.ID == id);
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
        public ActionResult Edit(CostCenterCategory model,string command)
        {
            try
            {
                var record = dbcontext.CostCenterCategory.FirstOrDefault(m => m.ID == model.ID);
                record.CategoryCode = model.CategoryCode;
                record.CategoryDesc = model.CategoryDesc;
                record.CategoryAltDesc = model.CategoryAltDesc;
                dbcontext.SaveChanges();
                if (command == "Submit")
                {
                    return RedirectToAction("Create", "CostCenter", new { id = record.ID });
                }
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
                var record = dbcontext.CostCenterCategory.FirstOrDefault(m => m.ID == id);
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
                var record = dbcontext.CostCenterCategory.FirstOrDefault(m => m.ID == id);
                dbcontext.CostCenterCategory.Remove(record);
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