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
    public class Rejection_ReasonsController : Controller
    {
        // GET: Rejection_Reasons
        ApplicationDbContext dbcontext = new ApplicationDbContext();

        public ActionResult Index()
        {
            var model = dbcontext.Rejection_Reasons.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            var model = new Rejection_Reasons { purpose=reject_purpose.Job_experience };

            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Basic).Structure_Code;
            var model_ = dbcontext.Rejection_Reasons.ToList();
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
        public ActionResult Create(Rejection_Reasons model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Rejection_Reasons record = new Rejection_Reasons();
                    record.Name = model.Name;
                    record.Description = model.Description;
                    record.Code = model.Code;
                    record.purpose = model.purpose;
                    dbcontext.Rejection_Reasons.Add(record);
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
                var record = dbcontext.Rejection_Reasons.FirstOrDefault(m => m.ID == id);
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
        public ActionResult Edit(Rejection_Reasons model)
        {
            try
            {
                var record = dbcontext.Rejection_Reasons.FirstOrDefault(m => m.ID == model.ID);
                record.Name = model.Name;
                record.Description = model.Description;
                record.Code = model.Code;
                record.purpose = model.purpose;
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
                var record = dbcontext.Rejection_Reasons.FirstOrDefault(m => m.ID == id);
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
            var record = dbcontext.Rejection_Reasons.FirstOrDefault(m => m.ID == id);

            try
            {
                dbcontext.Rejection_Reasons.Remove(record);
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