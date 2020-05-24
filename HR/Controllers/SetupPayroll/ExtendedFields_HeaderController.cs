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
    public class ExtendedFields_HeaderController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: ExtendedFields_Header
        public ActionResult Index()
        {
            var model = dbcontext.ExtendedFields_Header.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            //////
            var modell = new ExtendedFields_Header();

            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Payroll).Structure_Code;
            var model = dbcontext.ExtendedFields_Header.ToList();
            if (model.Count() == 0)
            {
                modell.ExtendedFields_Code = stru + "1";
            }
            else
            {
                modell.ExtendedFields_Code = stru + (model.LastOrDefault().ID + 1).ToString();
            }
            /////
            return View(modell);
        }
        [HttpPost]
        public ActionResult Create(ExtendedFields_Header model, string command, short Type)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ExtendedFields_Header record = new ExtendedFields_Header();
                    record.ExtendedFields_Code = model.ExtendedFields_Code;
                    record.ExtendedFields_Desc = model.ExtendedFields_Desc;
                    record.ExtendedFields_AltDesc = model.ExtendedFields_AltDesc;
                    record.Purpose = Type;
                    var cost = dbcontext.ExtendedFields_Header.Add(record);
                    dbcontext.SaveChanges();
                    if (command == "Submit")
                    {
                        return RedirectToAction("Create", "ExtendedFields_Details", new { id = cost.ID });
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
                var record = dbcontext.ExtendedFields_Header.FirstOrDefault(m => m.ID == id);
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
        public ActionResult Edit(ExtendedFields_Header model, string command, short Type)
        {
            try
            {
                var record = dbcontext.ExtendedFields_Header.FirstOrDefault(m => m.ID == model.ID);
                record.ExtendedFields_Code = model.ExtendedFields_Code;
                record.ExtendedFields_Desc = model.ExtendedFields_Desc;
                record.ExtendedFields_AltDesc = model.ExtendedFields_AltDesc;
                record.Purpose = Type;
                dbcontext.SaveChanges();
                if (command == "Submit")
                {
                    return RedirectToAction("Create", "ExtendedFields_Details", new { id = record.ID });
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
                var record = dbcontext.ExtendedFields_Header.FirstOrDefault(m => m.ID == id);
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
                var record = dbcontext.ExtendedFields_Header.FirstOrDefault(m => m.ID == id);
                dbcontext.ExtendedFields_Header.Remove(record);
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