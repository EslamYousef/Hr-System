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
    public class External_compainesController : BaseController
    {
        // GET: External_compaines

        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Country
        public ActionResult Index()
        {
            var model = dbcontext.External_compaines.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            var model = new External_compaines { Company_type = Company_type.None };
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Basic).Structure_Code;
            var model_ = dbcontext.External_compaines.ToList();
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
        public ActionResult Create(External_compaines model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    External_compaines record = new External_compaines();
                    record.Name = model.Name;
                    record.Description = model.Description;
                    record.Code = model.Code;
                    record.oil_sector = model.oil_sector;
                    record.address = model.address;
                    if (model.oil_sector==true)
                    {
                        record.Company_type = model.Company_type;
                    }
                    else
                    {
                        record.Company_type = Company_type.None;
                    }
                    dbcontext.External_compaines.Add(record);
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
                var record = dbcontext.External_compaines.FirstOrDefault(m => m.ID == id);
                if (record != null)
                {  return View(record); }
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
        public ActionResult Edit(External_compaines model)
        {
            try
            {
                var record = dbcontext.External_compaines.FirstOrDefault(m => m.ID == model.ID);
                record.Name = model.Name;
                record.Description = model.Description;
                record.Code = model.Code;
                record.address = model.address;
                record.oil_sector = model.oil_sector;
                if (model.oil_sector == true)
                {
                    record.Company_type = model.Company_type;
                }
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
                var record = dbcontext.External_compaines.FirstOrDefault(m => m.ID == id);
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
            var record = dbcontext.External_compaines.FirstOrDefault(m => m.ID == id);

            try
            {
                dbcontext.External_compaines.Remove(record);
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