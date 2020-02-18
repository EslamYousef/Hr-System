using HR.Models;
using HR.Models.Infra;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace HR.Controllers
{
    [Authorize]
    public class Employee_vehicle_profileController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Employee_vehicle_profile
        public ActionResult Index()
        {
            var model = dbcontext.Employee_vehicle_profile.ToList();
            return View(model);
        }
        public ActionResult Create()
        {
            ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel);
            var model = dbcontext.Employee_vehicle_profile.ToList();
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
            DateTime statis = Convert.ToDateTime("1/1/1900");
         //   var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == ID);
            var EmployeeVehicle = new Employee_vehicle_profile {/* Employee_ProfileId = emp.ID.ToString(),*/ Code = stru.Structure_Code + count.ToString(), From_date= statis, To_date= statis };

            return View(EmployeeVehicle);
        }
        [HttpPost]
        public ActionResult Create(Employee_vehicle_profile model )
        {
            try
            {   
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
              

                if (ModelState.IsValid)
                {
                                  
                    Employee_vehicle_profile record = new Employee_vehicle_profile();
                 
                    record.Code = model.Code;
                    record.Vehicle_plate_number = model.Vehicle_plate_number;
                    record.Vehicle_model = model.Vehicle_model;
                    record.Comments = model.Comments;
                    record.Employee_ProfileId = model.Employee_ProfileId;
                    var Employee_ProfileId = int.Parse(model.Employee_ProfileId);
                    record.Employee_Profile = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == Employee_ProfileId);
                    record.From_date= model.From_date;
                    record.To_date = model.To_date;
                    if (model.From_date > model.To_date)
                    {
                        TempData["Message"] = HR.Resource.Personnel.FromdatebiggerTodate;
                        return View(model);
                    }
                                
                    dbcontext.Employee_vehicle_profile.Add(record);
                    dbcontext.SaveChanges();
                  
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(model);
                }
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
                var record = dbcontext.Employee_vehicle_profile.FirstOrDefault(m => m.ID == id);

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
        public ActionResult Edit(Employee_vehicle_profile model)
        {
            try
            {
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });

                var record = dbcontext.Employee_vehicle_profile.FirstOrDefault(m => m.ID == model.ID);
                //    var emp = record.Employee_Profile;
                record.Code = model.Code;
                record.Vehicle_plate_number = model.Vehicle_plate_number;
                record.Vehicle_model = model.Vehicle_model;
                record.Comments = model.Comments;
                record.Employee_ProfileId = model.Employee_ProfileId;
                var Employee_ProfileId = int.Parse(model.Employee_ProfileId);
                record.Employee_Profile = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == Employee_ProfileId);
                record.From_date = model.From_date;
                record.To_date = model.To_date;
                if (model.From_date > model.To_date)
                {
                    TempData["Message"] = HR.Resource.Personnel.FromdatebiggerTodate;
                    return View(model);
                }

                dbcontext.SaveChanges();

               
                return RedirectToAction("index");
            }
            catch (DbUpdateException)
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
                var record = dbcontext.Employee_vehicle_profile.FirstOrDefault(m => m.ID == id);

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
            var record = dbcontext.Employee_vehicle_profile.FirstOrDefault(m => m.ID == id);
            ViewBag.idemp = record.Employee_Profile.ID.ToString();

            try
            {
                dbcontext.Employee_vehicle_profile.Remove(record);
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
                return View();
            }
        }
    }
}