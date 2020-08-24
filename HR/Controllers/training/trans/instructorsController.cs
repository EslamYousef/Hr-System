using HR.Models;
using HR.Models.Infra;
using HR.Models.Training.trans;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers.training.trans
{
    [Authorize(Roles = "Admin,talent,talentCards,courses_card")]
    public class instructorsController : BaseController
    {
        // GET: Training_center_branches
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Country
        public ActionResult Index()
        {
            var model = dbcontext.Instructor.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            //////
            var modell = new Instructor();
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Talent_Development).Structure_Code;
            var model = dbcontext.Instructor.ToList();
            var code = "";
            if (model.Count() == 0)
            {
                modell.InstructorID = stru + "1";
             
            }
            else
            {
                modell.InstructorID = stru + (model.LastOrDefault().ID + 1).ToString();
                
            }
            ViewBag.contact_method = dbcontext.Contact_methods.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
            ViewBag.country = dbcontext.Country.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
            ViewBag.area = new List<Area>();
            ViewBag.state = new List<the_states>();
            ViewBag.ter = new List<Territories>();
            ViewBag.city = new List<cities>();
            ViewBag.postal = new List<cities>();
            var create_model = new VM { Instructor = modell, gender = gender.Male };
            /////
            return View(create_model);
        }
        [HttpPost]
        public ActionResult create(FormCollection form, VM model)
        {
            try
            {
                ViewBag.contact_method = dbcontext.Contact_methods.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.country = dbcontext.Country.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.area = new List<Area>();
                ViewBag.state = new List<the_states>();
                ViewBag.ter = new List<Territories>();
                ViewBag.city = new List<cities>();
                ViewBag.postal = new List<cities>();
                if (model.Instructor.CountryCode != null && int.Parse(model.Instructor.CountryCode) > 0)
                {
                    var ID = int.Parse(model.Instructor.CountryCode);
                    var country = dbcontext.Country.FirstOrDefault(m => m.ID == ID);
                    ViewBag.area = dbcontext.Area.Where(m => m.Countryid == country.ID.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                    model.Instructor.CountryCode = country.Code;
                }
                if (model.Instructor.Region_Code != null && int.Parse(model.Instructor.Region_Code) > 0)
                {
                    var ID = int.Parse(model.Instructor.Region_Code);
                    var region = dbcontext.Area.FirstOrDefault(m => m.ID == ID);
                    ViewBag.state = dbcontext.the_states.Where(m => m.Areaid == region.ID.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                    model.Instructor.Region_Code = region.Code;
                }
                if (model.Instructor.State_Governorate_Code != null && int.Parse(model.Instructor.State_Governorate_Code) > 0)
                {
                    var ID = int.Parse(model.Instructor.State_Governorate_Code);
                    var state = dbcontext.the_states.FirstOrDefault(m => m.ID == ID);
                    ViewBag.ter = dbcontext.Territories.Where(m => m.the_statesid == state.ID.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                    model.Instructor.State_Governorate_Code = state.Code;
                }

                if (model.Instructor.County_Code != null && int.Parse(model.Instructor.County_Code) > 0)
                {
                    var id = int.Parse(model.Instructor.County_Code);
                    var county = dbcontext.Territories.FirstOrDefault(m => m.ID == id);
                    ViewBag.city = dbcontext.cities.Where(m => m.Territoriesid == county.ID.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                    model.Instructor.County_Code = county.Code;
                }

                if (model.Instructor.City_Code != null && int.Parse(model.Instructor.City_Code) > 0)
                {
                    var id_code = int.Parse(model.Instructor.City_Code);
                    var city = dbcontext.cities.FirstOrDefault(m => m.ID == id_code);
                    ViewBag.postal = dbcontext.postcode.Where(m => m.citiesid == city.ID.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                    model.Instructor.City_Code = city.Code;
                }
                if (model.Instructor.Zip_Postal_Code != null && int.Parse(model.Instructor.Zip_Postal_Code) > 0)
                {
                    var ID = int.Parse(model.Instructor.Zip_Postal_Code);
                    var postal = dbcontext.postcode.FirstOrDefault(m => m.ID == ID);
                    model.Instructor.Zip_Postal_Code = postal.Code;
                }
                if (ModelState.IsValid)
                {
                    model.Instructor.Created_By = User.Identity.Name;
                    model.Instructor.Created_Date = DateTime.Now.Date;
                    model.Instructor.Gender = (short)model.gender.GetHashCode();
                    var instr = dbcontext.Instructor.Add(model.Instructor);
                    dbcontext.SaveChanges();
                    var contact_id = form["contact_id"].Split(',');
                    var contact_details = form["contact_details1"].Split(',');
                    for (var i = 0; i < contact_id.Length; i++)
                    {
                        if (contact_id[i] != "")
                        {
                            var ID = int.Parse(contact_id[i]);
                            var details = contact_details[i];
                            var contact = dbcontext.Contact_methods.FirstOrDefault(m => m.ID == ID);
                            var branch_Contact = new Instructor_Contact { InstructorID = model.Instructor.InstructorID.ToString(),cost_des = contact.Code + "-" + contact.Name, cost_ID = ID,  ContactMethod_Code = contact.Code, Contact_Method_Detail = details, Created_By = User.Identity.Name, Created_Date = DateTime.Now.Date };
                            dbcontext.Instructor_Contact.Add(branch_Contact);
                            dbcontext.SaveChanges();
                        }
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
        public JsonResult getallpostal(string id)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            var postal = dbcontext.postcode.Where(m => m.citiesid == id).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            return Json(postal);

        }
        public JsonResult getallcontent(string id)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            var content = dbcontext.Instructor_Contact.Where(m => m.InstructorID == id).ToList();
            return Json(content);

        }
        public ActionResult edit(int id)
        {
            try
            {
                ViewBag.contact_method = dbcontext.Contact_methods.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                 ViewBag.country = dbcontext.Country.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.area = new List<Area>();
                ViewBag.state = new List<the_states>();
                ViewBag.ter = new List<Territories>();
                ViewBag.city = new List<cities>();
                ViewBag.postal = new List<cities>();
                var model = dbcontext.Instructor.FirstOrDefault(m => m.ID == id);
                if (model.CountryCode != null && model.CountryCode != "0")
                {

                    var country = dbcontext.Country.FirstOrDefault(m => m.Code == model.CountryCode);
                    ViewBag.area = dbcontext.Area.Where(m => m.Countryid == country.ID.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                    model.CountryCode = country.ID.ToString();
                    if (model.Region_Code != null && model.Region_Code != "0")
                    {

                        var region = dbcontext.Area.FirstOrDefault(m => m.Code == model.Region_Code);
                        ViewBag.state = dbcontext.the_states.Where(m => m.Areaid == region.ID.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                        model.Region_Code = region.ID.ToString();
                        if (model.State_Governorate_Code != null && model.State_Governorate_Code != "0")
                        {
                            //var ID = int.Parse(model.State_Governorate_Code);
                            var state = dbcontext.the_states.FirstOrDefault(m => m.Code == model.State_Governorate_Code);
                            ViewBag.ter = dbcontext.Territories.Where(m => m.the_statesid == state.ID.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                            model.State_Governorate_Code = state.ID.ToString();
                            if (model.County_Code != null && model.County_Code != "0")
                            {

                                var county = dbcontext.Territories.FirstOrDefault(m => m.Code == model.County_Code);
                                ViewBag.city = dbcontext.cities.Where(m => m.Territoriesid == county.ID.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                                model.County_Code = county.ID.ToString();
                                if (model.City_Code != null && model.City_Code != "0")
                                {

                                    var city = dbcontext.cities.FirstOrDefault(m => m.Code == model.City_Code);
                                    ViewBag.postal = dbcontext.postcode.Where(m => m.citiesid == city.ID.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                                    model.City_Code = city.ID.ToString();
                                    if (model.Zip_Postal_Code != null && model.Zip_Postal_Code != "0")
                                    {

                                        var postal = dbcontext.postcode.FirstOrDefault(m => m.Code == model.Zip_Postal_Code);
                                        model.Zip_Postal_Code = postal.ID.ToString();
                                    }
                                }
                            }
                        }
                    }
                }
                var edit_model = new VM { Instructor = model, gender = (gender)model.Gender };
                return View(edit_model);

            }
            catch (Exception e)
            {

                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult edit(FormCollection form, VM model)
        {
            try
            {
                ViewBag.contact_method = dbcontext.Contact_methods.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.country = dbcontext.Country.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.area = new List<Area>();
                ViewBag.state = new List<the_states>();
                ViewBag.ter = new List<Territories>();
                ViewBag.city = new List<cities>();
                ViewBag.postal = new List<cities>();
                if (model.Instructor.CountryCode != null && int.Parse(model.Instructor.CountryCode) > 0)
                {
                    var ID = int.Parse(model.Instructor.CountryCode);
                    var country = dbcontext.Country.FirstOrDefault(m => m.ID == ID);
                    ViewBag.area = dbcontext.Area.Where(m => m.Countryid == country.ID.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                    model.Instructor.CountryCode = country.Code;
                }
                if (model.Instructor.Region_Code != null && int.Parse(model.Instructor.Region_Code) > 0)
                {
                    var ID = int.Parse(model.Instructor.Region_Code);
                    var region = dbcontext.Area.FirstOrDefault(m => m.ID == ID);
                    ViewBag.state = dbcontext.the_states.Where(m => m.Areaid == region.ID.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                    model.Instructor.Region_Code = region.Code;
                }
                if (model.Instructor.State_Governorate_Code != null && int.Parse(model.Instructor.State_Governorate_Code) > 0)
                {
                    var ID = int.Parse(model.Instructor.State_Governorate_Code);
                    var state = dbcontext.the_states.FirstOrDefault(m => m.ID == ID);
                    ViewBag.ter = dbcontext.Territories.Where(m => m.the_statesid == state.ID.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                    model.Instructor.State_Governorate_Code = state.Code;
                }

                if (model.Instructor.County_Code != null && int.Parse(model.Instructor.County_Code) > 0)
                {
                    var id = int.Parse(model.Instructor.County_Code);
                    var county = dbcontext.Territories.FirstOrDefault(m => m.ID == id);
                    ViewBag.city = dbcontext.cities.Where(m => m.Territoriesid == county.ID.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                    model.Instructor.County_Code = county.Code;
                }

                if (model.Instructor.City_Code != null && int.Parse(model.Instructor.City_Code) > 0)
                {
                    var id_code = int.Parse(model.Instructor.City_Code);
                    var city = dbcontext.cities.FirstOrDefault(m => m.ID == id_code);
                    ViewBag.postal = dbcontext.postcode.Where(m => m.citiesid == city.ID.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                    model.Instructor.City_Code = city.Code;
                }
                if (model.Instructor.Zip_Postal_Code != null && int.Parse(model.Instructor.Zip_Postal_Code) > 0)
                {
                    var ID = int.Parse(model.Instructor.Zip_Postal_Code);
                    var postal = dbcontext.postcode.FirstOrDefault(m => m.ID == ID);
                    model.Instructor.Zip_Postal_Code = postal.Code;
                }
                if (ModelState.IsValid)
                {
                    model.Instructor.Modified_By = User.Identity.Name;
                    model.Instructor.Modified_Date = DateTime.Now.Date;
                    var instr = dbcontext.Instructor.FirstOrDefault(m => m.ID == model.Instructor.ID);
                    //=============================

                   
                    instr.Instructor_FullName = model.Instructor.Instructor_FullName;
                    instr.Instructor_ShortName = model.Instructor.Instructor_ShortName;
                    instr.CountryCode = model.Instructor.CountryCode;
                    instr.Region_Code = model.Instructor.Region_Code;
                    instr.State_Governorate_Code = model.Instructor.State_Governorate_Code;
                    instr.County_Code = model.Instructor.County_Code;
                    instr.City_Code = model.Instructor.City_Code;
                    instr.Zip_Postal_Code = model.Instructor.Zip_Postal_Code;
                    instr.Street_Name = model.Instructor.Street_Name;
                    instr.StreetNumber = model.Instructor.StreetNumber;
                    instr.PO_Box = model.Instructor.PO_Box;
                    instr.Gender = (short)model.gender.GetHashCode();
                    instr.Modified_By = User.Identity.Name;
                    instr.Modified_Date = DateTime.Now.Date;
                    dbcontext.SaveChanges();
                    //=============================
                    var content = dbcontext.Instructor_Contact.Where(m => m.InstructorID == model.Instructor.InstructorID).ToList();
                    dbcontext.Instructor_Contact.RemoveRange(content);
                    dbcontext.SaveChanges();
                    //============================
                    var contact_id = form["contact_id"].Split(',');
                    var contact_details = form["contact_details1"].Split(',');
                    for (var i = 0; i < contact_id.Length; i++)
                    {
                        if (contact_id[i] != "")
                        {
                            var ID = int.Parse(contact_id[i]);
                            var details = contact_details[i];
                            var contact = dbcontext.Contact_methods.FirstOrDefault(m => m.ID == ID);
                            var inst = new Instructor_Contact { InstructorID=model.Instructor.InstructorID.ToString(),cost_des = contact.Code + "-" + contact.Name, cost_ID = ID, ContactMethod_Code = contact.Code, Contact_Method_Detail = details, Created_By = User.Identity.Name, Created_Date = DateTime.Now.Date };
                            dbcontext.Instructor_Contact.Add(inst);
                            dbcontext.SaveChanges();
                        }
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
        public ActionResult Delete(int id)
        {
            try
            {
                var record = dbcontext.Instructor_Contact.FirstOrDefault(m => m.ID == id);
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
            var record = dbcontext.Instructor.FirstOrDefault(m => m.ID == id);
            var contect = dbcontext.Instructor_Contact.Where(m => m.InstructorID == record.InstructorID);
            try
            {
                dbcontext.Instructor.Remove(record);
                dbcontext.Instructor_Contact.RemoveRange(contect);
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
                return View(record);
            }
        }
    }
    public class VM
    {
        public Instructor Instructor { get; set; }
        public gender gender { get; set; }

    }
    public enum gender
    {
        Male=1,
        Female=2
    }
}
    
   
