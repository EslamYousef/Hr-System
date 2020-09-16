using HR.Models;
using HR.Models.CardPayroll;
using HR.Models.Infra;
using HR.Models.SetupPayroll;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers
{
    [Authorize(Roles = "Admin,payroll,payrollCards,salary item")]
    public class salary_codeController : BaseController
    {
        // GET: salary_code
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        public ActionResult Index()
        {
            var EN = Document_entry.Manual_payment.GetHashCode();
            var model = dbcontext.salary_code.Where(m => m.SourceEntry != EN).ToList();
            return View(model);
        }
        public ActionResult Create()
        {
            try
            {
                var new_record = new salary_code();
                new_record.SortingIndex = 0; new_record.FrequencyPerPeriod = 0; new_record.MinimumAmount = 0; new_record.MaximumAmount = 0;
                var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Payroll).Structure_Code;
                var model_ = dbcontext.salary_code.ToList();
                if (model_.Count() == 0)
                {
                    new_record.SalaryCodeID = stru + "1";
                }
                else
                {
                    new_record.SalaryCodeID = stru + (model_.LastOrDefault().ID + 1).ToString();
                }
                ViewBag.checktype = dbcontext.Checktype.ToList().Select(m => new { Code = m.Code + "-[" + m.Description + ']', ID = m.ID });
                ViewBag.extedned = dbcontext.ExtendedFields_Header.ToList().Select(m => new { Code = m.ExtendedFields_Code + "-[" + m.ExtendedFields_Desc + ']', ID = m.ID });
                ViewBag.Subscription_Syndicate = dbcontext.Subscription_Syndicate.ToList().Select(m => new { Code = m.Subscription_Code + "-[" + m.Subscription_Description + ']', ID = m.ID });
                ViewBag.debit = dbcontext.GL_AccountSetup.ToList().Select(m => new { Code = m.Account + "-[" + m.AccountName + ']', ID = m.ID });
                ViewBag.credit = dbcontext.GL_AccountSetup.ToList().Select(m => new { Code = m.Account + "-[" + m.AccountName + ']', ID = m.ID });
                ViewBag.formula = dbcontext.Formula_Header.ToList().Select(m => new { Code = m.FormulaCode + "-[" + m.FormulaDesc + ']', ID = m.FormulaCode });

                var vm = new salary_codeVM { salary_code = new_record, unit = new unit(), code_type = new code_type(), code_value_type = new code_value_type(), cost_center_type = new cost_center_type(), Document_entry = new Document_entry() };
                return View(vm);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult create(salary_codeVM model, FormCollection form, string Command)
        {
            try
            {
                ViewBag.checktype = dbcontext.Checktype.ToList().Select(m => new { Code = m.Code + "-[" + m.Description + ']', ID = m.ID });
                ViewBag.extedned = dbcontext.ExtendedFields_Header.ToList().Select(m => new { Code = m.ExtendedFields_Code + "-[" + m.ExtendedFields_Desc + ']', ID = m.ID });
                ViewBag.Subscription_Syndicate = dbcontext.Subscription_Syndicate.ToList().Select(m => new { Code = m.Subscription_Code + "-[" + m.Subscription_Description + ']', ID = m.ID });
                ViewBag.debit = dbcontext.GL_AccountSetup.ToList().Select(m => new { Code = m.Account + "-[" + m.AccountName + ']', ID = m.ID });
                ViewBag.credit = dbcontext.GL_AccountSetup.ToList().Select(m => new { Code = m.Account + "-[" + m.AccountName + ']', ID = m.ID });
                ViewBag.formula = dbcontext.Formula_Header.ToList().Select(m => new { Code = m.FormulaCode + "-[" + m.FormulaDesc + ']', ID = m.FormulaCode });

                var new_model = new salary_code();
                new_model = model.salary_code;
                new_model.CodeGroupType = model.code_type.GetHashCode();
                new_model.CodeValueType = model.code_value_type.GetHashCode();
                new_model.SourceEntry = model.Document_entry.GetHashCode();
                new_model.Costcenter_Type = (Int16)model.cost_center_type.GetHashCode();
                new_model.Created_By = User.Identity.Name;
                new_model.Created_Date = DateTime.Now.Date;

                var a1 = form["check_A"].Split(',');
                var a2 = form["check_c5"].Split(',');
                var a3 = form["check_c6"].Split(',');
                var a4 = form["check_c4"].Split(',');

                var a5 = form["check_c1"].Split(',');
                var a6 = form["check_c2"].Split(',');
                var a7 = form["check_c3"].Split(',');

                if (a5.Length == 1)
                {
                    new_model.PrintableInPayslip = false;
                    model.salary_code.PrintableInPayslip = false;
                }
                else
                {
                    new_model.PrintableInPayslip = true;
                    model.salary_code.PrintableInPayslip = true;
                }
                if (a6.Length == 1)
                {
                    new_model.AffectToNetAmount = false;
                    model.salary_code.AffectToNetAmount = false;
                }
                else
                {
                    new_model.AffectToNetAmount = true;
                    model.salary_code.AffectToNetAmount = true;
                }
                if (a7.Length == 1)
                {
                    new_model.AllowToUseInHRModules = false;
                    model.salary_code.AllowToUseInHRModules = false;
                }
                else
                {
                    new_model.AllowToUseInHRModules = true;
                    model.salary_code.AllowToUseInHRModules = true;
                }
                if (a2.Length == 2)
                {
                    model.salary_code.LinkedWithAccumulators = true;
                }
                if (a3.Length == 2)
                {
                    model.salary_code.ApplayRangeConstrains = true;
                }
                if (a4.Length == 2)
                {
                    model.salary_code.AllowToPostingItemToGL = true;
                }
                if (a1.Length == 2)
                {
                    model.salary_code.EnableExtendedFields = true;
                }
                if (a1.Length == 1)
                {
                    new_model.EnableExtendedFields = false;
                    new_model.ExtendedFields_Code = null;

                }
                else
                {
                    new_model.EnableExtendedFields = true;
                    if (model.salary_code.ExtendedFields_Code == null)
                    {

                        return View(model);
                    }
                    else
                    {
                        new_model.ExtendedFields_Code = model.salary_code.ExtendedFields_Code;
                    }

                }
                if (a2.Length == 1)
                {
                    new_model.LinkedWithAccumulators = false;
                    new_model.Subscription_Code = null;


                }
                else
                {
                    new_model.LinkedWithAccumulators = true;
                    if (model.salary_code.Subscription_Code == null)
                    {
                        new_model.Subscription_Code = null;

                    }
                    else
                    {
                        new_model.Subscription_Code = model.salary_code.Subscription_Code;
                    }
                }
                if (a3.Length == 1)
                {
                    new_model.ApplayRangeConstrains = false;
                    new_model.MinimumAmount = 0;
                    new_model.MaximumAmount = 0;
                }
                else
                {
                    new_model.ApplayRangeConstrains = true;
                    if (model.salary_code.MaximumAmount == null)
                    {
                        new_model.MaximumAmount = 0;
                    }
                    else
                    {
                        new_model.MaximumAmount = model.salary_code.MaximumAmount;

                    }
                    if (model.salary_code.MinimumAmount == null)
                    {
                        new_model.MinimumAmount = 0;
                    }
                    else
                    {
                        new_model.MinimumAmount = model.salary_code.MinimumAmount;

                    }

                }

                if (a4.Length == 1)
                {
                    new_model.AllowToPostingItemToGL = false;
                    new_model.DebitAccount = null;
                    new_model.CreditAccount = null;
                }
                else
                {


                    new_model.AllowToPostingItemToGL = true;
                    if (model.salary_code.DebitAccount == null || model.salary_code.CreditAccount == null)
                    {
                        return View(model);
                    }
                    else
                    {
                        new_model.DebitAccount = model.salary_code.DebitAccount;
                        new_model.CreditAccount = model.salary_code.CreditAccount;
                    }



                }


                dbcontext.salary_code.Add(new_model);
                dbcontext.SaveChanges();


                //=================================check for alert==================================
                var get_result_check = HR.Controllers.check.check_alert("Salary item card", HR.Models.user.Action.Create, HR.Models.user.type_field.form);
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
                if (Command == "assign")
                {
                    return RedirectToAction("EditAssign", "salary_code", new { id = model.salary_code.ID, code = model.salary_code.SalaryCodeID, name = model.salary_code.SalaryCodeDesc, codetype = model.code_type, codevaluetype = model.code_value_type });
                }

                //if (Command == "deleteitem")
                //{
                //    return RedirectToAction("DeleteAssign", "salary_code", new { id = model.salary_code.ID, code = model.salary_code.SalaryCodeID, name = model.salary_code.SalaryCodeDesc, codetype = model.code_type, codevaluetype = model.code_value_type });
                //}
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
                ViewBag.checktype = dbcontext.Checktype.ToList().Select(m => new { Code = m.Code + "-[" + m.Description + ']', ID = m.ID });
                ViewBag.extedned = dbcontext.ExtendedFields_Header.ToList().Select(m => new { Code = m.ExtendedFields_Code + "-[" + m.ExtendedFields_Desc + ']', ID = m.ID });
                ViewBag.Subscription_Syndicate = dbcontext.Subscription_Syndicate.ToList().Select(m => new { Code = m.Subscription_Code + "-[" + m.Subscription_Description + ']', ID = m.ID });
                ViewBag.debit = dbcontext.GL_AccountSetup.ToList().Select(m => new { Code = m.Account + "-[" + m.AccountName + ']', ID = m.ID });
                ViewBag.credit = dbcontext.GL_AccountSetup.ToList().Select(m => new { Code = m.Account + "-[" + m.AccountName + ']', ID = m.ID });
                ViewBag.formula = dbcontext.Formula_Header.ToList().Select(m => new { Code = m.FormulaCode + "-[" + m.FormulaDesc + ']', ID = m.FormulaCode });

                var model = dbcontext.salary_code.FirstOrDefault(m => m.ID == id);
                var new_model = new salary_codeVM { salary_code = model, code_type = (code_type)model.CodeGroupType, code_value_type = (code_value_type)model.CodeValueType, cost_center_type = (cost_center_type)model.Costcenter_Type, Document_entry = (Document_entry)model.SourceEntry };
                return View(new_model);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult edit(salary_codeVM model, FormCollection form, string Command)
        {
            try
            {
                ViewBag.checktype = dbcontext.Checktype.ToList().Select(m => new { Code = m.Code + "-[" + m.Description + ']', ID = m.ID });
                ViewBag.extedned = dbcontext.ExtendedFields_Header.ToList().Select(m => new { Code = m.ExtendedFields_Code + "-[" + m.ExtendedFields_Desc + ']', ID = m.ID });
                ViewBag.Subscription_Syndicate = dbcontext.Subscription_Syndicate.ToList().Select(m => new { Code = m.Subscription_Code + "-[" + m.Subscription_Description + ']', ID = m.ID });
                ViewBag.debit = dbcontext.GL_AccountSetup.ToList().Select(m => new { Code = m.Account + "-[" + m.AccountName + ']', ID = m.ID });
                ViewBag.credit = dbcontext.GL_AccountSetup.ToList().Select(m => new { Code = m.Account + "-[" + m.AccountName + ']', ID = m.ID });
                ViewBag.formula = dbcontext.Formula_Header.ToList().Select(m => new { Code = m.FormulaCode + "-[" + m.FormulaDesc + ']', ID = m.FormulaCode });

                var new_record = dbcontext.salary_code.FirstOrDefault(m => m.ID == model.salary_code.ID);

                new_record.CodeGroupType = model.code_type.GetHashCode();
                new_record.CodeValueType = model.code_value_type.GetHashCode();
                new_record.SourceEntry = model.Document_entry.GetHashCode();
                new_record.Costcenter_Type = (Int16)model.cost_center_type.GetHashCode();
                new_record.Modified_By = User.Identity.Name;
                new_record.Modified_Date = DateTime.Now.Date;

                new_record.SalaryCodeDesc = model.salary_code.SalaryCodeDesc;
                new_record.SalaryCodeAltDesc = model.salary_code.SalaryCodeAltDesc;
                new_record.Type_Code = model.salary_code.Type_Code;
                new_record.SortingIndex = model.salary_code.SortingIndex;
                new_record.FrequencyPerPeriod = model.salary_code.FrequencyPerPeriod;
                new_record.Description = model.salary_code.Description;
                new_record.FormulaCode = model.salary_code.FormulaCode;

                var a1 = form["check_A"].Split(',');
                var a2 = form["check_c5"].Split(',');
                var a3 = form["check_c6"].Split(',');
                var a4 = form["check_c4"].Split(',');

                var a5 = form["check_c1"].Split(',');
                var a6 = form["check_c2"].Split(',');
                var a7 = form["check_c3"].Split(',');

                if (a5.Length == 1)
                {
                    new_record.PrintableInPayslip = false;
                    model.salary_code.PrintableInPayslip = false;
                }
                else
                {
                    new_record.PrintableInPayslip = true;
                    model.salary_code.PrintableInPayslip = true;
                }
                if (a6.Length == 1)
                {
                    new_record.AffectToNetAmount = false;
                    model.salary_code.AffectToNetAmount = false;
                }
                else
                {
                    new_record.AffectToNetAmount = true;
                    model.salary_code.AffectToNetAmount = true;
                }
                if (a7.Length == 1)
                {
                    new_record.AllowToUseInHRModules = false;
                    model.salary_code.AllowToUseInHRModules = false;
                }
                else
                {
                    new_record.AllowToUseInHRModules = true;
                    model.salary_code.AllowToUseInHRModules = true;
                }
                if (a2.Length == 2)
                {
                    model.salary_code.LinkedWithAccumulators = true;
                }
                if (a3.Length == 2)
                {
                    model.salary_code.ApplayRangeConstrains = true;
                }
                if (a4.Length == 2)
                {
                    model.salary_code.AllowToPostingItemToGL = true;
                }
                if (a1.Length == 2)
                {
                    model.salary_code.EnableExtendedFields = true;
                }
                if (a1.Length == 1)
                {
                    new_record.EnableExtendedFields = false;
                    new_record.ExtendedFields_Code = null;

                }
                else
                {
                    new_record.EnableExtendedFields = true;
                    if (model.salary_code.ExtendedFields_Code == null)
                    {

                        return View(model);
                    }
                    else
                    {
                        new_record.ExtendedFields_Code = model.salary_code.ExtendedFields_Code;
                    }

                }
                if (a2.Length == 1)
                {
                    new_record.LinkedWithAccumulators = false;
                    new_record.Subscription_Code = null;


                }
                else
                {
                    new_record.LinkedWithAccumulators = true;
                    if (model.salary_code.Subscription_Code == null)
                    {
                        new_record.Subscription_Code = null;
                    }
                    else
                    {
                        new_record.Subscription_Code = model.salary_code.Subscription_Code;
                    }
                }
                if (a3.Length == 1)
                {
                    new_record.ApplayRangeConstrains = false;
                    new_record.MinimumAmount = 0;
                    new_record.MaximumAmount = 0;
                }
                else
                {
                    new_record.ApplayRangeConstrains = true;
                    if (model.salary_code.MaximumAmount == null)
                    {
                        new_record.MaximumAmount = 0;
                    }
                    else
                    {
                        new_record.MaximumAmount = model.salary_code.MaximumAmount;

                    }
                    if (model.salary_code.MinimumAmount == null)
                    {
                        new_record.MinimumAmount = 0;
                    }
                    else
                    {
                        new_record.MinimumAmount = model.salary_code.MinimumAmount;

                    }


                }

                if (a4.Length == 1)
                {
                    new_record.AllowToPostingItemToGL = false;
                    new_record.DebitAccount = null;
                    new_record.CreditAccount = null;
                }
                else
                {


                    new_record.AllowToPostingItemToGL = true;
                    if (model.salary_code.DebitAccount == null || model.salary_code.CreditAccount == null)
                    {
                        return View(model);
                    }
                    else
                    {
                        new_record.DebitAccount = model.salary_code.DebitAccount;
                        new_record.CreditAccount = model.salary_code.CreditAccount;
                    }



                }
                dbcontext.SaveChanges();
                //=================================check for alert==================================
                var get_result_check = HR.Controllers.check.check_alert("Salary item card", HR.Models.user.Action.edit, HR.Models.user.type_field.form);
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
                if (Command == "assign")
                {
                    return RedirectToAction("EditAssign", "salary_code", new { id = model.salary_code.ID, code = model.salary_code.SalaryCodeID, name = model.salary_code.SalaryCodeDesc, codetype = model.code_type, codevaluetype = model.code_value_type });
                }
                if (Command == "deleteitem")
                {
                    return RedirectToAction("DeleteAssign", "salary_code", new { id = model.salary_code.ID, code = model.salary_code.SalaryCodeID, name = model.salary_code.SalaryCodeDesc, codetype = model.code_type, codevaluetype = model.code_value_type });
                }
                return RedirectToAction("index");
            }
            catch (Exception)
            {
                return View(model);
            }
        }
        public ActionResult EditAssign(int id, string code, string name, string codetype, string codevaluetype)
        {

            try
            {
                ViewBag.checktype = dbcontext.Checktype.ToList().Select(m => new { Code = m.Code + "-[" + m.Description + ']', ID = m.ID });
                ViewBag.extedned = dbcontext.ExtendedFields_Header.ToList().Select(m => new { Code = m.ExtendedFields_Code + "-[" + m.ExtendedFields_Desc + ']', ID = m.ID });
                ViewBag.Subscription_Syndicate = dbcontext.Subscription_Syndicate.ToList().Select(m => new { Code = m.Subscription_Code + "-[" + m.Subscription_Description + ']', ID = m.ID });
                ViewBag.debit = dbcontext.GL_AccountSetup.ToList().Select(m => new { Code = m.Account + "-[" + m.AccountName + ']', ID = m.ID });
                ViewBag.credit = dbcontext.GL_AccountSetup.ToList().Select(m => new { Code = m.Account + "-[" + m.AccountName + ']', ID = m.ID });
                ViewBag.code = code;
                ViewBag.name = name;
                ViewBag.codetype = codetype;
                ViewBag.codevaluetype = codevaluetype;

                var model = dbcontext.salary_code.FirstOrDefault(m => m.ID == id);
                var new_model = new salary_codeVM {  code_type = (code_type)model.CodeGroupType, code_value_type = (code_value_type)model.CodeValueType, cost_center_type = (cost_center_type)model.Costcenter_Type, Document_entry = (Document_entry)model.SourceEntry };
                return View(new_model);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult EditAssign(salary_codeVM model, FormCollection form, string Command, string code, string name, string codetype, string codevaluetype)
        {
            try
            {
                ViewBag.checktype = dbcontext.Checktype.ToList().Select(m => new { Code = m.Code + "-[" + m.Description + ']', ID = m.ID });
                ViewBag.extedned = dbcontext.ExtendedFields_Header.ToList().Select(m => new { Code = m.ExtendedFields_Code + "-[" + m.ExtendedFields_Desc + ']', ID = m.ID });
                ViewBag.Subscription_Syndicate = dbcontext.Subscription_Syndicate.ToList().Select(m => new { Code = m.Subscription_Code + "-[" + m.Subscription_Description + ']', ID = m.ID });
                ViewBag.debit = dbcontext.GL_AccountSetup.ToList().Select(m => new { Code = m.Account + "-[" + m.AccountName + ']', ID = m.ID });
                ViewBag.credit = dbcontext.GL_AccountSetup.ToList().Select(m => new { Code = m.Account + "-[" + m.AccountName + ']', ID = m.ID });
                ViewBag.code = code;
                ViewBag.name = name;
                ViewBag.codetype = codetype;
                ViewBag.codevaluetype = codevaluetype;

                var list = dbcontext.Employee_Financial_Contract_Header.Where(a => a.IsActive == true).ToList();
                var lists = dbcontext.Employee_Financial_Contract_Header.ToList();
                var De = dbcontext.Employee_Financial_Contract_Detail.Where(a => a.SalaryCodeID == code).ToList();

                if (list != null)
                {

                    for (int i = 0; i < De.Count(); i++)
                    {
                        var delete = dbcontext.Employee_Financial_Contract_Detail.Remove(De[i]);
                        dbcontext.SaveChanges();
                    }

                    for (int i = 0; i < list.Count(); i++)
                    {

                        Employee_Financial_Contract_Detail record = new Employee_Financial_Contract_Detail();
                        record.Contract_Number = list[i].ID.ToString();
                        record.SalaryCodeID = code;
                        record.Salarycodedescription = name;
                        record.SalaryCodeValue = 0;
                        record.Type = codetype;
                        record.ValueType = codevaluetype;
                        dbcontext.Employee_Financial_Contract_Detail.Add(record);
                        dbcontext.SaveChanges();
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
        //[HttpPost]
        public ActionResult DeleteAssign(salary_codeVM model, FormCollection form, string Command, string code, string name, string codetype, string codevaluetype)
        {
            try
            {
                ViewBag.checktype = dbcontext.Checktype.ToList().Select(m => new { Code = m.Code + "-[" + m.Description + ']', ID = m.ID });
                ViewBag.extedned = dbcontext.ExtendedFields_Header.ToList().Select(m => new { Code = m.ExtendedFields_Code + "-[" + m.ExtendedFields_Desc + ']', ID = m.ID });
                ViewBag.Subscription_Syndicate = dbcontext.Subscription_Syndicate.ToList().Select(m => new { Code = m.Subscription_Code + "-[" + m.Subscription_Description + ']', ID = m.ID });
                ViewBag.debit = dbcontext.GL_AccountSetup.ToList().Select(m => new { Code = m.Account + "-[" + m.AccountName + ']', ID = m.ID });
                ViewBag.credit = dbcontext.GL_AccountSetup.ToList().Select(m => new { Code = m.Account + "-[" + m.AccountName + ']', ID = m.ID });
                ViewBag.code = code;
                ViewBag.name = name;
                ViewBag.codetype = codetype;
                ViewBag.codevaluetype = codevaluetype;

                var list = dbcontext.Employee_Financial_Contract_Header.Where(a => a.IsActive == true).ToList();
                var lists = dbcontext.Employee_Financial_Contract_Header.ToList();
                var De = dbcontext.Employee_Financial_Contract_Detail.Where(a => a.SalaryCodeID == code).ToList();

                if (list != null)
                {
                    for (int i = 0; i < De.Count(); i++)
                    {
                        var delete = dbcontext.Employee_Financial_Contract_Detail.Remove(De[i]);
                        dbcontext.SaveChanges();
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
        public ActionResult delete(int id)
        {
            try
            {
                var model = dbcontext.salary_code.FirstOrDefault(m => m.ID == id);
                return View(model);
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
            var model = dbcontext.salary_code.FirstOrDefault(m => m.ID == id);
            try
            {
                dbcontext.salary_code.Remove(model);
                dbcontext.SaveChanges();
                //=================================check for alert==================================
                var get_result_check = HR.Controllers.check.check_alert("Salary item card", HR.Models.user.Action.delete, HR.Models.user.type_field.form);
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
            catch (Exception)
            {
                return View(model);
            }
        }
    }
    public class salary_codeVM
    {
        public salary_code salary_code { get; set; }
        public code_type code_type { get; set; }

        public code_value_type code_value_type { get; set; }
        public Document_entry Document_entry { get; set; }
        public cost_center_type cost_center_type { get; set; }
        public unit unit { get; set; }

    }
    public enum code_type
    {
        Earning = 1,
        Deduction = 2
    }
    public enum code_value_type
    {
        Unkown = 1,
        Minites = 2,
        Hours = 3,
        Days = 4,
        Months = 5,
        Years = 6,
        Money = 7,
        Piece = 8,
        [Display(Name = "Calculated Value")]
        Calculated_Value = 9
    }
    public enum Document_entry
    {
        [Display(Name = "Financial contract")]
        Financial_contract = 1,
        [Display(Name = "Manual transaction")]
        Manual_transaction = 2,
        [Display(Name = "Manual payment")]
        Manual_payment = 3
    }
    public enum cost_center_type
    {
        None = 1,
        Employee = 2,
        Department = 3
    }
    public enum unit
    {
        [Display(Name = "End of current month")]
        end_of_current_month = 1,
        [Display(Name = "Transaction date of cuerrent month")]
        transaction_date_of_cuerrent_month = 2,
        [Display(Name = "Even_from_previous_month(s)")]
        even_from_previous_month = 3
    }
}