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
    
    public class workpermissionrequestController : BaseController
    {
        // GET: workpermissionrequest
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        [Authorize(Roles = "Admin,TM,TMTransaction,TMProcess,Work permission request,Work permission Approve")]
        public ActionResult Index()
        {
            var model = dbcontext.workpermissionrequest.ToList();
            return View(model);
        }
        [Authorize(Roles = "Admin,TM,TMTransaction,Work permission request")]
        public ActionResult create()
        {
            try
            {
                var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel).Structure_Code;
                var req = dbcontext.workpermissionrequest.ToList();
                string number;
                if (req.Count > 0)
                {
                    number = stru + (req.LastOrDefault().ID + 1).ToString();
                }
                else
                {
                    number = stru + 1;
                }

                ViewBag.emp = dbcontext.Employee_Profile.Where(m=>m.Active==true).ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                var model = new workpermissionrequest();
                model.date = DateTime.Now.Date;model.fromD = DateTime.Now.Date;model.toD = DateTime.Now.Date;
                model.number = number;
                return View(model);
            }
            catch(Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult create(workpermissionrequest model,FormCollection record)
        {
            try
            {

                ViewBag.emp = dbcontext.Employee_Profile.Where(m => m.Active == true).ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                var s = record["fromT"].Split(',');
                var e = record["toT"].Split(',');
                model.fromT = Convert.ToDateTime(s[0]).TimeOfDay;
                model.toT = Convert.ToDateTime(e[0]).TimeOfDay;

                var Date = Convert.ToDateTime("1/1/1900");
                var state= new status { statu = check_status.created, approved_bydate = Date, cancaled_bydate = Date, created_bydate = DateTime.Now.Date, Rejected_bydate = Date, return_to_reviewdate = Date };
                state.created_by = User.Identity.GetUserName();
                var st = dbcontext.status.Add(state);
                dbcontext.SaveChanges();


                model.statusID = st.ID;
                model.check_status = check_status.created;
                
                dbcontext.workpermissionrequest.Add(model);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch(Exception)
            {
                return View(model);
            }
        }
        [Authorize(Roles = "Admin,TM,TMTransaction,Work permission request")]
        public ActionResult edit(int id)
        {
            try
            {
                ViewBag.emp = dbcontext.Employee_Profile.Where(m => m.Active == true).ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });

                var model = dbcontext.workpermissionrequest.FirstOrDefault(m => m.ID == id);
                return View(model);
            }
            catch(Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult edit(workpermissionrequest model, FormCollection record)
        {
            try
            {
                var recod = dbcontext.workpermissionrequest.FirstOrDefault(m => m.ID == model.ID);
                recod.Employee_ProfileID = model.Employee_ProfileID;
                recod.position_profile_num = model.position_profile_num;
                recod.work_permission_type = model.work_permission_type;
                recod.date = model.date;
                recod.accomplish = model.accomplish;
                recod.securty = model.securty;
                recod.reason = model.reason;
                recod.remark = model.remark;
                recod.fromD = model.fromD;
                recod.toD = model.toD;
                recod.month = model.month;
                recod.year = model.year;
                recod.days = model.days;
                var s = record["fromT"].Split(',');
                var e = record["toT"].Split(',');
                recod.fromT = Convert.ToDateTime(s[0]).TimeOfDay;
                recod.toT = Convert.ToDateTime(e[0]).TimeOfDay;
                recod.meal = model.meal;
                recod.lunch = model.lunch;
                recod.lunch_basket = model.lunch_basket;
                recod.dinner = model.dinner;
                dbcontext.SaveChanges();
                return RedirectToAction("index");

            }
            catch (Exception)
            {
                return View(model);
            }
        }
        [Authorize(Roles = "Admin,TM,TMTransaction,Work permission request")]
        public ActionResult delete(int id)
        {
            try
            {
                var model = dbcontext.workpermissionrequest.FirstOrDefault(m => m.ID==id);
                return View(model);
            }
            catch(Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        [ActionName("delete")]
        public ActionResult Delete_method(int id)
        {
            var model = dbcontext.workpermissionrequest.FirstOrDefault(m => m.ID == id);
            try
            {
                dbcontext.workpermissionrequest.Remove(model);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (Exception)
            {
                return View(model);
            }
        }
        [Authorize(Roles = "Admin,TM,TMProcess,Work permission Approve")]
        public ActionResult status(string id)
        {
            try
            {
                var ID = int.Parse(id);
                var model = dbcontext.workpermissionrequest.FirstOrDefault(m => m.ID == ID);
                var st = model.status;
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
            var record = dbcontext.workpermissionrequest.FirstOrDefault(m => m.ID == model.opertion_id);
            var sta = dbcontext.status.FirstOrDefault(m=>m.ID==record.statusID);
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