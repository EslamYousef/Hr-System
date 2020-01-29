using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HR.Models;
using System.Data.Entity.Infrastructure;

namespace HR.Controllers
{
    public class Medical_Service_Classification_CardController : Controller
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Medical_Service_Classification_Card
        public ActionResult Index()
        {
            var model = dbcontext.Medical_Service_Classification.ToList();
            return View(model);
        }
        public ActionResult Create(string id)
        {
            ViewBag.Medical_Service_Group = dbcontext.Medical_Service_Group.ToList().Select(m => new { Code = +m.Group_Code + "-----[" + m.Name + ']', ID = m.ID });
            if (id != null)
            {
                var ID = int.Parse(id);
                var Medical_Service_Group = dbcontext.Medical_Service_Group.FirstOrDefault(m => m.ID == ID);
                var model = new Medical_Service_Classification { Medical_Service_Group = Medical_Service_Group, Group_Medical_Service_GroupId = Medical_Service_Group.ID.ToString() };
                return View(model);
            }
            return View();
        }
        [HttpPost]
        public ActionResult Create(Medical_Service_Classification model, string command)
        {
            try
            {
                ViewBag.Medical_Service_Group = dbcontext.Medical_Service_Group.ToList().Select(m => new { Code = +m.Group_Code + "-----[" + m.Name + ']', ID = m.ID });

                if (ModelState.IsValid)
                {
                    if (model.Group_Medical_Service_GroupId == "0" || model.Group_Medical_Service_GroupId == null)
                    {
                        ModelState.AddModelError("", "Medical Service Group Code must enter");
                        return View(model);
                    }
                    Medical_Service_Classification record = new Medical_Service_Classification();
                    //record.Code = model.Code;
                    record.Classification_Code = model.Classification_Code;
                    record.Description = model.Description;
                    record.TDescription = model.TDescription;
                    record.Group_Medical_Service_GroupId = model.Group_Medical_Service_GroupId;
                    var Group_Medical_Service_GroupId = int.Parse(model.Group_Medical_Service_GroupId);
                    record.Medical_Service_Group = dbcontext.Medical_Service_Group.FirstOrDefault(m => m.ID == Group_Medical_Service_GroupId);
                    dbcontext.Medical_Service_Classification.Add(record);
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
                ViewBag.Medical_Service_Group = dbcontext.Medical_Service_Group.ToList().Select(m => new { Code = +m.Group_Code + "-----[" + m.Name + ']', ID = m.ID });
                var record = dbcontext.Medical_Service_Classification.FirstOrDefault(m => m.ID == id);
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
        public ActionResult Edit(Medical_Service_Classification model)
        {
            try
            {
                ViewBag.Medical_Service_Group = dbcontext.Medical_Service_Group.ToList().Select(m => new { Code = +m.Group_Code + "-----[" + m.Name + ']', ID = m.ID });
                if (model.Group_Medical_Service_GroupId == "0" || model.Group_Medical_Service_GroupId == null)
                {
                    ModelState.AddModelError("", "Group Medical Service_Group Code must enter");
                    return View(model);
                }
                var record = dbcontext.Medical_Service_Classification.FirstOrDefault(m => m.ID == model.ID);
                //record.Code = model.Code;
                record.Classification_Code = model.Classification_Code;
                record.Description = model.Description;
                record.TDescription = model.TDescription;
                record.Group_Medical_Service_GroupId = model.Group_Medical_Service_GroupId;
                var Group_Medical_Service_GroupId = int.Parse(model.Group_Medical_Service_GroupId);
                record.Medical_Service_Group = dbcontext.Medical_Service_Group.FirstOrDefault(m => m.ID == Group_Medical_Service_GroupId);

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
                var record = dbcontext.Medical_Service_Classification.FirstOrDefault(m => m.ID == id);
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = "There is no Medical Service Classification";
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
            var record = dbcontext.Medical_Service_Classification.FirstOrDefault(m => m.ID == id);

            try
            {
                dbcontext.Medical_Service_Classification.Remove(record);
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