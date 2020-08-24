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
    public class business_trip_requestController : BaseController
    {
        // GET: business_trip_request
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        [Authorize(Roles = "Admin,TM,TMTransaction")]
        public ActionResult Index()
        {
            var model = dbcontext.business_trip_request.ToList();
            return View(model);
        }
        [Authorize(Roles = "Admin,TM,TMTransaction")]
        public ActionResult create()
        {
            try
            {
                var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel).Structure_Code;
                var req = dbcontext.business_trip_request.ToList();
               
                string number;
                if (req.Count > 0)
                {
                    number = stru + (req.LastOrDefault().ID + 1).ToString();
                }
                else
                {
                    number = stru + 1;
                }

                ViewBag.emp = dbcontext.Employee_Profile.Where(m => m.Active == true).ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.busi = dbcontext.Business_Trip.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.trans = dbcontext.TransportationMethod.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });

                var model = new business_trip_request();
                model.request_date = DateTime.Now.Date; model.go_date = DateTime.Now.Date; model.return_date = DateTime.Now.Date;
                model.number = number;
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult create(business_trip_request model, FormCollection record)
        {
            try
            {

                ViewBag.emp = dbcontext.Employee_Profile.Where(m => m.Active == true).ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.busi = dbcontext.Business_Trip.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.trans = dbcontext.TransportationMethod.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                var s = record["go_time"].Split(',');
                var e = record["return_time"].Split(',');
                model.go_time = Convert.ToDateTime(s[0]).TimeOfDay;
                model.return_time = Convert.ToDateTime(e[0]).TimeOfDay;
                model.Total_rate=double.Parse(record["rate"].Split(',')[0]);

                if (model.tionMethodID_return == 0)
                {
                    var trsn = dbcontext.TransportationMethod.FirstOrDefault(m => m.ID == model.tionMethodID_return);
                    if (trsn == null)
                    {
                        return View(model);
                    }
                }

                var Date = Convert.ToDateTime("1/1/1900");
                var state = new status { statu = check_status.created, approved_bydate = Date, cancaled_bydate = Date, created_bydate = DateTime.Now.Date, Rejected_bydate = Date, return_to_reviewdate = Date };
                state.created_by = User.Identity.GetUserName();
                var st = dbcontext.status.Add(state);
                dbcontext.SaveChanges();

                model.statusID = st.ID;
                model.check_status = check_status.created;
                dbcontext.business_trip_request.Add(model);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (Exception e)
            {
                return View(model);
            }
        }
        [Authorize(Roles = "Admin,TM,TMTransaction")]
        public ActionResult edit(int id)
        {
            try
            {
                ViewBag.emp = dbcontext.Employee_Profile.Where(m => m.Active == true).ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.busi = dbcontext.Business_Trip.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.trans = dbcontext.TransportationMethod.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });

                var model = dbcontext.business_trip_request.FirstOrDefault(m => m.ID == id);
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult edit(business_trip_request model, FormCollection record1)
        {
            try
            {
                ViewBag.emp = dbcontext.Employee_Profile.Where(m => m.Active == true).ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.busi = dbcontext.Business_Trip.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.trans = dbcontext.TransportationMethod.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });

                var recod = dbcontext.business_trip_request.FirstOrDefault(m => m.ID == model.ID);
                recod.Employee_ProfileID = model.Employee_ProfileID;
                recod.request_date = model.request_date;
                recod.location_of_duty = model.location_of_duty;
                recod.purpose_of_duty = model.purpose_of_duty;
                recod.Remarks_of_duty = model.Remarks_of_duty;
                recod.go_date = model.go_date;
                recod.return_date = model.return_date;
                recod.months = model.months;
                recod.years = model.years;
                recod.carry_important_documents = model.carry_important_documents;
                recod.days = model.days;
                var s = record1["go_time"].Split(',');
                var e = record1["return_time"].Split(',');
                recod.Total_rate = double.Parse(record1["rate"].Split(',')[0]);
                recod.go_time = Convert.ToDateTime(s[0]).TimeOfDay;
                recod.return_time = Convert.ToDateTime(e[0]).TimeOfDay;
                recod.Business_TripID = model.Business_TripID;
                recod.TransportationMethodID = model.TransportationMethodID;
                if(model.tionMethodID_return == 0)
                {
                    var trsn = dbcontext.TransportationMethod.FirstOrDefault(m => m.ID == model.tionMethodID_return);
                    if(trsn==null)
                    {
                        return View(model);
                    }
                    else
                    {
                        recod.tionMethodID_return = model.tionMethodID_return;
                    }
                }
                dbcontext.SaveChanges();
                return RedirectToAction("index");

            }
            catch (Exception)
            {
                return View(model);
            }
        }
        [Authorize(Roles = "Admin,TM,TMTransaction")]
        public ActionResult delete(int id)
        {
            try
            {
                var model = dbcontext.business_trip_request.FirstOrDefault(m => m.ID == id);
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        [ActionName("delete")]
        public ActionResult Delete_method(int id)
        {
            var model = dbcontext.business_trip_request.FirstOrDefault(m => m.ID == id);
            try
            {
                dbcontext.business_trip_request.Remove(model);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (Exception)
            {
                return View(model);
            }
        }
        [Authorize(Roles = "Admin,TM,TMProcess")]
        public ActionResult status(string id)
        {
            try
            {
                var ID = int.Parse(id);
                var model = dbcontext.business_trip_request.FirstOrDefault(m => m.ID == ID);
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
            var record = dbcontext.business_trip_request.FirstOrDefault(m => m.ID == model.opertion_id);
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
        public JsonResult Getdata(int id)
        {
            try
            {
             
                var emp_pos = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == id).Employee_Positions_Profile.FirstOrDefault(m=>m.Primary_Position==true);
                var ID =int.Parse(emp_pos.Default_location_descId);
                var loc = dbcontext.work_location.FirstOrDefault(m => m.ID == ID);
                var ob = new EMVM { unit= emp_pos.Organization_Chart.unit_Description,locatoin=loc.Name };
                return Json(ob);
            }
            catch(Exception)
            {
                return Json(false);
            }
        }
        public JsonResult Getrate(int id)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            var trip = dbcontext.Business_Trip.FirstOrDefault(m => m.ID == id);
            return Json(trip.alloeancereateberdaye);
        }

    }
    public class EMVM
    {
      public  string unit { get; set; }
        public string locatoin { get; set; }

    }
}