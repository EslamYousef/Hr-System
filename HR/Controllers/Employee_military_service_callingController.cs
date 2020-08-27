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
    [Authorize(Roles = "Admin,personnel,personnelCards,Employee Profile")]
    public class Employee_military_service_callingController : BaseController
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
            var Employee_military_service_calling = new Employee_military_service_calling { Code = stru.Structure_Code + count, Employee_ProfileId = emp.ID.ToString(), Start_date = DateTime.Now, End_date = DateTime.Now, Employee_Profile=emp };

            return View(Employee_military_service_calling);
        }
        [HttpPost]
        public ActionResult Create(Employee_military_service_calling model, string command)
        {
            try
            {

                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            
                //if (ModelState.IsValid)
                //{
                    //    var con = int.Parse(model.Employee_ProfileId);
                    //      var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == con);
                    var EmpObj = dbcontext.Employee_Profile.FirstOrDefault(a => a.ID == model.Employee_Profile.ID);

                    Employee_military_service_calling record = new Employee_military_service_calling();
                var empid = EmpObj.Code + "------" + EmpObj.Name;
                record.Employee_ProfileId = model.Employee_ProfileId == null ? model.Employee_ProfileId = EmpObj.ID.ToString() : model.Employee_ProfileId;
                ViewBag.idemp = model.Employee_ProfileId;
                record.Employee_Profile = EmpObj;

                record.Code = model.Code;                  
                    record.Years = model.Years;
                    record.Months = model.Months;
                    record.Start_date = model.Start_date;
                    record.End_date = model.End_date;
                if (model.Start_date > model.End_date)
                {
                    TempData["Message"] = HR.Resource.Personnel.FromdatebiggerTodate;
                    return View(model);
                }
                record.Days = model.Days;
                    record.Comments = model.Comments;



                    var pos = dbcontext.Employee_military_service_calling.Add(record);
                    dbcontext.SaveChanges();

                    if (command == "Submit")
                    {
                        return RedirectToAction("edit", "Employee_Profile", new { id = EmpObj.ID });
                    }
                    return RedirectToAction("Index", new { id = EmpObj.ID });
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
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                var record = dbcontext.Employee_military_service_calling.FirstOrDefault(m => m.ID == id);
                ViewBag.idemp = record.Employee_Profile.ID.ToString(); 


                if (record != null)
                {
                    return View(record);
                }
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
        public ActionResult Edit(Employee_military_service_calling model, string command)
        {
            try
            {

                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                
                var EmpObj = dbcontext.Employee_Profile.FirstOrDefault(a => a.ID == model.Employee_Profile.ID);
              //  var prof = int.Parse(model.Employee_ProfileId);
                //   var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == prof);
                var record = dbcontext.Employee_military_service_calling.FirstOrDefault(m => m.ID == model.ID);

               var emp = record.Employee_Profile;
                var empid = EmpObj.Code + "------" + EmpObj.Name;
                record.Employee_ProfileId = model.Employee_ProfileId == null ? model.Employee_ProfileId = EmpObj.ID.ToString() : model.Employee_ProfileId;
                ViewBag.idemp = model.Employee_ProfileId;
                record.Employee_Profile = EmpObj;
                record.Code = model.Code;
                record.Years = model.Years;
                record.Months = model.Months;
                record.Start_date = model.Start_date;
                record.End_date = model.End_date;
                if (model.Start_date > model.End_date)
                {
                    TempData["Message"] = HR.Resource.Personnel.FromdatebiggerTodate;
                    return View(model);
                }
                record.Days = model.Days;
                record.Comments = model.Comments;
             

                dbcontext.SaveChanges();

                if (command == "Submit")
                {
                    return RedirectToAction("edit", "Employee_Profile", new { id = EmpObj.ID });
                }
                return RedirectToAction("index", new { id = EmpObj.ID });
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

                var record = dbcontext.Employee_military_service_calling.FirstOrDefault(m => m.ID == id);
                ViewBag.idemp = record.Employee_Profile.ID.ToString();
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
                TempData["Message"] = HR.Resource.Basic.youcannotdeletethisRow;
                return View(record);
            }
            catch (Exception e)
            {
                return View();
            }
        }





        public JsonResult DifferenceTwoDates(DateTime StartDate, DateTime EndDate)
        {
            try
            {
                var Diff = BusinessLayer.DateTimeSpan.CompareDates(EndDate.Date, StartDate.Date);
                return Json(new { success = true, DateDiff = Diff },JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


    }
}