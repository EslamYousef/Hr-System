using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HR.Models;
using System.Data.Entity.Infrastructure;

namespace HR.Models
{
    public class Medical_Service_CardController : Controller
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Medical_Service_Card
        public ActionResult Index()
        {
            var model = dbcontext.Medical_Service.ToList();
            return View(model);
        }
        public ActionResult Create(string id)
        {
            ViewBag.Medical_Service_Classification = dbcontext.Medical_Service_Classification.ToList().Select(m => new { Code = +m.Classification_Code + "-----[" + m.Description + ']', ID = m.ID });
            if (id != null)
            {
                var ID = int.Parse(id);
                var Medical_Service_Classification = dbcontext.Medical_Service_Classification.FirstOrDefault(m => m.ID == ID);
                var model = new Medical_Service { Medical_Service_Classification = Medical_Service_Classification, Medical_Service_ClassificationId = Medical_Service_Classification.ID.ToString() };
                return View(model);
            }
            return View();
        }
        [HttpPost]
        public ActionResult Create(Medical_Service model, string command)
        {
            try
            {
                ViewBag.Medical_Service_Classification = dbcontext.Medical_Service_Classification.ToList().Select(m => new { Code = +m.Classification_Code + "-----[" + m.Description + ']', ID = m.ID });

                if (ModelState.IsValid)
                {
                    if (model.Medical_Service_ClassificationId == "0" || model.Medical_Service_ClassificationId == null)
                    {
                        ModelState.AddModelError("", "Medical Service Classification Code must enter");
                        return View(model);
                    }
                    Medical_Service record = new Medical_Service();
                    //record.Code = model.Code;
                    record.Service_Code = model.Service_Code;
                    record.Name = model.Name;
                    record.TName = model.TName;
                    record.Medical_Service_ClassificationId = model.Medical_Service_ClassificationId;
                    var Medical_Service_ClassificationId = int.Parse(model.Medical_Service_ClassificationId);
                    record.Medical_Service_Classification = dbcontext.Medical_Service_Classification.FirstOrDefault(m => m.ID == Medical_Service_ClassificationId);
                    dbcontext.Medical_Service.Add(record);
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
                ViewBag.Medical_Service_Classification = dbcontext.Medical_Service_Classification.ToList().Select(m => new { Code = +m.Classification_Code + "-----[" + m.Description + ']', ID = m.ID });
                var record = dbcontext.Medical_Service.FirstOrDefault(m => m.ID == id);
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = "There is no Medical Service Classification";
                    return View();
                }
            }
            catch
            (Exception e)
            { return View(); }
        }
        [HttpPost]
        public ActionResult Edit(Medical_Service model)
        {
            try
            {
                ViewBag.Medical_Service_Classification = dbcontext.Medical_Service_Classification.ToList().Select(m => new { Code = +m.Classification_Code + "-----[" + m.Description + ']', ID = m.ID });
                if (model.Medical_Service_ClassificationId == "0" || model.Medical_Service_ClassificationId == null)
                {
                    ModelState.AddModelError("", "Medical Service Classification Code must enter");
                    return View(model);
                }
                var record = dbcontext.Medical_Service.FirstOrDefault(m => m.ID == model.ID);
                //record.Code = model.Code;
                record.Service_Code = model.Service_Code;
                record.Name = model.Name;
                record.TName = model.TName;
                record.Medical_Service_ClassificationId = model.Medical_Service_ClassificationId;
                var Medical_Service_ClassificationId = int.Parse(model.Medical_Service_ClassificationId);
                record.Medical_Service_Classification = dbcontext.Medical_Service_Classification.FirstOrDefault(m => m.ID == Medical_Service_ClassificationId);

                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = "This code Is already exists";
                return View(model);
            }
            catch (Exception e)
            { return View(model); }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                var record = dbcontext.Medical_Service.FirstOrDefault(m => m.ID == id);
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = "There is no Medical Service";
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
            var record = dbcontext.Medical_Service.FirstOrDefault(m => m.ID == id);

            try
            {
                dbcontext.Medical_Service.Remove(record);
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