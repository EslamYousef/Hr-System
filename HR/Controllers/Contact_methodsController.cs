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
    public class Contact_methodsController : BaseController
    {
        // GET: Contact_methods
      
        ApplicationDbContext dbcontext = new ApplicationDbContext();

        public ActionResult Index()
        {
            var model = dbcontext.Contact_methods.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            //////
           
            var model = new Contact_methods { Contact_type = Type_of_contect_method.Email };
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Basic).Structure_Code;
            var model_ = dbcontext.Contact_methods.ToList();
            if (model_.Count() == 0)
            {
                model.Code = stru + "1";
            }
            else
            {
                model.Code = stru + (model_.LastOrDefault().ID + 1).ToString();
            }
            /////
        
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(Contact_methods model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Contact_methods record = new Contact_methods();
                    record.Name = model.Name;
                    record.Description = model.Description;
                    record.Code = model.Code;
                    record.Contact_type = model.Contact_type;
                    dbcontext.Contact_methods.Add(record);
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
                var record = dbcontext.Contact_methods.FirstOrDefault(m => m.ID == id);
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = "there is no country";
                    return View();
                }
            }
            catch
            (Exception e)
            { return View(); }
        }
        [HttpPost]
        public ActionResult Edit(Contact_methods model)
        {
            try
            {
                var record = dbcontext.Contact_methods.FirstOrDefault(m => m.ID == model.ID);
                record.Name = model.Name;
                record.Description = model.Description;
                record.Code = model.Code;
                record.Contact_type = model.Contact_type;
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
                var record = dbcontext.Contact_methods.FirstOrDefault(m => m.ID == id);
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = "there is no country";
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
            var record = dbcontext.Contact_methods.FirstOrDefault(m => m.ID == id);
            try
            {
                dbcontext.Contact_methods.Remove(record);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = "this code Is already exists";
                return View(record);
            }
            catch (Exception e)
            {
                return View();
            }
        }
    }
}