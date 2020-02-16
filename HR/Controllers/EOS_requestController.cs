using HR.Models;
using HR.Models.Infra;
using HR.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers
{
    [Authorize]
    public class EOS_requestController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: EOS_request
        public ActionResult Index()
        {
            //var an = dbcontext.Answer_EOS.ToList();
            //var an2 = dbcontext.check_list_EOS.ToList();
            var model = dbcontext.EOS_Request.ToList();
            return View(model);
        }
        public ActionResult Create()

        {
            try
            {
                var record = new EOS_Request();
                var my_model = new EOS_VM {selected_employee = 0,selected_EOS_group_interview=0 ,selected_EOS_group_checklist=0};
                ViewBag.employee = dbcontext.Employee_Profile.ToList().Select(m => new { Code = "" + m.Code + "-----[" + m.Full_Name + ']', ID = m.ID }).ToList();
                ViewBag.interview = dbcontext.EOS_Interview_Questions_Groups.ToList().Select(m => new { Code = "" + m.Questions_Group_Code + "-----[" + m.Description_of + ']', ID = m.ID }).ToList();
                ViewBag.checklist = dbcontext.Check_List_Item_Groups.ToList().Select(m => new { Code = "" + m.Group_Code + "-----[" + m.Description_Group + ']', ID = m.ID }).ToList();
                try
                {
                    var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel).Structure_Code;
                    var model = dbcontext.EOS_Request.ToList();
                    if (model.Count() == 0)
                    {
                        record.number = stru + "1";
                    }
                    else
                    {
                        record.number = stru + (model.LastOrDefault().ID + 1).ToString();
                    }
                    my_model.EOS = record;

                    return View(my_model);
                }
                catch (Exception e)
                {
                    return View(my_model);
                }
            }
            catch(Exception e)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult create(EOS_VM model,string command)
        {
            ViewBag.employee = dbcontext.Employee_Profile.ToList().Select(m => new { Code = "" + m.Code + "-----[" + m.Full_Name + ']', ID = m.ID }).ToList();
            ViewBag.interview = dbcontext.EOS_Interview_Questions_Groups.ToList().Select(m => new { Code = "" + m.Questions_Group_Code + "-----[" + m.Description_of + ']', ID = m.ID }).ToList();
            ViewBag.checklist = dbcontext.Check_List_Item_Groups.ToList().Select(m => new { Code = "" + m.Group_Code + "-----[" + m.Description_Group + ']', ID = m.ID }).ToList();

            try
            {
                if(model.selected_employee==0)
                {
                    TempData["Message"] =HR.Resource.pers_2.youmustchooseemployee;
                    return View(model);
                }
                var record = new EOS_Request();
                record = model.EOS;
                if (model.selected_employee > 0)
                {
                  

                    var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == model.selected_employee);
                    record.Employee = emp;
                    record.statID = emp.ID;
                }
                ///////////////status////////////////////////
                record.check_status = check_status.Report_as_ready;
                record.sss = record.check_status.GetTypeCode().ToString();
                var Date = Convert.ToDateTime("1/1/1900");
                var s = new status {statu=check_status.Report_as_ready, Type = Models.Infra.Type.employee_record, approved_bydate = Date, cancaled_bydate = Date, created_bydate = Date, Rejected_bydate = Date, report_as_ready_bydate = Date };
                var st = dbcontext.status.Add(s);
                dbcontext.SaveChanges();
                record.status = st;
                /////////////////checklist////////////////////
                List<check_list_EOS> check_list = new List<check_list_EOS>();
                if (model.selected_EOS_group_checklist > 0)
                {
                    /////
                    var group = dbcontext.Check_List_Item_Groups.FirstOrDefault(m => m.ID == model.selected_EOS_group_checklist);
                    record.Check_List_Item_Groups = group;
                    /////
                   
                    var items = group.check_items;
                    foreach (var item in items)
                    {
                        var item_of_chick = new check_list_EOS();
                        item_of_chick.item = item;
                        item_of_chick.interpolation = false;
                        
                        var inter = dbcontext.check_list_EOS.Add(item_of_chick);
                        dbcontext.SaveChanges();
                        check_list.Add(inter);
                    }
                }
                else
                {
                    record.Check_List_Item_Groups = null;
                }
                record.Check_List = check_list;
                ////////////////interview/////////////////////

                List<Answer_EOS> Answer_EOS_list = new List<Answer_EOS>();
                if (model.selected_EOS_group_interview>0)
                {
                    /////
                    var group = dbcontext.EOS_Interview_Questions_Groups.FirstOrDefault(m => m.ID == model.selected_EOS_group_interview);
                    record.EOS_group = group;
                    /////

                    var questions = group.questions;
                    foreach(var item in questions)
                    {
                        var interview = new Answer_EOS();
                        interview.question=item;
                        interview.answer = "";
                        interview.Notes = "";
                        var inter = dbcontext.Answer_EOS.Add(interview);
                        dbcontext.SaveChanges();
                        Answer_EOS_list.Add(inter);
                    }

                }
                else
                {
                    record.EOS_group = null; 
                }
                record.Answer_EOS_interview=Answer_EOS_list;
                ////////////////create//////////////////////
                record.req_date = record.Requset_date.ToShortDateString();
                record.eos_Date = record.date_of_eos_interview.ToShortDateString();
                record.name_state = nameof(check_status.Report_as_ready);
                var t = (EOS_type)(int)record.EOS_type;
                record.name_type = t.ToString();
                dbcontext.EOS_Request.Add(record);
                dbcontext.SaveChanges();
                ///////////////click EOS interview/////////
                if(command=="Submit2")
                {
                    return RedirectToAction("interview", new { id = record.ID});
                }
                else if (command == "Submit3")
                {
                    return RedirectToAction("chick", new { id = record.ID });
                }
                ////////////back to index /////////////////
                return RedirectToAction("index");
            }
            catch (DbUpdateException e)
            {
                TempData["Message"] = HR.Resource.organ.thiscodeIsalreadyexists;
                return View(model);
            }
            catch (Exception e)
            {
                return View();
            }
        }
        public ActionResult edit(string id,string error_message)
        {
            try
            {
                if(error_message!=null)
                {
                    TempData["Message"] = error_message;
                }
                ViewBag.employee = dbcontext.Employee_Profile.ToList().Select(m => new { Code = "" + m.Code + "-----[" + m.Full_Name + ']', ID = m.ID }).ToList();
                ViewBag.interview = dbcontext.EOS_Interview_Questions_Groups.ToList().Select(m => new { Code = "" + m.Questions_Group_Code + "-----[" + m.Description_of + ']', ID = m.ID }).ToList();
                ViewBag.checklist = dbcontext.Check_List_Item_Groups.ToList().Select(m => new { Code = "" + m.Group_Code + "-----[" + m.Description_Group + ']', ID = m.ID }).ToList();

                var ID = int.Parse(id);
                var model = dbcontext.EOS_Request.FirstOrDefault(m => m.ID == ID);
                var my_model = new EOS_VM ();
                my_model.EOS = model;
                if (model.Employee==null)
                {                  
                    my_model.selected_employee = 0;
                }
                else if (model.Employee != null)
                { 
                    my_model.selected_employee = model.Employee.ID;
                }
                if (model.EOS_group == null)
                { 
                    my_model.selected_EOS_group_interview = 0;
                }
                else if (model.EOS_group != null)
                {  
                    my_model.selected_EOS_group_interview = model.EOS_group.ID;
                }
                ////////////////////////////
                ///////////////////////////
                if (model.Check_List_Item_Groups == null)
                {
                    my_model.selected_EOS_group_checklist = 0;
                }
                else if (model.Check_List_Item_Groups != null)
                {
                    my_model.selected_EOS_group_checklist = model.Check_List_Item_Groups.ID;
                }
                return View(my_model);
            }
            catch (Exception e)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult edit(EOS_VM model,string command)
        {
            try
            {
                ViewBag.employee = dbcontext.Employee_Profile.ToList().Select(m => new { Code = "" + m.Code + "-----[" + m.Full_Name + ']', ID = m.ID }).ToList();
                ViewBag.interview = dbcontext.EOS_Interview_Questions_Groups.ToList().Select(m => new { Code = "" + m.Questions_Group_Code + "-----[" + m.Description_of + ']', ID = m.ID }).ToList();
                ViewBag.checklist = dbcontext.Check_List_Item_Groups.ToList().Select(m => new { Code = "" + m.Group_Code + "-----[" + m.Description_Group + ']', ID = m.ID }).ToList();
                if (model.selected_employee == 0)
                {
                    TempData["Message"] =HR.Resource.pers_2.youmustchooseemployee;
                    return View(model);
                }
                /////////////////////edit//////////////////////////
                var record = dbcontext.EOS_Request.FirstOrDefault(m => m.ID == model.EOS.ID);
                if (model.selected_employee > 0)
                {
                    var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == model.selected_employee);
                    record.Employee = emp;
                    record.statID = emp.ID;
                }
                else
                {
                    record.Employee = null;
                }
                record.are_the_employee_has_a_loan_or_advanced = model.EOS.are_the_employee_has_a_loan_or_advanced;
                record.are_the_settlement_transferred_to_payroll = model.EOS.are_the_settlement_transferred_to_payroll;
                record.Date_of_EOS = model.EOS.Date_of_EOS;
                record.date_of_eos_interview = model.EOS.date_of_eos_interview;
                record.date_of_settlement = model.EOS.date_of_settlement;
                record.EOS_type = model.EOS.EOS_type;
                record.last_Date_of_work_after_notice_period = model.EOS.last_Date_of_work_after_notice_period;
                record.last_work_day_before_request = model.EOS.last_work_day_before_request;
                record.note = model.EOS.note;
                record.Notice_period = model.EOS.Notice_period;
                record.Notice_period_type = model.EOS.Notice_period_type;
                record.Requset_date = model.EOS.Requset_date;
           
                //////////////////////checklist/////////////////////////
                if (model.selected_EOS_group_checklist == 0)
                {
                    var old_answer = record.Check_List;
                    record.Check_List = new List<check_list_EOS>();
                    dbcontext.check_list_EOS.RemoveRange(old_answer);
                    record.Check_List_Item_Groups = null;
                    dbcontext.SaveChanges();
                }
                else if (model.selected_EOS_group_checklist > 0)
                {
                    if (record.Check_List_Item_Groups == null || (model.selected_EOS_group_checklist != record.Check_List_Item_Groups.ID))
                    {
                        List<check_list_EOS> answer_list = new List<check_list_EOS>();
                        var old_answer = record.Check_List;
                        record.Check_List = new List<check_list_EOS>();
                        dbcontext.check_list_EOS.RemoveRange(old_answer);
                        dbcontext.SaveChanges();

                        record.Check_List_Item_Groups = dbcontext.Check_List_Item_Groups.FirstOrDefault(m => m.ID == model.selected_EOS_group_checklist);
                        var questions = dbcontext.Check_Lists_Items.Where(m => m.Check_List_Item_Groups.ID == record.Check_List_Item_Groups.ID);
                        foreach (var item in questions)
                        {
                            var answer = new check_list_EOS();
                            answer.item = item;
                            answer.interpolation = false;
                            var inter = dbcontext.check_list_EOS.Add(answer);
                            answer_list.Add(inter);
                        }
                        dbcontext.SaveChanges();
                        record.Check_List = answer_list;
                    }
                }

                dbcontext.SaveChanges();
                //////////////////////interview//////////////////////////
                if (model.selected_EOS_group_interview==0)
                {
                    var old_answer = record.Answer_EOS_interview;
                    record.Answer_EOS_interview= new List<Answer_EOS>();
                    dbcontext.Answer_EOS.RemoveRange(old_answer);
                    record.EOS_group = null;
                    dbcontext.SaveChanges();
                }
                else if(model.selected_EOS_group_interview>0)
                {
                    if (record.EOS_group==null||(model.selected_EOS_group_interview != record.EOS_group.ID))
                    {
                        List<Answer_EOS> answer_list = new List<Answer_EOS>();
                        var old_answer = record.Answer_EOS_interview;
                        record.Answer_EOS_interview = new List<Answer_EOS>();
                        dbcontext.Answer_EOS.RemoveRange(old_answer);
                        dbcontext.SaveChanges();

                        record.EOS_group = dbcontext.EOS_Interview_Questions_Groups.FirstOrDefault(m => m.ID == model.selected_EOS_group_interview);
                        var questions = dbcontext.Definition_of_EOS_Interview_Questions.Where(m => m.EOS_Interview_Questions_Groups.ID == record.EOS_group.ID);
                        foreach (var item in questions)
                        {
                            var answer = new Answer_EOS();
                            answer.question = item;
                            answer.Notes = "";
                            answer.answer = "";

                            var inter = dbcontext.Answer_EOS.Add(answer);
                           
                            answer_list.Add(inter);
                        }
                         dbcontext.SaveChanges();
                        record.Answer_EOS_interview = answer_list;

                    }
                }
                record.sss = record.check_status.GetTypeCode().ToString();
                record.req_date = record.Requset_date.ToShortDateString();
                record.eos_Date = record.date_of_eos_interview.ToShortDateString();
                var t = (EOS_type)(int)record.EOS_type;
                record.name_type = t.ToString();
                dbcontext.SaveChanges();
                if (command == "Submit2")
                {
                    return RedirectToAction("interview", new { id = record.ID });
                }
                else if (command == "Submit3")
                {
                    return RedirectToAction("chick", new { id = record.ID });
                }
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
                var model = dbcontext.EOS_Request.FirstOrDefault(m => m.ID == ID);
                return View(model);
            }
            catch(Exception e)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        [ActionName("delete")]
        public ActionResult method_delete(string id)
        {
            var ID = int.Parse(id);
            var model = dbcontext.EOS_Request.FirstOrDefault(m => m.ID == ID);
            var status = model.status;
            var inter = model.Answer_EOS_interview;
            var check = model.Check_List;
            try
            {
                dbcontext.check_list_EOS.RemoveRange(check);
                dbcontext.status.Remove(status);
                dbcontext.Answer_EOS.RemoveRange(inter);
                dbcontext.SaveChanges();
                dbcontext.EOS_Request.Remove(model);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = HR.Resource.organ.youcannotdeletethisRow;
                return View(model);
            }
            catch (Exception e)
            {
                TempData["Message"] = HR.Resource.organ.youcannotdeletethisRow;
                return View(model);
            }
        }

        public ActionResult chick(string id)
        {
            try
            {
                var EOS_id = int.Parse(id);
                var EOS = dbcontext.EOS_Request.FirstOrDefault(m => m.ID == EOS_id);
                if (EOS.Check_List_Item_Groups == null)
                {
                    return RedirectToAction("edit", new { id = EOS_id.ToString(), error_message = HR.Resource.pers_2.youmustchoosegroupofchicklistatfirst });
                }
                else
                {

                    return View(EOS);
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("edit", new { id = id, error_message = HR.Resource.pers_2.thereiserrorininterview});

            }
        }
        [HttpPost]
        public ActionResult chick(EOS_Request model)
        {
            try
            {
                var mm = model.Check_List;
                //var record = dbcontext.EOS_Request.FirstOrDefault(m => m.ID == model.ID);
                for (var i = 0; i < mm.Count(); i++)
                {
                    var id = mm[i].id;
                    var ans = dbcontext.check_list_EOS.FirstOrDefault(m => m.id == id);
                    ans.interpolation = model.Check_List[i].interpolation;
                    
                }

                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (Exception e)
            {
                return View(model);
            }
        }
        public ActionResult interview(string id)
        {
            try
            {
                var EOS_id = int.Parse(id);
                var EOS = dbcontext.EOS_Request.FirstOrDefault(m => m.ID == EOS_id);
                if (EOS.EOS_group == null)
                {
                    return RedirectToAction("edit", new { id = EOS_id.ToString(), error_message =HR.Resource.pers_2.youmustchoosegroupofchicklistatfirst });
                }
                else
                {
                 
                    return View(EOS);
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("edit", new { id =id, error_message = HR.Resource.pers_2.thereiserrorininterview });

            }
        }
        [HttpPost]
        public ActionResult interview(EOS_Request model)
        {
            try
            {
                var mm = model.Answer_EOS_interview;
                //var record = dbcontext.EOS_Request.FirstOrDefault(m => m.ID == model.ID);
                for(var i =0;i<mm.Count();i++)
                {
                    var id = mm[i].ID;
                   var ans= dbcontext.Answer_EOS.FirstOrDefault(m => m.ID ==id );
                    ans.answer = model.Answer_EOS_interview[i].answer;
                    ans.Notes = model.Answer_EOS_interview[i].Notes;
                }
            
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch(Exception e)
            {
                return View(model);
            }
        }
        public ActionResult status(string id)
        {
            try
            {
                var ID = int.Parse(id);
                var model = dbcontext.EOS_Request.FirstOrDefault(m => m.ID == ID);
                var st = model.status;
                ViewBag.statue = dbcontext.status.ToList().Select(m => new { code = m.approved_by });
                var my_model = new employeestate { status = st, empid = model.Employee.ID ,opertion_id=model.ID};
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
          //  var sta = dbcontext.status.FirstOrDefault(m => m.ID == model.status.ID);
            var record = dbcontext.EOS_Request.FirstOrDefault(m => m.ID == model.opertion_id);
            var sta = record.status;
            if (model.check_status == check_status.Approved)
            {
                sta.approved_by = model.status.approved_by;
                sta.approved_bydate = model.status.approved_bydate;
                sta.statu = check_status.Approved;
                record.check_status = check_status.Approved;
                record.sss = record.check_status.GetTypeCode().ToString();

                record.name_state = nameof(check_status.Approved);
                dbcontext.SaveChanges();


                var employee = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == model.empid);
                employee.Service_Information.EOS_date = record.Date_of_EOS;
                employee.Service_Information.Last_working_date = record.last_work_day_before_request;
                employee.Active = false;
                dbcontext.SaveChanges();
                ////    employee.Employee_Positions_Profile.EOS_reasons = record.EOS_type;
                var current_postion = dbcontext.Position_Information.FirstOrDefault(m => m.Primary_Position == true && m.Employee_Profile.ID == employee.ID);
                current_postion.End_of_service_date = record.last_Date_of_work_after_notice_period;
                current_postion.Last_working_date = record.last_work_day_before_request;
                current_postion.Primary_Position = false;
                dbcontext.SaveChanges();
                var slot_id =int.Parse(current_postion.SlotdescId);
                var slot = dbcontext.Slots.FirstOrDefault(m => m.ID ==slot_id);
                var job = dbcontext.job_title_cards.FirstOrDefault(m => m.ID == slot.job_title_cards.ID);////update vacant and hired number
                job.number_hired = job.number_hired - 1;
                job.number_vacant = job.number_vacant +1;
                dbcontext.SaveChanges();
                slot.Employee_Profile = null;
                slot.EmployeeID = null;
                slot.EmployeeName = null;
                dbcontext.SaveChanges();
         
            }
            else if (model.check_status == check_status.Canceled)
            {
                sta.cancaled_by = model.status.cancaled_by;
                sta.cancaled_bydate = model.status.cancaled_bydate;
                sta.statu = check_status.Canceled;
                record.check_status = check_status.Canceled;
                record.sss = record.check_status.GetType().ToString();
                record.name_state = nameof(check_status.Canceled);
                dbcontext.SaveChanges();
            }
            else if (model.check_status == check_status.created)
            {
                sta.created_by = model.status.created_by;
                sta.created_bydate = model.status.created_bydate;
                sta.statu = check_status.created;
                record.check_status = check_status.created;
                record.sss = record.check_status.GetType().ToString();
                record.name_state = nameof(check_status.created);
                dbcontext.SaveChanges();
            }
            else if (model.check_status == check_status.Rejected)
            {
                sta.Rejected_by = model.status.Rejected_by;
                sta.Rejected_bydate = model.status.Rejected_bydate;
                sta.statu = check_status.Rejected;
                record.check_status = check_status.Rejected;
                record.sss = record.check_status.GetType().ToString();
                record.name_state = nameof(check_status.Rejected);
                dbcontext.SaveChanges();
            }
            else if (model.check_status == check_status.Report_as_ready)
            {
                sta.report_as_ready_by = model.status.report_as_ready_by;
                sta.report_as_ready_bydate = model.status.report_as_ready_bydate;
                sta.statu = check_status.Report_as_ready;
                record.check_status = check_status.Report_as_ready;
                record.sss = record.check_status.GetHashCode().ToString();
                record.name_state = nameof(check_status.Report_as_ready);
                dbcontext.SaveChanges();
            }

            return RedirectToAction("index");
        }
        public JsonResult Getalll()
        {
            try
            {
                var mymodel = new eos_date();
                dbcontext.Configuration.ProxyCreationEnabled = false;
                var model = dbcontext.EOS_Request.ToList();
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
        public JsonResult Getone(DateTime from, DateTime to, List<string> status,List<string>type)
        {
            try
            {
                dbcontext.Configuration.ProxyCreationEnabled = false;
                //////
                /////
                var nn = new List<check_status>();
                foreach (var item in status)
                {
                    if (item != "all")
                    {
                        nn.Add((check_status)Enum.Parse(typeof(check_status), item));
                    }
                }
                /////
                ////
                var mm = new List<EOS_type>();
                foreach (var item in type)
                {
                    if (item != "all")
                    {
                        mm.Add((EOS_type)Enum.Parse(typeof(EOS_type), item));
                    }
                }
                var req = new List<EOS_Request>();
                List<EOS_Request> re1 = new List<EOS_Request>();
                if (status[0]=="all" && type[0] != "all")
                {
                    req = dbcontext.EOS_Request.Where(m => DateTime.Compare(m.Requset_date, from) >= 0 && DateTime.Compare(m.Requset_date, to) <= 0).ToList();
                    foreach (var item in mm)
                    {
                        re1.AddRange(req.Where(m => m.EOS_type == item).ToList());
                    }

                }
                else if (status[0] != "all" && type[0] == "all"){
                    req = dbcontext.EOS_Request.Where(m => DateTime.Compare(m.Requset_date, from) >= 0 && DateTime.Compare(m.Requset_date, to) <= 0 ).ToList();
                    foreach(var item in nn)
                    {
                        re1.AddRange(req.Where(m => m.check_status == item).ToList());
                    }
                }
                else if(status[0] == "all" && type[0] == "all")
                {
                    req = dbcontext.EOS_Request.Where(m => DateTime.Compare(m.Requset_date, from) >= 0 && DateTime.Compare(m.Requset_date, to) <= 0).ToList();
                    foreach (var itemm in req)
                    {
                        itemm.Employee = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == itemm.statID);

                    }
                    return Json(req);
                }
                else
                {
                    var uu = new List<EOS_Request>();
                    req = dbcontext.EOS_Request.Where(m => DateTime.Compare(m.Requset_date, from) >= 0 && DateTime.Compare(m.Requset_date, to) <= 0 ).ToList();
                    foreach (var item in nn)
                    {
                        uu.AddRange(req.Where(m => m.check_status == item).ToList());
                    }
                    foreach (var item in mm)
                    {
                        re1.AddRange(uu.Where(m => m.EOS_type == item).ToList());
                    }
                }
                //    var executed_list=  list.Except(status).ToList();

                foreach (var item in req)
                {
                    item.Date_of_EOS = Convert.ToDateTime(item.Date_of_EOS.ToShortDateString());
                   
                }
                foreach (var itemm in re1)
                {
                    itemm.Employee = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == itemm.statID);

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
        public JsonResult get_eos_type()
        {
            var list = new List<string>();
            list.Add("On_poinsor");
            list.Add("Resignation");
            list.Add("Termination");
            list.Add("Death");
            list.Add("External_loande_out");
            list.Add("End_of_cotract");
            list.Add("Early_EOS");
            list.Add("Early_EOS_for_medical");
            return Json(list);
        }
        public JsonResult getcheck(string id)
        {
            try
            {
                dbcontext.Configuration.ProxyCreationEnabled = false;
                var ID = int.Parse(id);
                var model = dbcontext.Check_List_Item_Groups.FirstOrDefault(m => m.ID ==ID);
                return Json(model);
            }
            catch(Exception e)
            {
                return Json(false);
            }
        }
    }
    
}