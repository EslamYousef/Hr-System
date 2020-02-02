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
    public class Employee_military_service_callingController : Controller
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Employee_military_service_calling
        public ActionResult Index(string id)
        {
            var ID = int.Parse(id);
            var new_model = dbcontext.Employee_military_service_calling.Where(m => m.Employee_Profile.ID == ID).ToList();
            ViewBag.idemp = id;
            return View(new_model);
        }
        public ActionResult Create(string id)
        {
            ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.idemp = id;
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel);
            var model = dbcontext.Employee_military_service_calling.ToList();
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
            DateTime statis2 = Convert.ToDateTime("1/1/1900");
            var ID = int.Parse(id);
            var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == ID);
            var Employee_military_service_calling = new Employee_military_service_calling { Code = stru.Structure_Code + count, Employee_ProfileId = id, Start_date = statis2, End_date = statis2 };

            return View(Employee_military_service_calling);
        }
        [HttpPost]
        public ActionResult Create(Employee_military_service_calling model, string command)
        {
            try
            {

                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.idemp = model.ID;
                if (ModelState.IsValid)
                {
                    var con = int.Parse(model.Employee_ProfileId);
                    var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == con);

                    Employee_military_service_calling record = new Employee_military_service_calling();
                    record.Code = model.Code;                  
                    record.Years = model.Years;
                    record.Months = model.Months;
                    record.Start_date = model.Start_date;
                    record.End_date = model.End_date;
                    record.Days = model.Days;
                    record.Comments = model.Comments;
                   
                    record.Employee_ProfileId = model.Employee_ProfileId;
                    var Employee_ProfileId = int.Parse(model.Employee_ProfileId);
                    record.Employee_Profile = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == Employee_ProfileId);
                   

                    var pos = dbcontext.Employee_military_service_calling.Add(record);
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
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                var record = dbcontext.Employee_military_service_calling.FirstOrDefault(m => m.ID == id);
                ViewBag.idemp = record.Employee_Profile.ID.ToString();


                if (record != null)
                {
                    return View(record);
                }
                else
                {
                    TempData["Message"] = "There is no Employee Military Service Profile";
                    return View();
                }
            }
            catch
            (Exception e)
            { return View(); }
        }
        [HttpPost]
        public ActionResult Edit(Employee_military_service_calling model, string command)
        {
            try
            {

                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.idemp = model.ID;
                var prof = int.Parse(model.Employee_ProfileId);
                //   var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == prof);
                var record = dbcontext.Employee_military_service_calling.FirstOrDefault(m => m.ID == model.ID);
                var emp = record.Employee_Profile;
                record.Code = model.Code;
                record.Years = model.Years;
                record.Months = model.Months;
                record.Start_date = model.Start_date;
                record.End_date = model.End_date;
                record.Days = model.Days;
                record.Comments = model.Comments;

                record.Employee_ProfileId = model.Employee_ProfileId;
                var Employee_ProfileId = int.Parse(model.Employee_ProfileId);
                record.Employee_Profile = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == Employee_ProfileId);
                dbcontext.SaveChanges();

                if (command == "Submit")
                {
                    return RedirectToAction("edit", "Employee_Profile", new { id = int.Parse(record.Employee_ProfileId) });
                }
                return RedirectToAction("index", new { id = model.Employee_ProfileId });
            }
            catch (DbUpdateException e)
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

                var record = dbcontext.Employee_military_service_calling.FirstOrDefault(m => m.ID == id);
                ViewBag.idemp = record.Employee_Profile.ID.ToString();
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = "There is no Employee Military Service Profile";
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

            var record = dbcontext.Employee_military_service_calling.FirstOrDefault(m => m.ID == id);
            ViewBag.idemp = record.Employee_Profile.ID.ToString();
            try
            {
                dbcontext.Employee_military_service_calling.Remove(record);
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