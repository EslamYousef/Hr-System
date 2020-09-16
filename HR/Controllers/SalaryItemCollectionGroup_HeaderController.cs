using HR.Models;
using HR.Models.Infra;
using HR.Models.SetupPayroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers
{
    [Authorize(Roles = "Admin,payroll,payrollCards,salary items collection groups")]
    public class SalaryItemGroup_HeaderController : BaseController
    {
        // GET: SalaryItemCollectionGroup_Header
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        public ActionResult Index()
        {
            var model = dbcontext.SalaryCodeGroup_Header.ToList();
            return View(model);
        }
        public ActionResult create()
        {
            try
            {
                var new_record = new SalaryCodeGroup_Header();
                var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Payroll).Structure_Code;
                var model_ = dbcontext.SalaryCodeGroup_Header.ToList();
                if (model_.Count() == 0)
                {
                    new_record.CodeGroupID = stru + "1";
                }
                else
                {
                    new_record.CodeGroupID = stru + (model_.LastOrDefault().ID + 1).ToString();
                }
                var model = new Header {SalaryItemCollectionGroup_Header=new_record, code_group_type=new code_group_type(),Eligibility=new Eligibility(),group_purpose=new group_purpose()};
                ViewBag.salaryitem = dbcontext.salary_code.ToList().Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + ']', ID = m.ID });
                return View(model);
            }
            catch(Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult Create(Header model,FormCollection form)
        {
            try
            {
                ViewBag.salaryitem = dbcontext.salary_code.ToList().Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + ']', ID = m.ID });
                var new_Record = model.SalaryItemCollectionGroup_Header;
                new_Record.EligibilityType = model.Eligibility.GetHashCode();
                new_Record.GroupPurpose = model.group_purpose.GetHashCode();
                new_Record.CodeGroupType = model.code_group_type.GetHashCode();
                new_Record.Created_By = User.Identity.Name;
                new_Record.Created_Date = DateTime.Now.Date;
               var Header= dbcontext.SalaryCodeGroup_Header.Add(new_Record);
                dbcontext.SaveChanges();

                ///////////////////
                var ID_item = form["ID_item"].Split(',');
                var value = form["value"].Split(',');
                var sort = form["sort"].Split(',');
                for(var i=0;i< ID_item.Length;i++)
                {
                    if(ID_item[i]!="")
                    {
                        var ID = int.Parse(ID_item[i]);
                        var item = dbcontext.salary_code.FirstOrDefault(m => m.ID == ID);
                        var new_details = new SalaryCodeGroup_Detail {salary_codeID= item.ID, CodeGroupID=Header.CodeGroupID,Created_By=User.Identity.Name,Created_Date=DateTime.Now.Date,SalaryCodeID=item.SalaryCodeID,SortIndex=int.Parse(sort[i]),DefaultValue=int.Parse(value[i]) };
                        dbcontext.SalaryCodeGroup_Detail.Add(new_details);
                        dbcontext.SaveChanges();
                    }
                }
                ////////////////
                //=================================check for alert==================================
                var get_result_check = HR.Controllers.check.check_alert("Salary item group card", HR.Models.user.Action.Create, HR.Models.user.type_field.form);
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
            catch(Exception)
            {
                return View(model);
            }
        }
        public ActionResult edit(int id)
        {
            try
            {
                ViewBag.salaryitem = dbcontext.salary_code.ToList().Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + ']', ID = m.ID });

                var old_model = dbcontext.SalaryCodeGroup_Header.FirstOrDefault(m => m.ID == id);
                var header = new Header { SalaryItemCollectionGroup_Header = old_model, code_group_type = (code_group_type)old_model.CodeGroupType, Eligibility = (Eligibility)old_model.EligibilityType, group_purpose = (group_purpose)old_model.GroupPurpose };
                var old_details = dbcontext.SalaryCodeGroup_Detail.Where(m => m.CodeGroupID == old_model.CodeGroupID).ToList();
                var new_model = new VM { SalaryCodeGroup_Detail=old_details,Header= header};
                return View(new_model);
            }
            catch(Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult edit(VM model,FormCollection form)
        {
            try
            {
                ViewBag.salaryitem = dbcontext.salary_code.ToList().Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + ']', ID = m.ID });
                ///update////
                var updated_model = dbcontext.SalaryCodeGroup_Header.FirstOrDefault(m => m.ID == model.Header.SalaryItemCollectionGroup_Header.ID);
                updated_model.Modified_By = User.Identity.Name;
                updated_model.Modified_Date = DateTime.Now.Date;
                updated_model.CodeGroupDesc = model.Header.SalaryItemCollectionGroup_Header.CodeGroupDesc;
                updated_model.CodeGroupAltDesc = model.Header.SalaryItemCollectionGroup_Header.CodeGroupAltDesc;
                updated_model.PayrollEligibility = model.Header.SalaryItemCollectionGroup_Header.PayrollEligibility;
                updated_model.EligibilityType = model.Header.Eligibility.GetHashCode();
                updated_model.GroupPurpose = model.Header.group_purpose.GetHashCode();
                updated_model.CodeGroupType = model.Header.code_group_type.GetHashCode();
                dbcontext.SaveChanges();
                ///////////delete//////////
                var update_details = dbcontext.SalaryCodeGroup_Detail.Where(m => m.CodeGroupID == updated_model.CodeGroupID).ToList();
                dbcontext.SalaryCodeGroup_Detail.RemoveRange(update_details);
                dbcontext.SaveChanges();
                ///////////////////add///////
                var ID_item = form["ID_item"].Split(',');
                var value = form["value"].Split(',');
                var sort = form["sort"].Split(',');
                for (var i = 0; i < ID_item.Length; i++)
                {
                    if (ID_item[i] != "")
                    {
                        var ID = int.Parse(ID_item[i]);
                        var item = dbcontext.salary_code.FirstOrDefault(m => m.ID == ID);
                        var nejjw_details = new SalaryCodeGroup_Detail { salary_codeID = item.ID, CodeGroupID = updated_model.CodeGroupID, Created_By = User.Identity.Name, Created_Date = DateTime.Now.Date, SalaryCodeID = item.SalaryCodeID, SortIndex = int.Parse(sort[i]), DefaultValue = decimal.Parse(value[i]) };
                        dbcontext.SalaryCodeGroup_Detail.Add(nejjw_details);
                        dbcontext.SaveChanges();
                    }
                }
                ////////////////
                //=================================check for alert==================================
                var get_result_check = HR.Controllers.check.check_alert("Salary item group card", HR.Models.user.Action.edit, HR.Models.user.type_field.form);
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
            catch (Exception e)
            {
                return View(model);
            }
        }
        public ActionResult delete(int id)
        {
            try
            {
                var model = dbcontext.SalaryCodeGroup_Header.FirstOrDefault(m => m.ID == id);
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
            var model = dbcontext.SalaryCodeGroup_Header.FirstOrDefault(m => m.ID == id);
            try
            {
                var details = dbcontext.SalaryCodeGroup_Detail.Where(m => m.CodeGroupID == model.CodeGroupID);
                dbcontext.SalaryCodeGroup_Detail.RemoveRange(details);
                dbcontext.SaveChanges();


                dbcontext.SalaryCodeGroup_Header.Remove(model);
                dbcontext.SaveChanges();
                //=================================check for alert==================================
                var get_result_check = HR.Controllers.check.check_alert("Salary item group card", HR.Models.user.Action.delete, HR.Models.user.type_field.form);
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
            catch(Exception)
            {
                return View(model);
            }
        }
        public JsonResult salaryitem(int id)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            var item = dbcontext.salary_code.FirstOrDefault(m => m.ID == id);
            return Json(item);
        }
    }
    public class VM
    {
        public Header Header { get; set; }
        public List<SalaryCodeGroup_Detail> SalaryCodeGroup_Detail { get; set; }
    }
    public class Header
    {
        public SalaryCodeGroup_Header SalaryItemCollectionGroup_Header { get; set; }
        public code_group_type code_group_type { get; set; }
        public Eligibility Eligibility { get; set; }
        public group_purpose group_purpose { get; set; }
    }
    public enum code_group_type
    {
        Eearning=1,
        Deduction=2,
        Both=3
    }
    public enum Eligibility
    {
        All_Employees=1,
        Specific_Employees=2
    }
    public enum group_purpose
    {
        Reporting=1,
        Batch_transaction=2,
        Summary=3,
        Penalties=4
    }
}