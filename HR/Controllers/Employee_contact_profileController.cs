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
    public class Employee_contact_profileController : Controller
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Employee_contact_profile
        public ActionResult Index()
        {
            var employee = dbcontext.Employee_Profile.ToList();
            var Employeecontact = dbcontext.Employee_contact_profile.ToList();
            var model = from a in employee
                        join b in Employeecontact on a.Employee_contact_profile.ID equals b.ID
                        select new EmployeeContact_VM
                        {
                            fullname = a.Full_Name,
                            code = a.Code,
                            EmployeeId = a.ID,
                            Employee_contact_profile = b
                        };
            return View(model);
        }
        public ActionResult Create(string id)
        {

            ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.Contactmethods = dbcontext.Contact_methods.ToList().Select(m => new { Code = m.Code + "------[" + m.Description + ']', ID = m.ID });
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel);
            var model = dbcontext.Employee_contact_profile.ToList();
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
            if (id != null)
            {
                var ID = int.Parse(id);
                var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == ID);
                var x = emp.Employee_contact_profile;
                return View(x);
            }

            var EmployeeContact = new Employee_contact_profile();
            return View(EmployeeContact);

        }
        [HttpPost]
        public ActionResult Create(Employee_contact_profile model, string command)
        {
            try
            {

                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Contactmethods = dbcontext.Contact_methods.ToList().Select(m => new { Code = m.Code + "------[" + m.Description + ']', ID = m.ID });
                if (ModelState.IsValid)
                {
                    var contact = int.Parse(model.Employee_ProfileId);
                    var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == contact);
                    var record = dbcontext.Employee_contact_profile.FirstOrDefault(m => m.ID == emp.Employee_contact_profile.ID);

                    record.ContactmethodsId = model.ContactmethodsId;
                    record.Primary = model.Primary;
                    record.Employee_ProfileId = model.Employee_ProfileId;
                    record.Comments = model.Comments;
                    record.Contact_method_detail = model.Contact_method_detail;

                    dbcontext.SaveChanges();
                    if (command == "Submit")
                    {
                        return RedirectToAction("edit", "Employee_Profile", new { id = int.Parse(record.Employee_ProfileId) });
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
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Contactmethods = dbcontext.Contact_methods.ToList().Select(m => new { Code = m.Code + "------[" + m.Description + ']', ID = m.ID });

        var record = dbcontext.Employee_contact_profile.FirstOrDefault(m => m.ID == id);
                if (record != null)
                {
                    return View(record);
                }
                else
                {
                    TempData["Message"] = "There is no Employee Contact Profile";
                    return View();
                }
            }
            catch
            (Exception e)
            { return View(); }
        }
        [HttpPost]
        public ActionResult Edit(Employee_contact_profile model, string command)
        {
            try
            {
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Contactmethods = dbcontext.Contact_methods.ToList().Select(m => new { Code = m.Code + "------[" + m.Description + ']', ID = m.ID });
              
                var record = dbcontext.Employee_contact_profile.FirstOrDefault(m => m.ID == model.ID);
                record.Code = model.Code;
                record.ContactmethodsId = model.ContactmethodsId;
                record.Primary = model.Primary;
                record.Employee_ProfileId = model.Employee_ProfileId;
                record.Comments = model.Comments;
                record.Contact_method_detail = model.Contact_method_detail;

                dbcontext.SaveChanges();

                if (command == "Submit")
                {
                    return RedirectToAction("edit", "Employee_Profile", new { id = int.Parse(record.Employee_ProfileId) });
                }
                return RedirectToAction("index");
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = "This code Is already exists";
                return View(model);
            }
            catch (Exception e)
            { return View(model); }
        }
    }
}