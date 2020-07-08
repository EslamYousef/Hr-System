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
    public class Allergy_TypeController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Allergy_Type
        public ActionResult Index()
        {
            var model = dbcontext.Allergy_Type.ToList();
            return View(model);
        }
        public ActionResult Create()
        {
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Medical);
            var model = dbcontext.Allergy_Type.ToList();
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

            var model_ = new Allergy_Type { Allergy_Code = stru.Structure_Code + count };
            return View(model_);
        }
        [HttpPost]
        public ActionResult Create(Allergy_Type model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Allergy_Type record = new Allergy_Type();
                    record.Allergy_Code = model.Allergy_Code;
                    record.Allergy_Name = model.Allergy_Name;
                    record.Allergy_TName = model.Allergy_TName;
                    record.Notes = model.Notes;
                    dbcontext.Allergy_Type.Add(record);
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
                var record = dbcontext.Allergy_Type.FirstOrDefault(m => m.ID == id);
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = "There is no Allergy Type";
                    return View();
                }
            }
            catch
            (Exception e)
            { return View(); }
        }
        [HttpPost]
        public ActionResult Edit(Allergy_Type model)
        {
            try
            {
                var record = dbcontext.Allergy_Type.FirstOrDefault(m => m.ID == model.ID);
                record.Allergy_Code = model.Allergy_Code;
                record.Allergy_Name = model.Allergy_Name;
                record.Allergy_TName = model.Allergy_TName;
                record.Notes = model.Notes;
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
                var record = dbcontext.Allergy_Type.FirstOrDefault(m => m.ID == id);
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = "There is no Allergy Type"; ;
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
            var record = dbcontext.Allergy_Type.FirstOrDefault(m => m.ID == id);
            try
            {
                dbcontext.Allergy_Type.Remove(record);
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