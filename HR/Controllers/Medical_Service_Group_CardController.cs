using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HR.Models;
using System.Data.Entity.Infrastructure;

namespace HR.Controllers
{
    public class Medical_Service_Group_CardController : Controller
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Medical_Service_Group_Card
        public ActionResult Index()
        {
            var model = dbcontext.Medical_Service_Group.ToList();
            return View(model);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Medical_Service_Group model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Medical_Service_Group record = new Medical_Service_Group();
                    record.Name = model.Name;
                    record.TName = model.TName;
                    record.Group_Code = model.Group_Code;
                    dbcontext.Medical_Service_Group.Add(record);
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
                var record = dbcontext.Medical_Service_Group.FirstOrDefault(m => m.ID == id);
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = "There is no Medical Service Group";
                    return View();
                }
            }
            catch
            (Exception e)
            { return View(); }
        }
        [HttpPost]
        public ActionResult Edit(Medical_Service_Group model)
        {
            try
            {
                var record = dbcontext.Medical_Service_Group.FirstOrDefault(m => m.ID == model.ID);
                record.Name = model.Name;
                record.TName = model.TName;
                record.Group_Code = model.Group_Code;
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
                var record = dbcontext.Medical_Service_Group.FirstOrDefault(m => m.ID == id);
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = "There is no Medical Service Group"; ;
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
            var record = dbcontext.Medical_Service_Group.FirstOrDefault(m => m.ID == id);
            try
            {
                dbcontext.Medical_Service_Group.Remove(record);
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