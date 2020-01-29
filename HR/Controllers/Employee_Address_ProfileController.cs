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
        public ActionResult Index()
        {
           var address = dbcontext.Employee_Address_Profile.ToList();
            var employeeprofile = dbcontext.Employee_Profile.ToList();
            var model = from a in employeeprofile
                        join b in  address on a.Employee_Address_Profile.ID equals b.ID
                        select new addressemployee_VM
                        {
                            fullname = a.Full_Name,code=a.Code,
                            EmployeeId =a.ID,
                            Employee_Address_Profile = b
                        };
            return View(model);
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
            if (id != null)
            {
                var ID = int.Parse(id);
                var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == ID);
                var x = emp.Employee_Address_Profile;
                return View(x);
            }

            var EmployeeAddress = new Employee_Address_Profile ();
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
       
                if (ModelState.IsValid)
                {     
                    var prof = int.Parse(model.Employee_ProfileId);
                    var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == prof);
                    var record = dbcontext.Employee_Address_Profile.FirstOrDefault(m =>m.ID== emp.Employee_Address_Profile.ID);
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
                        return RedirectToAction("edit", "Employee_Profile", new { id = int.Parse(record.Employee_ProfileId)});
                    }
                    return RedirectToAction("Index");
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
                var record = dbcontext.Employee_Address_Profile.FirstOrDefault(m => m.ID == model.ID);
                //record.Code = model.Code;
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
                return RedirectToAction("index");
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = "This code Is already exists";
                return View(model);
            }
            catch (Exception e)
            { return View(model); }
        }
        //public ActionResult Delete(int id)
        //{
        //    try
        //    {
        //        var record = dbcontext.Employee_Address_Profile.FirstOrDefault(m => m.ID == id);
        //        if (record != null)
        //        { return View(record); }
        //        else
        //        {
        //            TempData["Message"] = "There is no Employee Address Profile";
        //            return View();
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        return View();
        //    }

        //}
        //[ActionName("Delete")]
        //[HttpPost]
        //public ActionResult Deletemethod(int id)
        //{
        //    var record = dbcontext.Employee_Address_Profile.FirstOrDefault(m => m.ID == id);

        //    try
        //    {
        //        dbcontext.Employee_Address_Profile.Remove(record);
        //        dbcontext.SaveChanges();
        //        return RedirectToAction("index");
        //    }
        //    catch (DbUpdateException)
        //    {
        //        TempData["Message"] = "You can't delete beacause it have child";
        //        return View(record);
        //    }
        //    catch (Exception e)
        //    {
        //        return View();
        //    }
        //}
    }
}