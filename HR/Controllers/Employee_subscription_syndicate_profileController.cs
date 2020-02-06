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
    [Authorize]
    public class Employee_subscription_syndicate_profileController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Employee_subscription_syndicate_profile
        public ActionResult Index(string id)
        {
            var ID = int.Parse(id);
            var new_model = dbcontext.Employee_subscription_syndicate_profile.Where(m => m.Employee_Profile.ID == ID).ToList();
            ViewBag.idemp = id;
            return View(new_model);
        }
        public ActionResult Create(string id)
        {

          
            ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.Subscription_Syndicate = dbcontext.Subscription_Syndicate.Where(a => a.Type == Models.Infra.Type.Subscription).ToList().Select(m => new { Code = m.Subscription_Code + "-----[" + m.Subscription_Description + ']', ID = m.ID });
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
            DateTime statis = Convert.ToDateTime("1/1/1900");
            var ID = int.Parse(id);
            var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == ID);
            var EmployeeSubscription = new Employee_subscription_syndicate_profile { Employee_ProfileId = emp.ID, Code = stru.Structure_Code + count.ToString(), Subscription_date = statis, Boarder_election_date = statis, Head_election_date = statis, Last_date_paid = statis ,Employee_pay=0.00,Company_pay=0.00,Start_year_of_deduction=1900,End_month_of_deduction=0,Beneficiary_period=0.000,Start_month_of_deduction=0,End_year_of_deduction=1900,Balance=0.000,Subscription_fees=0.00,Family_subscription_fees=0.00};
            return View(EmployeeSubscription);

        }
        [HttpPost]
        public ActionResult Create(Employee_subscription_syndicate_profile model, string command)
        {
            try
            {
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Subscription_Syndicate = dbcontext.Subscription_Syndicate.Where(a => a.Type == Models.Infra.Type.Subscription).ToList().Select(m => new { Code = m.Subscription_Code + "-----[" + m.Subscription_Description + ']', ID = m.ID });
                ViewBag.idemp = model.ID;

                if (ModelState.IsValid)
                {
                    Employee_subscription_syndicate_profile record = new Employee_subscription_syndicate_profile();
                 
                    record.Code = model.Code;
                    record.Subscription_type = model.Subscription_type;
                    record.Subscription_date = model.Subscription_date;
                    record.Employee_pay = model.Employee_pay;
                    record.Company_pay = model.Company_pay;
                    record.Start_month_of_deduction = model.Start_month_of_deduction;
                    record.Start_year_of_deduction = model.Start_year_of_deduction;
                    record.End_month_of_deduction = model.End_month_of_deduction;
                    record.End_year_of_deduction = model.End_year_of_deduction;
                    record.Employee_ProfileId = model.Employee_ProfileId;
                    record.Employee_Profile = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == model.Employee_ProfileId);
                    record.Subscription_SyndicateId = model.Subscription_SyndicateId;
                    record.Subscription_Syndicate = dbcontext.Subscription_Syndicate.FirstOrDefault(m => m.ID == model.Subscription_SyndicateId);
                    record.Beneficiary_period = model.Beneficiary_period;
                    record.Balance = model.Balance;
                    record.Referance_number = model.Referance_number;
                    record.Last_date_paid = model.Last_date_paid;
                    record.Pay_to_entity = model.Pay_to_entity;
                    record.Pay_to_entity_type = model.Pay_to_entity_type;
                    record.Subscription_value_type = model.Subscription_value_type;
                    record.Membership = model.Membership;
                    record.Subscription_fees = model.Subscription_fees;
                    record.Family_subscription_fees = model.Family_subscription_fees;
                    record.Subscription_in_house = model.Subscription_in_house;
                    record.Is_family_subscribed = model.Is_family_subscribed;
                    record.Membership_type = model.Membership_type;
                    record.Boarder_election_date = model.Boarder_election_date;
                    record.Head_election_date = model.Head_election_date;
                    record.Contact_detail = model.Contact_detail;
                
                    dbcontext.Employee_subscription_syndicate_profile.Add(record);
                    dbcontext.SaveChanges();
                    if (command == "Submit")
                    {
                        return RedirectToAction("edit", "Employee_Profile", new { id = record.Employee_ProfileId });
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

                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Subscription_Syndicate = dbcontext.Subscription_Syndicate.Where(a => a.Type == Models.Infra.Type.Subscription).ToList().Select(m => new { Code = m.Subscription_Code + "-----[" + m.Subscription_Description + ']', ID = m.ID });

                var record = dbcontext.Employee_subscription_syndicate_profile.FirstOrDefault(m => m.ID == id);
                ViewBag.idemp = record.Employee_Profile.ID.ToString();
                if (record != null)
                {
                    return View(record);
                }
                else
                {
                    TempData["Message"] = "There is no Employee Subscription Syndicate Profile";
                    return View();
                }
            }
            catch
            (Exception e)
            { return View(); }
        }
        [HttpPost]
        public ActionResult Edit(Employee_subscription_syndicate_profile model, string command)
        {
            try
            {

                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Subscription_Syndicate = dbcontext.Subscription_Syndicate.Where(a => a.Type == Models.Infra.Type.Subscription).ToList().Select(m => new { Code = m.Subscription_Code + "-----[" + m.Subscription_Description + ']', ID = m.ID });

                ViewBag.idemp = model.ID;

                var record = dbcontext.Employee_subscription_syndicate_profile.FirstOrDefault(m => m.ID == model.ID);
                record.Code = model.Code;
                record.Subscription_type = model.Subscription_type;
                record.Subscription_date = model.Subscription_date;
                record.Employee_pay = model.Employee_pay;
                record.Company_pay = model.Company_pay;
                record.Start_month_of_deduction = model.Start_month_of_deduction;
                record.Start_year_of_deduction = model.Start_year_of_deduction;
                record.End_month_of_deduction = model.End_month_of_deduction;
                record.End_year_of_deduction = model.End_year_of_deduction;
                record.Employee_ProfileId = model.Employee_ProfileId;
                record.Employee_Profile = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == model.Employee_ProfileId);
                record.Subscription_SyndicateId = model.Subscription_SyndicateId;
                record.Subscription_Syndicate = dbcontext.Subscription_Syndicate.FirstOrDefault(m => m.ID == model.Subscription_SyndicateId);
                record.Beneficiary_period = model.Beneficiary_period;
                record.Balance = model.Balance;
                record.Referance_number = model.Referance_number;
                record.Last_date_paid = model.Last_date_paid;
                record.Pay_to_entity = model.Pay_to_entity;
                record.Pay_to_entity_type = model.Pay_to_entity_type;
                record.Subscription_value_type = model.Subscription_value_type;
                record.Membership = model.Membership;
                record.Subscription_fees = model.Subscription_fees;
                record.Family_subscription_fees = model.Family_subscription_fees;
                record.Subscription_in_house = model.Subscription_in_house;
                record.Is_family_subscribed = model.Is_family_subscribed;
                record.Membership_type = model.Membership_type;
                record.Boarder_election_date = model.Boarder_election_date;
                record.Head_election_date = model.Head_election_date;
                record.Contact_detail = model.Contact_detail;
                dbcontext.SaveChanges();

                if (command == "Submit")
                {
                    return RedirectToAction("edit", "Employee_Profile", new { id = record.Employee_ProfileId });
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

                var record = dbcontext.Employee_subscription_syndicate_profile.FirstOrDefault(m => m.ID == id);
                ViewBag.idemp = record.Employee_Profile.ID.ToString();
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = "There is no Employee Subscription Syndicate Profile";
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

            var record = dbcontext.Employee_subscription_syndicate_profile.FirstOrDefault(m => m.ID == id);
            ViewBag.idemp = record.Employee_Profile.ID.ToString();
            try
            {
                dbcontext.Employee_subscription_syndicate_profile.Remove(record);
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