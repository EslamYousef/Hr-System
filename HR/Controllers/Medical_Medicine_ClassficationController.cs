using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HR.Models;
using System.Data.Entity.Infrastructure;
using HR.Models.Infra;

namespace HR.Controllers
{
    [Authorize]
    public class Medical_Medicine_ClassficationController : BaseController
    {
        // GET: Medical_Medicine_Classfication
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        public ActionResult Index()
        {
            var model = dbcontext.Medical_Medicine_Classfication.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Medical);
            var model = dbcontext.Medical_Medicine_Classfication.ToList();
            var count = 0;
            if (model.Count() == 0)
            {
                count = 1;
            }
            else
            {
                var te = model.LastOrDefault().ID;
                count = te + 1;
            }

            var modell = new Medical_Medicine_Classfication { Code = stru.Structure_Code + count };
            return View(modell);
        }
        [HttpPost]
        public ActionResult Create(Medical_Medicine_Classfication model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Medical_Medicine_Classfication record = new Medical_Medicine_Classfication();
                    record.Name = model.Name;                  
                    record.Code = model.Code;
                    dbcontext.Medical_Medicine_Classfication.Add(record);
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
                var record = dbcontext.Medical_Medicine_Classfication.FirstOrDefault(m => m.ID == id);
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = "there is no Medical Medicine Classfication";
                    return View();
                }
            }
            catch
            (Exception e)
            { return View(); }
        }
        [HttpPost]
        public ActionResult Edit(Medical_Medicine_Classfication model)
        {
            try
            {
                var record = dbcontext.Medical_Medicine_Classfication.FirstOrDefault(m => m.ID == model.ID);
                record.Name = model.Name;
                record.Code = model.Code;
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
                var record = dbcontext.Medical_Medicine_Classfication.FirstOrDefault(m => m.ID == id);
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = "there is no Medical Medicine Classfication";
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
            var record = dbcontext.Medical_Medicine_Classfication.FirstOrDefault(m => m.ID == id);
            try
            {
                dbcontext.Medical_Medicine_Classfication.Remove(record);
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
                return View();
            }
        }

    }
}