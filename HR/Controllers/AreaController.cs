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
    public class AreaController : BaseController
    {
        // GET: Area
        ApplicationDbContext dbcontext = new ApplicationDbContext();
       
        public ActionResult Index()
        {
            var model = dbcontext.Area.ToList();
            return View(model);
        }

        public ActionResult Create(string id)
        {
            var modell = new Area();
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Basic).Structure_Code;
            var modelll = dbcontext.Area.ToList();
            var Code = "";
            if (modelll.Count() == 0)
            {
                Code = stru + "1";
            }
            else
            {
                Code = stru + (modelll.LastOrDefault().ID + 1).ToString();
            }


            ViewBag.country = dbcontext.Country.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            if (id!=null)
            {
                var ID = int.Parse(id);
                var country = dbcontext.Country.FirstOrDefault(m => m.ID == ID);
                var model = new Area {Code=Code, Country=country,Countryid=country.ID.ToString()};
                return View(model);
            }
            var mmodel = new Area();
            mmodel.Code = Code;
            return View(mmodel);
        }
        [HttpPost]
        public ActionResult Create(Area model,string command)
        {
            try
            {
                ViewBag.country = dbcontext.Country.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                if (ModelState.IsValid)
                {
                    if (model.Countryid == "0" || model.Countryid == null)
                    {
                        ModelState.AddModelError("", HR.Resource.Basic.countryCodemustenter1);
                        return View(model);
                    }
                    Area record = new Area();
                    record.Name = model.Name;
                    record.Description = model.Description;
                    record.Countryid = model.Countryid;
                    record.Code = model.Code;
                    var countryid = int.Parse(model.Countryid);
                    record.Country = dbcontext.Country.FirstOrDefault(m => m.ID == countryid);
                   var area= dbcontext.Area.Add(record);
                    dbcontext.SaveChanges();
                    if (command == "Submit")
                    {
                        return RedirectToAction("Create", "State", new { id = area.ID });
                    }
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
                ViewBag.country = dbcontext.Country.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                var record = dbcontext.Area.FirstOrDefault(m => m.ID == id);
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
        public ActionResult Edit(Area model, string command)
        {
            try
            {
                ViewBag.country = dbcontext.Country.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                if (model.Countryid == "0" || model.Countryid == null)
                {
                    ModelState.AddModelError("", HR.Resource.Basic.countryCodemustenter1);
                    return View(model);
                }
                var record = dbcontext.Area.FirstOrDefault(m => m.ID == model.ID);
                record.Name = model.Name;
                record.Description = model.Description;
                record.Countryid = model.Countryid;
                record.Code = model.Code;
                var countryid = int.Parse(model.Countryid);
                record.Country = dbcontext.Country.FirstOrDefault(m => m.ID ==countryid);
                dbcontext.SaveChanges();
                if (command == "Submit")
                {
                    return RedirectToAction("Create", "State", new { id = record.ID });
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
                var record = dbcontext.Area.FirstOrDefault(m => m.ID == id);
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
            var record = dbcontext.Area.FirstOrDefault(m => m.ID == id);

            try
            {
                dbcontext.Area.Remove(record);
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