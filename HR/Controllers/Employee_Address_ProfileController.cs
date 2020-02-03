using HR.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HR.Models.Infra;
using HR.Models.ViewModel;
namespace HR.Controllers
{
    public class Employee_Address_ProfileController : Controller
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Employee_Address_Profile
        public ActionResult Index(string id)
        {
            var ID = int.Parse(id);
            var new_model = dbcontext.Employee_Address_Profile.Where(m => m.Employee_Profile.ID == ID).ToList();
            ViewBag.idemp = id;
            return View(new_model);
        }
        public ActionResult Create(string id)
        {

            ViewBag.Country = dbcontext.Country.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.Area = dbcontext.Area.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.the_states = dbcontext.the_states.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.Territories = dbcontext.Territories.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.cities = dbcontext.cities.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.postcode = dbcontext.postcode.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.idemp = id;
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel);
            var model = dbcontext.Employee_Address_Profile.ToList();
            var count = 0;
            if (model.Count() == 0)
            {
                count = 1;
            }
            else
            {
                var te = model.LastOrDefault().ID;
                count = te + 1;
            }
        
            var ID = int.Parse(id);
              var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == ID);
            var EmployeeAddress = new Employee_Address_Profile { Employee_ProfileId = emp.ID.ToString(), Code = stru.Structure_Code + count.ToString() }; ;
            return View(EmployeeAddress);

        }
        [HttpPost]
        public ActionResult Create(Employee_Address_Profile model, string command)
        {
            try
            {
                if (model.countryid == null) { model.countryid = "0"; }
                if (model.areaid == null) { model.areaid = "0"; }
                if (model.stateid == null) { model.stateid = "0"; }
                if (model.Territoriesid == null) { model.Territoriesid = "0"; }
                if (model.citiesid == null) { model.citiesid = "0"; }
                if (model.postcodeId == null) { model.postcodeId = "0"; }

                ViewBag.Country = dbcontext.Country.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Area = dbcontext.Area.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.the_states = dbcontext.the_states.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Territories = dbcontext.Territories.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.cities = dbcontext.cities.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.postcode = dbcontext.postcode.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.idemp = model.ID;

                if (ModelState.IsValid)
                {
                    var prof = int.Parse(model.Employee_ProfileId);
                    var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == prof);
                    Employee_Address_Profile record = new Employee_Address_Profile();
                    record.Resident = model.Resident;
                    record.Code = model.Code;
                    record.Streetname = model.Streetname;
                    record.Streetnumber = model.Streetnumber;
                    record.Pobox = model.Pobox;
                    record.DistancefromMeetingpointtoworklocationkm = model.DistancefromMeetingpointtoworklocationkm;
                    record.Distancetoworklocationkm = model.Distancetoworklocationkm;
                    record.Transportation_method = model.Transportation_method;
                    record.Employee_ProfileId = model.Employee_ProfileId;
                    var Employee_ProfileId = int.Parse(model.Employee_ProfileId);
                    record.Employee_Profile = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == Employee_ProfileId);
                    record.countryid = model.countryid;
                    var Countryid = int.Parse(model.countryid);
                    record.Country = dbcontext.Country.FirstOrDefault(m => m.ID == Countryid);
                    record.areaid = model.areaid;
                    var Areaid = int.Parse(model.areaid);
                    record.Area = dbcontext.Area.FirstOrDefault(m => m.ID == Areaid);
                    record.stateid = model.stateid;
                    var the_statesid = int.Parse(model.stateid);
                    record.the_states = dbcontext.the_states.FirstOrDefault(m => m.ID == the_statesid);
                    record.Territoriesid = model.Territoriesid;
                    var Territoriesid = int.Parse(model.Territoriesid);
                    record.Territories = dbcontext.Territories.FirstOrDefault(m => m.ID == Territoriesid);
                    record.citiesid = model.citiesid;
                    var citiesid = int.Parse(model.citiesid);
                    record.cities = dbcontext.cities.FirstOrDefault(m => m.ID == citiesid);
                    record.postcodeId = model.postcodeId;
                    var postcodeId = int.Parse(model.postcodeId);
                    record.postcode = dbcontext.postcode.FirstOrDefault(m => m.ID == postcodeId);
                    dbcontext.Employee_Address_Profile.Add(record);
                    dbcontext.SaveChanges();
                    if (command == "Submit")
                    {
                        return RedirectToAction("edit", "Employee_Profile", new { id = int.Parse(record.Employee_ProfileId) });
                    }
                    return RedirectToAction("Index", new { id = model.Employee_ProfileId });
                }
                else
                {
                    return View(model);
                }
            }
            catch (DbUpdateException e)
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
            try
            {
                ViewBag.Country = dbcontext.Country.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Area = dbcontext.Area.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.the_states = dbcontext.the_states.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Territories = dbcontext.Territories.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.cities = dbcontext.cities.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.postcode = dbcontext.postcode.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                var record = dbcontext.Employee_Address_Profile.FirstOrDefault(m => m.ID == id);
                ViewBag.idemp = record.Employee_Profile.ID.ToString();

                if (record != null)
                {
                    return View(record);
                }
                else
                {
                    TempData["Message"] = "There is no Employee Address Profile";
                    return View();
                }
            }
            catch
            (Exception e)
            { return View(); }
        }
        [HttpPost]
        public ActionResult Edit(Employee_Address_Profile model, string command)
        {
            try
            {
                if (model.countryid == null) { model.countryid = "0"; }
                if (model.areaid == null) { model.areaid = "0"; }
                if (model.stateid == null) { model.stateid = "0"; }
                if (model.Territoriesid == null) { model.Territoriesid = "0"; }
                if (model.citiesid == null) { model.citiesid = "0"; }
                if (model.postcodeId == null) { model.postcodeId = "0"; }

                ViewBag.Country = dbcontext.Country.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Area = dbcontext.Area.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.the_states = dbcontext.the_states.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Territories = dbcontext.Territories.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.cities = dbcontext.cities.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.postcode = dbcontext.postcode.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.idemp = model.ID;

                var record = dbcontext.Employee_Address_Profile.FirstOrDefault(m => m.ID == model.ID);
                var emp = record.Employee_Profile;
                record.Code = model.Code;
                record.Resident = model.Resident;
                record.Streetname = model.Streetname;
                record.Streetnumber = model.Streetnumber;
                record.Pobox = model.Pobox;
                record.DistancefromMeetingpointtoworklocationkm = model.DistancefromMeetingpointtoworklocationkm;
                record.Distancetoworklocationkm = model.Distancetoworklocationkm;
                record.Transportation_method = model.Transportation_method;
                record.Employee_ProfileId = model.Employee_ProfileId;
                var Employee_ProfileId = int.Parse(model.Employee_ProfileId);
                record.countryid = model.countryid;
                var Countryid = int.Parse(model.countryid);
                record.Country = dbcontext.Country.FirstOrDefault(m => m.ID == Countryid);
                record.areaid = model.areaid;
                var Areaid = int.Parse(model.areaid);
                record.Area = dbcontext.Area.FirstOrDefault(m => m.ID == Areaid);
                record.stateid = model.stateid;
                var the_statesid = int.Parse(model.stateid);
                record.the_states = dbcontext.the_states.FirstOrDefault(m => m.ID == the_statesid);
                record.Territoriesid = model.Territoriesid;
                var Territoriesid = int.Parse(model.Territoriesid);
                record.Territories = dbcontext.Territories.FirstOrDefault(m => m.ID == Territoriesid);
                record.citiesid = model.citiesid;
                var citiesid = int.Parse(model.citiesid);
                record.cities = dbcontext.cities.FirstOrDefault(m => m.ID == citiesid);
                record.postcodeId = model.postcodeId;
                var postcodeId = int.Parse(model.postcodeId);
                record.postcode = dbcontext.postcode.FirstOrDefault(m => m.ID == postcodeId);

                dbcontext.SaveChanges();

                if (command == "Submit")
                {
                    return RedirectToAction("edit", "Employee_Profile", new { id = int.Parse(record.Employee_ProfileId) });
                }
                return RedirectToAction("index", new { id = model.Employee_ProfileId });
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = "This code Is already exists";
                return View(model);
            }
            catch (Exception e)
            { return View(model); }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                var record = dbcontext.Employee_Address_Profile.FirstOrDefault(m => m.ID == id);
                ViewBag.idemp = record.Employee_Profile.ID.ToString();

                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = "There is no Employee Address Profile";
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
            var record = dbcontext.Employee_Address_Profile.FirstOrDefault(m => m.ID == id);
            ViewBag.idemp = record.Employee_Profile.ID.ToString();

            try
            {
                dbcontext.Employee_Address_Profile.Remove(record);
                dbcontext.SaveChanges();
                return RedirectToAction("index", new { id = record.Employee_ProfileId });
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