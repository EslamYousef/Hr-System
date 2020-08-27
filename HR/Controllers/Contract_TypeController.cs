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
    [Authorize(Roles = "Admin,personnel,personnelSetup,Contract Types")]
    public class Contract_TypeController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Contract_Type
        public ActionResult Index()
        {
            var model = dbcontext.Contract_Type.ToList();
            return View(model);
        }
        public ActionResult Create()
        {
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel);
            //var count = dbcontext.Contract_Type.ToList().Count()+1;
            //      var count = dbcontext.Contract_Type.ToList().FindLastIndex().coun;
            var model = dbcontext.Contract_Type.ToList();
            var count=0;
          if(model.Count()==0)
            {
                count = 1;
            }
            else 
            {
                var te = model.LastOrDefault().ID ;
                count = te + 1;
            }
         
            var modell = new Contract_Type { Contract_Type_Code=stru.Structure_Code+count};
            return View(modell);
         
        }
        [HttpPost]
        public ActionResult Create(Contract_Type model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Contract_Type record = new Contract_Type();
                    record.Contract_Type_Code = model.Contract_Type_Code;
                    record.Contract_Type_Description = model.Contract_Type_Description;
                    record.Contract_Type_Alternative_Description = model.Contract_Type_Alternative_Description;
                    dbcontext.Contract_Type.Add(record);
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
                TempData["Message"] = HR.Resource.Personnel.thiscodeIsalreadyexists;
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
                var record = dbcontext.Contract_Type.FirstOrDefault(m => m.ID == id);
                
                if (record != null)
                { return View(record);

                }
                else
                {
                    TempData["Message"] = HR.Resource.Personnel.thereisnodata;
                    return View();
                }
            }
            catch
            (Exception e)
            { return View(); }
        }
        [HttpPost]
        public ActionResult Edit(Contract_Type model)
        {
            try
            {
                var record = dbcontext.Contract_Type.FirstOrDefault(m => m.ID == model.ID);
                record.Contract_Type_Code = model.Contract_Type_Code;
                record.Contract_Type_Description = model.Contract_Type_Description;
                record.Contract_Type_Alternative_Description = model.Contract_Type_Alternative_Description;
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = HR.Resource.Personnel.thiscodeIsalreadyexists;
                return View(model);
            }
            catch (Exception e)
            { return View(model); }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                var record = dbcontext.Contract_Type.FirstOrDefault(m => m.ID == id);
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = HR.Resource.Personnel.thereisnodata;
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
            var record = dbcontext.Contract_Type.FirstOrDefault(m => m.ID == id);
            try
            {
                dbcontext.Contract_Type.Remove(record);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = HR.Resource.Personnel.youcannotdeletethisRow;
                return View(record);
            }
            catch (Exception e)
            {
                return View();
            }
        }

    }
}