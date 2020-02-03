using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HR.Models;
using System.Data.Entity.Infrastructure;
using HR.Models.Infra;
using HR.Models.ViewModel;

namespace HR.Controllers
{
    public class Employee_family_profileController : Controller
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Employee_family_profile
        public ActionResult Index(string id)
        {
            var ID = int.Parse(id);
            var new_model = dbcontext.Employee_family_profile.Where(m => m.Employee_Profile.ID == ID).ToList();
            ViewBag.idemp = id;
            return View(new_model);
        }
        public ActionResult Create(string id)
        {

            ViewBag.Nationality = dbcontext.Nationality.ToList().Select(m => new { Code = m.Code + "-----[" + m.Name + ']', ID = m.ID });
            ViewBag.Educate_Title = dbcontext.Educate_Title.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.Name_of_educational_qualification = dbcontext.Name_of_educational_qualification.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.Employee_Profile2 = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.idemp = id;
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel);
            var model = dbcontext.Employee_family_profile.ToList();
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
            //if (id != null)
            //{
            //    var ID = int.Parse(id);
            //    var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == ID);
            //    var x = emp.Employee_family_profile;
            //    return View(x);
            //}
            DateTime statis3 = Convert.ToDateTime("1/1/1900");
            var ID = int.Parse(id);
            var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == ID);
            var EmployeeFamily = new Employee_family_profile { Employee_ProfileId = emp.ID.ToString(), Code = stru.Structure_Code + count.ToString(), Birth_date = statis3, Death_date = statis3, End_relation_date = statis3, Start_relation_date = statis3};
            return View(EmployeeFamily);

        }
        [HttpPost]
        public ActionResult Create(Employee_family_profile model, string command)
        {
            try
            {
                if (model.Name_of_educational_qualificationId == null) { model.Name_of_educational_qualificationId = "0"; }
                if (model.Employee_Profile_WorkId == null) { model.Employee_Profile_WorkId = "0"; }
                if (model.Educate_TitleId == null) { model.Educate_TitleId = "0"; }
                if (model.NationalityId == null) { model.NationalityId = "0"; }


                ViewBag.Nationality = dbcontext.Nationality.ToList().Select(m => new { Code = m.Code + "-----[" + m.Name + ']', ID = m.ID });
                ViewBag.Educate_Title = dbcontext.Educate_Title.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Name_of_educational_qualification = dbcontext.Name_of_educational_qualification.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Employee_Profile2 = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.idemp = model.ID;

                if (ModelState.IsValid)
                {
                    var family = int.Parse(model.Employee_ProfileId);
                    var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == family);
                    //      var record = dbcontext.Employee_family_profile.FirstOrDefault(m => m.ID == emp.Employee_family_profile.ID);
                    Employee_family_profile record = new Employee_family_profile();
                    record.Name = model.Name;
                    record.Code = model.Code;
                    record.Is_Reliable = model.Is_Reliable;
                    record.Family_member_type = model.Family_member_type;
                    record.Degree_of_kinship = model.Degree_of_kinship;
                    record.Gender = model.Gender;
                    record.Status = model.Status;
                    record.Start_relation_date = model.Start_relation_date;
                    record.End_relation_date = model.End_relation_date;
                    record.Employee_ProfileId = model.Employee_ProfileId;
                    var Employee_ProfileId = int.Parse(model.Employee_ProfileId);
                    record.Employee_Profile = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == Employee_ProfileId);
                    record.NationalityId = model.NationalityId;

                    record.Birth_date = model.Birth_date;
                    record.Death_date = model.Death_date;
                    if (model.Birth_date > model.Death_date)
                    {
                        TempData["Message"] = "Birth date  bigger Death date ";
                        return View(model);
                    }
                    if (model.Start_relation_date > model.End_relation_date)
                    {
                        TempData["Message"] = "Start relation date bigger End relation date ";
                        return View(model);
                    }
                    if (model.Start_relation_date > model.Birth_date)
                    {
                        TempData["Message"] = " Start relation date bigger Birth date";
                        return View(model);
                    }
                    record.Marital_Status = model.Marital_Status;
                    record.Id_type = model.Id_type;
                    record.Id_number = model.Id_number;
                    record.Father_name = model.Father_name;
                    record.Mother_name = model.Mother_name;
                    record.Educate_TitleId = model.Educate_TitleId;
                    record.Name_of_educational_qualificationId = model.Name_of_educational_qualificationId;
                    record.Subscribed = model.Subscribed;
                    record.Health_Status2 = model.Health_Status2;
                    record.Working_status = model.Working_status;
                    record.Working_in_oil_sector = model.Working_in_oil_sector;
                    record.Position_title = model.Position_title;
                    record.Company_name = model.Company_name;
                    record.Company_address = model.Company_address;
                    record.Working_in_company = model.Working_in_company;
                    record.Employee_Profile_WorkId = model.Employee_Profile_WorkId;
                    record.Is_emergency_contact_person = model.Is_emergency_contact_person;
                    record.Emergency_level = model.Emergency_level;
                    record.Home_phone_number = model.Home_phone_number;
                    record.Mobil_phone_number = model.Mobil_phone_number;
                    record.Address = model.Address;
                    dbcontext.Employee_family_profile.Add(record);
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
                ViewBag.Nationality = dbcontext.Nationality.ToList().Select(m => new { Code = m.Code + "-----[" + m.Name + ']', ID = m.ID });
                ViewBag.Educate_Title = dbcontext.Educate_Title.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Name_of_educational_qualification = dbcontext.Name_of_educational_qualification.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Employee_Profile2 = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });

                var record = dbcontext.Employee_family_profile.FirstOrDefault(m => m.ID == id);
                ViewBag.idemp = record.Employee_Profile.ID.ToString();
                if (record != null)
                {
                    return View(record);
                }
                else
                {
                    TempData["Message"] = "There is no Employee Family Profile";
                    return View();
                }
            }
            catch
            (Exception e)
            { return View(); }
        }
        [HttpPost]
        public ActionResult Edit(Employee_family_profile model, string command)
        {
            try
            {
                if (model.Name_of_educational_qualificationId == null) { model.Name_of_educational_qualificationId = "0"; }
                if (model.Employee_Profile_WorkId == null) { model.Employee_Profile_WorkId = "0"; }
                if (model.Educate_TitleId == null) { model.Educate_TitleId = "0"; }
                if (model.NationalityId == null) { model.NationalityId = "0"; }


                ViewBag.Nationality = dbcontext.Nationality.ToList().Select(m => new { Code = m.Code + "-----[" + m.Name + ']', ID = m.ID });
                ViewBag.Educate_Title = dbcontext.Educate_Title.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Name_of_educational_qualification = dbcontext.Name_of_educational_qualification.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Employee_Profile2 = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.idemp = model.ID;

                var record = dbcontext.Employee_family_profile.FirstOrDefault(m => m.ID == model.ID);
                record.Code = model.Code;
                record.Name = model.Name;
                record.Is_Reliable = model.Is_Reliable;
                record.Family_member_type = model.Family_member_type;
                record.Degree_of_kinship = model.Degree_of_kinship;
                record.Gender = model.Gender;
                record.Status = model.Status;
                record.Start_relation_date = model.Start_relation_date;
                record.End_relation_date = model.End_relation_date;
                record.Employee_ProfileId = model.Employee_ProfileId;
                record.NationalityId = model.NationalityId;
                record.Birth_date = model.Birth_date;
                record.Death_date = model.Death_date;
                record.Marital_Status = model.Marital_Status;
                record.Id_type = model.Id_type;
                record.Id_number = model.Id_number;
                record.Father_name = model.Father_name;
                record.Mother_name = model.Mother_name;
                record.Educate_TitleId = model.Educate_TitleId;
                record.Name_of_educational_qualificationId = model.Name_of_educational_qualificationId;
                record.Subscribed = model.Subscribed;
                record.Health_Status2 = model.Health_Status2;
                record.Working_status = model.Working_status;
                record.Working_in_oil_sector = model.Working_in_oil_sector;
                record.Position_title = model.Position_title;
                record.Company_name = model.Company_name;
                record.Company_address = model.Company_address;
                record.Working_in_company = model.Working_in_company;
                record.Employee_Profile_WorkId = model.Employee_Profile_WorkId;
                record.Is_emergency_contact_person = model.Is_emergency_contact_person;
                record.Emergency_level = model.Emergency_level;
                record.Home_phone_number = model.Home_phone_number;
                record.Mobil_phone_number = model.Mobil_phone_number;
                record.Address = model.Address;

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

                var record = dbcontext.Employee_family_profile.FirstOrDefault(m => m.ID == id);
                ViewBag.idemp = record.Employee_Profile.ID.ToString();
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = "There is no Employee Family Profile";
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

            var record = dbcontext.Employee_family_profile.FirstOrDefault(m => m.ID == id);
            ViewBag.idemp = record.Employee_Profile.ID.ToString();
            try
            {
                dbcontext.Employee_family_profile.Remove(record);
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