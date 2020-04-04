using HR.Models;
using HR.Models.Infra;
using HR.Models.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HR.Controllers
{
    [Authorize]
    public class Employee_recordsController : BaseController
    {
        // GET: Employee_records
        ApplicationDbContext dbcontext = new ApplicationDbContext();
       
      
        public ActionResult index()
        {
            try
            {
               
                var model = dbcontext.Employee_records.ToList();
                return View(model);
            }
            catch(Exception e)
            {
                return View();
            }
        }
        public ActionResult Create ()
        {
            ViewBag.employee = dbcontext.Employee_Profile.ToList().Select(m => new { Code = "" + m.Code + "-----[" + m.Full_Name + ']', ID = m.ID }).ToList(); 
            ViewBag.group = dbcontext.Employee_Recodes_Group.ToList().Select(m => new { Code = "" + m.Record_Group_Code + "-----[" + m.Record_Group_Description + ']', ID = m.ID }).ToList(); 
            var record = new Employee_records { record_amount=0.0,record_value=0.0,Record_date=DateTime.Now.Date,Record_expire_date=DateTime.Now.Date,Effictive_date=DateTime.Now.Date};
            var my_model = new employee_recordVM {record=record, selectedgroup=0,selectedEmpoyee=0};
            try
            {
                var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel).Structure_Code;
                var model = dbcontext.Employee_records.ToList();
                if (model.Count() == 0)
                {
                    record.Code = stru + "1";
                }
                else
                {
                    record.Code = stru + (model.LastOrDefault().ID + 1).ToString();
                }
                my_model.record = record;
                
                return View(my_model);
            }
            catch(Exception e)
            {
                return View(my_model);
            }
        }
        [HttpPost]
        public ActionResult Create(employee_recordVM model)
        {

            var username=User.Identity.GetUserName();
           
            ViewBag.employee = dbcontext.Employee_Profile.ToList().Select(m => new { Code = "" + m.Code + "-----[" + m.Full_Name + ']', ID = m.ID }).ToList();
            ViewBag.group = dbcontext.Employee_Recodes_Group.ToList().Select(m => new { Code = "" + m.Record_Group_Code + "-----[" + m.Record_Group_Description + ']', ID = m.ID }).ToList();
            try
            {
                var record = new Employee_records();
                record = model.record;
                if (model.selectedEmpoyee == 0)
                {
                    TempData["Message"] = HR.Resource.pers_2.youmustchooseemployee;
                    return View(model);
                }
                if (model.selectedEmpoyee > 0)
                {
                   

                    var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == model.selectedEmpoyee);
                    record.Employee_Profile = emp;
                    record.statID = emp.ID;
                }
                if (model.selectedgroup > 0)
                {
                    record.Employee_Recodes_Group = dbcontext.Employee_Recodes_Group.FirstOrDefault(m => m.ID == model.selectedgroup);
                }
                record.check_status = check_status.created;
               
                var Date = Convert.ToDateTime("1/1/1900");
                var s = new status {statu=check_status.created, created_by=username,Type = Models.Infra.Type.employee_record,approved_bydate=Date,cancaled_bydate=Date,created_bydate=DateTime.Now.Date,Rejected_bydate=Date,return_to_reviewdate=Date};
                var st= dbcontext.status.Add(s);
                dbcontext.SaveChanges();
                record.status = st;
                record.sss = record.Record_date.ToShortDateString();
                record.name_state = nameof(check_status.created);
                dbcontext.Employee_records.Add(record);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (DbUpdateException e)
            {
                TempData["Message"] = HR.Resource.organ.thiscodeIsalreadyexists;
                return View(model);
            }
            catch (Exception e)
            {
                return View(model);
            }
        }
        public ActionResult edit(string id)
        {
            ViewBag.employee = dbcontext.Employee_Profile.ToList().Select(m => new { Code = "" + m.Code + "-----[" + m.Full_Name + ']', ID = m.ID }).ToList();
            ViewBag.group = dbcontext.Employee_Recodes_Group.ToList().Select(m => new { Code = "" + m.Record_Group_Code + "-----[" + m.Record_Group_Description + ']', ID = m.ID }).ToList();
            var my_model = new employee_recordVM ();
            try
            {
                var ID = int.Parse(id);
                var reco = dbcontext.Employee_records.FirstOrDefault(m => m.ID == ID);
                my_model.record = reco;
                if (reco.Employee_Profile != null)
                {
                    my_model.selectedEmpoyee = reco.Employee_Profile.ID;
                }
                else
                {
                    my_model.selectedEmpoyee = 0;
                }
                if
                     (reco.Employee_Recodes_Group != null)
                {
                    my_model.selectedgroup = reco.Employee_Recodes_Group.ID;
                }
                else
                {
                    my_model.selectedgroup = 0;
                }
                return View(my_model);
            }
            catch(Exception e)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult edit(employee_recordVM model)
        {
            ViewBag.employee = dbcontext.Employee_Profile.ToList().Select(m => new { Code = "" + m.Code + "-----[" + m.Full_Name + ']', ID = m.ID }).ToList();
            ViewBag.group = dbcontext.Employee_Recodes_Group.ToList().Select(m => new { Code = "" + m.Record_Group_Code + "-----[" + m.Record_Group_Description + ']', ID = m.ID }).ToList();
            try
            {
                if (model.selectedEmpoyee == 0)
                {
                    TempData["Message"] = HR.Resource.pers_2.youmustchooseemployee;
                    return View(model);
                }
                var reco = dbcontext.Employee_records.FirstOrDefault(m => m.ID == model.record.ID);
                reco.Name = model.record.Name;
                reco.Description = model.record.Description;
                reco.Effictive_date = model.record.Effictive_date;
                reco.Record_date = model.record.Record_date;
                reco.Record_expire_date = model.record.Record_expire_date;
                reco.record_amount = model.record.record_amount;
                reco.record_value = model.record.record_value;
                if (model.selectedEmpoyee > 0)
                {
                    var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == model.selectedEmpoyee);
                    reco.Employee_Profile = emp;
                    reco.statID = emp.ID;
                }
                else
                {
                    reco.Employee_Profile = null;
                }
                if (model.selectedgroup > 0)
                {
                    reco.Employee_Recodes_Group = dbcontext.Employee_Recodes_Group.FirstOrDefault(m => m.ID == model.selectedgroup);
                }
                else
                {
                    reco.Employee_Recodes_Group = null;
                }
                reco.sss = reco.Record_date.ToShortDateString();
               
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = HR.Resource.organ.thiscodeIsalreadyexists;
                return View(model);
            }
            catch (Exception e)
            {
                return View(model);
            }
        }
        public ActionResult delete(string id)
        {
            try
            {
                var ID = int.Parse(id);
                var model = dbcontext.Employee_records.FirstOrDefault(m => m.ID == ID);
                return View(model);
            }
            catch(Exception e)
            {
                return RedirectToAction("index");
            }
        }
        [ActionName("delete")]
        [HttpPost]
        public ActionResult method_delete(string id)
        {
            var ID = int.Parse(id);
            var model = dbcontext.Employee_records.FirstOrDefault(m => m.ID == ID);
            var status = model.status;
            try
            {
                dbcontext.status.Remove(status);
                dbcontext.SaveChanges();
                dbcontext.Employee_records.Remove(model);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (DbUpdateException)
            {
                TempData["Message"] =HR.Resource.organ.youcannotdeletethisRow;
                return View(model);
            }
            catch (Exception e)
            {
                return View(model);
            }
        }
        public ActionResult status(string id)
        {
           try
            {
                var ID = int.Parse(id);
                var model = dbcontext.Employee_records.FirstOrDefault(m => m.ID == ID);
                var st = dbcontext.status.FirstOrDefault(m => m.ID ==model.status.ID);
                ViewBag.statue = dbcontext.status.ToList().Select(m=>new { code=m.approved_by });
                var my_model = new employeestate { status = st, empid = ID };
                //my_model.status.approved_by = User.Identity.GetUserName();
                //my_model.status.Rejected_by = User.Identity.GetUserName();
                //my_model.status.approved_bydate = DateTime.Now.Date;
                //my_model.status.Rejected_bydate = DateTime.Now.Date;

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
            catch(Exception e)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult status(employeestate model)
        {
            var sta = dbcontext.status.FirstOrDefault(m => m.ID == model.status.ID);
            var record = dbcontext.Employee_records.FirstOrDefault(m => m.ID == model.empid);
            if (model.check_status == check_status.Approved)
            {
                sta.approved_by = User.Identity.GetUserName();
                sta.approved_bydate = model.status.approved_bydate;
                record.check_status = check_status.Approved;
                record.name_state = nameof(check_status.Approved);
                dbcontext.SaveChanges();
            }
            //else if (model.check_status == check_status.Canceled)
            //{

            //    sta.cancaled_by = model.status.cancaled_by;
            //    sta.cancaled_bydate = model.status.cancaled_bydate;
            //    record.check_status = check_status.Canceled;
            //    record.name_state = nameof(check_status.Canceled);
            //    dbcontext.SaveChanges();
            //}
            //else if (model.check_status == check_status.created)
            //{
            //    sta.created_by = model.status.created_by;
            //    sta.created_bydate = model.status.created_bydate;
            //    record.check_status = check_status.created;
            //    record.name_state = nameof(check_status.created);
            //    dbcontext.SaveChanges();
            //}
            else if (model.check_status == check_status.Rejected)
            {
                sta.Rejected_by = User.Identity.GetUserName();
                sta.Rejected_bydate = model.status.Rejected_bydate;
                record.check_status = check_status.Rejected;
                record.name_state = nameof(check_status.Rejected);
                dbcontext.SaveChanges();
            }
            else if (model.check_status == check_status.Return_To_Review)
            {
                sta.return_to_reviewby = User.Identity.GetUserName();
                sta.return_to_reviewdate = model.status.return_to_reviewdate;
                record.check_status = check_status.Return_To_Review;
                record.name_state = nameof(check_status.Return_To_Review);
                dbcontext.SaveChanges();
            }

            return RedirectToAction("index");
        }
        public JsonResult Getalll(List<string> c)
        {
            try
            {
                dbcontext.Configuration.ProxyCreationEnabled = false;
                var model = dbcontext.Employee_records.ToList();
                foreach (var item in model)
                {
                    item.Effictive_date =Convert.ToDateTime(item.Effictive_date.ToShortDateString());
                    item.Employee_Profile = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == item.statID);

                }
              
                return Json(model);
            }
            catch(Exception e)
            {
                return Json(false);
            }
        }
        public JsonResult Getone(DateTime from, DateTime to, List<string> status)
        {
            try
            {
                dbcontext.Configuration.ProxyCreationEnabled = false;
                var nn = new List<check_status>();
                List<Employee_records> re1 = new List<Employee_records>();
                foreach (var item in status)
                {
                    if (item != "all")
                    {
                        nn.Add((check_status)Enum.Parse(typeof(check_status), item));
                    }
                }
                if (status[0] == "all")
                {
                    var req = dbcontext.Employee_records.Where(m => DateTime.Compare(m.Record_date, from) >= 0 && DateTime.Compare(m.Record_date, to) <= 0).ToList();
                    foreach (var itemm in req)
                    {
                        itemm.Employee_Profile = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == itemm.statID);

                    }
                    return Json(req);
                }
                else if (status[0] != "all" )
                {
                    var req = dbcontext.Employee_records.Where(m => DateTime.Compare(m.Record_date, from) >= 0 && DateTime.Compare(m.Record_date, to) <= 0).ToList();

                    foreach (var item in nn)
                    {
                        re1.AddRange(req.Where(m => m.check_status == item).ToList());
                    }
                    foreach (var itemm in re1)
                    {
                        itemm.Employee_Profile = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == itemm.statID);

                    }
                }
                
                return Json(re1);
            }
            catch (Exception e)
            {
                return Json(false);
            }
        }
        public JsonResult getallstatues()
        {
            var list = new List<string>();
            list.Add("created");
            list.Add("Canceled");
            list.Add("Rejected");
            list.Add("Approved");
            list.Add("Return_To_Review");
            return Json(list);
        }

    }
}