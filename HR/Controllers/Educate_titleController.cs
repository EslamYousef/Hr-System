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
    public class Educate_titleController : Controller
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();

        public ActionResult Index()
        {
            var model = dbcontext.Educate_Title.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            //////
            var modell = new Educate_Title();

            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Basic).Structure_Code;
            var model = dbcontext.Educate_Title.ToList();
            if (model.Count() == 0)
            {
                modell.Code = stru + "1";
            }
            else
            {
                modell.Code = stru + (model.LastOrDefault().ID + 1).ToString();
            }
            /////
            return View(modell);
        }
        [HttpPost]
        public ActionResult Create(Educate_Title model,string command)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Educate_Title record = new Educate_Title();
                    record.Name = model.Name;
                    record.Description = model.Description;
                    record.Code = model.Code;
                  var Educate_Level=  dbcontext.Educate_Title.Add(record);
                    dbcontext.SaveChanges();
                    if (command == "Submit")
                    {
                        return RedirectToAction("Create", "Name_of_educational_qualification", new { id = Educate_Level.ID });
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
                var record = dbcontext.Educate_Title.FirstOrDefault(m => m.ID == id);
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = "there is no data";
                    return View();
                }
            }
            catch
            (Exception e)
            { return View(); }
        }
        [HttpPost]
        public ActionResult Edit(Educate_Title model,string command)
        {
            try
            {
                var record = dbcontext.Educate_Title.FirstOrDefault(m => m.ID == model.ID);
                record.Name = model.Name;
                record.Description = model.Description;
                record.Code = model.Code;
                dbcontext.SaveChanges();
                if (command == "Submit")
                {
                    return RedirectToAction("Create", "Name_of_educational_qualification", new { id = record.ID });
                }
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
                var record = dbcontext.Educate_Title.FirstOrDefault(m => m.ID == id);
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = "there is no data";
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
                var record = dbcontext.Educate_Title.FirstOrDefault(m => m.ID == id);
                dbcontext.Educate_Title.Remove(record);
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