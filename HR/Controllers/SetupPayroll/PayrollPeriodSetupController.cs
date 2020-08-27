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
    [Authorize(Roles = "Admin,payroll,payrollSetup,payroll Periodic")]
    public class PayrollPeriodSetupController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: PayrollPeriodSetup
        public ActionResult Index()
        {
            var model = dbcontext.PayrollPeriodSetup.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            //////
            var modell = new PayrollPeriodSetup { StartPayMonthFromDay = 1,EndPayMonthToDay =1 ,NumberOfDays =1};

            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Payroll).Structure_Code;
            var model = dbcontext.PayrollPeriodSetup.ToList();
            if (model.Count() == 0)
            {
                modell.PeriodCode = stru + "1";
            }
            else
            {
                modell.PeriodCode = stru + (model.LastOrDefault().ID + 1).ToString();
            }
            /////
            return View(modell);
        }
        [HttpPost]
        public ActionResult Create(PayrollPeriodSetup model,int Type)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PayrollPeriodSetup record = new PayrollPeriodSetup();
                    record.PeriodCode = model.PeriodCode;
                    record.PeriodDesc = model.PeriodDesc;
                    record.PeriodAltDesc = model.PeriodAltDesc;
                    
                    record.PeriodType = Type;
                    record.NumberOfDays = model.NumberOfDays;
                    record.StartPayMonthFromDay = model.StartPayMonthFromDay;
                    record.EndPayMonthToDay = model.EndPayMonthToDay;

                    dbcontext.PayrollPeriodSetup.Add(record);
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
                var record = dbcontext.PayrollPeriodSetup.FirstOrDefault(m => m.ID == id);
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
        public ActionResult Edit(PayrollPeriodSetup model,int Type)
        {
            try
            {
                var record = dbcontext.PayrollPeriodSetup.FirstOrDefault(m => m.ID == model.ID);
                record.PeriodCode = model.PeriodCode;
                record.PeriodDesc = model.PeriodDesc;
                record.PeriodAltDesc = model.PeriodAltDesc;
                record.PeriodType = Type;
                record.NumberOfDays = model.NumberOfDays;
                record.StartPayMonthFromDay = model.StartPayMonthFromDay;
                record.EndPayMonthToDay = model.EndPayMonthToDay;
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
                var record = dbcontext.PayrollPeriodSetup.FirstOrDefault(m => m.ID == id);
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
                var record = dbcontext.PayrollPeriodSetup.FirstOrDefault(m => m.ID == id);
                dbcontext.PayrollPeriodSetup.Remove(record);
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