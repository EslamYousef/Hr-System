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
    [Authorize(Roles = "Admin,payroll,payrollSetup,Transaction journal")]
    public class PayrollTransactionJournalSetupController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: PayrollTransactionJournalSetup
        public ActionResult Index()
        {
            var model = dbcontext.PayrollTransactionJournalSetup.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            //////
            ViewBag.salaryitem = dbcontext.salary_code.ToList().Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + ']', ID = m.ID });

            var modell = new PayrollTransactionJournalSetup ();

            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Payroll).Structure_Code;
            var model = dbcontext.PayrollTransactionJournalSetup.ToList();
            if (model.Count() == 0)
            {
                modell.JournalName_BatchCode = stru + "1";
            }
            else
            {
                modell.JournalName_BatchCode = stru + (model.LastOrDefault().ID + 1).ToString();
            }
            /////
            return View(modell);
        }
        [HttpPost]
        public ActionResult Create(PayrollTransactionJournalSetup model, int Type)
        {
            try
            {
                ViewBag.salaryitem = dbcontext.salary_code.ToList().Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + ']', ID = m.ID });
                if (ModelState.IsValid)
                {
                    PayrollTransactionJournalSetup record = new PayrollTransactionJournalSetup();
                    record.JournalName_BatchCode = model.JournalName_BatchCode;
                    record.JournalDesc = model.JournalDesc;
                    record.JournalAltDesc = model.JournalAltDesc;
                    record.JournalType = Type;
                    record.SalaryCodeID = model.SalaryCodeID;

                    dbcontext.PayrollTransactionJournalSetup.Add(record);
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
                ViewBag.salaryitem = dbcontext.salary_code.ToList().Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + ']', ID = m.ID });

                var record = dbcontext.PayrollTransactionJournalSetup.FirstOrDefault(m => m.ID == id);
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
        public ActionResult Edit(PayrollTransactionJournalSetup model, int Type)
        {
            try
            {
                ViewBag.salaryitem = dbcontext.salary_code.ToList().Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + ']', ID = m.ID });
                var record = dbcontext.PayrollTransactionJournalSetup.FirstOrDefault(m => m.ID == model.ID);
                record.JournalName_BatchCode = model.JournalName_BatchCode;
                record.JournalDesc = model.JournalDesc;
                record.JournalAltDesc = model.JournalAltDesc;
                record.JournalType = Type;
                record.SalaryCodeID = model.SalaryCodeID;
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
                var record = dbcontext.PayrollTransactionJournalSetup.FirstOrDefault(m => m.ID == id);
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
                var record = dbcontext.PayrollTransactionJournalSetup.FirstOrDefault(m => m.ID == id);
                dbcontext.PayrollTransactionJournalSetup.Remove(record);
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