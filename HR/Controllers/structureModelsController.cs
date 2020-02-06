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
    [Authorize]
    public class structureModelsController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: structureModels
        public ActionResult Index()
        {
            var model = dbcontext.StructureModels.ToList();
            return View(model);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(StructureModels model)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    StructureModels record = new StructureModels();
                    record.Structure_Code = model.Structure_Code;
                    if (model.All_Models == 0 || model.All_Models != model.All_Models)
                    {
                        TempData["Message"] = "Please Choose in Model";
                        return View(model);
                    }

                    else
                    {
                        record.All_Models = model.All_Models;
                        var All = dbcontext.StructureModels.Add(record);
                        dbcontext.SaveChanges();
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
                TempData["Message"] = "The Model Really Exister";
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
                var record = dbcontext.StructureModels.FirstOrDefault(m => m.ID == id);
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = "This Model Is already exists";
                    return View();
                }
            }
            catch
            (Exception e)
            { return View(); }
        }
        [HttpPost]
        public ActionResult Edit(StructureModels model)
        {
            try
            {
                var record = dbcontext.StructureModels.FirstOrDefault(m => m.ID == model.ID);
                record.Structure_Code = model.Structure_Code;

                if (model.All_Models == 0 && model.All_Models != model.All_Models)
                {
                    TempData["Message"] = "Please Choose in Model";
                    return View(model);
                }
                else
                {
                    record.All_Models = model.All_Models;
                    dbcontext.SaveChanges();
                }

                return RedirectToAction("Index");

            }
            catch (DbUpdateException)
            {
                TempData["Message"] = "This Model Is already exists";
                return View(model);
            }
            catch (Exception e)
            { return View(model); }
        }
    }
    
}