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
    public class StateController : BaseController
    {
        // GET: State
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        public ActionResult Index()
        {
            var records = dbcontext.the_states.ToList();
            return View(records);
        }
        public ActionResult Create(string id)
        {

            var modell = new the_states();
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Basic).Structure_Code;
            var modelll = dbcontext.the_states.ToList();
            var Code = "";
            if (modelll.Count() == 0)
            {
                Code = stru + "1";
            }
            else
            {
                Code = stru + (modelll.LastOrDefault().ID + 1).ToString();
            }



            ViewBag.Area = new List<Area>();
            ViewBag.Country = dbcontext.Country.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            if (id != null)
            {
                var ID = int.Parse(id);
                var area = dbcontext.Area.FirstOrDefault(m => m.ID == ID);
                var model = new the_states {Code=Code, Area=area,Areaid=area.ID.ToString(),CountryID=area.Countryid};
                if (model.CountryID != null)
                {
                    ViewBag.Area = dbcontext.Area.Where(m => m.Countryid == model.CountryID).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                }
                return View(model);
            }
            var mmodel = new the_states();
            mmodel.Code = Code;
            return View(mmodel);
        }
        [HttpPost]
        public ActionResult Create(the_states model,string command)
        {

            ViewBag.Area = new List<Area>();
            ViewBag.Country = dbcontext.Country.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            if (model.CountryID != null)
            {
                ViewBag.Area = dbcontext.Area.Where(m => m.Countryid == model.CountryID).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID }); 
            }
            else
            {
                ViewBag.Area = new List<Area>();
            }

            try
            {
                the_states record = new the_states();
                if (model.Areaid == "0" || model.Areaid == null)
                {
                    ModelState.AddModelError("", "State Code must enter");
                    return View(model);
                }
                record.Name = model.Name;
                record.Description = model.Description;
                record.Areaid = model.Areaid;
                record.Code = model.Code;
                var areaid = int.Parse(model.Areaid);
                record.Area = dbcontext.Area.FirstOrDefault(m => m.ID == areaid);
                record.CountryID = record.Area.Countryid;
                var state= dbcontext.the_states.Add(record);
                dbcontext.SaveChanges();
                if (command == "Submit")
                {
                    return RedirectToAction("Create", "territory", new { id = state.ID });
                }
                return RedirectToAction("Index");
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
            ViewBag.Area = new List<Area>();
            ViewBag.Country = dbcontext.Country.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            var record = dbcontext.the_states.FirstOrDefault(m => m.ID == id);
            if (record.CountryID != null)
            {
                ViewBag.Area = dbcontext.Area.Where(m => m.Countryid == record.CountryID).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            }
            else
            {
                ViewBag.Area = new List<Area>();
            }
            if (record != null)
            {
                return View(record);
            }
            else
            {
                TempData["Message"] = "there is no state";
                return View();
            }
        }
        [HttpPost]
        public ActionResult Edit(the_states model, string command)
        {
            ViewBag.Area = new List<Area>();
            ViewBag.Country = dbcontext.Country.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            var record = dbcontext.the_states.FirstOrDefault(m => m.ID == model.ID);
            if (record.CountryID != null)
            {
                ViewBag.Area = dbcontext.Area.Where(m => m.Countryid == record.CountryID).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            }
            else
            {
                ViewBag.Area = new List<Area>();
            }
            try
            {

                ViewBag.area = dbcontext.Area.Where(m => m.Countryid == record.Area.Countryid).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                if (model.Areaid == "0" || model.Areaid == null)
                {
                    ModelState.AddModelError("", "Area Code must enter");
                    return View(model);
                }
                record.Name = model.Name;
                record.Description = model.Description;
                record.Areaid = model.Areaid;
                record.Code = model.Code;
                var areaid = int.Parse(model.Areaid);
                record.Area = dbcontext.Area.FirstOrDefault(m => m.ID == areaid);
                record.CountryID = model.CountryID;
                dbcontext.SaveChanges();
                if (command == "Submit")
                {
                    return RedirectToAction("Create", "territory", new { id = record.ID });
                }
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
                var record = dbcontext.the_states.FirstOrDefault(m => m.ID == id);
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
        public ActionResult methodDelete(int id)
        {
            var record = dbcontext.the_states.FirstOrDefault(m => m.ID == id);

            try
            {
                 dbcontext.the_states.Remove(record);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = "You can't delete beacause it have child";
                return View(record);
            }
            catch (Exception e)
            {
                return View();
            }
        }

    }
}