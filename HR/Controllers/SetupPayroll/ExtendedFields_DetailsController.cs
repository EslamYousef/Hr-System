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
    [Authorize(Roles = "Admin,payroll,payrollSetup,Extended Fields Details")]

    public class ExtendedFields_DetailsController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: ExtendedFields_Details
        public ActionResult Index()
        {
            var model = dbcontext.ExtendedFields_Details.ToList();
            return View(model);
        }

        public ActionResult Create(string id)

        {
            ViewBag.ExtendedFields_Header = dbcontext.ExtendedFields_Header.ToList().Select(m => new { Code = m.ExtendedFields_Code + "------[" + m.ExtendedFields_Desc + ']', ID = m.ID });
            var modell = new ExtendedFields_Details { Value=0};
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Payroll).Structure_Code;
            var model = dbcontext.ExtendedFields_Details.ToList();
            if (model.Count() == 0)
            {
                modell.Detail_Code = stru + "1";
            }
            else
            {
                modell.Detail_Code = stru + (model.LastOrDefault().ID + 1).ToString();
            }

            if (id != null)
            {
                var ID = int.Parse(id);
                var ExtendedFields_Details = dbcontext.ExtendedFields_Header.FirstOrDefault(m => m.ID == ID);
                var modells = new ExtendedFields_Details { Detail_Code = modell.Detail_Code, ExtendedFields_Code = ExtendedFields_Details.ID.ToString() , Value = 0 };
                return View(modells);
            }
            return View(modell);
        }
        [HttpPost]
        public ActionResult Create(ExtendedFields_Details model ,short Type)
        {
            try
            {
                ViewBag.ExtendedFields_Header = dbcontext.ExtendedFields_Header.ToList().Select(m => new { Code = m.ExtendedFields_Code + "------[" + m.ExtendedFields_Desc + ']', ID = m.ID });
                if (ModelState.IsValid)
                {
                    if (model.ExtendedFields_Code == "0" || model.ExtendedFields_Code == null)
                    {
                        ModelState.AddModelError("", HR.Resource.Payroll.ExtendedFieldsCodemustenter);
                        return View(model);
                    }
                    ExtendedFields_Details record = new ExtendedFields_Details();
                    record.ExtendedFields_Code = model.ExtendedFields_Code;
                    record.Detail_Code = model.Detail_Code;
                    record.Detail_AltDesc = model.Detail_AltDesc;
                    record.Detail_Desc = model.Detail_Desc;
                    record.Value = model.Value;
                    record.ValueType = Type;
                     dbcontext.ExtendedFields_Details.Add(record);
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
                ViewBag.ExtendedFields_Header = dbcontext.ExtendedFields_Header.ToList().Select(m => new { Code = m.ExtendedFields_Code + "------[" + m.ExtendedFields_Desc + ']', ID = m.ID });
                var record = dbcontext.ExtendedFields_Details.FirstOrDefault(m => m.ID == id);
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
        public ActionResult Edit(ExtendedFields_Details model, short Type)
        {
            try
            {
                ViewBag.ExtendedFields_Header = dbcontext.ExtendedFields_Header.ToList().Select(m => new { Code = m.ExtendedFields_Code + "------[" + m.ExtendedFields_Desc + ']', ID = m.ID });
                if (model.ExtendedFields_Code == "0" || model.ExtendedFields_Code == null)
                {
                    ModelState.AddModelError("", HR.Resource.Payroll.ExtendedFieldsCodemustenter);
                    return View(model);
                }
                var record = dbcontext.ExtendedFields_Details.FirstOrDefault(m => m.ID == model.ID);
                record.ExtendedFields_Code = model.ExtendedFields_Code;
                record.Detail_Code = model.Detail_Code;
                record.Detail_AltDesc = model.Detail_AltDesc;
                record.Detail_Desc = model.Detail_Desc;
                record.Value = model.Value;
                record.ValueType = Type;
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
                var record = dbcontext.ExtendedFields_Details.FirstOrDefault(m => m.ID == id);
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
                var record = dbcontext.ExtendedFields_Details.FirstOrDefault(m => m.ID == id);
                dbcontext.ExtendedFields_Details.Remove(record);
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