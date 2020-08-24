using HR.Models;
using HR.Models.Infra;
using HR.Models.penalities.setup;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers.penalites.setup
{
    [Authorize(Roles = "Admin,Penalties,PenaltiesSetup")]
    public class penalityitemController : BaseController
    {
        // GET: penalityitem
        ApplicationDbContext dbcontext = new ApplicationDbContext();

        public ActionResult Index()
        {
            var model = dbcontext.Discipline_PenaltyItem_Header.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            //////
            var i = (Int16)group_purpose.Penalties.GetHashCode();
            ViewBag.credit = dbcontext.SalaryCodeGroup_Header.Where(m => m.GroupPurpose == i).ToList().Select(m => new { Code = m.CodeGroupID + "-[" + m.CodeGroupDesc + ']', ID = m.ID });

            var modell = new Discipline_PenaltyItem_Header();

            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel).Structure_Code;
            var model = dbcontext.Discipline_PenaltyItem_Header.ToList();
            if (model.Count() == 0)
            {
                modell.PenaltyItem_Code = stru + "1";
            }
            else
            {
                modell.PenaltyItem_Code = stru + (model.LastOrDefault().ID + 1).ToString();
            }
            /////
            return View(modell);
        }
        [HttpPost]
        public ActionResult Create(Discipline_PenaltyItem_Header model, FormCollection form,string commend)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var i = (Int16)group_purpose.Penalties.GetHashCode();
                    ViewBag.credit = dbcontext.SalaryCodeGroup_Header.Where(m=>m.GroupPurpose==i).ToList().Select(m => new { Code = m.CodeGroupID + "-[" + m.CodeGroupDesc + ']', ID = m.ID });

                    model.Created_By = User.Identity.Name;
                    model.Created_Date = DateTime.Now.Date;
                    var a1 = form["check_A"].Split(',');
                    if (a1.Length == 1)
                    {
                        model.LinkToPayroll = false;
                        model.CodeGroupID = null;
                    }
                    else
                    {
                        model.LinkToPayroll = true;
                        model.CodeGroupID = model.CodeGroupID;
                    }

                    var tra_center = dbcontext.Discipline_PenaltyItem_Header.Add(model);
                    dbcontext.SaveChanges();
                    if(commend=="payroll")
                    {
                        if (tra_center.LinkToPayroll == true&& tra_center.CodeGroupID!=null)
                        {

                            return RedirectToAction("linkwithpayroll", new { header_id = tra_center.ID,path=0 });
                        }
                        else
                        {
                            TempData["Message"] = "You must choose sallary item group";
                            return RedirectToAction("edit",new { id= tra_center.ID });
                        }
                    }
                    return RedirectToAction("Index");
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
       
        public ActionResult  linkwithpayroll(int header_id,int? path)
        {
            try
            {
                ViewBag.header_id = header_id;
                if (path == 0)//create
                {

                 
                    var header = dbcontext.Discipline_PenaltyItem_Header.FirstOrDefault(m => m.ID == header_id);
                    ViewBag.header_code = header.PenaltyItem_Code;
                    var ID_group = int.Parse(header.CodeGroupID);
                    var salary_item_group = dbcontext.SalaryCodeGroup_Header.FirstOrDefault(m => m.ID == ID_group);
                    ViewBag.group = salary_item_group.CodeGroupID;
                    var D = dbcontext.SalaryCodeGroup_Detail.Where(m => m.CodeGroupID == salary_item_group.CodeGroupID.ToString()).ToList();
                    return View(D);
                }
                else//edit
                {
                    var header = dbcontext.Discipline_PenaltyItem_Header.FirstOrDefault(m => m.ID == header_id);
                    ViewBag.header_code = header.PenaltyItem_Code;
                    var ID_group = int.Parse(header.CodeGroupID);
                    var salary_item_group = dbcontext.SalaryCodeGroup_Header.FirstOrDefault(m => m.ID == ID_group);
                    ViewBag.group = salary_item_group.CodeGroupID;
                    var D = dbcontext.SalaryCodeGroup_Detail.Where(m => m.CodeGroupID == salary_item_group.CodeGroupID.ToString()).ToList();
                    return View(D);
                    
                }
            }
            catch(Exception)
            {
                return RedirectToAction("edit", new { id = header_id });
            }
        }
        [HttpPost]
        public ActionResult linkwithpayroll(int header_id_, FormCollection form)
        {
            ViewBag.header_id = header_id_;
            var header = dbcontext.Discipline_PenaltyItem_Header.FirstOrDefault(m => m.ID == header_id_);
            ViewBag.header_code = header.PenaltyItem_Code;
            var ID_group = int.Parse(header.CodeGroupID);
            var salary_item_group = dbcontext.SalaryCodeGroup_Header.FirstOrDefault(m => m.ID == ID_group);
            ViewBag.group = salary_item_group.CodeGroupID;
            var D = dbcontext.SalaryCodeGroup_Detail.Where(m => m.CodeGroupID == salary_item_group.CodeGroupID.ToString()).ToList();
            try
            {
                  //===============================================================================================================
                var old_link = dbcontext.Discipline_PenaltyItem_Detail.Where(m => m.PenaltyItem_Code == header.ID.ToString()).ToList();
                dbcontext.Discipline_PenaltyItem_Detail.RemoveRange(old_link);
                dbcontext.SaveChanges();
                //===============================================================================================================
                var ID_value = form["ID_value"].Split(',');
                var value = form["value_"].Split(',');
                for(var i=0;i<value.Count();i++)
                {
                    if(value[i]!="")
                    {
                        var ID = int.Parse(ID_value[i]);
                        var D1 = dbcontext.SalaryCodeGroup_Detail.FirstOrDefault(m => m.ID == ID);
                        D1.DefaultValue = decimal.Parse(value[i]);
                        dbcontext.SaveChanges();
                        //=======
                        var new_add = new Discipline_PenaltyItem_Detail { CodeGroupID= salary_item_group.ID.ToString(),PenaltyItem_Code= header.ID.ToString(),SalaryCodeID=D1.ID.ToString(),DefaultValue= (double)D1.DefaultValue, Created_By=User.Identity.Name,Created_Date=DateTime.Now};
                        dbcontext.Discipline_PenaltyItem_Detail.Add(new_add);
                        dbcontext.SaveChanges();
                        //=======
                    }
                }
                return RedirectToAction("edit", new { id = header_id_ });

            }
            catch (Exception)
            {
             
                return View(D);
            }
        }
        public ActionResult Edit(int id)
        {
            try
            {
                var i = (Int16)group_purpose.Penalties.GetHashCode();
                ViewBag.credit = dbcontext.SalaryCodeGroup_Header.Where(m => m.GroupPurpose == i).ToList().Select(m => new { Code = m.CodeGroupID + "-[" + m.CodeGroupDesc + ']', ID = m.ID });

                var record = dbcontext.Discipline_PenaltyItem_Header.FirstOrDefault(m => m.ID == id);
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
        public ActionResult Edit(Discipline_PenaltyItem_Header model, FormCollection form,string commend)
        {
            try
            {
                var i = (Int16 )group_purpose.Penalties.GetHashCode();
                ViewBag.credit = dbcontext.SalaryCodeGroup_Header.Where(m => m.GroupPurpose == i).ToList().Select(m => new { Code = m.CodeGroupID + "-[" + m.CodeGroupDesc + ']', ID = m.ID });

                var record = dbcontext.Discipline_PenaltyItem_Header.FirstOrDefault(m => m.ID == model.ID);

                record.PenaltyItem_Desc = model.PenaltyItem_Desc;
                record.PenaltyItem_AltDesc = model.PenaltyItem_AltDesc;
                record.Modified_By = User.Identity.Name;
                record.Modified_Date = DateTime.Now.Date;
                var a1 = form["check_A"].Split(',');
                if (a1.Length == 1)
                {
                    record.LinkToPayroll = false;
                    record.CodeGroupID = null;
                }
                else
                {
                    record.LinkToPayroll = true;
                    record.CodeGroupID = model.CodeGroupID;
                }

                dbcontext.SaveChanges();
                if (commend == "payroll")
                {
                    if (record.LinkToPayroll == true &&record.CodeGroupID!=null)
                    {
                        return RedirectToAction("linkwithpayroll", new { header_id = record.ID});
                    }
                    else
                    {
                        TempData["Message"] = "You must choose sallary item group";
                        return RedirectToAction("edit", new { id = record.ID });
                    }
                }
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
                var record = dbcontext.Discipline_PenaltyItem_Header.FirstOrDefault(m => m.ID == id);
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
            var record = dbcontext.Discipline_PenaltyItem_Header.FirstOrDefault(m => m.ID == id);
            var old_link = dbcontext.Discipline_PenaltyItem_Detail.Where(m => m.PenaltyItem_Code == record.ID.ToString()).ToList();
        
            try
            {
                dbcontext.Discipline_PenaltyItem_Header.Remove(record);
                dbcontext.Discipline_PenaltyItem_Detail.RemoveRange(old_link);
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
                return View(record);
            }
        }
    }
}