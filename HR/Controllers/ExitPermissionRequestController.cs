using HR.Models;
using HR.Models.Infra;
using HR.Models.Time_management;
using HR.Models.ViewModel;
using Microsoft.AspNet.Identity;
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

                var Date = Convert.ToDateTime("1/1/1900");
                var state = new status { statu = check_status.created, approved_bydate = Date, cancaled_bydate = Date, created_bydate = DateTime.Now.Date, Rejected_bydate = Date, return_to_reviewdate = Date };
                state.created_by = User.Identity.GetUserName();
                var st = dbcontext.status.Add(state);
                dbcontext.SaveChanges();

                model.statusID = st.ID;
                model.check_status = check_status.created;


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
        public ActionResult status(string id)
        {
            try
            {
                var ID = int.Parse(id);
                var model = dbcontext.Exit_permission_request.FirstOrDefault(m => m.ID == ID);
                var st = dbcontext.status.FirstOrDefault(m=>m.ID==model.statusID);
                ViewBag.statue = dbcontext.status.ToList().Select(m => new { code = m.approved_by });
                var my_model = new employeestate { status = model.status, opertion_id = model.ID };
                if (my_model.status.approved_by == null)
                    my_model.status.approved_bydate = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString());
                if (my_model.status.Rejected_by == null)
                    my_model.status.Rejected_bydate = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString());
                if (my_model.status.return_to_reviewby == null)
                    my_model.status.return_to_reviewdate = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString());
                if (my_model.status.cancaled_by == null)
                    my_model.status.cancaled_bydate = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString());
                return View(my_model);
            }
            catch (Exception e)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult status(employeestate model)
        {
            //  var sta = dbcontext.status.FirstOrDefault(m => m.ID == model.status.ID);
            var record = dbcontext.Exit_permission_request.FirstOrDefault(m => m.ID == model.opertion_id);
            var sta = dbcontext.status.FirstOrDefault(m => m.ID == record.statusID);
            if (model.check_status == check_status.Approved)
            {
                sta.approved_by = User.Identity.GetUserName();
                sta.approved_bydate = model.status.approved_bydate;
                sta.statu = check_status.Approved;
                record.check_status = check_status.Approved;
                dbcontext.SaveChanges();
            }
            //else if (model.check_status == check_status.Canceled)
            //{
            //    sta.cancaled_by = model.status.cancaled_by;
            //    sta.cancaled_bydate = model.status.cancaled_bydate;
            //    sta.statu = check_status.Canceled;
            //    record.check_status = check_status.Canceled;
            //    record.sss = record.check_status.GetType().ToString();
            //    record.name_state = nameof(check_status.Canceled);
            //    dbcontext.SaveChanges();
            //}
            //else if (model.check_status == check_status.created)
            //{
            //    sta.created_by = model.status.created_by;
            //    sta.created_bydate = model.status.created_bydate;
            //    sta.statu = check_status.created;
            //    record.check_status = check_status.created;
            //    record.sss = record.check_status.GetType().ToString();
            //    record.name_state = nameof(check_status.created);
            //    dbcontext.SaveChanges();
            //}
            else if (model.check_status == check_status.Rejected)
            {
                sta.Rejected_by = User.Identity.GetUserName();
                sta.Rejected_bydate = model.status.Rejected_bydate;
                sta.statu = check_status.Rejected;
                record.check_status = check_status.Rejected;
                dbcontext.SaveChanges();
            }
            else if (model.check_status == check_status.Return_To_Review)
            {
                sta.return_to_reviewby = User.Identity.GetUserName();
                sta.return_to_reviewdate = model.status.return_to_reviewdate;
                sta.statu = check_status.Return_To_Review;
                record.check_status = check_status.Return_To_Review;
                dbcontext.SaveChanges();
            }

            return RedirectToAction("index");
        }

    }
}