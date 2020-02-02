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
    public class Employee_beneficiary_profileController : Controller
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Employee_beneficiary_profile
        public ActionResult Index(string id)
        {
            var ID = int.Parse(id);
            var new_model = dbcontext.Employee_beneficiary_profile.Where(a => a.ID == ID).ToList();
            ViewBag.idemp = id;
            return View(new_model);
        }
        public ActionResult Create(string id,string id2)
        {

            ViewBag.Nationality = dbcontext.Subscription_Syndicate.Where(a=>a.Type== Models.Infra.Type.Subscription).ToList().Select(m => new { Code = m.Subscription_Code + "-----[" + m.Subscription_Description + ']', ID = m.ID });
         //   ViewBag.Employee_family_profile = dbcontext.Employee_family_profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.idemp = id;



            var ID2 = int.Parse(id2);
            var record = dbcontext.Employee_family_profile.FirstOrDefault(m => m.ID == ID2);
            ViewBag.family = record.Employee_Profile;






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
            var EmployeeBeneficiary= new Employee_beneficiary_profile { Employee_ProfileId = emp.ID.ToString(), Code = stru.Structure_Code + count.ToString()};
            return View(EmployeeBeneficiary);

        }
        [HttpPost]
        public ActionResult Create(Employee_beneficiary_profile model, string command)
        {
            try
            {


                ViewBag.Nationality = dbcontext.Subscription_Syndicate.Where(a => a.Type == Models.Infra.Type.Subscription).ToList().Select(m => new { Code = m.Subscription_Code + "-----[" + m.Subscription_Description + ']', ID = m.ID });
                ViewBag.Employee_family_profile = dbcontext.Employee_family_profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.idemp = model.ID;

                if (ModelState.IsValid)
                {
                    var Beneficiary = int.Parse(model.Employee_ProfileId);
                    var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == Beneficiary);
                    //      var record = dbcontext.Employee_family_profile.FirstOrDefault(m => m.ID == emp.Employee_family_profile.ID);
                    Employee_beneficiary_profile record = new Employee_beneficiary_profile();
                    record.Code = model.Code;
                    record.Legatee = model.Legatee;
                   
                    record.Employee_ProfileId = model.Employee_ProfileId;
                    var Employee_ProfileId = int.Parse(model.Employee_ProfileId);
                    record.Employee_Profile = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == Employee_ProfileId);
                    record.Benefit_type_codeId = model.Benefit_type_codeId;
                    var Benefit_type_codeId = int.Parse(model.Benefit_type_codeId);
                    record.Subscription_Syndicate = dbcontext.Subscription_Syndicate.FirstOrDefault(m => m.ID == Benefit_type_codeId);
                    record.Family_profile_No = model.Family_profile_No;
                    record.Family_name = model.Family_name;
                    record.Percentage = model.Percentage;
                   
                    dbcontext.Employee_beneficiary_profile.Add(record);
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







    }
}