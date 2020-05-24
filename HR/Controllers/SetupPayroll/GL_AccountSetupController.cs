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
    public class GL_AccountSetupController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: GL_AccountSetup
        public ActionResult Index()
        {
            var model = dbcontext.GL_AccountSetup.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            //////
            var modell = new GL_AccountSetup();

            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Payroll).Structure_Code;
            var model = dbcontext.GL_AccountSetup.ToList();
            if (model.Count() == 0)
            {
                modell.AccountNumber = stru + "1";
            }
            else
            {
                modell.AccountNumber = stru + (model.LastOrDefault().ID + 1).ToString();
            }
            /////
            return View(modell);
        }
        [HttpPost]
        public ActionResult Create(GL_AccountSetup model, int Type)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    GL_AccountSetup record = new GL_AccountSetup();
                    record.AccountNumber = model.AccountNumber;
                    record.AccountName = model.AccountName;
                    record.AccountType = Type;

                    dbcontext.GL_AccountSetup.Add(record);
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
                var record = dbcontext.GL_AccountSetup.FirstOrDefault(m => m.ID == id);
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
        public ActionResult Edit(GL_AccountSetup model, int Type)
        {
            try
            {
                var record = dbcontext.GL_AccountSetup.FirstOrDefault(m => m.ID == model.ID);
                record.AccountNumber = model.AccountNumber;
                record.AccountName = model.AccountName;
                record.AccountType = Type;
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
                var record = dbcontext.GL_AccountSetup.FirstOrDefault(m => m.ID == id);
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
                var record = dbcontext.GL_AccountSetup.FirstOrDefault(m => m.ID == id);
                dbcontext.GL_AccountSetup.Remove(record);
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