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
    public class CitiesController : BaseController
    {
        // GET: Cities
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        public ActionResult Index()
        {
            var records = dbcontext.cities.ToList();
            return View(records);
        }
        public ActionResult Create(string id)
        {

            var modell = new cities();
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Basic).Structure_Code;
            var modelll = dbcontext.cities.ToList();
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
            ViewBag.area = new List<Area>();
            ViewBag.state = new List<the_states>();
            ViewBag.ter = new List<Territories>();
            if (id != null)
            {
                var ID = int.Parse(id);
                var county = dbcontext.Territories.FirstOrDefault(m => m.ID == ID);
                var model = new cities { Code = Code,
                 Territories = county, Territoriesid = county.ID.ToString(), stateid=county.the_states.ID ,areaid = int.Parse(county.the_states.Areaid), countryid = int.Parse(county.the_states.Area.Countryid) };
                if (model.countryid > 0)
                {
                    ViewBag.area = dbcontext.Area.Where(m => m.Countryid == model.countryid.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });

                }
                if (model.areaid > 0)
                {
                    ViewBag.state = dbcontext.the_states.Where(m => m.Areaid == model.areaid.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                }
                if (model.stateid > 0)
                {
                    ViewBag.ter = dbcontext.Territories.Where(m => m.the_statesid == model.stateid.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });

                }
              
                return View(model);
            }
            var mmodel = new cities();
            mmodel.Code = Code;
            return View(mmodel);
        }
        [HttpPost]
        public ActionResult Create(cities model,string command)
        {
            ViewBag.country = dbcontext.Country.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.area = new List<Area>();
            ViewBag.state = new List<the_states>();
            ViewBag.ter = new List<Territories>();

            if (model.countryid > 0)
            {
                ViewBag.area = dbcontext.Area.Where(m => m.Countryid == model.countryid.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });

            }
            if (model.areaid > 0)
            {
                ViewBag.state = dbcontext.the_states.Where(m => m.Areaid == model.areaid.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            }
            if (model.stateid > 0)
            {
                ViewBag.ter = dbcontext.Territories.Where(m => m.the_statesid == model.stateid.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });

            }
          
            try
            {
                if (model.Territoriesid == "0")
                {
                    ModelState.AddModelError("", HR.Resource.Basic.countyCodemustenter11);
                    return View(model);
                }
                cities record = new cities();
                record.Name = model.Name;
                record.areaid = model.areaid;
                record.stateid = model.stateid;
                record.Territoriesid = model.Territoriesid;
                record.countryid = model.countryid;
                var ID = int.Parse(model.Territoriesid);
                record.Territories = dbcontext.Territories.FirstOrDefault(m => m.ID == ID);
                record.Description = model.Description;
                record.Code = model.Code;
                //var the_statesid = int.Parse(model.the_statesid);
                //record.the_states = dbcontext.the_states.FirstOrDefault(m => m.ID == the_statesid);
               var city= dbcontext.cities.Add(record);
                dbcontext.SaveChanges();
                if (command == "Submit")
                {
                    return RedirectToAction("Create", "Postalcode", new { id = city.ID });
                }
                return RedirectToAction("Index");
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
            var record = dbcontext.cities.FirstOrDefault(m => m.ID == id);
            if (record == null)
            {
                TempData["Message"] = HR.Resource.Basic.thereisnodata;
                return View();
            }
            ViewBag.country = dbcontext.Country.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.area = new List<Area>();
            ViewBag.state = new List<the_states>();
            ViewBag.ter = new List<Territories>();

            if (record.countryid > 0)
            {
                ViewBag.area = dbcontext.Area.Where(m => m.Countryid == record.countryid.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });

            }
            if (record.areaid > 0)
            {
                ViewBag.state = dbcontext.the_states.Where(m => m.Areaid == record.areaid.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            }
            if (record.stateid > 0)
            {
                ViewBag.ter = dbcontext.Territories.Where(m => m.the_statesid == record.stateid.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });

            }
            return View(record);


        }
        [HttpPost]
        public ActionResult Edit(cities model,string command)
        {
            try
            {
                var record = dbcontext.cities.FirstOrDefault(m => m.ID == model.ID);
                ViewBag.country = dbcontext.Country.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.area = new List<Area>();
                ViewBag.state = new List<the_states>();
                ViewBag.ter = new List<Territories>();

                if (model.countryid > 0)
                {
                    ViewBag.area = dbcontext.Area.Where(m => m.Countryid == model.countryid.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });

                }
                if (model.areaid > 0)
                {
                    ViewBag.state = dbcontext.the_states.Where(m => m.Areaid == model.areaid.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                }
                if (model.stateid > 0)
                {
                    ViewBag.ter = dbcontext.Territories.Where(m => m.the_statesid == model.stateid.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });

                }

                record.Name = model.Name;
                record.Description = model.Description;
                record.countryid = model.countryid;
                record.Code = model.Code;
                record.areaid = model.areaid;
                record.stateid = model.stateid;
                record.Territoriesid = model.Territoriesid;
                var ID = int.Parse(model.Territoriesid);
                record.Territories = dbcontext.Territories.FirstOrDefault(m => m.ID == ID);
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
                var record = dbcontext.cities.FirstOrDefault(m=>m.ID==id);
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
        public ActionResult methodDelete(int id)
        {
            var record = dbcontext.cities.FirstOrDefault(m => m.ID == id);

            try
            {
               dbcontext.cities.Remove(record);
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