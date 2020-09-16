using HR.Models;
using HR.Models.Infra;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers
{
    [Authorize]
    public class ExchangeRateController : BaseController
    {

        ApplicationDbContext dbcontext = new ApplicationDbContext();

        public ActionResult Index()
        {
            var model = dbcontext.Exchange_Rate.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            ViewBag.country = dbcontext.Currency.ToList().Select(m=>new { Code=m.Code+"------["+m.Name+']',ID=m.ID});
            var model = new Exchange_Rate() { Year = 1990,months=new List<months>() };
            DateTime now = DateTime.Now;
            for (int i = 0; i < 12; i++)
            {
                model.months.Add(new months
                {
                    Name = now.ToString("MMM"),
                    value = 0
                });
                now = now.AddMonths(1);                                    
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(Exchange_Rate model)
        {
            try
            {
                ViewBag.country = dbcontext.Currency.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID }) ;

                if (ModelState.IsValid)
                {

                    Exchange_Rate record = new Exchange_Rate();
                    record.CurrencyID = model.CurrencyID;
                    record.Currency = dbcontext.Currency.FirstOrDefault(m => m.ID == model.CurrencyID);
                    record.Year = model.Year;
                    record.months = model.months;
                    dbcontext.Exchange_Rate.Add(record);
                    dbcontext.SaveChanges();
                    //=================================check for alert==================================

                    var get_result_check = HR.Controllers.check.check_alert("exchange rate", HR.Models.user.Action.Create, HR.Models.user.type_field.form);
                    if (get_result_check != null)
                    {
                        var inbox = new Models.user.Alert_inbox { send_from_user_id = User.Identity.Name, send_to_user_id = get_result_check.send_to_ID_user, title = get_result_check.Subject, Subject = get_result_check.Message };
                        if (get_result_check.until != null)
                        {
                            if (get_result_check.until.Value.Year != 0001)
                            {
                                inbox.until = get_result_check.until;
                            }
                        }
                        ApplicationDbContext dbcontext = new ApplicationDbContext();
                        dbcontext.Alert_inbox.Add(inbox);
                        dbcontext.SaveChanges();

                    }
                    //===================================================================================
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(model);
                }
            }
            catch (DbUpdateException)
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
                ViewBag.country = dbcontext.Currency.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID }) ;

                var record = dbcontext.Exchange_Rate.FirstOrDefault(m => m.ID == id);
                if (record != null)
                { return View(record); }
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
        public ActionResult Edit(Exchange_Rate model)
        {
            try
            {
                ViewBag.country = dbcontext.Currency.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });

                var record = dbcontext.Exchange_Rate.FirstOrDefault(m => m.ID == model.ID);
                record.Year = model.Year;
                record.CurrencyID = model.CurrencyID;
                record.Currency = dbcontext.Currency.FirstOrDefault(m => m.ID == model.CurrencyID);
                for(var i=0;i<12;i++ )
                {
                    record.months[i].value = model.months[i].value;
                }
                //record.months = model.months;
                dbcontext.SaveChanges();
                //=================================check for alert==================================

                var get_result_check = HR.Controllers.check.check_alert("exchange rate", HR.Models.user.Action.edit, HR.Models.user.type_field.form);
                if (get_result_check != null)
                {
                    var inbox = new Models.user.Alert_inbox { send_from_user_id = User.Identity.Name, send_to_user_id = get_result_check.send_to_ID_user, title = get_result_check.Subject, Subject = get_result_check.Message };
                    if (get_result_check.until != null)
                    {
                        if (get_result_check.until.Value.Year != 0001)
                        {
                            inbox.until = get_result_check.until;
                        }
                    }
                    ApplicationDbContext dbcontext = new ApplicationDbContext();
                    dbcontext.Alert_inbox.Add(inbox);
                    dbcontext.SaveChanges();

                }
                //===================================================================================
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
                var record = dbcontext.Exchange_Rate.FirstOrDefault(m => m.ID == id);
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
            var record = dbcontext.Exchange_Rate.FirstOrDefault(m => m.ID == id);

            try
            {
                dbcontext.months.RemoveRange(record.months);
                dbcontext.Exchange_Rate.Remove(record);
                dbcontext.SaveChanges();
                //=================================check for alert==================================

                var get_result_check = HR.Controllers.check.check_alert("exchange rate", HR.Models.user.Action.delete, HR.Models.user.type_field.form);
                if (get_result_check != null)
                {
                    var inbox = new Models.user.Alert_inbox { send_from_user_id = User.Identity.Name, send_to_user_id = get_result_check.send_to_ID_user, title = get_result_check.Subject, Subject = get_result_check.Message };
                    if (get_result_check.until != null)
                    {
                        if (get_result_check.until.Value.Year != 0001)
                        {
                            inbox.until = get_result_check.until;
                        }
                    }
                    ApplicationDbContext dbcontext = new ApplicationDbContext();
                    dbcontext.Alert_inbox.Add(inbox);
                    dbcontext.SaveChanges();

                }
                //===================================================================================
                return RedirectToAction("index");
            }
            catch (DbUpdateException e)
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