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
    public class personnel_transactionController : BaseController
    {
        // GET: personnel_transaction
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        public ActionResult index(string mess)
        {
            try
            {
                if(mess!=null)
                {
                    TempData["Message"] = mess;
                }
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

                //ViewBag.Employee_Profile 
                var all_e = new List<Employee_Profile>();
                var empll= dbcontext.Employee_Profile.Where(m=>m.Active==true).ToList();
                
                foreach (var item in empll)
                {
                    if (item.Employee_Positions_Profile.Count() > 0)
                    {
                        all_e.Add(item);
                    }
                }
                if(all_e.Count()>0)
                {
                    ViewBag.Employee_Profile = all_e.Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });

                }
                else
                {
                    ViewBag.Employee_Profile = new List<Employee_Profile>();
                }
                /////////////////////
                ////////////////////
                ///////////////////
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
                DateTime statis = Convert.ToDateTime("1/1/1900").Date;
                ViewBag.da = statis;
               var vm = new personnel_transaction {Number= stru.Structure_Code + count,End_of_service_date = statis,
                    Position_Transaction_number = stru.Structure_Code + count,
                    From_date = statis,Last_working_date = statis,To_date = statis,
                    Position_transaction = statis,Approved_date = statis,Memo_date = statis,Resolution_date = statis,
                    job_descId="0",Job_level_gradeId="0",Location_descId="0",Default_location_descId="0",Organization_ChartId="0",SlotdescId="0",
                    Transaction_type=transaction_type.assignment,transaction_date=DateTime.Now.Date,Effective_date=DateTime.Now.Date
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
                    if (model.selected_employee == 0)
                    {
                        TempData["Message"] = HR.Resource.pers_2.youmustchooseemployee;
                        return View(model);
                    }
                    else if (model.personnel_transaction.job_descId == "0" || model.personnel_transaction.job_descId == null)
                    {
                        TempData["Message"] =HR.Resource.pers_2.youmustchoosejobtitle;
                        return View(model);
                    }
                    else if (model.personnel_transaction.SlotdescId == "0" || model.personnel_transaction.job_descId == null)
                    {
                        TempData["Message"] = HR.Resource.pers_2.youmustchooseslot;
                        return View(model);
                    }
                    var mymodel = new personnel_transaction();
                   mymodel = model.personnel_transaction;

                    ///////////////status////////////////////////
                    mymodel.check_status = check_status.created;
                    mymodel.ss = mymodel.check_status.GetTypeCode().ToString();
                    var Date = Convert.ToDateTime("1/1/1900");
                    var s = new status { statu = check_status.created, Type = Models.Infra.Type.employee_record, approved_bydate = Date, cancaled_bydate = Date, created_bydate = DateTime.Now.Date, Rejected_bydate = Date, return_to_reviewdate = Date };
                    s.created_by = User.Identity.GetUserName();
                    var st = dbcontext.status.Add(s);
                    dbcontext.SaveChanges();
                    mymodel.status = st;
                    mymodel.date = mymodel.transaction_date.ToShortDateString();

                    mymodel.name_state =nameof(check_status.created);

                    var tt = (int)mymodel.Transaction_type;
                    var t = (transaction_type)(int)mymodel.Transaction_type;
                    mymodel.name_type = t.ToString();
                    if (model.selected_employee > 0)
                    {
                       
                        var emp= dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == model.selected_employee);
                        mymodel.Employee = emp;
                        mymodel.statID = emp.ID;
                    }
                    else
                    {
                        mymodel.Employee = null;
                    }

                    if (model.personnel_transaction.job_descId != "0")
                    {
                        mymodel.Job_level_gradeId = model.personnel_transaction.Job_level_gradeId;
                        var ID = int.Parse(model.personnel_transaction.job_descId);
                        mymodel.job_title_cards = dbcontext.job_title_cards.FirstOrDefault(m => m.ID == ID);
                    }
                    else
                    {
                        mymodel.job_descId = "0";
                        mymodel.job_title_cards = null;
                    }
                    if (model.personnel_transaction.Job_level_gradeId != "0")
                    {
                        var ID = int.Parse(model.personnel_transaction.Job_level_gradeId);
                        mymodel.job_level_setup = dbcontext.job_level_setup.FirstOrDefault(m => m.ID == ID);
                        mymodel.Job_level_gradeId = model.personnel_transaction.Job_level_gradeId;
                    }
                    else
                    {
                        mymodel.Job_level_gradeId = "0";
                        mymodel.job_title_cards = null;
                    }

                    if (model.personnel_transaction.Location_descId != "0")
                    {
                        var ID = int.Parse(model.personnel_transaction.Location_descId);
                        mymodel.work_location = dbcontext.work_location.FirstOrDefault(m => m.ID == ID);
                        mymodel.Location_descId = model.personnel_transaction.Job_level_gradeId;
                    }
                    else
                    {
                        mymodel.work_location = null;
                        mymodel.Location_descId = "0";
                    }
                    if (model.personnel_transaction.Organization_ChartId != "0")
                    {
                        var ID = int.Parse(model.personnel_transaction.Organization_ChartId);
                        mymodel.Organization_Chart = dbcontext.Organization_Chart.FirstOrDefault(m => m.ID == ID);
                        mymodel.Organization_ChartId = model.personnel_transaction.Organization_ChartId;
                    }
                    else
                    {
                        mymodel.Organization_Chart = null;
                        mymodel.Organization_ChartId = "0";
                    }
                    mymodel.name_state = nameof(check_status.created);
                  
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
                TempData["Message"] = HR.Resource.Basic.thiscodeIsalreadyexists  ;
                return View(model);
            }
            catch (Exception e)
            {
                return View(model);
            }
        }

        public ActionResult edit(string id)
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
                var ID = int.Parse(id);
                var model = dbcontext.personnel_transaction.FirstOrDefault(m => m.ID ==ID);
                var vm = new TRANS_VM { personnel_transaction = model };
                if (model.Employee!=null)
                {
                    vm.selected_employee = model.Employee.ID;
                }
               else
                {
                    vm.selected_employee = 0;
                }
                return View(vm);
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
                ViewBag.job_desc = dbcontext.job_title_cards.ToList().Select(m => new { Code = m.Code + "------[" + m.name + ']', ID = m.ID });
                ViewBag.job_slot_desc = dbcontext.job_title_cards.ToList().Select(m => new { Code = m.num_slots + "------[" + m.name + ']', ID = m.ID });
                ViewBag.Default_location = dbcontext.work_location.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.location_desc = dbcontext.work_location.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Job_level_grade = dbcontext.job_level_setup.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Organization_Chart = dbcontext.Organization_Chart.ToList().Select(m => new { Code = m.Code + "------[" + m.unit_Description + ']', ID = m.ID });
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                if (model.selected_employee == 0)
                {
                    TempData["Message"] = HR.Resource.pers_2.youmustchooseemployee;
                    return View(model);
                }
                else if (model.personnel_transaction.job_descId == "0"|| model.personnel_transaction.job_descId == null)
                {
                    TempData["Message"] = HR.Resource.pers_2.youmustchoosejobtitle;
                    return View(model);
                }
                else if (model.personnel_transaction.SlotdescId == "0" || model.personnel_transaction.job_descId == null)
                {
                    TempData["Message"] = HR.Resource.pers_2.youmustchooseslot;
                    return View(model);
                }
                var record = dbcontext.personnel_transaction.FirstOrDefault(m => m.ID == model.personnel_transaction.ID);
                ////////////////////////////////////////////////
                ////////////////////////////////////////////////
                ////////////////////////////////////////////////
                record.Activity_number = model.personnel_transaction.Activity_number;
                record.Approved_by = model.personnel_transaction.Approved_by;
                record.Approved_date = model.personnel_transaction.Approved_date;
               // record.check_status = model.personnel_transaction.status.statu;
                record.Commnets = model.personnel_transaction.Commnets;
                record.date = model.personnel_transaction.transaction_date.ToShortDateString();
                record.Fixed_basic_salary_by = model.personnel_transaction.Fixed_basic_salary_by;
                record.Default_location_descId = model.personnel_transaction.Default_location_descId;
                record.Effective_date = model.personnel_transaction.Effective_date;
                record.End_of_service_date = model.personnel_transaction.End_of_service_date;
                record.EOS_reasons = model.personnel_transaction.EOS_reasons;
                record.From_date = model.personnel_transaction.From_date;
                record.Last_working_date = model.personnel_transaction.Last_working_date;
                record.Memo_date = model.personnel_transaction.Memo_date;
                record.Memo_number = model.personnel_transaction.Memo_number;
                record.Months = model.personnel_transaction.Months;
                record.Number = model.personnel_transaction.Number;
                record.Position_status = model.personnel_transaction.Position_status;
                record.Position_transaction = model.personnel_transaction.Position_transaction;
                record.Recommended_by = model.personnel_transaction.Recommended_by;
                record.Resolution_date = model.personnel_transaction.Resolution_date;
                record.Resolution_number = model.personnel_transaction.Resolution_number;
                record.SlotdescId = model.personnel_transaction.SlotdescId;
                record.To_date = model.personnel_transaction.To_date;
                record.transaction_date = model.personnel_transaction.transaction_date;
                record.Transaction_type = model.personnel_transaction.Transaction_type;
                record.Transaction_Type_ = model.personnel_transaction.Transaction_Type_;
                record.working_system = model.personnel_transaction.working_system;
                record.work_location = model.personnel_transaction.work_location;
                record.Years = model.personnel_transaction.Years;
                var tt = (int)record.Transaction_type;
                var t=(transaction_type)(int)record.Transaction_type;
                record.name_type = t.ToString();
                if (model.selected_employee>0)
                {
                    var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == model.selected_employee);
                    record.Employee = emp;
                    record.statID = emp.ID;
                }
                else
                {
                    record.Employee = null;
                }
              
                if (model.personnel_transaction.job_descId != "0")
                {
                    record.Job_level_gradeId = model.personnel_transaction.Job_level_gradeId;
                    var ID = int.Parse(model.personnel_transaction.job_descId);
                    record.job_title_cards = dbcontext.job_title_cards.FirstOrDefault(m => m.ID == ID);
                }
                else
                {
                    record.job_descId = "0";
                    record.job_title_cards=null;
                }
                if (model.personnel_transaction.Job_level_gradeId != "0")
                {
                    var ID = int.Parse(model.personnel_transaction.Job_level_gradeId);
                    record.job_level_setup = dbcontext.job_level_setup.FirstOrDefault(m => m.ID == ID);
                    record.Job_level_gradeId = model.personnel_transaction.Job_level_gradeId;
                }
                else
                {
                    record.Job_level_gradeId = "0";
                    record.job_title_cards = null;
                }
           
                if (model.personnel_transaction.Location_descId != "0")
                {
                    var ID = int.Parse(model.personnel_transaction.Location_descId);
                    record.work_location = dbcontext.work_location.FirstOrDefault(m => m.ID == ID);
                    record.Location_descId = model.personnel_transaction.Job_level_gradeId;
                }
                else
                {
                    record.work_location = null;
                    record.Location_descId = "0";
                }
                if (model.personnel_transaction.Organization_ChartId != "0")
                {
                    var ID = int.Parse(model.personnel_transaction.Organization_ChartId);
                    record.Organization_Chart = dbcontext.Organization_Chart.FirstOrDefault(m => m.ID == ID);
                    record.Organization_ChartId = model.personnel_transaction.Organization_ChartId;
                }
                else
                {
                    record.Organization_Chart = null;
                    record.Organization_ChartId = "0";
                }
                ////////////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////////////////////////////////
                dbcontext.SaveChanges();
                return RedirectToAction("index");
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
                TempData["Message"] = HR.Resource.Basic.youcannotdeletethisRow ;
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
                var my_model = new employeestate { status = st, empid = model.Employee.ID,opertion_id=ID };
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
            var record = dbcontext.personnel_transaction.FirstOrDefault(m => m.ID == model.opertion_id);
            if (model.check_status == check_status.Approved)
            {
                sta.approved_by = User.Identity.GetUserName();
                sta.approved_bydate = model.status.approved_bydate;
                record.check_status = check_status.Approved;
                record.name_state = nameof(check_status.Approved);
                dbcontext.SaveChanges();
                var go = new TRANS_VM { personnel_transaction = record, selected_employee = model.empid };
                ///////////////update old position//////////
                var old_position = dbcontext.Position_Information.FirstOrDefault(m => m.Primary_Position == true && m.Employee_Profile.ID == go.selected_employee);
                old_position.Primary_Position = false;
                old_position.To_date = go.personnel_transaction.Last_working_date;
                dbcontext.SaveChanges();
                 //old_slot = new Slots();
                var slot_id = int.Parse(old_position.SlotdescId);
                var old_slot = dbcontext.Slots.FirstOrDefault(m => m.ID == slot_id);
                ////var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == old_slot.Employee_Profile.ID);
                var job = dbcontext.job_title_cards.FirstOrDefault(m => m.ID == old_slot.job_title_cards.ID);////update vacant and hired number
                job.number_hired = job.number_hired - 1;
                job.number_vacant = job.number_vacant + 1;
                dbcontext.SaveChanges();
                old_slot.Employee_Profile = null;
                dbcontext.SaveChanges();
                old_slot.EmployeeName = null;
                old_slot.EmployeeID = null;
                dbcontext.SaveChanges();

                add_new_postion(go);


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
        public JsonResult Getalll()
        {
            try
            {
                dbcontext.Configuration.ProxyCreationEnabled = false;
                var model = dbcontext.personnel_transaction.ToList();
                foreach (var item in model)
                {
                 
                    item.Employee = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == item.statID);

                }
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
                    foreach (var itemm in req)
                    {
                        itemm.Employee = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == itemm.statID);
                      
                    }
                    return Json(req);
                }
                else if (status[0] != "all")
                {
                    var req = dbcontext.personnel_transaction.Where(m => DateTime.Compare(m.transaction_date, from) >= 0 && DateTime.Compare(m.transaction_date, to) <= 0).ToList();
                    
                    foreach (var item in nn)
                    {
                        re1.AddRange(req.Where(m => m.check_status == item).ToList());
                    }
                    foreach(var itemm in re1)
                    {
                         itemm.Employee = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == itemm.statID);
                      
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

        public bool add_new_postion(TRANS_VM mmodel)
        {
            try
            {
                //////////////////////////////////////////////
                ///////////////add new position///////////////
                /////////////////////////////////////////////
                var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel);
                var model = dbcontext.Position_Information.ToList();
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
                var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == mmodel.selected_employee);
             

                ///////
                Position_Transaction_Information information = new Position_Transaction_Information();
                information.Position_transaction = mmodel.personnel_transaction.Position_transaction;
                information.Position_transaction_no = mmodel.personnel_transaction.Position_Transaction_number;
                information.Transaction_Type = mmodel.personnel_transaction.Transaction_Type_;
                information.Fixed_basic_salary_by = mmodel.personnel_transaction.Fixed_basic_salary_by;
                information.Activity_number = mmodel.personnel_transaction.Activity_number;
                information.Position_transaction = mmodel.personnel_transaction.Position_transaction;
                information.Memo_number = mmodel.personnel_transaction.Memo_number;
                information.Resolution_number = mmodel.personnel_transaction.Resolution_number;
                information.Approved_by = mmodel.personnel_transaction.Approved_by;
                information.Recommended_by = mmodel.personnel_transaction.Recommended_by;
                information.Approved_date = mmodel.personnel_transaction.Approved_date;
                information.Memo_date = mmodel.personnel_transaction.Memo_date;
                information.Resolution_date = mmodel.personnel_transaction.Resolution_date;
                var info = dbcontext.Position_Transaction_Information.Add(information);
                dbcontext.SaveChanges();
                ///////
                Position_Information record = new Position_Information();
                record.Code = stru.Structure_Code+count;
                record.Primary_Position =true;
                record.From_date = mmodel.personnel_transaction.Last_working_date;
                record.To_date = mmodel.personnel_transaction.To_date;
                record.Years = mmodel.personnel_transaction.Years;
                record.Months = mmodel.personnel_transaction.Months;
                record.End_of_service_date = mmodel.personnel_transaction.End_of_service_date;
                record.Last_working_date = mmodel.personnel_transaction.Last_working_date;
                record.Commnets = mmodel.personnel_transaction.Commnets;
                record.working_system = mmodel.personnel_transaction.working_system;
                record.Position_status = mmodel.personnel_transaction.Position_status;
                record.EOS_reasons = mmodel.personnel_transaction.EOS_reasons;
                record.Employee_ProfileId =mmodel.selected_employee.ToString();
                record.Employee_Profile = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == mmodel.selected_employee);
                record.job_descId = mmodel.personnel_transaction.job_descId;
                var job_descId = int.Parse(mmodel.personnel_transaction.job_descId);
                record.job_title_cards = dbcontext.job_title_cards.FirstOrDefault(m => m.ID == job_descId);
                record.SlotdescId = mmodel.personnel_transaction.SlotdescId;
                if (mmodel.personnel_transaction.SlotdescId != "0")
                {
                    var SlotdescId = int.Parse(mmodel.personnel_transaction.SlotdescId);
                    var my_slot = dbcontext.Slots.FirstOrDefault(m => m.ID == SlotdescId);
                    my_slot.Employee_Profile = emp;
                    my_slot.EmployeeName = emp.Full_Name;
                    my_slot.EmployeeID = emp.ID.ToString();
                    dbcontext.SaveChanges();
                    var job = dbcontext.job_title_cards.FirstOrDefault(m => m.ID == my_slot.job_title_cards.ID);////update vacant and hired number
                    job.number_hired = job.number_hired + 1;
                    job.number_vacant = job.number_vacant - 1;
                    dbcontext.SaveChanges();

                }

                record.Default_location_descId = mmodel.personnel_transaction.Default_location_descId;
                var Default_location_descId = int.Parse(mmodel.personnel_transaction.Default_location_descId);
                record.work_location = dbcontext.work_location.FirstOrDefault(m => m.ID == Default_location_descId);
                record.Location_descId = mmodel.personnel_transaction.Location_descId;
                var Location_descId = int.Parse(mmodel.personnel_transaction.Location_descId);
                record.work_location = dbcontext.work_location.FirstOrDefault(m => m.ID == Location_descId);
                record.Job_level_gradeId = mmodel.personnel_transaction.Job_level_gradeId;
                var Job_level_gradeId = int.Parse(mmodel.personnel_transaction.Job_level_gradeId);
                record.Job_level_grade = dbcontext.Job_level_gradee.FirstOrDefault(m => m.ID == Job_level_gradeId);
                record.Organization_ChartId = mmodel.personnel_transaction.Organization_ChartId;
                var Organization_ChartId = int.Parse(mmodel.personnel_transaction.Organization_ChartId);
                record.Organization_Chart = dbcontext.Organization_Chart.FirstOrDefault(m => m.ID == Organization_ChartId);
                record.Position_Transaction_Information = info;
                var pos = dbcontext.Position_Information.Add(record);
                dbcontext.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}