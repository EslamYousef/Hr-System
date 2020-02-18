using HR.Models;
using HR.Models.Infra;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers
{
    [Authorize]
    public class Budget_class_itemsController : BaseController
    {
        // GET: Budget_class_items
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        public ActionResult Index()
        {
            var model = dbcontext.Budget_class_items.ToList();
            return View(model);
        }

        public ActionResult Create(string id)
        {



            var modell = new Budget_class_items();
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Organization).Structure_Code;
            var modelll = dbcontext.Budget_class_items.ToList();
            var Code = "";
            if (modelll.Count() == 0)
            {
                Code = stru + "1";
            }
            else
            {
                Code = stru + (modelll.LastOrDefault().ID + 1).ToString();
            }


            ViewBag.Budget_class = dbcontext.Budget_class.ToList();
            if (id != null)
            {
                var ID = int.Parse(id);
                var Budget_class = dbcontext.Budget_class.FirstOrDefault(m => m.ID == ID);
                var model = new Budget_class_items {Code=Code, Budget_class=Budget_class,Budget_classId=Budget_class.ID.ToString() };
                return View(model);
            }
            var mm = new Budget_class_items();
            mm.Code = Code;
            return View(mm);
        }
        [HttpPost]
        public ActionResult Create(Budget_class_items model)
        {
            try
            {
                ViewBag.Budget_class = dbcontext.Budget_class.ToList();
                if (ModelState.IsValid)
                {
                    Budget_class_items record = new Budget_class_items();
                    record.Name = model.Name;
                    record.Description = model.Description;
                    record.Code = model.Code;
                    record.Budget_classId = model.Budget_classId;
                    var ID = int.Parse(model.Budget_classId);
                    record.Budget_class = dbcontext.Budget_class.FirstOrDefault(m => m.ID == ID);
                    dbcontext.Budget_class_items.Add(record);
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
                ViewBag.Budget_class = dbcontext.Budget_class.ToList();
                var record = dbcontext.Budget_class_items.FirstOrDefault(m => m.ID == id);
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
        public ActionResult Edit(Budget_class_items model)
        {
            try
            {
                ViewBag.Budget_class = dbcontext.Budget_class.ToList();
                var record = dbcontext.Budget_class_items.FirstOrDefault(m => m.ID == model.ID);
                record.Name = model.Name;
                record.Description = model.Description;
                record.Code = model.Code;
                record.Budget_classId = model.Budget_classId;
                var ID = int.Parse(model.Budget_classId);
                record.Budget_class = dbcontext.Budget_class.FirstOrDefault(m => m.ID == ID);
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
                var record = dbcontext.Budget_class_items.FirstOrDefault(m => m.ID == id);
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
            var record = dbcontext.Budget_class_items.FirstOrDefault(m => m.ID == id);

            try
            {
                dbcontext.Budget_class_items.Remove(record);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = HR.Resource.Basic.youcannotdeletethisRow;
                return View(record);
            }
            catch (Exception e)
            {
                return View();
            }
        }
    }
}