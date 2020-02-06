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
    public class Employee_beneficiary_profileController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Employee_beneficiary_profile
        public ActionResult Index(string id)
        {
            var ID = int.Parse(id);
            var new_model = dbcontext.Employee_beneficiary_profile.Where(a => a.Employee_Profile.ID == ID).ToList();
            ViewBag.idemp = id;
            return View(new_model);
        }
        public ActionResult Create(string id)
        {

            ViewBag.Subscription_Syndicate = dbcontext.Subscription_Syndicate.Where(a=>a.Type== Models.Infra.Type.Subscription).ToList().Select(m => new { Code = m.Subscription_Code + "-----[" + m.Subscription_Description + ']', ID = m.ID });
         //   ViewBag.Employee_family_profile = dbcontext.Employee_family_profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.idemp = id;

            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel);
            var model = dbcontext.Employee_beneficiary_profile.ToList();
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
        //    DateTime statis3 = Convert.ToDateTime("1/1/1900");
            var ID = int.Parse(id);
            var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == ID);
            ViewBag.family = emp.Employee_family_profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID }); 

            var EmployeeBeneficiary = new Employee_beneficiary_profile { Employee_ProfileId = emp.ID.ToString(), Code = stru.Structure_Code + count.ToString()};
            return View(EmployeeBeneficiary);

        }
        [HttpPost]
        public ActionResult Create(FormCollection form, Employee_beneficiary_profile model, string command)
        {
            try
            {


                ViewBag.Subscription_Syndicate = dbcontext.Subscription_Syndicate.Where(a => a.Type == Models.Infra.Type.Subscription).ToList().Select(m => new { Code = m.Subscription_Code + "-----[" + m.Subscription_Description + ']', ID = m.ID });
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.idemp = model.ID;
                ViewBag.family = model.ID;
                if (ModelState.IsValid)
                {
                  

                    var Employee_ProfileId = int.Parse(model.Employee_ProfileId);
                    var Employee_Profile = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == Employee_ProfileId);
                   
                    var Family_profile =form["Family_profile_No2"].Split(char.Parse(","));
                    var Family_name = form["Family_name"].Split(char.Parse(","));
                    var Percentage = form["Percentage"].Split(char.Parse(","));
                    var items = new List<Append_beneficiary_Family>();
                    for (var i = 0; i < Family_profile.Count(); i++)
                    {
                        if (Family_profile[i] != "" && Family_name[i] != "" && Percentage[i] != "" )
                        {
                            items.Add(new Append_beneficiary_Family { Percentage = int.Parse(Percentage[i]), Family_name = Family_name[i], Family_profile = Family_profile[i] });
                         
                          
                        }
                  
                    }
                    if (items.Count() > 0)
                    {
                        var add_items= dbcontext.Append_beneficiary_Family.AddRange(items);
                        dbcontext.SaveChanges();
                        var benfit = new Employee_beneficiary_profile { Append_beneficiary_Family = add_items.ToList(), Code = model.Code, Employee_Profile = Employee_Profile, Legatee = model.Legatee, Employee_ProfileId = Employee_Profile.ID.ToString() };
                        dbcontext.Employee_beneficiary_profile.Add(benfit);
                        dbcontext.SaveChanges();
                    }
                   
                    dbcontext.SaveChanges();
                    if (command == "Submit")
                    {
                        return RedirectToAction("edit", "Employee_Profile", new { id = int.Parse(Employee_Profile.ID.ToString()) });
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
        public ActionResult Edit(string id)

        {
            try
            {
                ViewBag.Subscription_Syndicate = dbcontext.Subscription_Syndicate.Where(a => a.Type == Models.Infra.Type.Subscription).ToList().Select(m => new { Code = m.Subscription_Code + "-----[" + m.Subscription_Description + ']', ID = m.ID });
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                 var ID = int.Parse(id);
                var record = dbcontext.Employee_beneficiary_profile.FirstOrDefault(m => m.ID == ID);
                var emp = record.Employee_Profile;
                ViewBag.family = emp.Employee_family_profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
              
     
                ViewBag.idemp = record.Employee_Profile.ID.ToString();
          
             

                if (record != null)
                {
                    return View(record);
                }
                else
                {
                    TempData["Message"] = "There is no Employee Beneficiary Profile";
                    return View();
                }
            }
            catch
            (Exception e)
            { return View(); }
        }
        [HttpPost]
        public ActionResult Edit(Employee_beneficiary_profile model, string command,FormCollection form)
        {
            try
            {

                ViewBag.Subscription_Syndicate = dbcontext.Subscription_Syndicate.Where(a => a.Type == Models.Infra.Type.Subscription).ToList().Select(m => new { Code = m.Subscription_Code + "-----[" + m.Subscription_Description + ']', ID = m.ID });
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.idemp = model.ID;
                var record = dbcontext.Employee_beneficiary_profile.FirstOrDefault(m => m.ID == model.ID);
                var empId = int.Parse(model.Employee_ProfileId);
               var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == empId);
                ViewBag.family = emp.Employee_family_profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });



          //      var record = dbcontext.Employee_beneficiary_profile.FirstOrDefault(m => m.ID == model.ID);
                //var emp = record.Employee_Profile;
                record.Legatee = model.Legatee;
                ///////////delete old Append_beneficiary_Family///////
               // var all_items = model.Append_beneficiary_Family;
                dbcontext.Append_beneficiary_Family.RemoveRange(record.Append_beneficiary_Family);
                dbcontext.SaveChanges();

                /////////////////add new Append_beneficiary_Family////////
                var Family_profile = form["Family_profile_No2"].Split(char.Parse(","));
                var Family_name = form["Family_name"].Split(char.Parse(","));
                var Percentage = form["Percentage"].Split(char.Parse(","));

                var items = new List<Append_beneficiary_Family>();
                for (var i = 0; i < Family_profile.Count(); i++)
                {
                    if (Family_profile[i] != "" && Family_name[i] != "" && Percentage[i] != "")
                    {
                        items.Add(new Append_beneficiary_Family { Percentage = int.Parse(Percentage[i]), Family_name = Family_name[i], Family_profile = Family_profile[i] });


                    }

                    dbcontext.SaveChanges();
                }
                if (items.Count() > 0)
                {
                    var add_items = dbcontext.Append_beneficiary_Family.AddRange(items);
                    dbcontext.SaveChanges();
                    record.Append_beneficiary_Family = add_items.ToList();
                    //var benfit = new Employee_beneficiary_profile { Append_beneficiary_Family = add_items.ToList(), Code = model.Code, Employee_Profile = emp, Legatee = model.Legatee, Employee_ProfileId = Employee_Profile.ID.ToString() };
                    //dbcontext.Employee_beneficiary_profile.Add(benfit);
                    dbcontext.SaveChanges();
                }


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
                var record = dbcontext.Employee_beneficiary_profile.FirstOrDefault(m => m.ID == id);
                ViewBag.idemp = record.Employee_Profile.ID.ToString();

                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = "There is no Employee Beneficiary Profile";
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
            var record = dbcontext.Employee_beneficiary_profile.FirstOrDefault(m => m.ID == id);
            ViewBag.idemp = record.Employee_Profile.ID.ToString();

            try
            {
                dbcontext.Employee_beneficiary_profile.Remove(record);
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