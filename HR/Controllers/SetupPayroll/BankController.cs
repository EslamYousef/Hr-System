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
    public class BankController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Bank
        public ActionResult Index()
        {
            var model = dbcontext.Bank.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            //////
            var modell = new Bank();

            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Payroll).Structure_Code;
            var model = dbcontext.Bank.ToList();
            if (model.Count() == 0)
            {
                modell.Bank_Code = stru + "1";
            }
            else
            {
                modell.Bank_Code = stru + (model.LastOrDefault().ID + 1).ToString();
            }
            /////
            return View(modell);
        }
        [HttpPost]
        public ActionResult Create(Bank model, string command)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Bank record = new Bank();
                    record.Bank_Code = model.Bank_Code;
                    record.Bank_Desc = model.Bank_Desc;
                    record.Bank_AltDesc = model.Bank_AltDesc;
                    record.Swift_Number = model.Swift_Number;
                    var cost = dbcontext.Bank.Add(record);
                    dbcontext.SaveChanges();
                    if (command == "Submit")
                    {
                        return RedirectToAction("Create", "Branch", new { id = cost.ID });
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
                var record = dbcontext.Bank.FirstOrDefault(m => m.ID == id);
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
        public ActionResult Edit(Bank model, string command)
        {
            try
            {
                var record = dbcontext.Bank.FirstOrDefault(m => m.ID == model.ID);
                record.Bank_Code = model.Bank_Code;
                record.Bank_Desc = model.Bank_Desc;
                record.Bank_AltDesc = model.Bank_AltDesc;
                record.Swift_Number = model.Swift_Number;
                dbcontext.SaveChanges();
                if (command == "Submit")
                {
                    return RedirectToAction("Create", "Branch", new { id = record.ID });
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
                var record = dbcontext.Bank.FirstOrDefault(m => m.ID == id);
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
                var record = dbcontext.Bank.FirstOrDefault(m => m.ID == id);
                dbcontext.Bank.Remove(record);
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