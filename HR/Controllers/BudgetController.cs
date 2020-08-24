using HR.Models;
using HR.Models.Infra;
using HR.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers
{
    [Authorize(Roles = "Admin,Organization,OrganizationCards,Budget_card")]
    public class BudgetController : BaseController
    {
        // GET: Budget
        ApplicationDbContext dbcontext = new ApplicationDbContext();
   

        public ActionResult create()
        {
            try
            {
                ViewBag.items = dbcontext.Budget_class_items.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']' , ID = m.ID });
                ViewBag.sign = dbcontext.Currency.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']'+m.symbol, ID = m.ID }); 
                ViewBag.organization = dbcontext.Organization_Chart.ToList().Select(m => new { Code = m.Code + "------[" + m.unit_Description + ']', ID = m.ID });
                var model = new Budget();
                model.budget_type = budget_type.Operating;
                model.Year_From = 1990;
                model.Year_To = 1991;
                model.Currency_rate = 0.0;
                model.amount_native = 0.0;
                model.ammount_forigne = 0.0;
                model.Remaining_forgin = 0.0;
                model.Remaining_native = 0.0;


                //////
             
                var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Organization).Structure_Code;
                var modell = dbcontext.Budget.ToList();
                if (modell.Count() == 0)
                {
                    model.Code = stru + "1";
                }
                else
                {
                    model.Code = stru + (modell.LastOrDefault().ID + 1).ToString();
                }
                /////
                var st = new status { statu = check_status.created ,created_bydate=DateTime.Now,return_to_reviewdate=DateTime.Now,approved_bydate=DateTime.Now,Rejected_bydate=DateTime.Now,cancaled_bydate=DateTime.Now};
                var reco = new budgetVM { status = st, justification = new justification(), Budget = model };
                return View(reco);
            }
            catch(Exception e)
            {
                return View();
            }
        }

        [HttpPost]

        public ActionResult create(budgetVM model,FormCollection Form)
        {
            try
            {
                ViewBag.items = dbcontext.Budget_class_items.ToList();
                ViewBag.sign = dbcontext.Currency.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']' + m.symbol, ID = m.ID });
                ViewBag.organization = dbcontext.Organization_Chart.ToList().Select(m => new { Code = m.Code + "------[" + m.unit_Description + ']', ID = m.ID });
                if((model.Budget.Year_From+1)!=model.Budget.Year_To)
                {
                    TempData["Message"] = HR.Resource.organ.Differencebetweenyearsmustbeoneyear;
                    return View(model);
                }
                var ID = int.Parse(model.Budget.Organization_unitid);
                model.Budget.organization_unit = dbcontext.Organization_Chart.FirstOrDefault(m => m.ID == ID);
                var budget = new Budget();
                var status = new status();
                var just = new justification();
                budget = model.Budget;

                var item = Form["item"].Split(char.Parse(","));
                var AmountN = Form["AmountN"].Split(char.Parse(","));
                var AmountF = Form["AmountF"].Split(char.Parse(","));
                var commentt = Form["commentt"].Split(char.Parse(","));
                var ss = new List<Budget_details>();
                var ssf = new List<int>();
                for (var iii = 0; iii < item.Count(); iii++)
                {

                    if (item[iii] != "" && AmountN[iii] != "" && AmountF[iii] != "")
                    {
                        var IDf = int.Parse(item[iii]);
                        var le = dbcontext.Budget_class_items.FirstOrDefault(m => m.ID == IDf);

                        var item_de = new Budget_details
                        {
                            Budget_class_items = le,amount_forign= float.Parse(AmountF[iii]),amount_native= float.Parse(AmountN[iii]),comment= commentt[iii],sign_forgin=model.Budget.sign_forign,sign_native=model.Budget.sign_native
                            
                        };
                       
                        var sss = dbcontext.Budget_details.Add(item_de);
                        dbcontext.SaveChanges();
                        ssf.Add(sss.ID);
                        ss.Add(sss);
                    }
                }
                //if (model.Budget.sign_native != null && model.Budget.sign_forign != null)
                //{
                //    var IDSigN = int.Parse(model.Budget.sign_native);
                //    budget.sign_native = dbcontext.Currency.FirstOrDefault(m => m.ID == IDSigN).symbol;
                //    var IDSignF = int.Parse(model.Budget.sign_forign);
                //    budget.sign_forign = dbcontext.Currency.FirstOrDefault(m => m.ID == IDSignF).symbol;
                //}
                status = model.status;
                status.Type = Models.Infra.Type.Budget;
                just = model.justification;
                var s= dbcontext.status.Add(status);
                var j= dbcontext.justification.Add(just);
                dbcontext.SaveChanges();
                budget.justification = j;
                budget.status = s;
                budget.Budget_details = ss;
                budget.Budget_detailsID = ssf;
                dbcontext.Budget.Add(budget);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (Exception e)
            {
                return View(model);
            }
        }


        public ActionResult edit( string id)
        {
            try
            {

                var ID = int.Parse(id);
                var model = dbcontext.Budget.FirstOrDefault(m => m.ID == ID);
                model.Budget_detailsID = new List<int>();
                foreach (var item in model.Budget_details)
                {
                    model.Budget_detailsID.Add(item.ID);
                }
                var sta = model.status;
                var jus = model.justification;
                var record = new budgetVM { Budget=model,status=sta,justification=jus};
                ViewBag.items = dbcontext.Budget_class_items.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.sign = dbcontext.Currency.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']' + m.symbol, ID = m.ID });
                ViewBag.organization = dbcontext.Organization_Chart.ToList().Select(m => new { Code = m.Code + "------[" + m.unit_Description + ']', ID = m.ID });
                return View(record);
            }
            catch (Exception e)
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult edit(budgetVM model,FormCollection Form)
        {
            try
            {
                var newbudget = dbcontext.Budget.FirstOrDefault(m => m.ID == model.Budget.ID);
                var  status= dbcontext.status.FirstOrDefault(m => m.ID == model.status.ID);
                var  just= dbcontext.justification.FirstOrDefault(m => m.ID == model.justification.ID);

                ViewBag.items = dbcontext.Budget_class_items.ToList();
                ViewBag.sign = dbcontext.Currency.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']' + m.symbol, ID = m.ID });
                ViewBag.organization = dbcontext.Organization_Chart.ToList().Select(m => new { Code = m.Code + "------[" + m.unit_Description + ']', ID = m.ID });
               
                if (model.Budget.Budget_detailsID != null)
                {
                    for (var itemm=0;itemm < model.Budget.Budget_detailsID.Count();itemm++)
                    {
                        var idd = model.Budget.Budget_detailsID[itemm];
                        var t = dbcontext.Budget_details.FirstOrDefault(m => m.ID ==idd );
                        model.Budget.Budget_details[itemm]=t;
                    }
                }
                if ((model.Budget.Year_From + 1) != model.Budget.Year_To)
                {
                    TempData["Message"] = HR.Resource.organ.Differencebetweenyearsmustbeoneyear;
                    return View(model);
                }
                /////////////////////////
                var ID = int.Parse(model.Budget.Organization_unitid);
                newbudget.organization_unit = dbcontext.Organization_Chart.FirstOrDefault(m => m.ID == ID);
                ///////////////////////////
                //if (model.Budget.sign_native != null && model.Budget.sign_forign != null)
                //{
                //    var IDSigN = int.Parse(model.Budget.sign_native);
                //    newbudget.sign_native = dbcontext.Currency.FirstOrDefault(m => m.ID == IDSigN).symbol;
                //    var IDSignF = int.Parse(model.Budget.sign_forign);
                //    newbudget.sign_forign = dbcontext.Currency.FirstOrDefault(m => m.ID == IDSignF).symbol;
                //}
                /////////////////////////
                ///////////////////////////////
                //////////////////////////////
                var old_details = model.Budget.Budget_detailsID;
                newbudget.Budget_details = null;
                newbudget.Budget_detailsID = null;
                if (old_details != null)
                {
                    foreach (var iitem in old_details)
                    {
                        var de = dbcontext.Budget_details.FirstOrDefault(m => m.ID == iitem);
                        dbcontext.Budget_details.Remove(de);
                        dbcontext.SaveChanges();
                    }
                }
                var item = Form["item"].Split(char.Parse(","));
                var AmountN = Form["AmountN"].Split(char.Parse(","));
                var AmountF = Form["AmountF"].Split(char.Parse(","));
                var commentt = Form["commentt"].Split(char.Parse(","));
                var ss = new List<Budget_details>();
                var ssf = new List<int>();
                for (var iii = 0; iii < item.Count(); iii++)
                {

                    if (item[iii] != "" && AmountN[iii] != "" && AmountF[iii] != "")
                    {
                        var IDf = int.Parse(item[iii]);
                        var le = dbcontext.Budget_class_items.FirstOrDefault(m => m.ID == IDf);

                        var item_de = new Budget_details
                        {
                            Budget_class_items = le,
                            amount_forign = float.Parse(AmountF[iii]),
                            amount_native = float.Parse(AmountN[iii]),
                            comment = commentt[iii],
                            sign_forgin = model.Budget.sign_forign,
                            sign_native = model.Budget.sign_native

                        };

                        var sss = dbcontext.Budget_details.Add(item_de);
                        dbcontext.SaveChanges();
                        ssf.Add(sss.ID);
                        ss.Add(sss);
                    }
                }
                // newbudget = model.Budget;
                newbudget.Code = model.Budget.Code;
                newbudget.Organization_unitid = model.Budget.Organization_unitid;
                newbudget.sign_forign = model.Budget.sign_forign;
                newbudget.sign_native = model.Budget.sign_native;
                newbudget.Year_From = model.Budget.Year_From;
                newbudget.Year_To = model.Budget.Year_To;
                newbudget.amount_native = model.Budget.amount_native;
                newbudget.ammount_forigne = model.Budget.ammount_forigne;
                newbudget.Remaining_native = model.Budget.Remaining_native;
                newbudget.Remaining_forgin = model.Budget.Remaining_forgin;
                newbudget.Budget_details = ss;
                newbudget.Budget_detailsID = ssf;
                newbudget.Currency_rate = model.Budget.Currency_rate;
                dbcontext.SaveChanges();
                status.approved_by = model.status.approved_by;
                status.approved_bydate = model.status.approved_bydate;
                status.cancaled_by = model.status.cancaled_by;
                status.cancaled_bydate = model.status.cancaled_bydate;
                status.created_by = model.status.created_by;
                status.created_bydate = model.status.created_bydate;
                status.Rejected_by = model.status.Rejected_by;
                status.Rejected_bydate = model.status.Rejected_bydate;
                status.return_to_reviewby = model.status.return_to_reviewby;
                status.return_to_reviewdate = model.status.return_to_reviewdate;
                status.statu = model.status.statu;
                dbcontext.SaveChanges();
                just.justifiaction = model.justification.justifiaction;
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (Exception e)
            {
                return View(model);
            }
        }
        public JsonResult getorg(string id)
        {
            var ID = int.Parse(id);
            var model = dbcontext.Organization_Chart.FirstOrDefault(m => m.ID == ID);
            return Json(model);
        }
        public JsonResult getcurrency(string id)
        {
            var ID = int.Parse(id);
            var model = dbcontext.Currency.FirstOrDefault(m => m.ID == ID);
            return Json(model);
        }
        public JsonResult items(string id)
        {
            var  ID = int.Parse(id);
                var model = dbcontext.Budget_class_items.FirstOrDefault(m =>  m.ID==ID );
                return Json(model);
            
     
        }

        public ActionResult index()
        {
            var model = dbcontext.Budget.ToList();
            return View(model);
                 
        }
        public ActionResult delete(string id)
        {
            var ID = int.Parse(id);
            var model = dbcontext.Budget.FirstOrDefault(m => m.ID == ID);
            return View(model);
        }
        [HttpPost]
        [ActionName("delete")]
        public ActionResult delete_method(string id)
        {
            var ID = int.Parse(id);
            var model = dbcontext.Budget.FirstOrDefault(m => m.ID == ID);
            var details = new List<int>();
            foreach (var item in model.Budget_details)
            {
                details.Add(item.ID);
            }
            try
            {
               
                var j = model.justification;
                var s = model.status;
                dbcontext.Budget.Remove(model);
                dbcontext.SaveChanges();
                foreach (var item in details)
                {
                    var d = dbcontext.Budget_details.FirstOrDefault(m => m.ID == item);
                    dbcontext.Budget_details.Remove(d);
                    dbcontext.SaveChanges();
                }
                dbcontext.status.Remove(s);
                dbcontext.justification.Remove(j);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (DbUpdateException e)
            {
                TempData["Message"] = HR.Resource.organ.youcannotdeletethisRow;
                return View(model);
            }
            catch (Exception e)
            {
                return View();
            }
        }
    }
}