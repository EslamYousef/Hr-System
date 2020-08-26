using HR.Models;
using HR.Models.Infra;
using HR.Models.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers
{
    [Authorize]
    public class request_new_contractController : BaseController
    {
        // GET: request_new_contract
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        [Authorize(Roles = "Admin,personnel,personnel transaction transaction,personnelTransaction,personnel transaction process,personnelprocess")]
        public ActionResult Index(string mess)
        {
            if(mess!=null)
            {
                TempData["Message"] = mess;
            }
            var model = dbcontext.new_contrct.ToList();
            return View(model);
        }
        [Authorize(Roles = "Admin,personnel,personnel transaction transaction,personnelTransaction")]
        public ActionResult create()
        {
           
            ViewBag.Contract_Type = dbcontext.Contract_Type.ToList().Select(m => new { Code = m.Contract_Type_Code + "------[" + m.Contract_Type_Description + ']', ID = m.ID });
            ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.Approved_date = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel);
            var model = dbcontext.new_contrct.ToList();
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
            DateTime statis2 = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            var Employee_contract_profile = new request_new_contract { Code = stru.Structure_Code + count, Employee_ProfileId = "0",
                                                                       Approved_date = statis2, Contract_start_date = statis2,
                                                                       Contract_end_date = statis2, Medical_date = statis2 };

            return View(Employee_contract_profile);
        }
        [HttpPost]
        public ActionResult create(request_new_contract model)
        {
            try
            {
                if (model.ContractTypeId == null) { model.ContractTypeId = "0"; }
                if (model.ApprovedbyId == null) { model.ApprovedbyId = "0"; }
                ViewBag.Contract_Type = dbcontext.Contract_Type.ToList().Select(m => new { Code = m.Contract_Type_Code + "------[" + m.Contract_Type_Description + ']', ID = m.ID });
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Approved_date = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                var con = int.Parse(model.Employee_ProfileId);
                var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == con);
                model.Employee_Profile = emp;
                if (model.Contract_start_date > model.Contract_end_date)
                {
                    TempData["Message"] = HR.Resource.pers_2.FromdateIssuebiggerdateExpire;
                    return View(model);
                }
                var Date = Convert.ToDateTime("1/1/1900");
                var s = new status { statu = check_status.created, Type = Models.Infra.Type.employee_record, approved_bydate = Date, cancaled_bydate = Date, created_bydate = DateTime.Now.Date, Rejected_bydate = Date, return_to_reviewdate = Date };
                s.statu = check_status.created;
                s.created_by = User.Identity.GetUserName();
                var st = dbcontext.status.Add(s);
                dbcontext.SaveChanges();
                model.status = st;
                dbcontext.new_contrct.Add(model);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (DbUpdateException e)
            {
                TempData["Message"] =HR.Resource.organ.thiscodeIsalreadyexists;
                return View(model);
            }
            catch (Exception e)
            {
                return View(model);
            }
        }
        [Authorize(Roles = "Admin,personnel,personnel transaction transaction,personnelTransaction")]

        public ActionResult edit(string id)
        {
            try
            {
                ViewBag.Contract_Type = dbcontext.Contract_Type.ToList().Select(m => new { Code = m.Contract_Type_Code + "------[" + m.Contract_Type_Description + ']', ID = m.ID });
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Approved_date = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                var ID = int.Parse(id);
                var record = dbcontext.new_contrct.FirstOrDefault(m => m.ID == ID);
                if (record != null)
                {
                    return View(record);
                }
                else
                {
                    TempData["Message"] = HR.Resource.organ.thereisnodata;
                    return View();
                }
            }
            catch(Exception e)
            { return View(); }
        }
        [HttpPost]
        public ActionResult edit(request_new_contract model)
        {
            try
            {
                if (model.ContractTypeId == null) { model.ContractTypeId = "0"; }
                if (model.ApprovedbyId == null) { model.ApprovedbyId = "0"; }
              
                 ViewBag.Contract_Type = dbcontext.Contract_Type.ToList().Select(m => new { Code = m.Contract_Type_Code + "------[" + m.Contract_Type_Description + ']', ID = m.ID });
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Approved_date = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });

                var prof = int.Parse(model.Employee_ProfileId);
                var record = dbcontext.new_contrct.FirstOrDefault(m => m.ID == model.ID);
               
                record.Code = model.Code;
                record.Employment_type = model.Employment_type;
                record.Contract_start_date = model.Contract_start_date;
                record.Contract_end_date = model.Contract_end_date;
                if (model.Contract_start_date > model.Contract_end_date)
                {
                    TempData["Message"] = HR.Resource.pers_2.FromdateIssuebiggerdateExpire;
                    return View(model);
                }
                record.Years = model.Years;
                record.Months = model.Months;
                record.Approved_date = model.Approved_date;
                record.Notes = model.Notes;
                record.Medical_date = model.Medical_date;
                record.Medical_entity_name = model.Medical_entity_name;
                record.Not_fit_reason = model.Not_fit_reason;
                record.Medical_commite_comments = model.Medical_commite_comments;
                record.Medical_commite_recomindation = model.Medical_commite_recomindation;
                record.Result = model.Result;
               
                record.Employee_ProfileId = model.Employee_ProfileId;
                var Employee_ProfileId = int.Parse(model.Employee_ProfileId);
                record.Employee_Profile = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == Employee_ProfileId);
                record.ContractTypeId = model.ContractTypeId;
                record.ApprovedbyId = model.ApprovedbyId;
                dbcontext.SaveChanges();
                return RedirectToAction("index", new { id = model.ID });
            }
            catch (DbUpdateException e)
            {
                TempData["Message"] = HR.Resource.organ.thiscodeIsalreadyexists;
                return View(model);
            }
            catch (Exception e)
            { return View(model); }
        }
        [Authorize(Roles = "Admin,personnel,personnel transaction transaction,personnelTransaction")]
        public ActionResult Delete(int id)
        {
            try
            {
                var record = dbcontext.new_contrct.FirstOrDefault(m => m.ID == id);
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] =HR.Resource.organ.thereisnodata;
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

            var record = dbcontext.new_contrct.FirstOrDefault(m => m.ID == id);
            try
            {
                dbcontext.new_contrct.Remove(record);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = HR.Resource.organ.youcannotdeletethisRow;
                return View(record);
            }
            catch (Exception e)
            {
                return View();
            }
        }
        [Authorize(Roles = "Admin,personnel,personnel transaction process,personnelprocess")]

        public ActionResult status(string id)
        {
            try
            {
                var ID = int.Parse(id);
                var model = dbcontext.new_contrct.FirstOrDefault(m => m.ID == ID);
                var st = dbcontext.status.FirstOrDefault(m => m.ID == model.status.ID);

                ViewBag.statue = dbcontext.status.ToList().Select(m => new { code = m.approved_by });

                var my_model = new employeestate { status = st, empid = model.Employee_Profile.ID, opertion_id = ID };
                if (my_model.status.approved_by == null)
                    my_model.status.approved_bydate = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString());
                if (my_model.status.Rejected_by == null)
                    my_model.status.Rejected_bydate = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString());
                if (my_model.status.return_to_reviewby == null)
                    my_model.status.return_to_reviewdate = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString());
                if (my_model.status.cancaled_by == null)
                    my_model.status.cancaled_bydate = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString());

                return View(my_model);
            }
            catch (Exception e)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult status(employeestate model)
        {
            var sta = dbcontext.status.FirstOrDefault(m => m.ID == model.status.ID);
            var record = dbcontext.new_contrct.FirstOrDefault(m => m.ID == model.opertion_id);
            if (model.check_status == check_status.Approved)
            {
                sta.approved_by = User.Identity.GetUserName();
                sta.approved_bydate = model.status.approved_bydate;
                sta.statu = check_status.Approved;
                dbcontext.SaveChanges();
                var flag=add_contract_(model.opertion_id.ToString());
                if(flag==true)
                {
                    var ms= HR.Resource.pers_2.addedSuccessfully;
                    return RedirectToAction("Index", new { mess = ms });
                   
                }
                else
                {
                    var ms =HR.Resource.pers_2.Faild;
                    return RedirectToAction("Index", new { mess = ms });
                }

            }
            //else if (model.check_status == check_status.Canceled)
            //{

            //    sta.cancaled_by = model.status.cancaled_by;
            //    sta.cancaled_bydate = model.status.cancaled_bydate;
            //    sta.statu = check_status.Canceled;
            //    dbcontext.SaveChanges();
            //}
            //else if (model.check_status == check_status.created)
            //{
            //    sta.created_by = model.status.created_by;
            //    sta.created_bydate = model.status.created_bydate;
            //    sta.statu = check_status.created;
            //    dbcontext.SaveChanges();
            //}
            else if (model.check_status == check_status.Rejected)
            {
                sta.Rejected_by = User.Identity.GetUserName();
                sta.Rejected_bydate = model.status.Rejected_bydate;
                sta.statu = check_status.Rejected;
                dbcontext.SaveChanges();
            }
            else if (model.check_status == check_status.Return_To_Review)
            {
                sta.return_to_reviewby = User.Identity.GetUserName();
                sta.return_to_reviewdate = model.status.return_to_reviewdate;
                sta.statu = check_status.Return_To_Review;
                dbcontext.SaveChanges();
            }

            return RedirectToAction("index");
        }
        public bool add_contract_(string id )
        {

            try
            {
                var ID = int.Parse(id);
                var model = dbcontext.new_contrct.FirstOrDefault(m => m.ID == ID);



                /////
                Employee_contract_profile record = new Employee_contract_profile();
                var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel);
                var list = dbcontext.Employee_contract_profile.ToList();
                var count = 0;
                if (list.Count() == 0)
                {
                    count = 1;
                }
                else
                {
                    var te = list.LastOrDefault().ID;
                    count = te + 1;
                }
           var  Code = stru.Structure_Code + count;
                record.Code = Code;
                if (list.Count() == 0)
                {
                    record.Active = true;
                }
                else
                {
                    for (int i = 0; i < list.Count(); i++)
                    {
                        list[i].Active = false;
                    }
                    record.Active = true;
                }
                //     record.Contract = model.Contract;
                record.Employment_type = model.Employment_type;
                record.Contract_start_date = model.Contract_start_date;
                record.Contract_end_date = model.Contract_end_date;

                record.Years = model.Years;
                record.Months = model.Months;
                record.Approved_date = model.Approved_date;
                record.Notes = model.Notes;
                record.Medical_date = model.Medical_date;
                record.Medical_entity_name = model.Medical_entity_name;
                record.Not_fit_reason = model.Not_fit_reason;
                record.Medical_commite_comments = model.Medical_commite_comments;
                record.Medical_commite_recomindation = model.Medical_commite_recomindation;
                record.Result = model.Result;
                record.Employee_ProfileId = model.Employee_ProfileId;
                var Employee_ProfileId = int.Parse(model.Employee_ProfileId);
                record.Employee_Profile = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == Employee_ProfileId);
                record.ContractTypeId = model.ContractTypeId;
                record.ApprovedbyId = model.ApprovedbyId;

                var pos = dbcontext.Employee_contract_profile.Add(record);
                dbcontext.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }

        }

    }
}