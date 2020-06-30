using HR.Models;
using HR.Models.Infra;
using HR.Models.Training.setup;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers.training.setup
{
    public class Training_center_branchesController : BaseController
    {
        // GET: Training_center_branches
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Country
        public ActionResult Index()
        {
            var model = dbcontext.TrainingCenters_Branch.ToList();
            return View(model);
        }

        public ActionResult Create(string id)
        {
            //////
            var modell = new TrainingCenters_Branch();
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Talent_Development).Structure_Code;
            var model = dbcontext.TrainingCenters_Branch.ToList();
            var code = "";
            if (model.Count() == 0)
            {
                modell.Branch_Code = stru + "1";
                code= stru + "1";
            }
            else
            {
                modell.Branch_Code = stru + (model.LastOrDefault().ID + 1).ToString();
                code = stru + (model.LastOrDefault().ID + 1).ToString();
            }
            ViewBag.contact_method = dbcontext.Contact_methods.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
            ViewBag.centers = dbcontext.TrainingCenter.ToList().Select(m => new { Code = m.TrainingCenters_Code + "-[" + m.TrainingCenters_Desc + ']', ID = m.ID });
            ViewBag.country = dbcontext.Country.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
            ViewBag.area = new List<Area>();
            ViewBag.state = new List<the_states>();
            ViewBag.ter = new List<Territories>();
            ViewBag.city = new List<cities>();
            ViewBag.postal = new List<cities>();
                if (id != null)
            {
                var ID = int.Parse(id);
                var center = dbcontext.TrainingCenter.FirstOrDefault(m => m.ID == ID);
                var new_model = new TrainingCenters_Branch { Branch_Code = code, TrainingCenters_Code = center.TrainingCenters_Code };
               
                return View(model);
            }
            /////
            return View(modell);
        }
        [HttpPost]
        public ActionResult create(FormCollection form,TrainingCenters_Branch model)
        {
            try
            {
                ViewBag.contact_method = dbcontext.Contact_methods.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.centers = dbcontext.TrainingCenter.ToList().Select(m => new { Code = m.TrainingCenters_Code + "-[" + m.TrainingCenters_Desc + ']', ID = m.ID });
                ViewBag.country = dbcontext.Country.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.area = new List<Area>();
                ViewBag.state = new List<the_states>();
                ViewBag.ter = new List<Territories>();
                ViewBag.city = new List<cities>();
                ViewBag.postal = new List<cities>();
                if (model.CountryCode !=null && int.Parse(model.CountryCode) > 0 )
                {
                    var ID = int.Parse(model.CountryCode);
                    var country = dbcontext.Country.FirstOrDefault(m => m.ID == ID);
                    ViewBag.area = dbcontext.Area.Where(m => m.Countryid ==country.ID.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                    model.CountryCode = country.Code;
                }
                if (model.Region_Code!=null && int.Parse(model.Region_Code) > 0)
                {
                    var ID = int.Parse(model.Region_Code);
                    var region = dbcontext.Area.FirstOrDefault(m => m.ID ==ID);
                    ViewBag.state = dbcontext.the_states.Where(m => m.Areaid == region.ID.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                    model.Region_Code = region.Code;
                }
                if (model.State_Governorate_Code!=null&&int.Parse(model.State_Governorate_Code) > 0)
                {
                    var ID = int.Parse(model.State_Governorate_Code);
                    var state = dbcontext.the_states.FirstOrDefault(m => m.ID ==ID);
                    ViewBag.ter = dbcontext.Territories.Where(m => m.the_statesid == state.ID.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                    model.State_Governorate_Code = state.Code;
                }
               
                if (model.County_Code!=null&& int.Parse(model.County_Code) > 0)
                {
                    var id = int.Parse(model.County_Code);
                    var county = dbcontext.Territories.FirstOrDefault(m => m.ID == id);
                    ViewBag.city = dbcontext.cities.Where(m => m.Territoriesid == county.ID.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                    model.County_Code = county.Code;
                }
              
                if (model.City_Code!=null && int.Parse(model.City_Code) > 0)
                {
                    var id_code = int.Parse(model.City_Code);
                    var city = dbcontext.cities.FirstOrDefault(m => m.ID == id_code);
                    ViewBag.postal = dbcontext.postcode.Where(m => m.citiesid == city.ID.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                    model.City_Code = city.Code;
                }
                if (model.Zip_Postal_Code!=null && int.Parse(model.Zip_Postal_Code) > 0)
                {
                    var ID = int.Parse(model.Zip_Postal_Code);
                    var postal = dbcontext.postcode.FirstOrDefault(m => m.ID == ID);
                    model.Zip_Postal_Code = postal.Code;
                }
                if (ModelState.IsValid)
                {
                    model.Created_By = User.Identity.Name;
                    model.Created_Date = DateTime.Now.Date;
                    var branch= dbcontext.TrainingCenters_Branch.Add(model);
                    dbcontext.SaveChanges();
                    var contact_id = form["contact_id"].Split(',');
                    var contact_details = form["contact_details1"].Split(',');
                    for(var i= 0;i< contact_id.Length;i++)
                    {
                        if (contact_id[i] != "")
                        {
                            var ID = int.Parse(contact_id[i]);
                            var details = contact_details[i];
                            var contact = dbcontext.Contact_methods.FirstOrDefault(m => m.ID == ID);
                            var branch_Contact = new TrainingCenters_Branch_Contact {contact_name=contact.Code+"-"+contact.Name,contact_id=ID, Branch_Code = branch.Branch_Code, ContactMethod_Code=contact.Code,Contact_Method_Detail= details ,Created_By=User.Identity.Name,Created_Date=DateTime.Now.Date};
                            dbcontext.TrainingCenters_Branch_Contact.Add(branch_Contact);
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
           
            var content = dbcontext.TrainingCenters_Branch_Contact.Where(m => m.Branch_Code == id).ToList();
            return Json(content);

        }
        public ActionResult edit(int id)
        {
            try
            {
                ViewBag.contact_method = dbcontext.Contact_methods.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.centers = dbcontext.TrainingCenter.ToList().Select(m => new { Code = m.TrainingCenters_Code + "-[" + m.TrainingCenters_Desc + ']', ID = m.ID });
                ViewBag.country = dbcontext.Country.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.area = new List<Area>();
                ViewBag.state = new List<the_states>();
                ViewBag.ter = new List<Territories>();
                ViewBag.city = new List<cities>();
                ViewBag.postal = new List<cities>();
                var model = dbcontext.TrainingCenters_Branch.FirstOrDefault(m => m.ID == id);
                if (model.CountryCode != null &&model.CountryCode!="0")
                {
                   
                    var country = dbcontext.Country.FirstOrDefault(m => m.Code == model.CountryCode);
                    ViewBag.area = dbcontext.Area.Where(m => m.Countryid == country.ID.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                    model.CountryCode = country.ID.ToString();
                    if (model.Region_Code != null &&model.Region_Code!="0")
                    {

                        var region = dbcontext.Area.FirstOrDefault(m => m.Code == model.Region_Code);
                        ViewBag.state = dbcontext.the_states.Where(m => m.Areaid == region.ID.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                        model.Region_Code = region.ID.ToString();
                        if (model.State_Governorate_Code != null && model.State_Governorate_Code!="0")
                        {
                            //var ID = int.Parse(model.State_Governorate_Code);
                            var state = dbcontext.the_states.FirstOrDefault(m => m.Code == model.State_Governorate_Code);
                            ViewBag.ter = dbcontext.Territories.Where(m => m.the_statesid == state.ID.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                            model.State_Governorate_Code = state.ID.ToString();
                            if (model.County_Code != null && model.County_Code!="0")
                            {

                                var county = dbcontext.Territories.FirstOrDefault(m => m.Code == model.County_Code);
                                ViewBag.city = dbcontext.cities.Where(m => m.Territoriesid == county.ID.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                                model.County_Code = county.ID.ToString();
                                if (model.City_Code != null && model.City_Code!="0")
                                {

                                    var city = dbcontext.cities.FirstOrDefault(m => m.Code == model.City_Code);
                                    ViewBag.postal = dbcontext.postcode.Where(m => m.citiesid == city.ID.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                                    model.City_Code = city.ID.ToString();
                                    if (model.Zip_Postal_Code != null && model.Zip_Postal_Code!= "0")
                                    {

                                        var postal = dbcontext.postcode.FirstOrDefault(m => m.Code == model.Zip_Postal_Code);
                                        model.Zip_Postal_Code = postal.ID.ToString();
                                    }
                                }
                            }
                        }
                    }
                }
                return View(model);
                
            }
            catch (Exception e)
            {

                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult edit(FormCollection form, TrainingCenters_Branch model)
        {
            try
            {
                ViewBag.contact_method = dbcontext.Contact_methods.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.centers = dbcontext.TrainingCenter.ToList().Select(m => new { Code = m.TrainingCenters_Code + "-[" + m.TrainingCenters_Desc + ']', ID = m.ID });
                ViewBag.country = dbcontext.Country.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.area = new List<Area>();
                ViewBag.state = new List<the_states>();
                ViewBag.ter = new List<Territories>();
                ViewBag.city = new List<cities>();
                ViewBag.postal = new List<cities>();
                if (model.CountryCode != null && int.Parse(model.CountryCode) > 0)
                {
                    var ID = int.Parse(model.CountryCode);
                    var country = dbcontext.Country.FirstOrDefault(m => m.ID == ID);
                    ViewBag.area = dbcontext.Area.Where(m => m.Countryid == country.ID.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                    model.CountryCode = country.Code;
                }
                if (model.Region_Code != null && int.Parse(model.Region_Code) > 0)
                {
                    var ID = int.Parse(model.Region_Code);
                    var region = dbcontext.Area.FirstOrDefault(m => m.ID == ID);
                    ViewBag.state = dbcontext.the_states.Where(m => m.Areaid == region.ID.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                    model.Region_Code = region.Code;
                }
                if (model.State_Governorate_Code != null && int.Parse(model.State_Governorate_Code) > 0)
                {
                    var ID = int.Parse(model.State_Governorate_Code);
                    var state = dbcontext.the_states.FirstOrDefault(m => m.ID == ID);
                    ViewBag.ter = dbcontext.Territories.Where(m => m.the_statesid == state.ID.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                    model.State_Governorate_Code = state.Code;
                }

                if (model.County_Code != null && int.Parse(model.County_Code) > 0)
                {
                    var id = int.Parse(model.County_Code);
                    var county = dbcontext.Territories.FirstOrDefault(m => m.ID == id);
                    ViewBag.city = dbcontext.cities.Where(m => m.Territoriesid == county.ID.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                    model.County_Code = county.Code;
                }

                if (model.City_Code != null && int.Parse(model.City_Code) > 0)
                {
                    var id_code = int.Parse(model.City_Code);
                    var city = dbcontext.cities.FirstOrDefault(m => m.ID == id_code);
                    ViewBag.postal = dbcontext.postcode.Where(m => m.citiesid == city.ID.ToString()).ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                    model.City_Code = city.Code;
                }
                if (model.Zip_Postal_Code != null && int.Parse(model.Zip_Postal_Code) > 0)
                {
                    var ID = int.Parse(model.Zip_Postal_Code);
                    var postal = dbcontext.postcode.FirstOrDefault(m => m.ID == ID);
                    model.Zip_Postal_Code = postal.Code;
                }
                if (ModelState.IsValid)
                {
                    model.Modified_By = User.Identity.Name;
                    model.Modified_Date = DateTime.Now.Date;
                    var branch = dbcontext.TrainingCenters_Branch.FirstOrDefault(m=>m.ID==model.ID);
                    //=============================

                    branch.TrainingCenters_Code = model.TrainingCenters_Code;
                    branch.Branch_Desc = model.Branch_Desc;
                    branch.Branch_AltDesc = model.Branch_AltDesc;
                    branch.CountryCode = model.CountryCode;
                    branch.Region_Code = model.Region_Code;
                    branch.State_Governorate_Code = model.State_Governorate_Code;
                    branch.County_Code = model.County_Code;
                    branch.City_Code = model.City_Code;
                    branch.Zip_Postal_Code = model.Zip_Postal_Code;
                    branch.Street_Name = model.Street_Name;
                    branch.StreetNumber = model.StreetNumber;
                    branch.PO_Box = model.PO_Box;
                    branch.Modified_By = User.Identity.Name;
                    branch.Modified_Date = DateTime.Now.Date;
                    dbcontext.SaveChanges();
                    //=============================
                    var content = dbcontext.TrainingCenters_Branch_Contact.Where(m => m.Branch_Code == model.Branch_Code).ToList();
                    dbcontext.TrainingCenters_Branch_Contact.RemoveRange(content);
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
                            var branch_Contact = new TrainingCenters_Branch_Contact { contact_name = contact.Code + "-" + contact.Name, contact_id = ID, Branch_Code = branch.Branch_Code, ContactMethod_Code = contact.Code, Contact_Method_Detail = details, Created_By = User.Identity.Name, Created_Date = DateTime.Now.Date };
                            dbcontext.TrainingCenters_Branch_Contact.Add(branch_Contact);
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
                var record = dbcontext.TrainingCenters_Branch.FirstOrDefault(m => m.ID == id);
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
            var record = dbcontext.TrainingCenters_Branch.FirstOrDefault(m => m.ID == id);
            var contect = dbcontext.TrainingCenters_Branch_Contact.Where(m => m.Branch_Code == record.Branch_Code);
            try
            {
                dbcontext.TrainingCenters_Branch.Remove(record);
                dbcontext.TrainingCenters_Branch_Contact.RemoveRange(contect);
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
}