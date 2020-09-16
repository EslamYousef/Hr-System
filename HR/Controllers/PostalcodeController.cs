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
    [Authorize(Roles = "Admin,Basic,BasicSetup,Address")]
    public class PostalcodeController : BaseController
    {
        // GET: Postalcode
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        public ActionResult Index()
        {
            var records = dbcontext.postcode.ToList();
            return View(records);
        }
        public ActionResult Create(string id)
        {
            var modell = new postcode();
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Basic).Structure_Code;
            var modelll = dbcontext.postcode.ToList();
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
            ViewBag.city = new List<cities>();
            if (id != null)
            {
                var ID = int.Parse(id);
                var city = dbcontext.cities.FirstOrDefault(m => m.ID == ID);
                var model = new postcode {Code=Code, cities=city,citiesid=city.ID.ToString(),  Territoriesid = city.Territories.ID.ToString(), stateid = city.Territories.the_states.ID, areaid =city.Territories.the_states.Area.ID , countryid = city.Territories.the_states.Area.Country.ID };
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
                var IdD = int.Parse(model.Territoriesid);
                if (IdD > 0)
                {
                    ViewBag.city = dbcontext.cities.Where(m => m.Territoriesid == model.Territoriesid.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });

                }
                return View(model);
            }
            var mmodel = new postcode();
            mmodel.Code = Code;
            return View(mmodel);
        }
        [HttpPost]
        public ActionResult Create(postcode model)
        {
            ViewBag.country = dbcontext.Country.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.area = new List<Area>();
            ViewBag.state = new List<the_states>();
            ViewBag.ter = new List<Territories>();
            ViewBag.city = new List<cities>();
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
            if (model.citiesid =="0"||model.citiesid==null)
            {
                ModelState.AddModelError("", HR.Resource.Basic.cityCodemustenter111);
                return View(model);
            }
            var id = int.Parse(model.Territoriesid);
            if (id > 0)
            {
                ViewBag.city = dbcontext.cities.Where(m => m.Territoriesid == model.Territoriesid.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            }
            try
            {
                if (ModelState.IsValid)
                {
                    postcode record = new postcode();
                    if (model.citiesid == "0" || model.citiesid == null)
                    {
                        ModelState.AddModelError("", HR.Resource.Basic.cityCodemustenter111);
                        return View(model);
                    }
                    record.Name = model.Name;
                    record.Description = model.Description;
                    record.Code = model.Code;
                    ////////////
                    record.citiesid = model.citiesid;
                    record.areaid = model.areaid;
                    record.stateid = model.stateid;
                    record.Territoriesid = model.Territoriesid;
                    record.countryid = model.countryid;
                    var ID = int.Parse(model.citiesid);
                    record.cities = dbcontext.cities.FirstOrDefault(m => m.ID == ID);
                    //////////
                    record.streetname = model.streetname;
                    record.numstreetfrom = model.numstreetfrom;
                    record.numstreetto = model.numstreetto;
                    record.typenumstreet = model.typenumstreet;
                    //////////
                    dbcontext.postcode.Add(record);
                    dbcontext.SaveChanges();
                    //=================================check for alert==================================

                    var get_result_check = HR.Controllers.check.check_alert("postal", HR.Models.user.Action.Create, HR.Models.user.type_field.form);
                    if (get_result_check != null)
                    {
                        var inbox = new Models.user.Alert_inbox { send_from_user_id = User.Identity.Name, send_to_user_id = get_result_check.send_to_ID_user, title = get_result_check.Subject, Subject = get_result_check.Message };
                        if (get_result_check.until != null)
                        {
                            if (get_result_check.until.Value.Year != 0001)
                            {
                                inbox.until = get_result_check.until;
                            }
                        }
                        ApplicationDbContext dbcontext = new ApplicationDbContext();
                        dbcontext.Alert_inbox.Add(inbox);
                        dbcontext.SaveChanges();

                    }
                    //===================================================================================
                    return RedirectToAction("Index");
                }
                else
                { return View(model); }
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
            var record = dbcontext.postcode.FirstOrDefault(m => m.ID == id);
            if (record == null)
            {
                TempData["Message"] = HR.Resource.Basic.thereisnodata;
                return View();
            }
            ViewBag.country = dbcontext.Country.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.area = new List<Area>();
            ViewBag.state = new List<the_states>();
            ViewBag.ter = new List<Territories>();
            ViewBag.city = new List<cities>();
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
            var ID = int.Parse(record.Territoriesid);
            if (ID > 0)
            {
                ViewBag.city = dbcontext.cities.Where(m => m.Territoriesid == record.Territoriesid.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });

            }
            return View(record);


        }
        [HttpPost]
        public ActionResult Edit(postcode model)
        {
            try
            {
                var record = dbcontext.postcode.FirstOrDefault(m => m.ID == model.ID);
                ViewBag.country = dbcontext.Country.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.area = new List<Area>();
                ViewBag.state = new List<the_states>();
                ViewBag.ter = new List<Territories>();
                ViewBag.city = new List<cities>();
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
                if (model.citiesid == "0" || model.citiesid == null)
                {
                    ModelState.AddModelError("", HR.Resource.Basic.cityCodemustenter111);
                    return View(model);
                }
                var id = int.Parse(model.Territoriesid);
                if (id > 0)
                {
                    ViewBag.city = dbcontext.cities.Where(m => m.Territoriesid == model.Territoriesid.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });

                }

                if (ModelState.IsValid)
                {
                    record.Name = model.Name;
                    record.Description = model.Description;
                    record.Code = model.Code;
                    ////////////
                    record.citiesid = model.citiesid;
                    record.areaid = model.areaid;
                    record.stateid = model.stateid;
                    record.Territoriesid = model.Territoriesid;
                    record.countryid = model.countryid;
                    var ID = int.Parse(model.citiesid);
                    record.cities = dbcontext.cities.FirstOrDefault(m => m.ID == ID);
                    //////////
                    record.streetname = model.streetname;
                    record.numstreetfrom = model.numstreetfrom;
                    record.numstreetto = model.numstreetto;
                    record.typenumstreet = model.typenumstreet;
                    //////////

                    dbcontext.SaveChanges();
                    //=================================check for alert==================================

                    var get_result_check = HR.Controllers.check.check_alert("postal", HR.Models.user.Action.edit, HR.Models.user.type_field.form);
                    if (get_result_check != null)
                    {
                        var inbox = new Models.user.Alert_inbox { send_from_user_id = User.Identity.Name, send_to_user_id = get_result_check.send_to_ID_user, title = get_result_check.Subject, Subject = get_result_check.Message };
                        if (get_result_check.until != null)
                        {
                            if (get_result_check.until.Value.Year != 0001)
                            {
                                inbox.until = get_result_check.until;
                            }
                        }
                        ApplicationDbContext dbcontext = new ApplicationDbContext();
                        dbcontext.Alert_inbox.Add(inbox);
                        dbcontext.SaveChanges();

                    }
                    //===================================================================================
                    return RedirectToAction("index");
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
            { return View(model); }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                var record = dbcontext.postcode.FirstOrDefault(m => m.ID == id);
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
            var record = dbcontext.postcode.FirstOrDefault(m => m.ID == id);

            try
            {
                dbcontext.postcode.Remove(record);
                dbcontext.SaveChanges();
                //=================================check for alert==================================

                var get_result_check = HR.Controllers.check.check_alert("postal", HR.Models.user.Action.delete, HR.Models.user.type_field.form);
                if (get_result_check != null)
                {
                    var inbox = new Models.user.Alert_inbox { send_from_user_id = User.Identity.Name, send_to_user_id = get_result_check.send_to_ID_user, title = get_result_check.Subject, Subject = get_result_check.Message };
                    if (get_result_check.until != null)
                    {
                        if (get_result_check.until.Value.Year != 0001)
                        {
                            inbox.until = get_result_check.until;
                        }
                    }
                    ApplicationDbContext dbcontext = new ApplicationDbContext();
                    dbcontext.Alert_inbox.Add(inbox);
                    dbcontext.SaveChanges();

                }
                //===================================================================================
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