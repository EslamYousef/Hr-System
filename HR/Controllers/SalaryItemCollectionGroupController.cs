using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HR.Models;
using System.Data.Entity.Infrastructure;
using HR.Models.Infra;
using HR.Models.ViewModel;
using HR.Models.CardPayroll;

namespace HR.Controllers
{
    [Authorize]
    public class SalaryItemCollectionGroupController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: SalaryItemCollectionGroup
        public ActionResult Index()
        {
            var model = dbcontext.SalaryItemCollectionGroup_Header.ToList();
            return View(model);
        }
        public ActionResult Create(string id)
        {

            ViewBag.SalaryCodeGroup_Header = dbcontext.SalaryCodeGroup_Header.Where(a=>a.GroupPurpose==1).ToList().Select(m => new { Code = m.CodeGroupID + "-----[" + m.CodeGroupDesc + ']', ID = m.ID });
           
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Payroll);
            var model = dbcontext.SalaryItemCollectionGroup_Header.ToList();
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
            var SalaryItemCollectionGroup_Header = new SalaryItemCollectionGroup_Header { CollectionId = stru.Structure_Code + count.ToString() };
            return View(SalaryItemCollectionGroup_Header);

        }
        [HttpPost]
        public ActionResult Create(FormCollection form, SalaryItemCollectionGroup_Header model)
        {
            try
            {


                ViewBag.SalaryCodeGroup_Header = dbcontext.SalaryCodeGroup_Header.Where(a => a.GroupPurpose == 1).ToList().Select(m => new { Code = m.CodeGroupID + "-----[" + m.CodeGroupDesc + ']', ID = m.ID });

                if (ModelState.IsValid)
                {

                    SalaryItemCollectionGroup_Header new_Record = new SalaryItemCollectionGroup_Header();
                    new_Record.CollectionId = model.CollectionId;
                    new_Record.CollectionDesc = model.CollectionDesc;
                    new_Record.CollectionAltDesc = model.CollectionAltDesc;
                    new_Record.Created_By = User.Identity.Name;
                    new_Record.Created_Date = DateTime.Now.Date;
                    var Header = dbcontext.SalaryItemCollectionGroup_Header.Add(new_Record);
                    dbcontext.SaveChanges();

                    var Family_profile = form["Family_profile_No2"].Split(char.Parse(","));
                    var Family_name = form["Family_name"].Split(char.Parse(","));
                    var Percentage = form["Percentage"].Split(char.Parse(","));
                    for (var i = 0; i < Family_profile.Length; i++)
                    {
                        if (Family_profile[i] != "")
                        {
                            //var ID = int.Parse(Family_profile[i]);
                            //var item = dbcontext.salary_code.FirstOrDefault(m => m.ID == ID);
                            var new_details = new SalaryItemCollectionGroup_Detail { CollectionId = Header.ID.ToString(), CodeGroupID = Family_profile[i], Created_By = User.Identity.Name, Created_Date = DateTime.Now.Date, SortIndex = int.Parse(Percentage[i]), CodeGroupDescription= Family_name[i] };
                            dbcontext.SalaryItemCollectionGroup_Detail.Add(new_details);
                            dbcontext.SaveChanges();
                        }
                    }
                    return RedirectToAction("index");

                   
                }
                else
                {
                    return View(model);
                }
            }
            catch (DbUpdateException e)
            {
                TempData["Message"] = HR.Resource.Basic.thiscodeIsalreadyexists;
                return View(model);
            }
            catch (Exception e)
            {
                return View(model);
            }

        }
        public ActionResult Edit(string id)

        {
            try
            {
                ViewBag.SalaryCodeGroup_Header = dbcontext.SalaryCodeGroup_Header.Where(a => a.GroupPurpose == 1).ToList().Select(m => new { Code = m.CodeGroupID + "-----[" + m.CodeGroupDesc + ']', ID = m.ID });
                var ID = int.Parse(id);
                var old_model = dbcontext.SalaryItemCollectionGroup_Header.FirstOrDefault(m => m.ID == ID);
                var old_details = dbcontext.SalaryItemCollectionGroup_Detail.Where(m => m.CollectionId == old_model.ID.ToString()).ToList();
                var new_model = new Models.CardPayroll.VMs { SalaryItemCollectionGroup_Detail = old_details,SalaryItemCollectionGroup_Header =old_model };
                return View(new_model);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
            
        [HttpPost]
        public ActionResult Edit(Models.CardPayroll.VMs model, FormCollection form)
        {
            try
            {
                ViewBag.SalaryCodeGroup_Header = dbcontext.SalaryCodeGroup_Header.Where(a => a.GroupPurpose == 1).ToList().Select(m => new { Code = m.CodeGroupID + "-----[" + m.CodeGroupDesc + ']', ID = m.ID });
                ///update////
                var updated_model = dbcontext.SalaryItemCollectionGroup_Header.FirstOrDefault(m => m.ID == model.SalaryItemCollectionGroup_Header.ID);
                updated_model.Modified_By = User.Identity.Name;
                updated_model.Modified_Date = DateTime.Now.Date;
                updated_model.CollectionDesc = model.SalaryItemCollectionGroup_Header.CollectionDesc;
                updated_model.CollectionAltDesc = model.SalaryItemCollectionGroup_Header.CollectionAltDesc;
                dbcontext.SaveChanges();
                ///////////delete//////////
                var update_details = dbcontext.SalaryItemCollectionGroup_Detail.Where(m => m.CollectionId == updated_model.ID.ToString()).ToList();
                dbcontext.SalaryItemCollectionGroup_Detail.RemoveRange(update_details);
                dbcontext.SaveChanges();
                ///////////////////add///////
                var Family_profile = form["Family_profile_No2"].Split(char.Parse(","));
                var Family_name = form["Family_name"].Split(char.Parse(","));
                var Percentage = form["Percentage"].Split(char.Parse(","));
                for (var i = 0; i < Family_profile.Length; i++)
                {
                    if (Family_profile[i] != "")
                    {
                        //var ID = int.Parse(Family_profile[i]);
                        //var item = dbcontext.salary_code.FirstOrDefault(m => m.ID == ID);
                        var new_details = new SalaryItemCollectionGroup_Detail { CollectionId = updated_model.ID.ToString(), CodeGroupID = Family_profile[i], Created_By = User.Identity.Name, Created_Date = DateTime.Now.Date, SortIndex = int.Parse(Percentage[i]), CodeGroupDescription = Family_name[i] };
                        dbcontext.SalaryItemCollectionGroup_Detail.Add(new_details);
                        dbcontext.SaveChanges();
                    }
                }
                ////////////////
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
                var model = dbcontext.SalaryItemCollectionGroup_Header.FirstOrDefault(m => m.ID == id);
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult Delete_method(int id)
        {
            var model = dbcontext.SalaryItemCollectionGroup_Header.FirstOrDefault(m => m.ID == id);
            try
            {
                var details = dbcontext.SalaryItemCollectionGroup_Detail.Where(m => m.CollectionId == model.ID.ToString());
                dbcontext.SalaryItemCollectionGroup_Detail.RemoveRange(details);
                dbcontext.SaveChanges();


                dbcontext.SalaryItemCollectionGroup_Header.Remove(model);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (Exception)
            {
                return View(model);
            }
        }
        public class VMs
        {
            public SalaryItemCollectionGroup_Header SalaryItemCollectionGroup_Header { get; set; }
            public List<SalaryItemCollectionGroup_Detail> SalaryItemCollectionGroup_Detail { get; set; }
        }
    }
}