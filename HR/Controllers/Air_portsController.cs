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
    public class Air_portsController : BaseController
    {
        // GET: Air_ports
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        public ActionResult Index()
        {
            var records = dbcontext.Air_ports.ToList();
            return View(records);
        }
        public ActionResult Create()
        {
            ViewBag.country = dbcontext.Country.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.area = new List<Area>();
            ViewBag.state = new List<the_states>();

            var modell = new Air_ports();
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Basic).Structure_Code;
            var model_ = dbcontext.Air_ports.ToList();
            if (model_.Count() == 0)
            {
                modell.Code = stru + "1";
            }
            else
            {
                modell.Code = stru + (model_.LastOrDefault().ID + 1).ToString();
            }
            /////
            return View(modell);
        }
        [HttpPost]
        public ActionResult Create(Air_ports model)
        {
            ViewBag.country = dbcontext.Country.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.area = new List<Area>();
            ViewBag.state = new List<the_states>();
            if (model.countryid > 0)
            {
                ViewBag.area = dbcontext.Area.Where(m => m.Countryid == model.countryid.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });

            }
            if (model.areaid > 0)
            {
                ViewBag.state = dbcontext.the_states.Where(m => m.Areaid == model.areaid.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            }
            try
            {
              
                if (model.the_statesid == "0" || model.the_statesid == null)
                {
                    ModelState.AddModelError("", "State Code must enter");
                    return View(model);
                }
                Air_ports record = new Air_ports();
                record.Name = model.Name;
                record.the_statesid = model.the_statesid;
                record.areaid = model.areaid;
                record.countryid = model.countryid;
                record.Description = model.Description;
                record.Code = model.Code;
                var the_statesid = int.Parse(model.the_statesid);
                record.the_states = dbcontext.the_states.FirstOrDefault(m => m.ID == the_statesid);
                dbcontext.Air_ports.Add(record);
                dbcontext.SaveChanges();
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
            var record = dbcontext.Air_ports.FirstOrDefault(m => m.ID == id);
            if (record == null)
            {
                TempData["Message"] = "there is no state";
                return View();
            }
            ViewBag.country = dbcontext.Country.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.area = new List<Area>();
            ViewBag.state = new List<the_states>();
            if (record.countryid > 0)
            {
                ViewBag.area = dbcontext.Area.Where(m => m.Countryid == record.countryid.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });

            }
            if (record.areaid > 0)
            {
                ViewBag.state = dbcontext.the_states.Where(m => m.Areaid == record.areaid.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            }
            return View(record);


        }
        [HttpPost]
        public ActionResult Edit(Air_ports model)
        {
            try
            {
                var record = dbcontext.Air_ports.FirstOrDefault(m => m.ID == model.ID);
                ViewBag.country = dbcontext.Country.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.area = new List<Area>();
                ViewBag.state = new List<the_states>();
                if (record.countryid > 0)
                {
                    ViewBag.area = dbcontext.Area.Where(m => m.Countryid == record.countryid.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });

                }
                if (record.areaid > 0)
                {
                    ViewBag.state = dbcontext.the_states.Where(m => m.Areaid == record.areaid.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                }
                if (model.the_statesid == "0" || model.the_statesid == null)
                {
                    ModelState.AddModelError("", "State Code must enter");
                    return View(model);
                }
                record.Name = model.Name;
                record.Description = model.Description;
                record.countryid = model.countryid;
                record.areaid = model.areaid;
                record.Code = model.Code;
                record.the_statesid = model.the_statesid;
                var the_statesid = int.Parse(model.the_statesid);
                record.the_states = dbcontext.the_states.FirstOrDefault(m => m.ID == the_statesid);
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
                var record = dbcontext.Air_ports.FirstOrDefault(m => m.ID == id);
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
            var record = dbcontext.Air_ports.FirstOrDefault(m => m.ID == id);

            try
            {
                dbcontext.Air_ports.Remove(record);
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