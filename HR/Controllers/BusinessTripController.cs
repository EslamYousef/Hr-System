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
    [Authorize(Roles = "Admin,TM,TMSetup")]
    public class BusinessTripController : BaseController
    {
        // GET: BusinessTrip
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        public ActionResult Index()
        {
            var model = dbcontext.Business_Trip.ToList();
            return View(model);
        }
        public ActionResult Create()
        {
            try
            {
                ViewBag.shift = dbcontext.Shift_setup.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.trans = dbcontext.TransportationMethod.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.salary_code = dbcontext.salary_code.ToList().Select(m => new { Code = m.SalaryCodeID + "------[" + m.SalaryCodeDesc + ']', ID = m.ID });
                ViewBag.ManualPaymentTypes_Header = dbcontext.ManualPaymentTypes_Header.ToList().Select(m => new { Code = m.PaymentTypeCode + "------[" + m.PaymentTypeDesc + ']', ID = m.ID });

                var record = new Business_Trip { linkedtomnothelypayroll = true };
                var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel).Structure_Code;
                var model = dbcontext.Business_Trip.ToList();
                if (model.Count() == 0)
                {
                    record.Code = stru + "1";
                }
                else
                {
                    record.Code = stru + (model.LastOrDefault().ID + 1).ToString();
                }
                return View(record);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult Create(Business_Trip model)
        {
            try
            {
                ViewBag.shift = dbcontext.Shift_setup.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.trans = dbcontext.TransportationMethod.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.salary_code = dbcontext.salary_code.ToList().Select(m => new { Code = m.SalaryCodeID + "------[" + m.SalaryCodeDesc + ']', ID = m.ID });
                ViewBag.ManualPaymentTypes_Header = dbcontext.ManualPaymentTypes_Header.ToList().Select(m => new { Code = m.PaymentTypeCode + "------[" + m.PaymentTypeDesc + ']', ID = m.ID });

                if (model.transporation_return_ID != null)
                    model.transporation_return_name = dbcontext.TransportationMethod.FirstOrDefault(m => m.ID == model.transporation_return_ID).Name;
                else
                    model.transporation_return_name = "";
                dbcontext.Business_Trip.Add(model);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (Exception)
            {
                return View(model);
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                ViewBag.shift = dbcontext.Shift_setup.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.trans = dbcontext.TransportationMethod.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.salary_code = dbcontext.salary_code.ToList().Select(m => new { Code = m.SalaryCodeID + "------[" + m.SalaryCodeDesc + ']', ID = m.ID });
                ViewBag.ManualPaymentTypes_Header = dbcontext.ManualPaymentTypes_Header.ToList().Select(m => new { Code = m.PaymentTypeCode + "------[" + m.PaymentTypeDesc + ']', ID = m.ID });

                var model = dbcontext.Business_Trip.FirstOrDefault(m => m.ID == id);
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult Edit(Business_Trip model)
        {
            try
            {
                ViewBag.shift = dbcontext.Shift_setup.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.trans = dbcontext.TransportationMethod.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.salary_code = dbcontext.salary_code.ToList().Select(m => new { Code = m.SalaryCodeID + "------[" + m.SalaryCodeDesc + ']', ID = m.ID });
                ViewBag.ManualPaymentTypes_Header = dbcontext.ManualPaymentTypes_Header.ToList().Select(m => new { Code = m.PaymentTypeCode + "------[" + m.PaymentTypeDesc + ']', ID = m.ID });

                var record = dbcontext.Business_Trip.FirstOrDefault(m => m.ID == model.ID);
                record.Name = model.Name;
                record.Description = model.Description;
                record.alloeancereateberdaye = model.alloeancereateberdaye;
                record.ShiftdaystatusID = model.ShiftdaystatusID;
                record.TransportationMethodID = model.TransportationMethodID;
                record.transporation_return_ID = model.transporation_return_ID;
                record.linkedtomanyalpayment = model.linkedtomanyalpayment;
                record.linkedtomnothelypayroll = model.linkedtomnothelypayroll;
                record.Salarycode = model.Salarycode;
                record.Manualpaymenttypecode = model.Manualpaymenttypecode;
                if (model.transporation_return_ID != null)
                    record.transporation_return_name = dbcontext.TransportationMethod.FirstOrDefault(m => m.ID == model.transporation_return_ID).Name;
                else
                    record.transporation_return_name = "";
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
                var record = dbcontext.Business_Trip.FirstOrDefault(m => m.ID == id);
                return View(record);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        [ActionName("delete")]
        public ActionResult delete_method(int id)
        {
            var record = dbcontext.Business_Trip.FirstOrDefault(m => m.ID == id);

            try
            {
                dbcontext.Business_Trip.Remove(record);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (Exception)
            {
                return View(record);
            }
        }
    }
}