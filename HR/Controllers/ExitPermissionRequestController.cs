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
    public class ExitPermissionRequestController : Controller
    {
        // GET: ExitPermissionRequest
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        public ActionResult index()
        {
            try
            {
                var model = dbcontext.Exit_permission_request.ToList();
                return View(model);
            }
            catch(Exception)
            {
                return View();
            }

        }
        public ActionResult Create()
        {
            try
            {
                ViewBag.emp= dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.type = dbcontext.Exit_permission_type.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.reasons = dbcontext.Exit_Permission_Reason.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });


                var model = new Exit_permission_request();
                var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel).Structure_Code;
                var all_data = dbcontext.Exit_permission_request.ToList();
                string number;
                if (all_data.Count > 0)
                {
                    number = stru + (all_data.LastOrDefault().ID + 1).ToString();
                }
                else
                {
                    number = stru + 1;
                }
                model.Request_Number = number;model.Date = DateTime.Now.Date;
                return View(model);
            }
            catch(Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult Create(Exit_permission_request model,FormCollection form)
        {
            try
            {
                ViewBag.emp = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.type = dbcontext.Exit_permission_type.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.reasons = dbcontext.Exit_Permission_Reason.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                var s = form["From"].Split(',');
                var e = form["To"].Split(',');
                model.From = Convert.ToDateTime(s[0]).TimeOfDay;
                model.To = Convert.ToDateTime(e[0]).TimeOfDay;
                dbcontext.Exit_permission_request.Add(model);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (Exception)
            {
                return View(model);
            }
        }
        public ActionResult edit(int id)
        {
            try
            {
                ViewBag.emp = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.type = dbcontext.Exit_permission_type.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.reasons = dbcontext.Exit_Permission_Reason.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });

                var model = dbcontext.Exit_permission_request.FirstOrDefault(m => m.ID == id);
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult edit(Exit_permission_request model, FormCollection form)
        {
            try
            {
                ViewBag.emp = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.type = dbcontext.Exit_permission_type.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.reasons = dbcontext.Exit_Permission_Reason.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });

                var old_record = dbcontext.Exit_permission_request.FirstOrDefault(m => m.ID == model.ID);
                old_record.Date = model.Date;
                old_record.Employee_ProfileID = model.Employee_ProfileID;
                old_record.Exit_Permission_ReasonID = model.Exit_Permission_ReasonID;
                old_record.Exit_permission_typeID = model.Exit_permission_typeID;
                old_record.Notes = model.Notes;
                var s = form["From"].Split(',');
                var e = form["To"].Split(',');
                old_record.From = Convert.ToDateTime(s[0]).TimeOfDay;
                old_record.To = Convert.ToDateTime(e[0]).TimeOfDay;
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (Exception)
            {
                return View(model);
            }
        }
        public ActionResult delete(int id)
        {
            try
            {
                var model = dbcontext.Exit_permission_request.FirstOrDefault(m => m.ID == id);
                return View(model);
            }
            catch(Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        [ActionName("delete")]
        public ActionResult delete_method(int id)
        {
            var model = dbcontext.Exit_permission_request.FirstOrDefault(m => m.ID == id);

            try
            {
                dbcontext.Exit_permission_request.Remove(model);
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