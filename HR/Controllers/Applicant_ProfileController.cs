using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HR.Models;
using System.Data.Entity.Infrastructure;
using HR.Models.Infra;
using HR.Models.ViewModel;
using System.IO;

namespace HR.Controllers
{
    public class Applicant_ProfileController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Applicant_Profile
        public ActionResult Index()
        {
            var model = dbcontext.Applicant_Profile.ToList();
            return View(model);
        }
        public ActionResult Create(string id)
        {

            ViewBag.Country = dbcontext.Country.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.Area = dbcontext.Area.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.the_states = dbcontext.the_states.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.Territories = dbcontext.Territories.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.cities = dbcontext.cities.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.Religion = dbcontext.Religion.ToList().Select(m => new { Code = m.Code + "-----[" + m.Name + ']', ID = m.ID });
            ViewBag.Nationality = dbcontext.Nationality.ToList().Select(m => new { Code = m.Code + "-----[" + m.Name + ']', ID = m.ID });
            ViewBag.job_title_cards = dbcontext.job_title_cards.ToList().Select(m => new { Code = m.Code + "-----[" + m.name + ']', ID = m.ID });

          
                DateTime statis = Convert.ToDateTime("1/1/1900");
                var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Recuirtment);
                var model = dbcontext.Applicant_Profile.ToList();
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
                var Applicant = new Applicant_Profile { Code = stru.Structure_Code + count, Birth_date = statis, Issue_date = statis, Expire_date = statis, registration_date = statis };
                return View(Applicant);
        }

        [HttpPost]
        public ActionResult Create(Applicant_Profile model, HttpPostedFileBase MyItem, string command, string command2, string command3, string command4, string command5, string command6, string command7, string command8, string command9, string command10, string command11, string command12)
        {
            try
            {
                if (model.Countryid == 0) { model.Countryid = 0; }
                if (model.Areaid == 0) { model.Areaid = 0; }
                if (model.the_statesid == 0) { model.the_statesid = 0; }
                if (model.Territoriesid == 0) { model.Territoriesid = 0; }
                if (model.citiesid == 0) { model.citiesid = 0; }

                if (model.Countryaddressid == 0) { model.Countryaddressid = 0; }
                if (model.Areaaddressid == 0) { model.Areaaddressid = 0; }
                if (model.Territoriesaddressid == 0) { model.Territoriesaddressid = 0; }
                if (model.citiesaddressid == 0) { model.citiesaddressid = 0; }
                if (model.the_statesaddressid == 0) { model.the_statesaddressid = 0; }

                ViewBag.Country = dbcontext.Country.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Area = dbcontext.Area.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.the_states = dbcontext.the_states.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Territories = dbcontext.Territories.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.cities = dbcontext.cities.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Religion = dbcontext.Religion.ToList().Select(m => new { Code = m.Code + "-----[" + m.Name + ']', ID = m.ID });
                ViewBag.Nationality = dbcontext.Nationality.ToList().Select(m => new { Code = m.Code + "-----[" + m.Name + ']', ID = m.ID });
                ViewBag.job_title_cards = dbcontext.job_title_cards.ToList().Select(m => new { Code = m.Code + "-----[" + m.name + ']', ID = m.ID });

                Applicant_Profile record = new Applicant_Profile();
                if (model.Health_Status == Health_Status.Ability)
                {
                    record.Health_Status = model.Health_Status;
                    model.registration_date = Convert.ToDateTime("01/01/1900").Date;
                    record.registration_date = model.registration_date;
                }
                else if ((model.Health_Status == Health_Status.In_Ability || (model.Health_Status == Health_Status.In_Ability_military_operations)))
                {
                    record.Health_Status = model.Health_Status;
                    record.registration_date = model.registration_date;
                }

                //if (ModelState.IsValid)
                //{
                    record.Code = model.Code;

                    //if (model.Service_Information.CurrencyId == "0" || model.Service_Information.CurrencyId == null)
                    //{
                    //    ModelState.AddModelError("", "Currency Code must enter");
                    //    return View(model);
                    //}

                    if (model.ReligionId == 0)
                    {
                        ModelState.AddModelError("", HR.Resource.Personnel.ReligionCodemustenter);
                        return View(model);
                    }
                    if (model.NationalityId == 0)
                    {
                        ModelState.AddModelError("", HR.Resource.Personnel.NationalityCodemustenter);
                        return View(model);
                    }
                //       Employee_Profile record = new Employee_Profile();
                record.Name = model.Name;
                    record.Full_Name = model.Full_Name;
                    record.Surname = model.Surname;
                    record.Arabic = model.Arabic;
                    record.Full = model.Full;
                    record.Sur_Name = model.Sur_Name;
                    record.Gender = model.Gender;
                    record.Marital_Status = model.Marital_Status;
                    record.ReligionId = model.ReligionId;
                    record.Religion = dbcontext.Religion.FirstOrDefault(m => m.ID == record.ReligionId);
                    record.NationalityId = model.NationalityId;
                    record.Nationality = dbcontext.Nationality.FirstOrDefault(m => m.ID == record.NationalityId);
                    record.Citizen = model.Citizen;
                    record.Blood_group = model.Blood_group;
                    record.ID_type = model.ID_type;
                    //record.Active = true;

                    record.Birth_date = model.Birth_date;

                    record.Issue_date = model.Issue_date;
                    record.Expire_date = model.Expire_date;
                    if (model.Issue_date > model.Expire_date)
                    {
                        TempData["Message"] = HR.Resource.Personnel.IssuedatebiggerExpirydate;
                        return View(model);
                    }
                    record.ID_number = model.ID_number;
                    record.Countryid = model.Countryid;
                    record.Areaid = model.Areaid;
                    record.the_statesid = model.the_statesid;
                    record.Territoriesid = model.Territoriesid;
                    record.citiesid = model.citiesid;

                    record.Countryaddressid = model.Countryaddressid;
                    record.Areaaddressid = model.Areaaddressid;
                    var Areaaddressid = model.Areaaddressid;
                    record.the_statesaddressid = model.the_statesaddressid;
                    record.Territoriesaddressid = model.Territoriesaddressid;
                    record.citiesaddressid = model.citiesaddressid;
                    record.Inability_reason = model.Inability_reason;
                    record.Inability_description = model.Inability_description;
                    record.registration_number = model.registration_number;

                    record.Pension = model.Pension;
                    record.Pension_source = model.Pension_source;
                    record.Have_pension = model.Have_pension;

                    record.job_title_cardsId = model.job_title_cardsId;
                    //record.job_title_cards = dbcontext.job_title_cards.FirstOrDefault(m => m.ID == record.job_title_cardsId);

                   
                    var emp = dbcontext.Applicant_Profile.Add(record);
                    dbcontext.SaveChanges();
                    // var fileId = 1;
                    //   var File =  Guid.NewGuid();
                    if (MyItem == null)
                    {
                        emp.EmpProfileIMG = null;

                    }
                    else if (MyItem.FileName != null)
                    {
                        var code = record.Code;
                        string folderpath = Server.MapPath("~/RecIMGFiles/") /*(@"c:\users\3lamya\documents\visual studio 2015\projects\systemuserfakahany\systemuserfakahany\files\")*/;
                        Directory.CreateDirectory(folderpath + code);
                        string mypath = folderpath + code;
                        string filename = Guid.NewGuid() + Path.GetExtension(MyItem.FileName);
                        MyItem.SaveAs(mypath + "/" + filename);

                        model.EmpProfileIMG = filename;
                        emp.EmpProfileIMG = model.EmpProfileIMG;
                    }
                    dbcontext.SaveChanges();
                    ///////////////////////////////////////////////////////////////////////////

                    if (record.Gender == Gender.female && command8 == "Submit")
                    {
                        TempData["Message"] = HR.Resource.Personnel.Youmustchooseamalefromthegender;
                        return View(model);
                    }
                    else if (record.Gender == Gender.other && command8 == "Submit")
                    {
                        TempData["Message"] = HR.Resource.Personnel.Youmustchooseamalefromthegender;
                        return View(model);
                    }





                    //////////////////////////
                    if (command == "Submit")
                    {
                        return RedirectToAction("index", "Applicant_Address_Profile", new { id = emp.ID });
                    }
                if (command == "Submit2")
                {
                    return RedirectToAction("index", "Applicant_Attachment_Profile", new { id = emp.ID });
                }
                if (command == "Submit3")
                    {
                        return RedirectToAction("index", "Applicant_Qualification_Profile", new { id = emp.ID });
                    }
                    if (command == "Submit4")
                    {
                        return RedirectToAction("index", "Applicant_Family_Profile", new { id = emp.ID });
                    }
                    if (command == "Submit5")
                    {
                        return RedirectToAction("index", "Applicant_Previous_Experiences_Profile", new { id = emp.ID });
                    }
                    if (command == "Submit6")
                    {
                        return RedirectToAction("index", "Applicant_Contact_Profile", new { id = emp.ID });
                    }
                    if (command == "Submit7")
                    {
                        return RedirectToAction("index", "Applicant_Military_Service_Profile", new { id = emp.ID });
                    }
                    if (command == "Submit8")
                    {
                        return RedirectToAction("index", "Applicant_Subscription_Syndicate_Profile", new { id = emp.ID });
                    }
                  
                 
                    return RedirectToAction("Index");
                //}
                //else
                //{
                //    return View(model);
                //}
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
                ViewBag.Country = dbcontext.Country.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Area = dbcontext.Area.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.the_states = dbcontext.the_states.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Territories = dbcontext.Territories.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.cities = dbcontext.cities.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Religion = dbcontext.Religion.ToList().Select(m => new { Code = m.Code + "-----[" + m.Name + ']', ID = m.ID });
                ViewBag.Nationality = dbcontext.Nationality.ToList().Select(m => new { Code = m.Code + "-----[" + m.Name + ']', ID = m.ID });
                ViewBag.job_title_cards = dbcontext.job_title_cards.ToList().Select(m => new { Code = m.Code + "-----[" + m.name + ']', ID = m.ID });

                var record = dbcontext.Applicant_Profile.FirstOrDefault(m => m.ID == id);
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
        public ActionResult Edit(Applicant_Profile model, HttpPostedFileBase MyItem, string command, string command2, string command3, string command4, string command5, string command6, string command7, string command8, string command9, string command10, string command11, string command12)
        {
            try
            {
                //if (model.Countryid == 0) { model.Countryid = 0; }
                //if (model.Areaid == 0) { model.Areaid = 0; }
                //if (model.the_statesid == 0) { model.the_statesid = 0; }
                //if (model.Territoriesid == 0) { model.Territoriesid = 0; }
                //if (model.citiesid == 0) { model.citiesid = 0; }

                //if (model.Countryaddressid == 0) { model.Countryaddressid = 0; }
                //if (model.Areaaddressid == 0) { model.Areaaddressid = 0; }
                //if (model.Territoriesaddressid == 0) { model.Territoriesaddressid = 0; }
                //if (model.citiesaddressid == 0) { model.citiesaddressid = 0; }
                //if (model.the_statesaddressid == 0) { model.the_statesaddressid = 0; }

                ViewBag.Country = dbcontext.Country.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Area = dbcontext.Area.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.the_states = dbcontext.the_states.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Territories = dbcontext.Territories.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.cities = dbcontext.cities.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Religion = dbcontext.Religion.ToList().Select(m => new { Code = m.Code + "-----[" + m.Name + ']', ID = m.ID });
                ViewBag.Nationality = dbcontext.Nationality.ToList().Select(m => new { Code = m.Code + "-----[" + m.Name + ']', ID = m.ID });
                ViewBag.job_title_cards = dbcontext.job_title_cards.ToList().Select(m => new { Code = m.Code + "-----[" + m.name + ']', ID = m.ID });

               
                var record = dbcontext.Applicant_Profile.FirstOrDefault(m => m.ID == model.ID);

                if (model.Health_Status == Health_Status.Ability)
                {
                    record.Health_Status = model.Health_Status;
                    model.registration_date = record.registration_date;
                }
                else if ((model.Health_Status == Health_Status.In_Ability || (model.Health_Status == Health_Status.In_Ability_military_operations)))
                {
                    record.Health_Status = model.Health_Status;
                    record.registration_date = model.registration_date;
                }
                record.Code = model.Code;
                if (MyItem != null)
                {
                    var code = model.Code;
                    //var File = Guid.NewGuid();

                    string folderpath = Server.MapPath("~/RecIMGFiles/") /*(@"c:\users\3lamya\documents\visual studio 2015\projects\systemuserfakahany\systemuserfakahany\files\")*/;
                    Directory.CreateDirectory(folderpath + code);
                    string mypath = folderpath + code;
                    string filename = Guid.NewGuid() + Path.GetExtension(MyItem.FileName);
                    MyItem.SaveAs(mypath + "/" + filename);
                    model.EmpProfileIMG = filename;
                    record.EmpProfileIMG = model.EmpProfileIMG;
                }
                record.Name = model.Name;
                record.Full_Name = model.Full_Name;
                record.Surname = model.Surname;
                record.Arabic = model.Arabic;
                record.Full = model.Full;
                record.Sur_Name = model.Sur_Name;
                record.Gender = model.Gender;
                record.Marital_Status = model.Marital_Status;
                record.ReligionId = model.ReligionId;
                var ReligionId = model.ReligionId;
                record.Religion = dbcontext.Religion.FirstOrDefault(m => m.ID == ReligionId);
                record.NationalityId = model.NationalityId;
                var NationalityId = model.NationalityId;
                record.Nationality = dbcontext.Nationality.FirstOrDefault(m => m.ID == NationalityId);
                record.Citizen = model.Citizen;
                record.Blood_group = model.Blood_group;
                record.ID_type = model.ID_type;

                record.Birth_date = model.Birth_date;

                record.Issue_date = model.Issue_date;
                record.Expire_date = model.Expire_date;
                if (model.Issue_date > model.Expire_date)
                {
                    TempData["Message"] = HR.Resource.Personnel.IssuedatebiggerExpirydate;
                    return View(model);
                }
                record.ID_number = model.ID_number;
                record.Countryid = model.Countryid;
                record.Areaid = model.Areaid;
                record.the_statesid = model.the_statesid;
                record.Territoriesid = model.Territoriesid;
                record.citiesid = model.citiesid;
             
                record.Countryaddressid = model.Countryaddressid;
                record.Areaaddressid = model.Areaaddressid;
                record.the_statesaddressid = model.the_statesaddressid;
                record.Territoriesaddressid = model.Territoriesaddressid;
                record.citiesaddressid = model.citiesaddressid;
                                
                record.Inability_reason = model.Inability_reason;
                record.Inability_description = model.Inability_description;
                record.registration_number = model.registration_number;

                record.Pension = model.Pension;
                record.Pension_source = model.Pension_source;
                record.Have_pension = model.Have_pension;
                record.job_title_cardsId = model.job_title_cardsId;

                dbcontext.SaveChanges();

                if (command == "Submit")
                {
                    return RedirectToAction("index", "Applicant_Address_Profile", new { id = record.ID });
                }
                if (command == "Submit2")
                {
                    return RedirectToAction("index", "Applicant_Attachment_Profile", new { id = record.ID });
                }
                if (command == "Submit3")
                {
                    return RedirectToAction("index", "Applicant_Qualification_Profile", new { id = record.ID });
                }
                if (command == "Submit4")
                {
                    return RedirectToAction("index", "Applicant_Family_Profile", new { id = record.ID });
                }
                if (command == "Submit5")
                {
                    return RedirectToAction("index", "Applicant_Previous_Experiences_Profile", new { id = record.ID });
                }
                if (command == "Submit6")
                {
                    return RedirectToAction("index", "Applicant_Contact_Profile", new { id = record.ID });
                }
                if (record.Gender == Gender.male && command == "Submit7")
                {
                    return RedirectToAction("index", "Applicant_Military_Service_Profile", new { id = record.ID });
                }
                else if (record.Gender == Gender.female && command == "Submit7")
                {
                    TempData["Message"] = HR.Resource.Personnel.Youmustchooseamalefromthegender;
                    return View(model);
                }
                else if (record.Gender == Gender.other && command == "Submit7")
                {
                    TempData["Message"] = HR.Resource.Personnel.Youmustchooseamalefromthegender;
                    return View(model);
                }
                if (command == "Submit8")
                {
                    return RedirectToAction("index", "Applicant_Subscription_Syndicate_Profile", new { id = record.ID });
                }
              
               
                return RedirectToAction("index");
            }
            catch (DbUpdateException e)
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
                var record = dbcontext.Applicant_Profile.FirstOrDefault(m => m.ID == id);
                var Employee_Address_Profile = dbcontext.Employee_Address_Profile.FirstOrDefault(m => m.ID == record.ID);
                var Employee_Qualification_Profile = dbcontext.Employee_Qualification_Profile.FirstOrDefault(m => m.ID == record.ID);
               
                var Position_Information = dbcontext.Position_Information.Where(m => m.Employee_Profile.ID == record.ID);
                //var Position_Transaction_Information = dbcontext.Position_Transaction_Information.FirstOrDefault(m => m.ID == record.Position_Transaction_Information.ID);
                var Employee_family_profile = dbcontext.Employee_family_profile.FirstOrDefault(m => m.ID == record.ID);
                var Employee_experience_profile = dbcontext.Employee_experience_profile.FirstOrDefault(m => m.ID == record.ID);

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
            var record = dbcontext.Applicant_Profile.FirstOrDefault(m => m.ID == id);
            var Employee_Address_Profile = dbcontext.Employee_Address_Profile.FirstOrDefault(m => m.ID == record.ID);
            var Employee_Qualification_Profile = dbcontext.Employee_Qualification_Profile.FirstOrDefault(m => m.ID == record.ID);
          
            var Position_Information = dbcontext.Position_Information.Where(m => m.Employee_Profile.ID == record.ID);

            var solt = dbcontext.Slots.FirstOrDefault(m => m.Employee_Profile.ID == record.ID);
            var Employee_family_profile = dbcontext.Employee_family_profile.FirstOrDefault(m => m.ID == record.ID);
            var Employee_experience_profile = dbcontext.Employee_experience_profile.FirstOrDefault(m => m.ID == record.ID);
            try
            {
                var old_slot = dbcontext.Slots.FirstOrDefault(m => m.Employee_Profile.ID == record.ID);
                if (old_slot != null)
                {
                    old_slot.Employee_Profile = null;
                    dbcontext.SaveChanges();
                }


                dbcontext.SaveChanges();
    
                if (Employee_Address_Profile != null)
                {
                    dbcontext.Employee_Address_Profile.Remove(Employee_Address_Profile);
                }
                if (Employee_Qualification_Profile != null)
                {
                    dbcontext.Employee_Qualification_Profile.Remove(Employee_Qualification_Profile);
                }
                if (Position_Information != null)
                {
                    dbcontext.Position_Information.RemoveRange(Position_Information);
                }
            
                if (Employee_family_profile != null)
                {
                    dbcontext.Employee_family_profile.Remove(Employee_family_profile);
                }
                if (Employee_experience_profile != null)
                {
                    dbcontext.Employee_experience_profile.Remove(Employee_experience_profile);
                }
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (DbUpdateException e)
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