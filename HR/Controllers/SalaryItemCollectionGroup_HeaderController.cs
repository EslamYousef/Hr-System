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
    public class SalaryItemGroup_HeaderController : Controller
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
                ViewBag.salaryitem = dbcontext.salary_code.ToList();
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
                        var new_details = new SalaryCodeGroup_Detail { CodeGroupID=Header.CodeGroupID,Created_By=User.Identity.Name,Created_Date=DateTime.Now.Date,SalaryCodeID=item.SalaryCodeID,SortIndex=int.Parse(sort[i]),DefaultValue=int.Parse(value[i]) };
                        dbcontext.SalaryCodeGroup_Detail.Add(new_details);
                        dbcontext.SaveChanges();
                    }
                }
                ////////////////
                return RedirectToAction("index");
            }
            catch(Exception)
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
                return RedirectToAction("index");
            }
            catch(Exception)
            {
                return View(model);
            }
        }
        public JsonResult salaryitem(int id)
        {
            var item = dbcontext.salary_code.FirstOrDefault(m => m.ID == id);
            return Json(item);
        }
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