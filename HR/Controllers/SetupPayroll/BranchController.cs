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
    public class BranchController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Branch
        public ActionResult Index()
        {
            var model = dbcontext.Branch.ToList();
            return View(model);
        }

        public ActionResult Create(string id)

        {
            ViewBag.Bank = dbcontext.Bank.ToList().Select(m => new { Code = m.Bank_Code + "------[" + m.Bank_Desc + ']', ID = m.ID });
            var modell = new Branch();
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Payroll).Structure_Code;
            var model = dbcontext.Branch.ToList();
            if (model.Count() == 0)
            {
                modell.Branch_Code = stru + "1";
            }
            else
            {
                modell.Branch_Code = stru + (model.LastOrDefault().ID + 1).ToString();
            }

            if (id != null)
            {
                var ID = int.Parse(id);
                var Bank = dbcontext.Bank.FirstOrDefault(m => m.ID == ID);
                var modells = new Branch { Branch_Code = modell.Branch_Code, Bank_Code = Bank.ID.ToString() };
                return View(modells);
            }
            return View(modell);
        }
        [HttpPost]
        public ActionResult Create(Branch model)
        {
            try
            {
                ViewBag.Bank = dbcontext.Bank.ToList().Select(m => new { Code = m.Bank_Code + "------[" + m.Bank_Desc + ']', ID = m.ID });
                if (ModelState.IsValid)
                {
                    if (model.Bank_Code == "0" || model.Bank_Code == null)
                    {
                        ModelState.AddModelError("", HR.Resource.Payroll.BankCodemustenter);
                        return View(model);
                    }
                    Branch record = new Branch();
                    record.Bank_Code = model.Bank_Code;
                    record.Branch_Code = model.Branch_Code;
                    record.Branch_Desc = model.Branch_Desc;
                    record.Branch_AltDesc = model.Branch_AltDesc;
                    record.Swift_Number = model.Swift_Number;
                    var cost = dbcontext.Branch.Add(record);
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
                ViewBag.Bank = dbcontext.Bank.ToList().Select(m => new { Code = m.Bank_Code + "------[" + m.Bank_Desc + ']', ID = m.ID });
                var record = dbcontext.Branch.FirstOrDefault(m => m.ID == id);
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
        public ActionResult Edit(Branch model)
        {
            try
            {
                ViewBag.Bank = dbcontext.Bank.ToList().Select(m => new { Code = m.Bank_Code + "------[" + m.Bank_Desc + ']', ID = m.ID });
                if (model.Bank_Code == "0" || model.Bank_Code == null)
                {
                    ModelState.AddModelError("", HR.Resource.Payroll.BankCodemustenter);
                    return View(model);
                }
                var record = dbcontext.Branch.FirstOrDefault(m => m.ID == model.ID);
                record.Bank_Code = model.Bank_Code;
                record.Branch_Code = model.Branch_Code;
                record.Branch_Desc = model.Branch_Desc;
                record.Branch_AltDesc = model.Branch_AltDesc;
                record.Swift_Number = model.Swift_Number;
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
                var record = dbcontext.Branch.FirstOrDefault(m => m.ID == id);
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
                var record = dbcontext.Branch.FirstOrDefault(m => m.ID == id);
                dbcontext.Branch.Remove(record);
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