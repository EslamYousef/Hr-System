﻿using System;
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
    public class Employee_ProfileController : Controller
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Employee_Profile
        public ActionResult Index()
        {
            var model = dbcontext.Employee_Profile.ToList();
            return View(model);
        }
        public ActionResult Create(string id)
        {

            ViewBag.Country = dbcontext.Country.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.Area = dbcontext.Area.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.the_states = dbcontext.the_states.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.Territories = dbcontext.Territories.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.cities = dbcontext.cities.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.Currency = dbcontext.Currency.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.Religion = dbcontext.Religion.ToList().Select(m => new { Code = m.Code + "-----[" + m.Name + ']', ID = m.ID });
            ViewBag.Nationality = dbcontext.Nationality.ToList().Select(m => new { Code = m.Code + "-----[" + m.Name + ']', ID = m.ID });
            if (id != null)
            {
                var ID = int.Parse(id);

                var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == ID);
                var abil = emp.Ability;
                var personnel = emp.Personnel_Information;
                var service = emp.Service_Information;
                var z = new Employee_Profile_VM { Employee_Profile = emp, Ability = abil, Personnel_Information = personnel, Service_Information = service };
                return View(z);
            }
            else
            {
                DateTime statis = Convert.ToDateTime("1/1/1900");
                var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel);
                var model = dbcontext.Employee_Profile.ToList();
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
                var Employee = new Employee_Profile { Code = stru.Structure_Code + count, Birth_date = statis, Issue_date = statis, Expire_date = statis };
                var Ability = new Ability { registration_date = DateTime.Now.Date };
                var Personnel_Information = new Personnel_Information { Hire_Date = statis, Join_Date = statis, Boarding_Date = statis, Sector_Join_Date = statis, Social_Insurance_Date = statis };
                var Service_Information = new Service_Information { EOS_date = statis, Last_working_date = statis, Retired_expected_EOS = statis };

                var x = new Employee_Profile_VM { Employee_Profile = Employee, Ability = Ability, Personnel_Information = Personnel_Information, Service_Information = Service_Information };

                return View(x);
            }



        }
        [HttpPost]
        public ActionResult Create(Employee_Profile_VM model, string command, string command2, string command3, string command4, string command5, string command6)
        {
            try
            {
                if (model.Employee_Profile.Countryid == null) { model.Employee_Profile.Countryid = "0"; }
                if (model.Employee_Profile.Areaid == null) { model.Employee_Profile.Areaid = "0"; }
                if (model.Employee_Profile.the_statesid == null) { model.Employee_Profile.the_statesid = "0"; }
                if (model.Employee_Profile.Territoriesid == null) { model.Employee_Profile.Territoriesid = "0"; }
                if (model.Employee_Profile.citiesid == null) { model.Employee_Profile.citiesid = "0"; }

                if (model.Employee_Profile.Countryaddressid == null) { model.Employee_Profile.Countryaddressid = "0"; }
                if (model.Employee_Profile.Areaaddressid == null) { model.Employee_Profile.Areaaddressid = "0"; }
                if (model.Employee_Profile.Territoriesaddressid == null) { model.Employee_Profile.Territoriesaddressid = "0"; }
                if (model.Employee_Profile.citiesaddressid == null) { model.Employee_Profile.citiesaddressid = "0"; }
                if (model.Employee_Profile.the_statesaddressid == null) { model.Employee_Profile.the_statesaddressid = "0"; }
                if (model.Service_Information.CurrencyId == null) { model.Service_Information.CurrencyId = "0"; }

                ViewBag.Country = dbcontext.Country.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Area = dbcontext.Area.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.the_states = dbcontext.the_states.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Territories = dbcontext.Territories.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.cities = dbcontext.cities.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Currency = dbcontext.Currency.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Religion = dbcontext.Religion.ToList().Select(m => new { Code = m.Code + "-----[" + m.Name + ']', ID = m.ID });
                ViewBag.Nationality = dbcontext.Nationality.ToList().Select(m => new { Code = m.Code + "-----[" + m.Name + ']', ID = m.ID });

                if (ModelState.IsValid)
                {

                    //if (model.Service_Information.CurrencyId == "0" || model.Service_Information.CurrencyId == null)
                    //{
                    //    ModelState.AddModelError("", "Currency Code must enter");
                    //    return View(model);
                    //}

                    if (model.Employee_Profile.ReligionId == "0" || model.Employee_Profile.ReligionId == null)
                    {
                        ModelState.AddModelError("", "Religion Code must enter");
                        return View(model);
                    }
                    if (model.Employee_Profile.NationalityId == "0" || model.Employee_Profile.NationalityId == null)
                    {
                        ModelState.AddModelError("", "Nationality Code must enter");
                        return View(model);
                    }
                    Employee_Profile record = new Employee_Profile();
                    record.Code = model.Employee_Profile.Code;
                    record.Name = model.Employee_Profile.Name;
                    record.Full_Name = model.Employee_Profile.Full_Name;
                    record.Surname = model.Employee_Profile.Surname;
                    record.Arabic = model.Employee_Profile.Arabic;
                    record.Full = model.Employee_Profile.Full;
                    record.Sur_Name = model.Employee_Profile.Sur_Name;
                    record.Gender = model.Employee_Profile.Gender;
                    record.Marital_Status = model.Employee_Profile.Marital_Status;
                    record.ReligionId = model.Employee_Profile.ReligionId;
                    var ReligionId = int.Parse(model.Employee_Profile.ReligionId);
                    record.Religion = dbcontext.Religion.FirstOrDefault(m => m.ID == ReligionId);
                    record.NationalityId = model.Employee_Profile.NationalityId;
                    var NationalityId = int.Parse(model.Employee_Profile.NationalityId);
                    record.Nationality = dbcontext.Nationality.FirstOrDefault(m => m.ID == NationalityId);
                    record.Citizen = model.Employee_Profile.Citizen;
                    record.Blood_group = model.Employee_Profile.Blood_group;
                    record.ID_type = model.Employee_Profile.ID_type;
                    record.Health_Status = model.Employee_Profile.Health_Status;

                    record.Birth_date = model.Employee_Profile.Birth_date;

                    record.Issue_date = model.Employee_Profile.Issue_date;
                    record.Expire_date = model.Employee_Profile.Expire_date;
                    if (model.Employee_Profile.Issue_date > model.Employee_Profile.Expire_date)
                    {
                        TempData["Message"] = "From date Issue bigger date Expire";
                        return View(model);
                    }
                    record.ID_number = model.Employee_Profile.ID_number;
                    record.ID_number_in_attendance_machine = model.Employee_Profile.ID_number_in_attendance_machine;
                    record.Countryid = model.Employee_Profile.Countryid;
                    var Countryid = int.Parse(model.Employee_Profile.Countryid);
                    record.Country = dbcontext.Country.FirstOrDefault(m => m.ID == Countryid);
                    record.Areaid = model.Employee_Profile.Areaid;
                    var Areaid = int.Parse(model.Employee_Profile.Areaid);
                    record.Area = dbcontext.Area.FirstOrDefault(m => m.ID == Areaid);
                    record.the_statesid = model.Employee_Profile.the_statesid;
                    var the_statesid = int.Parse(model.Employee_Profile.the_statesid);
                    record.the_states = dbcontext.the_states.FirstOrDefault(m => m.ID == the_statesid);
                    record.Territoriesid = model.Employee_Profile.Territoriesid;
                    var Territoriesid = int.Parse(model.Employee_Profile.Territoriesid);
                    record.Territories = dbcontext.Territories.FirstOrDefault(m => m.ID == Territoriesid);
                    record.citiesid = model.Employee_Profile.citiesid;
                    var citiesid = int.Parse(model.Employee_Profile.citiesid);
                    record.cities = dbcontext.cities.FirstOrDefault(m => m.ID == citiesid);

                    record.Countryaddressid = model.Employee_Profile.Countryaddressid;
                    var Countryaddressid = int.Parse(model.Employee_Profile.Countryaddressid);
                    record.Country = dbcontext.Country.FirstOrDefault(m => m.ID == Countryaddressid);
                    record.Areaaddressid = model.Employee_Profile.Areaaddressid;
                    var Areaaddressid = int.Parse(model.Employee_Profile.Areaaddressid);
                    record.Area = dbcontext.Area.FirstOrDefault(m => m.ID == Areaaddressid);
                    record.the_statesaddressid = model.Employee_Profile.the_statesaddressid;
                    var the_statesaddressid = int.Parse(model.Employee_Profile.the_statesaddressid);
                    record.the_states = dbcontext.the_states.FirstOrDefault(m => m.ID == the_statesaddressid);
                    record.Territoriesaddressid = model.Employee_Profile.Territoriesaddressid;
                    var Territoriesaddressid = int.Parse(model.Employee_Profile.Territoriesaddressid);
                    record.Territories = dbcontext.Territories.FirstOrDefault(m => m.ID == Territoriesaddressid);
                    record.citiesaddressid = model.Employee_Profile.citiesaddressid;
                    var citiesaddressid = int.Parse(model.Employee_Profile.citiesaddressid);
                    record.cities = dbcontext.cities.FirstOrDefault(m => m.ID == citiesaddressid);


                    Ability AbilityRecode = new Ability();
                    AbilityRecode.Inability_reason = model.Ability.Inability_reason;
                    AbilityRecode.Inability_description = model.Ability.Inability_description;
                    AbilityRecode.registration_number = model.Ability.registration_number;
                    AbilityRecode.registration_date = model.Ability.registration_date;
                    //record.tab = model.tab;

                    Personnel_Information Personnel = new Personnel_Information();
                    Personnel.Main_Status = model.Personnel_Information.Main_Status;
                    Personnel.Sub_Status = model.Personnel_Information.Sub_Status;
                    Personnel.Hire_Date = model.Personnel_Information.Hire_Date;
                    Personnel.Sector_Join_Date = model.Personnel_Information.Sector_Join_Date;
                    Personnel.Join_Date = model.Personnel_Information.Join_Date;
                    Personnel.Pratice_profession_Out_Of_Company = model.Personnel_Information.Pratice_profession_Out_Of_Company;
                    Personnel.Social_Insurance = model.Personnel_Information.Social_Insurance;
                    Personnel.Social_Insurance_Date = model.Personnel_Information.Social_Insurance_Date;
                    Personnel.Service_period_ins_Y = model.Personnel_Information.Service_period_ins_Y;
                    Personnel.Service_period_ins_M = model.Personnel_Information.Service_period_ins_M;
                    Personnel.Boarding_membership = model.Personnel_Information.Boarding_membership;
                    Personnel.Boarding_Date = model.Personnel_Information.Boarding_Date;

                    Service_Information Service = new Service_Information();
                    Service.Pension = model.Service_Information.Pension;
                    Service.Pension_source = model.Service_Information.Pension_source;
                    Service.EOS_date = model.Service_Information.EOS_date;
                    Service.Have_pension = model.Service_Information.Have_pension;
                    Service.Retired_expected_EOS = model.Service_Information.Retired_expected_EOS;
                    Service.Last_working_date = model.Service_Information.Last_working_date;
                    Service.Is_merits_The_date_of_death = model.Service_Information.Is_merits_The_date_of_death;
                    Service.Pension = model.Service_Information.Pension;
                    Service.CurrencyId = model.Service_Information.CurrencyId;
                    var CurrencyId = int.Parse(model.Service_Information.CurrencyId);
                    Service.Currency = dbcontext.Currency.FirstOrDefault(m => m.ID == CurrencyId);

                    //dbcontext.Employee_Profile.Add(record);
                    var ab = dbcontext.Ability.Add(AbilityRecode);
                    var per = dbcontext.Personnel_Information.Add(Personnel);
                    var ser = dbcontext.Service_Information.Add(Service);
                    dbcontext.SaveChanges();
                    record.Ability = ab;
                    record.Personnel_Information = per;
                    record.Service_Information = ser;
                    var emp = dbcontext.Employee_Profile.Add(record);
                    dbcontext.SaveChanges();

                    ///////////////////////////////////////////////////////////////////////////
                    var addmodel = dbcontext.Employee_Address_Profile.ToList();
                    var couunt = 0;
                    if (addmodel.Count() == 0)
                    {
                        couunt = 1;
                    }
                    else
                    {
                        var te = addmodel.LastOrDefault().ID;
                        couunt = te + 1;
                    }
                    var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel);

                    var adddd = new Employee_Address_Profile
                    { Employee_ProfileId = emp.ID.ToString(), Code = stru.Structure_Code + couunt.ToString() };
                    var address_emp = dbcontext.Employee_Address_Profile.Add(adddd);
                    dbcontext.SaveChanges();
                    emp.Employee_Address_Profile = address_emp;
                    dbcontext.SaveChanges();
                    ///////////////////////////////////////////////
                    var addmodel1 = dbcontext.Employee_Qualification_Profile.ToList();
                    var tr = 0;

                    if (addmodel1.Count() == 0)
                    {
                        tr = 1;
                    }
                    else
                    {
                        var te = addmodel1.LastOrDefault().ID;
                        tr = te + 1;
                    }
                    DateTime statis = Convert.ToDateTime("1/1/1900");
                    var strus = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel);
                    var text = new Employee_Qualification_Profile
                    { Employee_ProfileId = emp.ID.ToString(), Code = strus.Structure_Code + tr.ToString(), Qualification_start_date = statis, Qualification_end_date = statis };
                    var e = dbcontext.Employee_Qualification_Profile.Add(text);
                    dbcontext.SaveChanges();

                    emp.Employee_Qualification_Profile = e;
                    dbcontext.SaveChanges();
                    //////////////////////////////////////////////////////////
                    var addmodel2 = dbcontext.Position_Information.ToList();
                    var tr2 = 0;

                    if (addmodel2.Count() == 0)
                    {
                        tr2 = 1;
                    }
                    else
                    {
                        var te2 = addmodel2.LastOrDefault().ID;
                        tr2 = te2 + 1;
                    }
                    DateTime statis2 = Convert.ToDateTime("1/1/1900");
                    var strus2 = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel);
                    var text2 = new Position_Information
                    { Employee_ProfileId = emp.ID.ToString(), Code = strus2.Structure_Code + tr2.ToString(), From_date = statis2, To_date = statis2, End_of_service_date = statis2, Last_working_date = statis2, SlotdescId = "0" };
                    var e2 = dbcontext.Position_Information.Add(text2);
                    dbcontext.SaveChanges();

                    var text22 = new Position_Transaction_Information
                    { Position_transaction = statis2, Approved_date = statis2, Memo_date = statis2, Resolution_date = statis2 };
                    var e22 = dbcontext.Position_Transaction_Information.Add(text22);
                    dbcontext.SaveChanges();

                    emp.Employee_Positions_Profile = e2;
                    dbcontext.SaveChanges();
                    emp.Position_Transaction_Information = e22;
                    dbcontext.SaveChanges();
            
                    ///////////////////////////////////////////////

                    var employeefamily = dbcontext.Employee_family_profile.ToList();
                    var famcount = 0;

                    if (employeefamily.Count() == 0)
                    {
                        famcount = 1;
                    }
                    else
                    {
                        var te3 = employeefamily.LastOrDefault().ID;
                        famcount = te3 + 1;
                    }
                    DateTime statis3 = Convert.ToDateTime("1/1/1900");
                    var strus3 = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel);
                    var text3 = new Employee_family_profile
                    { Employee_ProfileId = emp.ID.ToString(), Code = strus3.Structure_Code + famcount.ToString(), Birth_date = statis3, Death_date = statis3, End_relation_date = statis3, Start_relation_date = statis3 };
                    var e3 = dbcontext.Employee_family_profile.Add(text3);
                    dbcontext.SaveChanges();
                    emp.Employee_family_profile = e3;
                    dbcontext.SaveChanges();
                    ///////////////////////////////////////////////////////////////
                    var employeeexperience = dbcontext.Employee_experience_profile.ToList();
                    var expcount = 0;

                    if (employeeexperience.Count() == 0)
                    {
                        expcount = 1;
                    }
                    else
                    {
                        var te5 = employeeexperience.LastOrDefault().ID;
                        expcount = te5 + 1;
                    }
                    DateTime statis5 = Convert.ToDateTime("1/1/1900");
                    var strus5 = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel);
                    var text5 = new Employee_experience_profile
                    { Employee_ProfileId = emp.ID.ToString(), Code = strus5.Structure_Code + expcount.ToString(), Approval_date = statis5, Start_date = statis5, End_date = statis5 ,Rejection_ReasonsId="0",External_compainesId="0"};
                    var e5 = dbcontext.Employee_experience_profile.Add(text5);
                    dbcontext.SaveChanges();
                    emp.Employee_experience_profile = e5;
                    dbcontext.SaveChanges();
                    ////////////////////////////////////////////////
                    var employeecontact= dbcontext.Employee_contact_profile.ToList();
                    var contactcount = 0;

                    if (employeecontact.Count() == 0)
                    {
                        contactcount = 1;
                    }
                    else
                    {
                        var te6 = employeecontact.LastOrDefault().ID;
                        contactcount = te6 + 1;
                    }
                    var strus6 = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel);
                    var text6 = new Employee_contact_profile
                    { Employee_ProfileId = emp.ID.ToString(), Code = strus6.Structure_Code + contactcount.ToString() };
                    var e6 = dbcontext.Employee_contact_profile.Add(text6);
                    dbcontext.SaveChanges();
                    emp.Employee_contact_profile = e6;
                    dbcontext.SaveChanges();

                    if (command == "Submit")
                    {
                        return RedirectToAction("Create", "Employee_Address_Profile", new { id = emp.ID });
                    }
                    if (command2 == "Submit")
                    {
                        return RedirectToAction("Create", "Employee_Qualification_Profile", new { id = emp.ID });
                    }
                    if (command3 == "Submit")
                    {
                        return RedirectToAction("Create", "Employee_Positions_Profile", new { id = emp.ID });
                    }
                    if (command4 == "Submit")
                    {
                        return RedirectToAction("Create", "Employee_family_profile", new { id = emp.ID });
                    }
                    if (command5 == "Submit")
                    {
                        return RedirectToAction("Create", "Employee_experience_profile", new { id = emp.ID });
                    }
                    if (command6 == "Submit")
                    {
                        return RedirectToAction("Create", "Employee_contact_profile", new { id = emp.ID });
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
                ViewBag.Currency = dbcontext.Currency.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Religion = dbcontext.Religion.ToList().Select(m => new { Code = m.Code + "-----[" + m.Name + ']', ID = m.ID });
                ViewBag.Nationality = dbcontext.Nationality.ToList().Select(m => new { Code = m.Code + "-----[" + m.Name + ']', ID = m.ID });

                var record = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == id);
                var vm = new Employee_Profile_VM { Employee_Profile = record, Ability = record.Ability, Service_Information = record.Service_Information, Personnel_Information = record.Personnel_Information };
                if (vm != null)
                { return View(vm); }
                else
                {
                    TempData["Message"] = "there is no Employee Profile";
                    return View();
                }
            }
            catch
            (Exception e)
            { return View(); }


        }
        [HttpPost]
        public ActionResult Edit(Employee_Profile_VM model, string command, string command2, string command3, string command4, string command5, string command6)
        {
            try
            {
                if (model.Employee_Profile.Countryid == null) { model.Employee_Profile.Countryid = "0"; }
                if (model.Employee_Profile.Areaid == null) { model.Employee_Profile.Areaid = "0"; }
                if (model.Employee_Profile.the_statesid == null) { model.Employee_Profile.the_statesid = "0"; }
                if (model.Employee_Profile.Territoriesid == null) { model.Employee_Profile.Territoriesid = "0"; }
                if (model.Employee_Profile.citiesid == null) { model.Employee_Profile.citiesid = "0"; }
                if (model.Employee_Profile.Countryaddressid == null) { model.Employee_Profile.Countryaddressid = "0"; }
                if (model.Employee_Profile.Areaaddressid == null) { model.Employee_Profile.Areaaddressid = "0"; }
                if (model.Employee_Profile.Territoriesaddressid == null) { model.Employee_Profile.Territoriesaddressid = "0"; }
                if (model.Employee_Profile.citiesaddressid == null) { model.Employee_Profile.citiesaddressid = "0"; }
                if (model.Employee_Profile.the_statesaddressid == null) { model.Employee_Profile.the_statesaddressid = "0"; }
                if (model.Service_Information.CurrencyId == null) { model.Service_Information.CurrencyId = "0"; }

                ViewBag.Country = dbcontext.Country.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Area = dbcontext.Area.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.the_states = dbcontext.the_states.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Territories = dbcontext.Territories.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.cities = dbcontext.cities.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Currency = dbcontext.Currency.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Religion = dbcontext.Religion.ToList().Select(m => new { Code = m.Code + "-----[" + m.Name + ']', ID = m.ID });
                ViewBag.Nationality = dbcontext.Nationality.ToList().Select(m => new { Code = m.Code + "-----[" + m.Name + ']', ID = m.ID });


                var record = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == model.Employee_Profile.ID);

                record.Code = model.Employee_Profile.Code;
                record.Name = model.Employee_Profile.Name;
                record.Full_Name = model.Employee_Profile.Full_Name;
                record.Surname = model.Employee_Profile.Surname;
                record.Arabic = model.Employee_Profile.Arabic;
                record.Full = model.Employee_Profile.Full;
                record.Sur_Name = model.Employee_Profile.Sur_Name;
                record.Gender = model.Employee_Profile.Gender;
                record.Marital_Status = model.Employee_Profile.Marital_Status;
                record.ReligionId = model.Employee_Profile.ReligionId;
                var ReligionId = int.Parse(model.Employee_Profile.ReligionId);
                record.Religion = dbcontext.Religion.FirstOrDefault(m => m.ID == ReligionId);
                record.NationalityId = model.Employee_Profile.NationalityId;
                var NationalityId = int.Parse(model.Employee_Profile.NationalityId);
                record.Nationality = dbcontext.Nationality.FirstOrDefault(m => m.ID == NationalityId);
                record.Citizen = model.Employee_Profile.Citizen;
                record.Blood_group = model.Employee_Profile.Blood_group;
                record.ID_type = model.Employee_Profile.ID_type;
                record.Health_Status = model.Employee_Profile.Health_Status;

                record.Birth_date = model.Employee_Profile.Birth_date;

                record.Issue_date = model.Employee_Profile.Issue_date;
                record.Expire_date = model.Employee_Profile.Expire_date;
                if (model.Employee_Profile.Issue_date > model.Employee_Profile.Expire_date)
                {
                    TempData["Message"] = "From date Issue bigger date Expire";
                    return View(model);
                }
                record.ID_number = model.Employee_Profile.ID_number;
                record.ID_number_in_attendance_machine = model.Employee_Profile.ID_number_in_attendance_machine;
                record.Countryid = model.Employee_Profile.Countryid;
                var Countryid = int.Parse(model.Employee_Profile.Countryid);
                record.Country = dbcontext.Country.FirstOrDefault(m => m.ID == Countryid);
                record.Areaid = model.Employee_Profile.Areaid;
                var Areaid = int.Parse(model.Employee_Profile.Areaid);
                record.Area = dbcontext.Area.FirstOrDefault(m => m.ID == Areaid);
                record.the_statesid = model.Employee_Profile.the_statesid;
                var the_statesid = int.Parse(model.Employee_Profile.the_statesid);
                record.the_states = dbcontext.the_states.FirstOrDefault(m => m.ID == the_statesid);
                record.Territoriesid = model.Employee_Profile.Territoriesid;
                var Territoriesid = int.Parse(model.Employee_Profile.Territoriesid);
                record.Territories = dbcontext.Territories.FirstOrDefault(m => m.ID == Territoriesid);
                record.citiesid = model.Employee_Profile.citiesid;
                var citiesid = int.Parse(model.Employee_Profile.citiesid);
                record.cities = dbcontext.cities.FirstOrDefault(m => m.ID == citiesid);

                record.Countryaddressid = model.Employee_Profile.Countryaddressid;
                var Countryaddressid = int.Parse(model.Employee_Profile.Countryaddressid);
                record.Country = dbcontext.Country.FirstOrDefault(m => m.ID == Countryaddressid);
                record.Areaaddressid = model.Employee_Profile.Areaaddressid;
                var Areaaddressid = int.Parse(model.Employee_Profile.Areaaddressid);
                record.Area = dbcontext.Area.FirstOrDefault(m => m.ID == Areaaddressid);
                record.the_statesaddressid = model.Employee_Profile.the_statesaddressid;
                var the_statesaddressid = int.Parse(model.Employee_Profile.the_statesaddressid);
                record.the_states = dbcontext.the_states.FirstOrDefault(m => m.ID == the_statesaddressid);
                record.Territoriesaddressid = model.Employee_Profile.Territoriesaddressid;
                var Territoriesaddressid = int.Parse(model.Employee_Profile.Territoriesaddressid);
                record.Territories = dbcontext.Territories.FirstOrDefault(m => m.ID == Territoriesaddressid);
                record.citiesaddressid = model.Employee_Profile.citiesaddressid;
                var citiesaddressid = int.Parse(model.Employee_Profile.citiesaddressid);
                record.cities = dbcontext.cities.FirstOrDefault(m => m.ID == citiesaddressid);
                dbcontext.SaveChanges();

                var AbilityRecode = dbcontext.Ability.FirstOrDefault(m => m.ID == model.Employee_Profile.Ability.ID);
                AbilityRecode.Inability_reason = model.Ability.Inability_reason;
                AbilityRecode.Inability_description = model.Ability.Inability_description;
                AbilityRecode.registration_number = model.Ability.registration_number;
                AbilityRecode.registration_date = model.Ability.registration_date;
                dbcontext.SaveChanges();

                var Personnel = dbcontext.Personnel_Information.FirstOrDefault(m => m.ID == model.Employee_Profile.Personnel_Information.ID);
                Personnel.Main_Status = model.Personnel_Information.Main_Status;
                Personnel.Sub_Status = model.Personnel_Information.Sub_Status;
                Personnel.Hire_Date = model.Personnel_Information.Hire_Date;
                Personnel.Sector_Join_Date = model.Personnel_Information.Sector_Join_Date;
                Personnel.Join_Date = model.Personnel_Information.Join_Date;
                Personnel.Pratice_profession_Out_Of_Company = model.Personnel_Information.Pratice_profession_Out_Of_Company;
                Personnel.Social_Insurance = model.Personnel_Information.Social_Insurance;
                Personnel.Social_Insurance_Date = model.Personnel_Information.Social_Insurance_Date;
                Personnel.Service_period_ins_Y = model.Personnel_Information.Service_period_ins_Y;
                Personnel.Service_period_ins_M = model.Personnel_Information.Service_period_ins_M;
                Personnel.Boarding_membership = model.Personnel_Information.Boarding_membership;
                Personnel.Boarding_Date = model.Personnel_Information.Boarding_Date;
                dbcontext.SaveChanges();

                var Service = dbcontext.Service_Information.FirstOrDefault(m => m.ID == model.Employee_Profile.Service_Information.ID);
                Service.Pension = model.Service_Information.Pension;
                Service.Pension_source = model.Service_Information.Pension_source;
                Service.EOS_date = model.Service_Information.EOS_date;
                Service.Have_pension = model.Service_Information.Have_pension;
                Service.Retired_expected_EOS = model.Service_Information.Retired_expected_EOS;
                Service.Last_working_date = model.Service_Information.Last_working_date;
                Service.Is_merits_The_date_of_death = model.Service_Information.Is_merits_The_date_of_death;
                Service.Pension = model.Service_Information.Pension;
                Service.CurrencyId = model.Service_Information.CurrencyId;
                var CurrencyId = int.Parse(model.Service_Information.CurrencyId);
                Service.Currency = dbcontext.Currency.FirstOrDefault(m => m.ID == CurrencyId);
                dbcontext.SaveChanges();

                if (command == "Submit")
                {
                    return RedirectToAction("Edit", "Employee_Address_Profile", new { id = record.Employee_Address_Profile.ID });
                }
                if (command2 == "Submit")
                {
                    return RedirectToAction("Edit", "Employee_Qualification_Profile", new { id = record.Employee_Qualification_Profile.ID });
                }
                if (command3 == "Submit")
                {
                    return RedirectToAction("Edit", "Employee_Positions_Profile", new { id = record.Employee_Positions_Profile.ID });
                }
                if (command4 == "Submit")
                {
                    return RedirectToAction("Edit", "Employee_family_profile", new { id = record.Employee_family_profile.ID });
                }
                if (command5 == "Submit")
                {
                    return RedirectToAction("Edit", "Employee_experience_profile", new { id = record.Employee_experience_profile.ID });
                }
                if (command6 == "Submit")
                {
                    return RedirectToAction("Edit", "Employee_contact_profile", new { id = record.Employee_contact_profile.ID });
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
                var record = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == id);
                var Employee_Address_Profile = dbcontext.Employee_Address_Profile.FirstOrDefault(m => m.ID == record.Employee_Address_Profile.ID);
                var Employee_Qualification_Profile = dbcontext.Employee_Qualification_Profile.FirstOrDefault(m => m.ID == record.Employee_Qualification_Profile.ID);
                var Ability = dbcontext.Ability.FirstOrDefault(m => m.ID == record.Ability.ID);
                var Service_Information = dbcontext.Service_Information.FirstOrDefault(m => m.ID == record.Service_Information.ID);
                var Personnel_Information = dbcontext.Personnel_Information.FirstOrDefault(m => m.ID == record.Personnel_Information.ID);
                var Position_Information = dbcontext.Position_Information.FirstOrDefault(m => m.ID == record.Employee_Positions_Profile.ID);
                var Position_Transaction_Information = dbcontext.Position_Transaction_Information.FirstOrDefault(m => m.ID == record.Position_Transaction_Information.ID);
                var Employee_family_profile = dbcontext.Employee_family_profile.FirstOrDefault(m => m.ID == record.Employee_family_profile.ID);

                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = "There is no Employee Profile";
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
            var record = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == id);
            var Employee_Address_Profile = dbcontext.Employee_Address_Profile.FirstOrDefault(m => m.ID == record.Employee_Address_Profile.ID);
            var Employee_Qualification_Profile = dbcontext.Employee_Qualification_Profile.FirstOrDefault(m => m.ID == record.Employee_Qualification_Profile.ID);
            var Ability = dbcontext.Ability.FirstOrDefault(m => m.ID == record.Ability.ID);
            var Service_Information = dbcontext.Service_Information.FirstOrDefault(m => m.ID == record.Service_Information.ID);
            var Personnel_Information = dbcontext.Personnel_Information.FirstOrDefault(m => m.ID == record.Personnel_Information.ID);
            var Position_Information = dbcontext.Position_Information.FirstOrDefault(m => m.ID == record.Employee_Positions_Profile.ID);
            var Position_Transaction_Information = dbcontext.Position_Transaction_Information.FirstOrDefault(m => m.ID == record.Position_Transaction_Information.ID);
            var solt = dbcontext.Slots.FirstOrDefault(m => m.Employee_Profile.ID == record.ID);
            var Employee_family_profile = dbcontext.Employee_family_profile.FirstOrDefault(m => m.ID == record.Employee_family_profile.ID);
            var Employee_experience_profile = dbcontext.Employee_experience_profile.FirstOrDefault(m => m.ID == record.Employee_experience_profile.ID);
            try
            {

                var old_slot = dbcontext.Slots.FirstOrDefault(m => m.Employee_Profile.ID == record.ID);
                if (old_slot != null)
                {
                    old_slot.Employee_Profile = null;
                    dbcontext.SaveChanges();
                }


                var emp = dbcontext.Employee_Profile.Remove(record);
                dbcontext.SaveChanges();
                if (Ability != null)
                {
                    dbcontext.Ability.Remove(Ability);
                }
                if (Service_Information != null)
                {
                    dbcontext.Service_Information.Remove(Service_Information);
                }
                if (Personnel_Information != null)
                {
                    dbcontext.Personnel_Information.Remove(Personnel_Information);
                }
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
                    dbcontext.Position_Information.Remove(Position_Information);
                }
                if (Position_Transaction_Information != null)
                {
                    dbcontext.Position_Transaction_Information.Remove(Position_Transaction_Information);
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