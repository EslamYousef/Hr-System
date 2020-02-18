using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HR.Models;
using System.Data.Entity.Infrastructure;
using HR.Models.Infra;
namespace HR.Controllers
{
    [Authorize]
    public class Subscription_SyndicateController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Subscription_Syndicate
        public ActionResult Index()
        {
            var model = dbcontext.Subscription_Syndicate.ToList();
            return View(model);
        }
        public ActionResult Create()
        {

            var o = dbcontext.Contact_methods.ToList().Select(m => new { Code = "" + m.Code + "-----[" + m.Name + ']', ID = m.ID }).ToList();
            ViewBag.Contact_methods = o;
            //if (o == null || o.Count() == 0)
            //{

            //    TempData["Message"] = "Don't Create Contact methods";
            //    var modelll = dbcontext.Subscription_Syndicate.ToList();
            //    return View("index", modelll);

            //}
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel);
            var model = dbcontext.Subscription_Syndicate.ToList();
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
            var modell = new Subscription_Syndicate { Subscription_Code = stru.Structure_Code + count };
            return View(modell);

        }
        [HttpPost]
        public ActionResult Create(Subscription_Syndicate model, string command)
        {
            try
            {
                ViewBag.Contact_methods = dbcontext.Contact_methods.ToList().Select(m => new { Code = "" + m.Code + "-----[" + m.Name + ']', ID = m.ID }).ToList();

                if (ModelState.IsValid)
                {
                    //if (model.Contact_methodsId == "0" || model.Contact_methodsId == null)
                    //{
                    //    ModelState.AddModelError("", "Contact Methods Code must enter");
                    //    return View(model);
                    //}
                    Subscription_Syndicate record = new Subscription_Syndicate();
                    record.Subscription_Code = model.Subscription_Code;
                    record.Server_Legatees = model.Server_Legatees;
                    record.Subscription_Description = model.Subscription_Description;

                    record.Subscription_Alternative_Description = model.Subscription_Alternative_Description;
                    record.Contact_Detail = model.Contact_Detail;
                    record.Default_Subscription_Fees = model.Default_Subscription_Fees;
                    if (model.Deduction_Period == 0)
                    {
                        TempData["Message"] = HR.Resource.Personnel.PleaseChoosefromTheDeductionPeriod;
                        return View(model);
                    }
                    if (model.Type == 0)
                    {
                        TempData["Message"] = HR.Resource.Personnel.PleaseChoosefromTheType;
                        return View(model);
                    }
                    else
                    {
                    record.Deduction_Period = model.Deduction_Period;
                    record.Type = model.Type;

                    record.Phone_1 = model.Phone_1;
                    record.Phone_2 = model.Phone_2;
                    record.Fax = model.Fax;
                    record.Email = model.Email;
                    record.Address = model.Address;

                    record.Contact_methodsId = model.Contact_methodsId;
                  //  var Contact_methodsId = int.Parse(model.Contact_methodsId);
                //    record.Contact_methods = dbcontext.Contact_methods.FirstOrDefault(m => m.ID == Contact_methodsId);
                    }
                   
                    dbcontext.Subscription_Syndicate.Add(record);
                    dbcontext.SaveChanges();
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
                ViewBag.Contact_methods = dbcontext.Contact_methods.ToList().Select(m => new { Code = "" + m.Code + "-----[" + m.Name + ']', ID = m.ID }).ToList();
                var record = dbcontext.Subscription_Syndicate.FirstOrDefault(m => m.ID == id);
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
        public ActionResult Edit(Subscription_Syndicate model)
        {
            try
            {
                ViewBag.Contact_methods = dbcontext.Contact_methods.ToList().Select(m => new { Code = "" + m.Code + "-----[" + m.Name + ']', ID = m.ID }).ToList();
                //if (model.Contact_methodsId == "0" || model.Contact_methodsId == null)
                //{
                //    ModelState.AddModelError("", "Contact methods Code must enter");
                //    return View(model);
                //}
                var record = dbcontext.Subscription_Syndicate.FirstOrDefault(m => m.ID == model.ID);
                record.Subscription_Code = model.Subscription_Code;
                record.Server_Legatees = model.Server_Legatees;
                record.Subscription_Description = model.Subscription_Description;

                record.Subscription_Alternative_Description = model.Subscription_Alternative_Description;
                record.Contact_Detail = model.Contact_Detail;
                record.Default_Subscription_Fees = model.Default_Subscription_Fees;
                if (model.Deduction_Period == 0)
                {
                    TempData["Message"] = HR.Resource.Personnel.PleaseChoosefromTheDeductionPeriod;
                    return View(model);
                }
                if (model.Type == 0)
                {
                    TempData["Message"] = HR.Resource.Personnel.PleaseChoosefromTheType;
                    return View(model);
                }
                else
                {
                    record.Deduction_Period = model.Deduction_Period;
                    record.Type = model.Type;

                    record.Phone_1 = model.Phone_1;
                    record.Phone_2 = model.Phone_2;
                    record.Fax = model.Fax;
                    record.Email = model.Email;
                    record.Address = model.Address;

                    record.Contact_methodsId = model.Contact_methodsId;
                //    var Contact_methodsId = int.Parse(model.Contact_methodsId);
              //      record.Contact_methods = dbcontext.Contact_methods.FirstOrDefault(m => m.ID == Contact_methodsId);
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
                var record = dbcontext.Subscription_Syndicate.FirstOrDefault(m => m.ID == id);
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
            var record = dbcontext.Subscription_Syndicate.FirstOrDefault(m => m.ID == id);

            try
            {
                dbcontext.Subscription_Syndicate.Remove(record);
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

    