using HR.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static HR.Controllers.infra;

namespace HR.Controllers
{
    public class PayrollGeneralSetupController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: PayrollGeneralSetup
        public ActionResult Index()
        {
            var model = dbcontext.PayrollGeneralSetup.ToList();
            return View(model);
        }
        public ActionResult create()
        {

            try
            {
                ViewBag.defaultpayrollperiod = dbcontext.PayrollPeriodSetup.ToList().Select(m => new { Code = m.PeriodCode + "-[" + m.PeriodDesc + ']', ID = m.ID }).ToList();
                ViewBag.accountnumberforaccruedasallery = dbcontext.GL_AccountSetup.ToList().Select(m => new { Code = m.Account + "-[" + m.AccountName + ']', ID = m.ID }).ToList();
                ViewBag.defaultaccountnumberforaccruedpayment = dbcontext.GL_AccountSetup.ToList().Select(m => new { Code = m.Account + "-[" + m.AccountName + ']', ID = m.ID }).ToList();
                ViewBag.checktype = dbcontext.Checktype.ToList().Select(m => new { Code = m.Code + "-[" + m.Description + ']', ID = m.ID }).ToList();
                ViewBag.deathdaysubscriptioncode = dbcontext.Subscription_Syndicate.ToList().Select(m => new { Code = m.Subscription_Code + "-[" + m.Subscription_Description + ']', ID = m.ID }).ToList();
                ViewBag.Basicsallarycode = dbcontext.salary_code.Where(m => m.SourceEntry == 1 || m.SourceEntry == 2).Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + "-]", ID = m.ID }).ToList(); ///contract and transaction
                ViewBag.totalexcludedallwencecode = dbcontext.salary_code.Where(m => m.SourceEntry == 1 || m.SourceEntry == 2).Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + "-]", ID = m.ID }).ToList();   ///contract and transaction
                ViewBag.earnedsallarycode = dbcontext.salary_code.Where(m => m.CodeGroupType == 1).Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + "-]", ID = m.ID }).ToList();      ///earned money type
                ViewBag.deductedsallerycode = dbcontext.salary_code.Where(m => m.CodeGroupType == 2).Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + "-]", ID = m.ID }).ToList();    ///deducted money type
                ViewBag.Deathdaysallarycode = dbcontext.salary_code.Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + "-]", ID = m.ID }).ToList();

                var recod = new PayrollGeneralSetup {AllowToRounding=false, Length_Of_Segment=0,Number_Of_Account_Segments=0,Value=0,Rest_On_The_First_Punishment=true};
                var model = new PayrollGeneralSetupVM {PayrollGeneralSetup=recod, Rounding_method=new Rounding_method(),ERP_INTERGRATION_TYPE=new ERP_INTERGRATION_TYPE(),GL_cost_center_distribution_behavior=new GL_cost_center_distribution_behavior()};
                return View(model);
            }
            catch(Exception e)
            {
                return RedirectToAction("index");
            }

        }
        [HttpPost]
        public ActionResult create(PayrollGeneralSetupVM model,FormCollection form)
        {
            try
            {

                ViewBag.defaultpayrollperiod = dbcontext.PayrollPeriodSetup.ToList().Select(m => new { Code = m.PeriodCode + "-[" + m.PeriodDesc + ']', ID = m.ID });
                ViewBag.accountnumberforaccruedasallery = dbcontext.GL_AccountSetup.ToList().Select(m => new { Code = m.Account + "-[" + m.AccountName + ']', ID = m.ID });
                ViewBag.defaultaccountnumberforaccruedpayment = dbcontext.GL_AccountSetup.ToList().Select(m => new { Code = m.Account + "-[" + m.AccountName + ']', ID = m.ID });
                ViewBag.checktype = dbcontext.Checktype.ToList().Select(m => new { Code = m.Code + "-[" + m.Description + ']', ID = m.ID });
                ViewBag.deathdaysubscriptioncode = dbcontext.Subscription_Syndicate.ToList().Select(m => new { Code = m.Subscription_Code + "-[" + m.Subscription_Description + ']', ID = m.ID });
                ViewBag.Basicsallarycode = dbcontext.salary_code.Where(m => m.SourceEntry == 1 || m.SourceEntry == 2).Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + "-]", ID = m.ID }).ToList(); ///contract and transaction
                ViewBag.totalexcludedallwencecode = dbcontext.salary_code.Where(m => m.SourceEntry == 1 || m.SourceEntry == 2).Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + "-]", ID = m.ID }).ToList();   ///contract and transaction
                ViewBag.earnedsallarycode = dbcontext.salary_code.Where(m => m.CodeGroupType == 1).Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + "-]", ID = m.ID }).ToList();      ///earned money type
                ViewBag.deductedsallerycode = dbcontext.salary_code.Where(m => m.CodeGroupType == 2).Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + "-]", ID = m.ID }).ToList();    ///deducted money type
                ViewBag.Deathdaysallarycode = dbcontext.salary_code.Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + "-]", ID = m.ID }).ToList();

                var new_model = model.PayrollGeneralSetup;
                var allow = form["check_A"].Split(',');
                var PUNI = form["check_B"].Split(',');

                if (allow.Length == 1)
                {
                    new_model.AllowToRounding = false;
                }
                else
                {
                    new_model.AllowToRounding = true;
                }
                if (PUNI[0] == "first")
                {
                    new_model.Rest_On_The_First_Punishment = true;
                    new_model.Rest_On_The_Last_Punishment = false;
                }
                else
                {
                    new_model.Rest_On_The_First_Punishment = false;
                    new_model.Rest_On_The_Last_Punishment = true;
                }

                new_model.Rounding_Method = Convert.ToInt16(model.Rounding_method.GetHashCode());
                new_model.IntegrationType = Convert.ToInt16(model.ERP_INTERGRATION_TYPE.GetHashCode());
                new_model.GL_DistrbutionBehavior = Convert.ToInt16(model.GL_cost_center_distribution_behavior.GetHashCode());
                new_model.Created_By = User.Identity.Name;
                new_model.Created_Date = DateTime.Now.Date;

                new_model.SetupKey = 0;
                var saved=dbcontext.PayrollGeneralSetup.Add(new_model);
                dbcontext.SaveChanges();
                saved.SetupKey = saved.ID;
                dbcontext.SaveChanges();
                return RedirectToAction("index");

            }
            catch(Exception e)
            {
                return View(model);
            }
        }
        public ActionResult edit(int id)
        {
            try
            {
                ViewBag.defaultpayrollperiod = dbcontext.PayrollPeriodSetup.ToList().Select(m => new { Code = m.PeriodCode + "-[" + m.PeriodDesc + ']', ID = m.ID });
                ViewBag.accountnumberforaccruedasallery = dbcontext.GL_AccountSetup.ToList().Select(m => new { Code = m.Account + "-[" + m.AccountName + ']', ID = m.ID });
                ViewBag.defaultaccountnumberforaccruedpayment = dbcontext.GL_AccountSetup.ToList().Select(m => new { Code = m.Account + "-[" + m.AccountName + ']', ID = m.ID });
                ViewBag.checktype = dbcontext.Checktype.ToList().Select(m => new { Code = m.Code + "-[" + m.Description + ']', ID = m.ID });
                ViewBag.deathdaysubscriptioncode = dbcontext.Subscription_Syndicate.ToList().Select(m => new { Code = m.Subscription_Code + "-[" + m.Subscription_Description + ']', ID = m.ID });
                ViewBag.Basicsallarycode = dbcontext.salary_code.Where(m => m.SourceEntry == 1 || m.SourceEntry == 2).Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + "-]", ID = m.ID }).ToList(); ///contract and transaction
                ViewBag.totalexcludedallwencecode = dbcontext.salary_code.Where(m => m.SourceEntry == 1 || m.SourceEntry == 2).Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + "-]", ID = m.ID }).ToList();   ///contract and transaction
                ViewBag.earnedsallarycode = dbcontext.salary_code.Where(m => m.CodeGroupType == 1).Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + "-]", ID = m.ID }).ToList();      ///earned money type
                ViewBag.deductedsallerycode = dbcontext.salary_code.Where(m => m.CodeGroupType == 2).Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + "-]", ID = m.ID }).ToList();    ///deducted money type
                ViewBag.Deathdaysallarycode = dbcontext.salary_code.Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + "-]", ID = m.ID }).ToList();


                var rec = dbcontext.PayrollGeneralSetup.FirstOrDefault(m => m.ID == id);

                var model = new PayrollGeneralSetupVM { PayrollGeneralSetup = rec, Rounding_method = (Rounding_method)rec.Rounding_Method , ERP_INTERGRATION_TYPE =  (ERP_INTERGRATION_TYPE)rec.IntegrationType, GL_cost_center_distribution_behavior =  (GL_cost_center_distribution_behavior)rec.GL_DistrbutionBehavior };
                return View(model);
            }
            catch(Exception e)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult edit(PayrollGeneralSetupVM model, FormCollection form)
        {
            ViewBag.defaultpayrollperiod = dbcontext.PayrollPeriodSetup.ToList().Select(m => new { Code = m.PeriodCode + "-[" + m.PeriodDesc + ']', ID = m.ID });
            ViewBag.accountnumberforaccruedasallery = dbcontext.GL_AccountSetup.ToList().Select(m => new { Code = m.Account + "-[" + m.AccountName + ']', ID = m.ID });
            ViewBag.defaultaccountnumberforaccruedpayment = dbcontext.GL_AccountSetup.ToList().Select(m => new { Code = m.Account + "-[" + m.AccountName + ']', ID = m.ID });
            ViewBag.checktype = dbcontext.Checktype.ToList().Select(m => new { Code = m.Code + "-[" + m.Description + ']', ID = m.ID });
            ViewBag.deathdaysubscriptioncode = dbcontext.Subscription_Syndicate.ToList().Select(m => new { Code = m.Subscription_Code + "-[" + m.Subscription_Description + ']', ID = m.ID });
            ViewBag.Basicsallarycode = dbcontext.salary_code.Where(m => m.SourceEntry == 1 || m.SourceEntry == 2).Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + "-]", ID = m.ID }).ToList(); ///contract and transaction
            ViewBag.totalexcludedallwencecode = dbcontext.salary_code.Where(m => m.SourceEntry == 1 || m.SourceEntry == 2).Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + "-]", ID = m.ID }).ToList();   ///contract and transaction
            ViewBag.earnedsallarycode = dbcontext.salary_code.Where(m => m.CodeGroupType == 1).Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + "-]", ID = m.ID }).ToList();      ///earned money type
            ViewBag.deductedsallerycode = dbcontext.salary_code.Where(m => m.CodeGroupType == 2).Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + "-]", ID = m.ID }).ToList();    ///deducted money type
            ViewBag.Deathdaysallarycode = dbcontext.salary_code.Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + "-]", ID = m.ID }).ToList();

            var record_edit = dbcontext.PayrollGeneralSetup.FirstOrDefault(m => m.ID == model.PayrollGeneralSetup.ID);

            //var new_model = model.PayrollGeneralSetup;
            var allow = form["check_A"].Split(',');
            var PUNI = form["check_B"].Split(',');

            if (allow.Length == 1)
            {
                record_edit.AllowToRounding = false;
            }
            else
            {
                record_edit.AllowToRounding = true;
            }
            if (PUNI[0] == "first")
            {
                record_edit.Rest_On_The_First_Punishment = true;
                record_edit.Rest_On_The_Last_Punishment = false;
            }
            else
            {
                record_edit.Rest_On_The_First_Punishment = false;
                record_edit.Rest_On_The_Last_Punishment = true;
            }

            record_edit.Rounding_Method = Convert.ToInt16(model.Rounding_method.GetHashCode());
            record_edit.IntegrationType = Convert.ToInt16(model.ERP_INTERGRATION_TYPE.GetHashCode());
            record_edit.GL_DistrbutionBehavior = Convert.ToInt16(model.GL_cost_center_distribution_behavior.GetHashCode());
            record_edit.Modified_By = User.Identity.Name;
            record_edit.Modified_Date = DateTime.Now.Date;
            record_edit.AccountNumberForNetSalary = model.PayrollGeneralSetup.AccountNumberForNetSalary;
            record_edit.DefaultAccountNumberForNetPayment = model.PayrollGeneralSetup.DefaultAccountNumberForNetPayment;
            record_edit.DefaultPayrollPeriod = model.PayrollGeneralSetup.DefaultPayrollPeriod;
            record_edit.GL_DistrbutionBehavior = model.PayrollGeneralSetup.GL_DistrbutionBehavior;
            record_edit.IntegrationType = model.PayrollGeneralSetup.IntegrationType;
            record_edit.Length_Of_Segment = model.PayrollGeneralSetup.Length_Of_Segment;
            record_edit.Number_Of_Account_Segments = model.PayrollGeneralSetup.Number_Of_Account_Segments;
            record_edit.SalaryCodeID_BasicSalary = model.PayrollGeneralSetup.SalaryCodeID_BasicSalary;
            record_edit.SalaryCodeID_D = model.PayrollGeneralSetup.SalaryCodeID_D;
            record_edit.SalaryCodeID_DeathDate = model.PayrollGeneralSetup.SalaryCodeID_DeathDate;
            record_edit.SalaryCodeID_E = model.PayrollGeneralSetup.SalaryCodeID_E;
            record_edit.SalaryCodeID_ExecludedAllwance = model.PayrollGeneralSetup.SalaryCodeID_ExecludedAllwance;
            record_edit.Segment_Number_Of_Costcenter = model.PayrollGeneralSetup.Segment_Number_Of_Costcenter;
            record_edit.Subscription_Code_DeathDate = model.PayrollGeneralSetup.Subscription_Code_DeathDate;
            record_edit.Type_Code = model.PayrollGeneralSetup.Type_Code;
            record_edit.Value = model.PayrollGeneralSetup.Value;
            
            dbcontext.SaveChanges();
            return RedirectToAction("index");

        }

        public ActionResult delete(int id)
        {
            try
            {
                var model = dbcontext.PayrollGeneralSetup.FirstOrDefault(m => m.ID == id);
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
            var model = dbcontext.PayrollGeneralSetup.FirstOrDefault(m => m.ID == id);
            try
            {
                dbcontext.PayrollGeneralSetup.Remove(model);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch(Exception)
            {
                return View(model);
            }
        }

    }
    public class infra
    {
        public enum GL_cost_center_distribution_behavior
        {
            [Display(Name = "transfer location/organization behavior")]
            transfer_location_organization_behavior=1,
            [Display(Name = "primary location/organization behavior")]
            primary_location_organization_behavior = 2,
        }
        public enum ERP_INTERGRATION_TYPE
        {
            [Display(Name = "Stand Alone")]
            stand_alone =1,
            [Display(Name = "Microsoft GP")]
            microsoft_GP =2,
            [Display(Name = "Microsoft AX2012")]
            microsoft_AX2012 =3,
            [Display(Name = "Microsoft AX2009")]
            microsoft_AX2009 =4,
        }
        public enum Rounding_method
        {
            None=1,
            Up=2,
            Down=3,
            Nearest=4

        }
    }
    public class PayrollGeneralSetupVM
    {
        public PayrollGeneralSetup PayrollGeneralSetup { get; set; }
        public GL_cost_center_distribution_behavior GL_cost_center_distribution_behavior { get; set; }
        public ERP_INTERGRATION_TYPE ERP_INTERGRATION_TYPE { get; set; }
        public Rounding_method Rounding_method { get; set; }
    }
}