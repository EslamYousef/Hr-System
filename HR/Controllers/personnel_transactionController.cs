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
    public class personnel_transactionController : Controller
    {
        // GET: personnel_transaction
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        public ActionResult index()
        {
            try
            {
                var model = dbcontext.personnel_transaction.ToList();
                return View(model);
            }
            catch(Exception e)
            {
                return View();
            }
        }
         public ActionResult create()
        {
            try
            {
                ViewBag.job_desc = dbcontext.job_title_cards.ToList().Select(m => new { Code = m.Code + "------[" + m.name + ']', ID = m.ID });
                ViewBag.job_slot_desc = dbcontext.job_title_cards.ToList().Select(m => new { Code = m.num_slots + "------[" + m.name + ']', ID = m.ID });
                ViewBag.Default_location = dbcontext.work_location.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.location_desc = dbcontext.work_location.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Job_level_grade = dbcontext.job_level_setup.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Organization_Chart = dbcontext.Organization_Chart.ToList().Select(m => new { Code = m.Code + "------[" + m.unit_Description + ']', ID = m.ID });
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });

                var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel);
                var model = dbcontext.personnel_transaction.ToList();
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
                DateTime statis = Convert.ToDateTime("1/1/1900");
               var vm = new personnel_transaction {Number= stru.Structure_Code + count,End_of_service_date = statis,
                    From_date = statis,Last_working_date = statis,To_date = statis,
                    Position_transaction = statis,Approved_date = statis,Memo_date = statis,Resolution_date = statis
                };
                var mymodel = new TRANS_VM {personnel_transaction=vm,selected_employee=0 };
                //    var PositionInformation = new Position_Information();
                return View(mymodel);
            }
            catch (Exception e)
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult create(TRANS_VM model)
        {
            try
            {
                if (model.personnel_transaction.job_descId == null) { model.personnel_transaction.job_descId = "0"; }
                if (model.personnel_transaction.Default_location_descId == null) { model.personnel_transaction.Default_location_descId = "0"; }
                if (model.personnel_transaction.Location_descId == null) { model.personnel_transaction.Location_descId = "0"; }
                if (model.personnel_transaction.Job_level_gradeId == null) { model.personnel_transaction.Job_level_gradeId = "0"; }
                if (model.personnel_transaction.SlotdescId == null) { model.personnel_transaction.SlotdescId = "0"; }
                if (model.personnel_transaction.Organization_ChartId == null) { model.personnel_transaction.Organization_ChartId = "0"; }


                ViewBag.job_desc = dbcontext.job_title_cards.ToList().Select(m => new { Code = m.Code + "------[" + m.name + ']', ID = m.ID });
                ViewBag.job_slot_desc = dbcontext.job_title_cards.ToList().Select(m => new { Code = m.num_slots + "------[" + m.name + ']', ID = m.ID });
                ViewBag.Default_location = dbcontext.work_location.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.location_desc = dbcontext.work_location.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Job_level_grade = dbcontext.job_level_setup.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Organization_Chart = dbcontext.Organization_Chart.ToList().Select(m => new { Code = m.Code + "------[" + m.unit_Description + ']', ID = m.ID });
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });

                if (ModelState.IsValid)
                {
                  
                    var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == model.selected_employee);
                    var mymodel = new personnel_transaction();
                    mymodel = model.personnel_transaction;

                    ///////////////status////////////////////////
                    mymodel.check_status = check_status.Report_as_ready;
                    mymodel.ss = mymodel.check_status.GetTypeCode().ToString();
                    var Date = Convert.ToDateTime("1/1/1900");
                    var s = new status { statu = check_status.Report_as_ready, Type = Models.Infra.Type.employee_record, approved_bydate = Date, cancaled_bydate = Date, created_bydate = Date, Rejected_bydate = Date, report_as_ready_bydate = Date };
                    var st = dbcontext.status.Add(s);
                    dbcontext.SaveChanges();
                    mymodel.status = st;
                    mymodel.date = mymodel.transaction_date.ToShortDateString();
                    mymodel.Employee = emp;
                    dbcontext.personnel_transaction.Add(mymodel);
                    //var record = dbcontext.Position_Information.FirstOrDefault(m => m.ID == emp.Employee_Positions_Profile.ID);
                    //record.Primary_Position = model.Primary_Position;
                    //record.From_date = model.From_date;
                    //record.To_date = model.To_date;
                    //record.Years = model.Years;
                    //record.Months = model.Months;
                    //record.End_of_service_date = model.End_of_service_date;
                    //record.Last_working_date = model.Last_working_date;
                    //record.Commnets = model.Commnets;
                    //record.working_system = model.working_system;
                    //record.Position_status = model.Position_status;
                    //record.EOS_reasons = model.EOS_reasons;

                    //record.Employee_ProfileId = model.Employee_ProfileId;
                    //var Employee_ProfileId = int.Parse(model.Employee_ProfileId);
                    //record.Code = model.Code;
                    //record.job_descId = model.job_descId;
                    //var job_descId = int.Parse(model..job_descId);
                    //record.job_title_cards = dbcontext.job_title_cards.FirstOrDefault(m => m.ID == job_descId);
                    //record.SlotdescId = model.SlotdescId;
                    //var SlotdescId = int.Parse(model.SlotdescId);
                    //record.job_title_cards = dbcontext.job_title_cards.FirstOrDefault(m => m.ID == SlotdescId);
                    //record.Default_location_descId = model.Default_location_descId;
                    //var Default_location_descId = int.Parse(model.Default_location_descId);
                    //record.work_location = dbcontext.work_location.FirstOrDefault(m => m.ID == Default_location_descId);
                    //record.Location_descId = model.Location_descId;
                    //var Location_descId = int.Parse(model.Location_descId);
                    //record.work_location = dbcontext.work_location.FirstOrDefault(m => m.ID == Location_descId);
                    //record.Job_level_gradeId = model.Job_level_gradeId;
                    //var Job_level_gradeId = int.Parse(model.Job_level_gradeId);
                    //record.Job_level_grade = dbcontext.Job_level_gradee.FirstOrDefault(m => m.ID == Job_level_gradeId);
                    //record.Organization_ChartId = model.Organization_ChartId;
                    //var Organization_ChartId = int.Parse(model.Organization_ChartId);
                    //record.Organization_Chart = dbcontext.Organization_Chart.FirstOrDefault(m => m.ID == Organization_ChartId);
                    //dbcontext.SaveChanges();

                    //Position_Transaction_Information information = new Position_Transaction_Information();
                    //information.Position_transaction = model.Position_transaction;
                    //information.Position_transaction_no = model.Position_transaction_no;
                    //information.Transaction_Type = model.Transaction_Type;
                    //information.Fixed_basic_salary_by = model.Fixed_basic_salary_by;
                    //information.Activity_number = model.Activity_number; information.Position_transaction = model.Position_transaction;
                    //information.Memo_number = model.Memo_number;
                    //information.Resolution_number = model.Resolution_number;
                    //information.Approved_by = model.Approved_by;
                    //information.Recommended_by = model.Recommended_by;
                    //information.Approved_date = model.Approved_date;
                    //information.Memo_date = model.Memo_date;
                    //information.Resolution_date = model.Resolution_date;
                    dbcontext.SaveChanges();

                    //if (command == "Submit")
                    //{
                    //    return RedirectToAction("edit", "Employee_Profile", new { id = int.Parse(record.Employee_ProfileId) });
                    //}
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(model);
                }
            }
            catch (DbUpdateException e)
            {
                TempData["Message"] = "this code Is already exists";
                return View(model);
            }
            catch (Exception e)
            {
                return View(model);
            }
        }

        public ActionResult edit()
        {
            try
            {
                return View();
            }
            catch(Exception e)
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult edit(TRANS_VM model)
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                return View();
            }
        }
        public ActionResult delete(string id)
        {
            try
            {
                var ID = int.Parse(id);
                var request = dbcontext.personnel_transaction.FirstOrDefault(m => m.ID == ID);

                return View(request);
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
            var request = dbcontext.personnel_transaction.FirstOrDefault(m => m.ID == ID);

            try
            {
                var status = request.status;
                dbcontext.personnel_transaction.Remove(request);
                dbcontext.SaveChanges();

                //dbcontext.status.Remove(status);
                //dbcontext.SaveChanges();


                return RedirectToAction("index");
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = "this request can't delete";
                return View(request);
            }
            catch (Exception e)
            {
                return View(Request);
            }
        }



        public ActionResult status(string id)
        {
            try
            {
                var ID = int.Parse(id);
                var model = dbcontext.personnel_transaction.FirstOrDefault(m => m.ID == ID);
                var st = dbcontext.status.FirstOrDefault(m => m.ID == model.status.ID);
                ViewBag.statue = dbcontext.status.ToList().Select(m => new { code = m.approved_by });
                var my_model = new employeestate { status = st, empid = ID };
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
            var record = dbcontext.personnel_transaction.FirstOrDefault(m => m.ID == model.empid);
            if (model.check_status == check_status.Approved)
            {
                sta.approved_by = model.status.approved_by;
                sta.approved_bydate = model.status.approved_bydate;
                record.check_status = check_status.Approved;
                dbcontext.SaveChanges();
            }
            else if (model.check_status == check_status.Canceled)
            {

                sta.cancaled_by = model.status.cancaled_by;
                sta.cancaled_bydate = model.status.cancaled_bydate;
                record.check_status = check_status.Canceled;
                dbcontext.SaveChanges();
            }
            else if (model.check_status == check_status.created)
            {
                sta.created_by = model.status.created_by;
                sta.created_bydate = model.status.created_bydate;
                record.check_status = check_status.created;
                dbcontext.SaveChanges();
            }
            else if (model.check_status == check_status.Rejected)
            {
                sta.Rejected_by = model.status.Rejected_by;
                sta.Rejected_bydate = model.status.Rejected_bydate;
                record.check_status = check_status.Rejected;
                dbcontext.SaveChanges();
            }
            else if (model.check_status == check_status.Report_as_ready)
            {
                sta.report_as_ready_by = model.status.report_as_ready_by;
                sta.report_as_ready_bydate = model.status.report_as_ready_bydate;
                record.check_status = check_status.Report_as_ready;
                dbcontext.SaveChanges();
            }

            return RedirectToAction("index");
        }
        public JsonResult Getalll()
        {
            try
            {
                var model = dbcontext.personnel_transaction.ToList();
                return Json(model);
            }
            catch (Exception e)
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
                List<personnel_transaction> re1 = new List<personnel_transaction>();
                foreach (var item in status)
                {
                    if (item != "all")
                    {
                        nn.Add((check_status)Enum.Parse(typeof(check_status), item));
                    }
                }
                if (status[0] == "all")
                {
                    var req = dbcontext.personnel_transaction.Where(m => DateTime.Compare(m.transaction_date, from) >= 0 && DateTime.Compare(m.transaction_date, to) <= 0).ToList();
                    return Json(req);
                }
                else if (status[0] != "all")
                {
                    var req = dbcontext.personnel_transaction.Where(m => DateTime.Compare(m.transaction_date, from) >= 0 && DateTime.Compare(m.transaction_date, to) <= 0).ToList();

                    foreach (var item in nn)
                    {
                        re1.AddRange(req.Where(m => m.check_status == item).ToList());
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
            list.Add("Report_as_ready");
            return Json(list);
        }
    }
}