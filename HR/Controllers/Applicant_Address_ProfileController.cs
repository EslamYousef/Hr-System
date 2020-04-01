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
    public class Applicant_Address_ProfileController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Applicant_Address_Profile
        public ActionResult Index(string id)
        {
            var ID = int.Parse(id);
            var new_model = dbcontext.Applicant_Address_Profile.Where(m => m.Applicant_Profile.ID == ID).ToList();
            var record = dbcontext.Applicant_Address_Profile.FirstOrDefault(m => m.ID == ID);
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
            ViewBag.Employee_Profile = dbcontext.Applicant_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.idemp = id;
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Recuirtment);
            var model = dbcontext.Applicant_Address_Profile.ToList();
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
            var emp = dbcontext.Applicant_Profile.FirstOrDefault(m => m.ID == ID);
            var Applicant_Address = new Applicant_Address_Profile { Applicant_Profile = emp, Employee_ProfileId = emp.ID.ToString(), Code = stru.Structure_Code + count.ToString() };
            return View(Applicant_Address);

        }
        [HttpPost]
        public ActionResult Create(Applicant_Address_Profile model, string command)
        {
            try
            {
                if (model.countryid == 0) { model.countryid = null; }
                if (model.areaid == 0) { model.areaid = null; }
                if (model.stateid == 0) { model.stateid = null; }
                if (model.Territoriesid == 0) { model.Territoriesid = null; }
                if (model.citiesid == 0) { model.citiesid = null; }
                if (model.postcodeId == 0) { model.postcodeId = null; }
                ViewBag.Employee_Profile = dbcontext.Applicant_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Country = dbcontext.Country.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Area = dbcontext.Area.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.the_states = dbcontext.the_states.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Territories = dbcontext.Territories.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.cities = dbcontext.cities.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.postcode = dbcontext.postcode.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });

                var EmpObj = dbcontext.Applicant_Profile.FirstOrDefault(a => a.ID == model.Applicant_Profile.ID);
                var list = dbcontext.Applicant_Address_Profile.ToList();

                Applicant_Address_Profile record = new Applicant_Address_Profile();
                if (list.Count() == 0)
                {
                    record.Resident = true;
                }
                else
                {
                    var te = list.LastOrDefault();
                    te.Resident = false;
                    record.Resident = true;
                }

                record.Code = model.Code;
                record.Streetname = model.Streetname;
                record.Streetnumber = model.Streetnumber;
                record.Pobox = model.Pobox;
                record.DistancefromMeetingpointtoworklocationkm = model.DistancefromMeetingpointtoworklocationkm;
                record.Distancetoworklocationkm = model.Distancetoworklocationkm;
                record.Transportation_method = model.Transportation_method;

                var empid = EmpObj.Code + "------" + EmpObj.Name;
                record.Employee_ProfileId = model.Employee_ProfileId == null ? model.Employee_ProfileId = EmpObj.ID.ToString() : model.Employee_ProfileId;
                ViewBag.idemp = model.Employee_ProfileId;
                record.Applicant_Profile = EmpObj;

                record.countryid = model.countryid;
                record.areaid = model.areaid;
                record.stateid = model.stateid;
                record.Territoriesid = model.Territoriesid;
                record.citiesid = model.citiesid;
                record.postcodeId = model.postcodeId;
                dbcontext.Applicant_Address_Profile.Add(record);
                dbcontext.SaveChanges();
                if (command == "Submit")
                {
                    return RedirectToAction("edit", "Applicant_Profile", new { id = EmpObj.ID });
                }
                return RedirectToAction("Index", new { id = EmpObj.ID }); 
                return View(model);
            }
            catch (DbUpdateException e)
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
                ViewBag.Employee_Profile = dbcontext.Applicant_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Country = dbcontext.Country.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Area = dbcontext.Area.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.the_states = dbcontext.the_states.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Territories = dbcontext.Territories.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.cities = dbcontext.cities.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.postcode = dbcontext.postcode.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                var record = dbcontext.Applicant_Address_Profile.FirstOrDefault(m => m.ID == id);
                ViewBag.idemp = record.Applicant_Profile.ID.ToString();

                if (record != null)
                {
                    return View(record);
                }
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
        public ActionResult Edit(Applicant_Address_Profile model, string command)
        {
            try
            {
                if (model.countryid == 0) { model.countryid = null; }
                if (model.areaid == 0) { model.areaid = null; }
                if (model.stateid == 0) { model.stateid = null; }
                if (model.Territoriesid == 0) { model.Territoriesid = null; }
                if (model.citiesid == 0) { model.citiesid = null; }
                if (model.postcodeId == 0) { model.postcodeId = null; }

                ViewBag.Employee_Profile = dbcontext.Applicant_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Country = dbcontext.Country.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Area = dbcontext.Area.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.the_states = dbcontext.the_states.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Territories = dbcontext.Territories.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.cities = dbcontext.cities.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.postcode = dbcontext.postcode.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                //     ViewBag.idemp = model.Employee_ProfileId;
                var EmpObj = dbcontext.Applicant_Profile.FirstOrDefault(a => a.ID == model.Applicant_Profile.ID);

                var record = dbcontext.Applicant_Address_Profile.FirstOrDefault(m => m.ID == model.ID);
                var list = dbcontext.Applicant_Address_Profile.Where(a => a.Resident == true).ToList();
                //     var emp = record.Employee_Profile;
                record.Code = model.Code;
                if (list != null)
                {
                    for (int i = 0; i < list.Count(); i++)
                    {
                        list[i].Resident = false;
                    }
                    record.Resident = true;
                }


                record.Streetname = model.Streetname;
                record.Streetnumber = model.Streetnumber;
                record.Pobox = model.Pobox;
                record.DistancefromMeetingpointtoworklocationkm = model.DistancefromMeetingpointtoworklocationkm;
                record.Distancetoworklocationkm = model.Distancetoworklocationkm;
                record.Transportation_method = model.Transportation_method;
                var empid = EmpObj.Code + "------" + EmpObj.Name;
                record.Employee_ProfileId = model.Employee_ProfileId == null ? model.Employee_ProfileId = EmpObj.ID.ToString() : model.Employee_ProfileId;
                ViewBag.idemp = model.Employee_ProfileId;
                record.Applicant_Profile = EmpObj;

                record.countryid = model.countryid;
                record.areaid = model.areaid;
                record.stateid = model.stateid;
                record.Territoriesid = model.Territoriesid;
                record.citiesid = model.citiesid;
                record.postcodeId = model.postcodeId;

                dbcontext.SaveChanges();

                if (command == "Submit")
                {
                    return RedirectToAction("edit", "Applicant_Profile", new { id = EmpObj.ID });
                }
                return RedirectToAction("index", new { id = EmpObj.ID });
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
                var record = dbcontext.Applicant_Address_Profile.FirstOrDefault(m => m.ID == id);
                ViewBag.idemp = record.Applicant_Profile.ID.ToString();

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
            var record = dbcontext.Applicant_Address_Profile.FirstOrDefault(m => m.ID == id);
            ViewBag.idemp = record.Applicant_Profile.ID.ToString();

            try
            {
                dbcontext.Applicant_Address_Profile.Remove(record);
                dbcontext.SaveChanges();
                return RedirectToAction("index", new { id = record.Employee_ProfileId });
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