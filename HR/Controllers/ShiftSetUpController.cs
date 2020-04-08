using HR.Models;
using HR.Models.Infra;
using HR.Models.Time_management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers
{
    public class ShiftSetUpController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: ShiftSetUp
        public ActionResult Index()
        {
            var model = dbcontext.Shift_setup.ToList();
            return View(model);
        }
        public ActionResult Create()
        {
            try
            {
                ViewBag.location = dbcontext.work_location.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.weekend = dbcontext.Weekend_setup.ToList().Select(m => new { Code = m.Code + "------[" + m.Description + ']', ID = m.ID });

                var req = dbcontext.Shift_setup.ToList();
                var model = new Shift_setup();
                var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel).Structure_Code;
                string number;
                if (req.Count > 0)
                {
                    number = stru + (req.LastOrDefault().ID + 1).ToString();
                }
                else
                {
                    number = stru + 1;
                }
                model.Code = number;model.working_system = working_system.Day;
                return View(model);
            }
            catch(Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult Create(Shift_setup model,FormCollection record)
        {
            try
            {
                ViewBag.location = dbcontext.work_location.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
               ViewBag.weekend = dbcontext.Weekend_setup.ToList().Select(m => new { Code = m.Code + "------[" + m.Description + ']', ID = m.ID });
               var s=record["Start_time"].Split(',');
                var e = record["End_time"].Split(',');
                model.Start_time = Convert.ToDateTime(s[0]).TimeOfDay;
                model.End_time = Convert.ToDateTime(e[0]).TimeOfDay;
                dbcontext.Shift_setup.Add(model);
                dbcontext.SaveChanges();
            return RedirectToAction("index");
        }
            catch(Exception)
            {
                return View(model);
            }
        }
        public ActionResult Edit(int id)
        {
            try
            {
                ViewBag.location = dbcontext.work_location.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.weekend = dbcontext.Weekend_setup.ToList().Select(m => new { Code = m.Code + "------[" + m.Description + ']', ID = m.ID });
                var model = dbcontext.Shift_setup.FirstOrDefault(m => m.ID == id);
               if(model.Start_time.TotalHours>12)
                {
                    model.Start_time =Convert.ToDateTime("01/01/2000 "+(model.Start_time.Hours - 12).ToString()+":"+model.Start_time.Minutes.ToString()+":"+model.Start_time.Seconds.ToString()+" pm").TimeOfDay;
                }
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult Edit(Shift_setup model,FormCollection rec)
        {

            try
            {
                ViewBag.location = dbcontext.work_location.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.weekend = dbcontext.Weekend_setup.ToList().Select(m => new { Code = m.Code + "------[" + m.Description + ']', ID = m.ID });
                var original_model = dbcontext.Shift_setup.FirstOrDefault(m => m.ID == model.ID);
                original_model.Description = model.Description;
                original_model.Name = model.Name;
                original_model.Total_Hours = model.Total_Hours;
                original_model.Weekend_setupID = model.Weekend_setupID;
                original_model.Weekend_setupID = model.Weekend_setupID;
                var s = rec["Start_time"].Split(',');
                var e = rec["End_time"].Split(',');
                original_model.Start_time = Convert.ToDateTime(s[0]).TimeOfDay;
                original_model.End_time = Convert.ToDateTime(e[0]).TimeOfDay;
                dbcontext.SaveChanges();
                return RedirectToAction("index");
                
            }
            catch (Exception)
            {
                return View(model);
            }

        }
        public ActionResult Deltet_method(int id)
        {
            try
            {
                var model = dbcontext.Shift_setup.FirstOrDefault(m => m.ID == id);
                return View(model);
            }
            catch(Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        [ActionName("Deltet_method")]
        public ActionResult Deltet(int id)
        {
            var model = dbcontext.Shift_setup.FirstOrDefault(m => m.ID == id);
            try
            {
                dbcontext.Shift_setup.Remove(model);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (Exception)
            {
                return View(model);
            }
        }
    }
}