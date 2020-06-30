using HR.Models;
using HR.Models.Infra;
using HR.Models.Training.setup;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers.training.setup
{
    public class Trainig_centerController : BaseController
    {
        // GET: Trainig_center
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        
        public ActionResult Index()
        {
            var model = dbcontext.TrainingCenter.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            //////
            var modell = new TrainingCenter();

            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Talent_Development).Structure_Code;
            var model = dbcontext.TrainingCenter.ToList();
            if (model.Count() == 0)
            {
                modell.TrainingCenters_Code = stru + "1";
            }
            else
            {
                modell.TrainingCenters_Code = stru + (model.LastOrDefault().ID + 1).ToString();
            }
            /////
            return View(modell);
        }
        [HttpPost]
        public ActionResult Create(TrainingCenter model, string command)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.Created_By = User.Identity.Name;
                    model.Created_Date = DateTime.Now.Date;
                    var tra_center = dbcontext.TrainingCenter.Add(model);
                    dbcontext.SaveChanges();
                    if (command == "Submit")
                    {
                        return RedirectToAction("Create", "Postalcode", new { id = tra_center.ID });
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(model);
                }
            }
            catch (DbUpdateException e)
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
                var record = dbcontext.TrainingCenter.FirstOrDefault(m => m.ID == id);
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
        public ActionResult Edit(TrainingCenter model,string command)
        {
            try
            {
                var record = dbcontext.TrainingCenter.FirstOrDefault(m => m.ID == model.ID);
                
                record.TrainingCenters_Desc = model.TrainingCenters_Desc;
                record.TrainingCenters_AltDesc = model.TrainingCenters_AltDesc;
                record.Modified_By = User.Identity.Name;
                record.Modified_Date = DateTime.Now.Date;
                dbcontext.SaveChanges();
                if (command == "Submit")
                {
                    return RedirectToAction("Create", "Postalcode", new { id = record.ID });
                }
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
                var record = dbcontext.TrainingCenter.FirstOrDefault(m => m.ID == id);
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
            var record = dbcontext.TrainingCenter.FirstOrDefault(m => m.ID == id);
            try
            {
                dbcontext.TrainingCenter.Remove(record);
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
                return View(record);
            }
        }
    }
}