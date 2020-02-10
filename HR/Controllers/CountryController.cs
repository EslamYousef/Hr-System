using HR.Models;
using HR.Models.Infra;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers
{
    [Authorize]
    public class CountryController : BaseController
    {

            ApplicationDbContext dbcontext = new ApplicationDbContext();
       
            // GET: Country
            public ActionResult Index(/*string language*/)
            {
            //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);

            var model = dbcontext.Country.ToList();
                return View(model);
            }

            public ActionResult Create()
            {
            //////
            var modell = new Country();

            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Basic).Structure_Code;
            var model = dbcontext.Country.ToList();
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
            public ActionResult Create(Country model,string command)
            {
                try
                {
                    if(ModelState.IsValid)
                    {
                        Country record = new Country();
                        record.Name = model.Name;
                        record.Description = model.Description;
                        record.Code = model.Code;
                    var re= dbcontext.Country.Add(record);
                        dbcontext.SaveChanges();
                    if(command== "Submit")
                    {
                        return RedirectToAction("Create", "Area", new {id=re.ID });
                    }
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View(model);
                    }
                }  
            catch(DbUpdateException)
            {
                TempData["Message"] = HR.Resource.Basic.thiscodeIsalreadyexists;
                return View(model);
            }
            catch (Exception e)
                {
                var type = e.GetType().ToString();
                    return View(model);
                }
            
            }
            public ActionResult Edit(int id)
            {
                try
                {
                    var record = dbcontext.Country.FirstOrDefault(m => m.ID == id);
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
            public ActionResult Edit(Country model, string command)
            {
                try {
                    var record = dbcontext.Country.FirstOrDefault(m => m.ID == model.ID);
                    record.Name = model.Name;
                    record.Description = model.Description;
                    record.Code = model.Code;
                    dbcontext.SaveChanges();
                if (command == "Submit")
                {
                    return RedirectToAction("Create", "Area", new { id = record.ID });
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
                    var record = dbcontext.Country.FirstOrDefault(m => m.ID == id);
                    if (record != null)
                    { return View(record); }
                    else
                    {
                        TempData["Message"] = HR.Resource.Basic.thereisnodata;
                    return View();
                    }
                
                }
                catch(Exception e)
                {
                    return View();
                }

            }
            [ActionName("Delete")]
            [HttpPost]
            public ActionResult Deletemethod(int  id)
            {
            var record = dbcontext.Country.FirstOrDefault(m => m.ID == id);

            try
            {
                      dbcontext.Country.Remove(record);
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