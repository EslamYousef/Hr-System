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
    public class OrganizationunittypeController : BaseController
    {
        // GET: Organizationunittype
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        public ActionResult Index()
        {
            var model = dbcontext.Organization_Unit_Type.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            ViewBag.Organization_Unit_Schema = dbcontext.Organization_Unit_Schema.ToList().Select(m=>new {ID=m.ID, Code = m.Code+"->"+m.Name});
            ViewBag.Organization_Unit_Level = dbcontext.Organization_Unit_Level.ToList().Select(m => new { ID = m.ID, Code = m.Code + "->" + m.Name });

            //////
            var modell = new Organization_Unit_Type();

            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Organization).Structure_Code;
            var model = dbcontext.Organization_Unit_Type.ToList();
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
        public ActionResult Create(Organization_Unit_Type model)
        {
            try
            {
                ViewBag.Organization_Unit_Schema = dbcontext.Organization_Unit_Schema.ToList().Select(m => new { ID = m.ID, Code = m.Code + "->" + m.Name });
                ViewBag.Organization_Unit_Level = dbcontext.Organization_Unit_Level.ToList().Select(m => new { ID = m.ID, Code = m.Code + "->" + m.Name });
                if (ModelState.IsValid)
                {
                    Organization_Unit_Type record = new Organization_Unit_Type();
                    record.Name = model.Name;
                    record.Description = model.Description;
                    record.Code = model.Code;
                    record.Organization_Unit_LevelID =model.Organization_Unit_LevelID;
                    record.Organization_Unit_SchemaID = model.Organization_Unit_SchemaID;
                  
                    dbcontext.Organization_Unit_Type.Add(record);
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
                ViewBag.Organization_Unit_Schema = dbcontext.Organization_Unit_Schema.ToList().Select(m => new { ID = m.ID, Code = m.Code + "->" + m.Name });
                ViewBag.Organization_Unit_Level = dbcontext.Organization_Unit_Level.ToList().Select(m => new { ID = m.ID, Code = m.Code + "->" + m.Name });

                var record = dbcontext.Organization_Unit_Type.FirstOrDefault(m => m.ID == id);
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
        public ActionResult Edit(Organization_Unit_Type model)
        {
            try
            {
                ViewBag.Organization_Unit_Schema = dbcontext.Organization_Unit_Schema.ToList();
                ViewBag.Organization_Unit_Level = dbcontext.Organization_Unit_Level.ToList();

                var record = dbcontext.Organization_Unit_Type.FirstOrDefault(m => m.ID == model.ID);
                record.Name = model.Name;
                record.Description = model.Description;
                record.Code = model.Code;
                record.Organization_Unit_LevelID = model.Organization_Unit_LevelID;
                record.Organization_Unit_SchemaID = model.Organization_Unit_SchemaID;

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
                var record = dbcontext.Organization_Unit_Type.FirstOrDefault(m => m.ID == id);
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
            var record = dbcontext.Organization_Unit_Type.FirstOrDefault(m => m.ID == id);
            try
            {
                dbcontext.Organization_Unit_Type.Remove(record);
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