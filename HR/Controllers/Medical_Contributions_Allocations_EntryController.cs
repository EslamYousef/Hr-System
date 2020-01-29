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
    public class Medical_Contributions_Allocations_EntryController : Controller
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Medical_Contributions_Allocations_Entry
        public ActionResult Index()
        {
            var model = dbcontext.Medical_Contributions_Allocations_Entry.ToList();
            return View(model);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Medical_Contributions_Allocations_Entry model, string command, string command2)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Medical_Contributions_Allocations_Entry record = new Medical_Contributions_Allocations_Entry();
                    record.Code = model.Code;
                    record.Name = model.Name;
                    record.TName = model.TName;
                    if (model.Allowed_To ==0)
                    {
                        TempData["Message"] = "Please Complete in Allowed To";
                        return View(model);
                    }
                    else
                    {
                        record.Allowed_To = model.Allowed_To;
                    record.Is_Contribution = model.Is_Contribution;

                    var allocations=dbcontext.Medical_Contributions_Allocations_Entry.Add(record);
                        
                    dbcontext.SaveChanges();
                   
                    if (command == "Submit")
                    {
                        return RedirectToAction("Create", "Medical_Contributions_Allocations_Services", new { id = allocations.ID });
                    }
                    if (command2 == "Submit")
                    {
                        return RedirectToAction("Create", "Medical_Contributions_Allocations_Services", new { id = allocations.ID });
                    }
                    return RedirectToAction("Index");
                    }
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
                var record = dbcontext.Medical_Contributions_Allocations_Entry.FirstOrDefault(m => m.ID == id);
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = "There is no Medical Contributions Allocations Entry";
                    return View();
                }
            }
            catch
            (Exception e)
            { return View(); }
        }
        [HttpPost]
        public ActionResult Edit(Medical_Contributions_Allocations_Entry model,string command,string command2)
        {
            try
            {
                var record = dbcontext.Medical_Contributions_Allocations_Entry.FirstOrDefault(m => m.ID == model.ID);
                record.Code = model.Code;
                record.Name = model.Name;
                record.TName = model.TName;
                if (model.Allowed_To == 0)
                {
                    TempData["Message"] = "Please Complete in Allowed To";
                    return View(model);
                }
                else
                {
                    record.Allowed_To = model.Allowed_To;
                    record.Is_Contribution = model.Is_Contribution;
                    dbcontext.SaveChanges();
                }
                //if (command == "Submit")
                //{
                //    return RedirectToAction("Create", "Authorities", new { id = Authority_Type.ID });
                //}
                //if (command2 == "Submit")
                //{
                //    return RedirectToAction("Create", "Authorities", new { id = Authority_Type.ID });
                //}
                return RedirectToAction("Index");                
               
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
                var record = dbcontext.Medical_Contributions_Allocations_Entry.FirstOrDefault(m => m.ID == id);
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = "There is no Medical Contributions Allocations Entry"; ;
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
            var record = dbcontext.Medical_Contributions_Allocations_Entry.FirstOrDefault(m => m.ID == id);
            try
            {
                dbcontext.Medical_Contributions_Allocations_Entry.Remove(record);
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