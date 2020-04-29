using HR.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HR.Models.Infra;
using HR.Models.ViewModel;

namespace HR.Controllers
{
    [Authorize]
    public class Employee_Positions_ProfileController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Employee_Positions_Profile
        public ActionResult Index(string id)
        {
          //  var Employee_Profile = dbcontext.Employee_Profile.ToList();
           // var Position_Information = dbcontext.Position_Information.ToList();
            //var model = from a in Employee_Profile
            //            join b in Position_Information on a.Employee_Positions_Profile.ID equals b.ID
            //            select new PositionEmployee_VM
            //            {
            //                fullname = a.Full_Name,
            //                code = a.Code,
            //                EmployeeId = a.ID,
            //                Position_Information = b
            //            };

            var ID = int.Parse(id);
            var new_model = dbcontext.Position_Information.Where(m => m.Employee_Profile.ID == ID).ToList();
            ViewBag.idemp = id;
            return View(new_model);
        }
        public ActionResult Create(string id)
        {

            ViewBag.job_desc = dbcontext.job_title_cards.ToList().Select(m => new { Code = m.Code + "------[" + m.name + ']', ID = m.ID });
            ViewBag.job_slot_desc = dbcontext.job_title_cards.ToList().Select(m => new { Code = m.num_slots + "------[" + m.name + ']', ID = m.ID });
            ViewBag.Default_location = dbcontext.work_location.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.location_desc = dbcontext.work_location.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.Job_level_grade = dbcontext.job_level_setup.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.Organization_Chart = dbcontext.Organization_Chart.ToList().Select(m => new { Code = m.Code + "------[" + m.unit_Description + ']', ID = m.ID });
            ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.idemp = id;
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
         DateTime statis2 = Convert.ToDateTime("1/1/1900");
            var ID = int.Parse(id);
                var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == ID);
                var x = new Position_Information { Code = stru.Structure_Code +count,Employee_ProfileId=id, From_date = statis2, To_date = statis2, End_of_service_date = statis2, Last_working_date = statis2 ,Primary_Position=true};
                var y = new Position_Transaction_Information { Position_transaction = statis2, Approved_date = statis2, Memo_date = statis2, Resolution_date = statis2 };
                var z = new Employee_Positions_Profile_VM
                { Position_Information = x, Position_Transaction_Information = y };
                return View(z);
           
        }
        [HttpPost]
        public ActionResult Create(Employee_Positions_Profile_VM model, string command)
        {
            try
            {
                if (model.Position_Information.job_descId == null) { model.Position_Information.job_descId = "0"; }
                if (model.Position_Information.Default_location_descId == null) { model.Position_Information.Default_location_descId = "0"; }
                if (model.Position_Information.Location_descId == null) { model.Position_Information.Location_descId = "0"; }
                if (model.Position_Information.Job_level_gradeId == null) { model.Position_Information.Job_level_gradeId = "0"; }
                if (model.Position_Information.SlotdescId == null) { model.Position_Information.SlotdescId = "0"; }
                if (model.Position_Information.Organization_ChartId == null) { model.Position_Information.Organization_ChartId = "0"; }


                ViewBag.job_desc = dbcontext.job_title_cards.ToList().Select(m => new { Code = m.Code + "------[" + m.name + ']', ID = m.ID });
                ViewBag.job_slot_desc = dbcontext.job_title_cards.ToList().Select(m => new { Code = m.num_slots + "------[" + m.name + ']', ID = m.ID });
                ViewBag.Default_location = dbcontext.work_location.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.location_desc = dbcontext.work_location.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Job_level_grade = dbcontext.job_level_setup.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Organization_Chart = dbcontext.Organization_Chart.ToList().Select(m => new { Code = m.Code + "------[" + m.unit_Description + ']', ID = m.ID });
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.idemp = model.Position_Information.Employee_ProfileId;

                if (ModelState.IsValid)
                {
                     var prof = int.Parse(model.Position_Information.Employee_ProfileId);
                    var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == prof);
                    //   var record = dbcontext.Position_Information.FirstOrDefault(m => m.ID == emp.Employee_Positions_Profile.ID);

                    
                    ///////
                    Position_Transaction_Information information = new Position_Transaction_Information();
                    information.Position_transaction = model.Position_Transaction_Information.Position_transaction;
                    information.Position_transaction_no = model.Position_Transaction_Information.Position_transaction_no;
                    information.Transaction_Type = model.Position_Transaction_Information.Transaction_Type;
                    information.Fixed_basic_salary_by = model.Position_Transaction_Information.Fixed_basic_salary_by;
                    information.Activity_number = model.Position_Transaction_Information.Activity_number; information.Position_transaction = model.Position_Transaction_Information.Position_transaction;
                    information.Memo_number = model.Position_Transaction_Information.Memo_number;
                    information.Resolution_number = model.Position_Transaction_Information.Resolution_number;
                    information.Approved_by = model.Position_Transaction_Information.Approved_by;
                    information.Recommended_by = model.Position_Transaction_Information.Recommended_by;
                    information.Approved_date = model.Position_Transaction_Information.Approved_date;
                    information.Memo_date = model.Position_Transaction_Information.Memo_date;
                    information.Resolution_date = model.Position_Transaction_Information.Resolution_date;
                   var info= dbcontext.Position_Transaction_Information.Add(information);
                    dbcontext.SaveChanges();
                    ///////

                    Position_Information record = new Position_Information();
                    var list = dbcontext.Position_Information.Where(a=>a.Employee_ProfileId== emp.ID.ToString()).ToList();
                    if (list.Count() == 0)
                    {
                        record.Primary_Position = true;
                    }
                    else
                    {
                        var te = list.Where(m=>m.Primary_Position==true);
                        foreach(var item in te )
                        {
                            item.Primary_Position = false;
                            item.End_of_service_date = DateTime.Now;
                            item.Last_working_date = DateTime.Now;
                            int slot_id = int.Parse(item.SlotdescId);
                            var slot = dbcontext.Slots.FirstOrDefault(m => m.ID == slot_id);
                            slot.EmployeeID = null;
                            slot.EmployeeName = null;
                            slot.Employee_Profile = null;
                            dbcontext.SaveChanges();
                        }
                        record.Primary_Position = true;
                    }
                    dbcontext.SaveChanges();
                    record.Code = model.Position_Information.Code;
                    //record.Primary_Position = model.Position_Information.Primary_Position;
                    record.From_date = model.Position_Information.From_date;
                    record.To_date = model.Position_Information.To_date;
                    if (model.Position_Information.From_date > model.Position_Information.To_date)
                    {
                        TempData["Message"] = HR.Resource.Personnel.FromdatebiggerTodate;
                        return View(model);
                    }
                    record.Years = model.Position_Information.Years;
                    record.Months = model.Position_Information.Months;
                    record.End_of_service_date = model.Position_Information.End_of_service_date;
                    record.Last_working_date = model.Position_Information.Last_working_date;
                    record.Commnets = model.Position_Information.Commnets;
                    record.working_system = model.Position_Information.working_system;
                    record.Position_status = model.Position_Information.Position_status;
                    record.EOS_reasons = model.Position_Information.EOS_reasons;

                    record.Employee_ProfileId = model.Position_Information.Employee_ProfileId;
                    var Employee_ProfileId = int.Parse(model.Position_Information.Employee_ProfileId);
                    record.Employee_Profile = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == Employee_ProfileId);
                    record.Code = model.Position_Information.Code;
                    record.job_descId = model.Position_Information.job_descId;
                    var job_descId = int.Parse(model.Position_Information.job_descId);
                    record.job_title_cards = dbcontext.job_title_cards.FirstOrDefault(m => m.ID == job_descId);
                    record.SlotdescId = model.Position_Information.SlotdescId;
                    if (model.Position_Information.SlotdescId != "0")
                    {
                        var SlotdescId = int.Parse(model.Position_Information.SlotdescId);
                        var my_slot = dbcontext.Slots.FirstOrDefault(m => m.ID == SlotdescId);
                        my_slot.Employee_Profile = emp;
                        my_slot.EmployeeName = emp.Full_Name;
                        my_slot.EmployeeID = emp.ID.ToString();
                        var job = dbcontext.job_title_cards.FirstOrDefault(m => m.ID == my_slot.job_title_cards.ID);
                        job.number_hired = job.number_hired + 1;
                        job.number_vacant = job.number_vacant - 1;
                        dbcontext.SaveChanges();
                    }

                    //    record.job_title_cards = dbcontext.job_title_cards.FirstOrDefault(m => m.ID == SlotdescId);
                    record.Default_location_descId = model.Position_Information.Default_location_descId;
                    var Default_location_descId = int.Parse(model.Position_Information.Default_location_descId);
                    record.work_location = dbcontext.work_location.FirstOrDefault(m => m.ID == Default_location_descId);
                    record.Location_descId = model.Position_Information.Location_descId;
                    var Location_descId = int.Parse(model.Position_Information.Location_descId);
                    record.work_location = dbcontext.work_location.FirstOrDefault(m => m.ID == Location_descId);
                    record.Job_level_gradeId = model.Position_Information.Job_level_gradeId;
                    var Job_level_gradeId = int.Parse(model.Position_Information.Job_level_gradeId);
                    record.Job_level_grade = dbcontext.Job_level_gradee.FirstOrDefault(m => m.ID == Job_level_gradeId);
                    record.Organization_ChartId = model.Position_Information.Organization_ChartId;
                    var Organization_ChartId = int.Parse(model.Position_Information.Organization_ChartId);
                    record.Organization_Chart = dbcontext.Organization_Chart.FirstOrDefault(m => m.ID == Organization_ChartId);
                    record.Position_Transaction_Information = info;
                   var pos= dbcontext.Position_Information.Add(record);
                    dbcontext.SaveChanges();
                    //        if (record.SlotdescId.Where(m => m.Employee_Profile != null).ToList().Select(m => m.Employee_Profile.ID).ToList())



                    if (command == "Submit")
                    {
                        return RedirectToAction("edit", "Employee_Profile", new { id = int.Parse(record.Employee_ProfileId) });
                    }
                    return RedirectToAction("Index",new { id=model.Position_Information.Employee_ProfileId});
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
        public ActionResult Edit(int id)
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
                //     var position = dbcontext.Position_Information.FirstOrDefault(a => a.ID == id);
           
                var record = dbcontext.Position_Information.FirstOrDefault(m => m.ID == id);
                var record2 = record.Position_Transaction_Information;
                ViewBag.idemp = record.Employee_Profile.ID.ToString();
                var vm = new Employee_Positions_Profile_VM { Position_Information = record, Position_Transaction_Information = record2 };
                if (vm != null)
                {
                    return View(vm);
                }
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
        public ActionResult Edit(Employee_Positions_Profile_VM model, string command)
        {
            try
            {
                if (model.Position_Information.job_descId == null) { model.Position_Information.job_descId = "0"; }
                if (model.Position_Information.Default_location_descId == null) { model.Position_Information.Default_location_descId = "0"; }
                if (model.Position_Information.Location_descId == null) { model.Position_Information.Location_descId = "0"; }
                if (model.Position_Information.Job_level_gradeId == null) { model.Position_Information.Job_level_gradeId = "0"; }
                if (model.Position_Information.SlotdescId == null) { model.Position_Information.SlotdescId = "0"; }
                if (model.Position_Information.Organization_ChartId == null) { model.Position_Information.Organization_ChartId = "0"; }


                ViewBag.job_desc = dbcontext.job_title_cards.ToList().Select(m => new { Code = m.Code + "------[" + m.name + ']', ID = m.ID });
                ViewBag.job_slot_desc = dbcontext.job_title_cards.ToList().Select(m => new { Code = m.num_slots + "------[" + m.name + ']', ID = m.ID });
                ViewBag.Default_location = dbcontext.work_location.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.location_desc = dbcontext.work_location.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Job_level_grade = dbcontext.job_level_setup.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Organization_Chart = dbcontext.Organization_Chart.ToList().Select(m => new { Code = m.Code + "------[" + m.unit_Description + ']', ID = m.ID });
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.idemp = model.Position_Information.Employee_ProfileId;
                var prof = int.Parse(model.Position_Information.Employee_ProfileId);
             //   var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == prof);
                var record = dbcontext.Position_Information.FirstOrDefault(m => m.ID ==model.Position_Information.ID);
                var emp = record.Employee_Profile;

                var list = dbcontext.Position_Information.Where(a => a.Primary_Position == true).ToList();
                list.LastOrDefault().Last_working_date = DateTime.Now;
                list.LastOrDefault().End_of_service_date = DateTime.Now;
                if (list != null)
                {
                    for (int i = 0; i < list.Count(); i++)
                    {
                        list[i].Primary_Position = false;
                    }
                    record.Primary_Position = true;
                }
                record.Code = model.Position_Information.Code;
                //record.Primary_Position = model.Position_Information.Primary_Position;
          //      model.Position_Information.From_date = record.From_date;
        //        model.Position_Information.To_date = record.To_date;
                record.From_date = model.Position_Information.From_date;
                record.To_date = model.Position_Information.To_date;
                if (model.Position_Information.From_date > model.Position_Information.To_date)
                {
                    TempData["Message"] = HR.Resource.Personnel.FromdatebiggerTodate;
                    return View(model);
                }
                record.Years = model.Position_Information.Years;
                record.Months = model.Position_Information.Months;
          //      record.End_of_service_date = model.Position_Information.End_of_service_date;
           //     record.Last_working_date = model.Position_Information.Last_working_date;
                record.Commnets = model.Position_Information.Commnets;
                record.working_system = model.Position_Information.working_system;
                record.Position_status = model.Position_Information.Position_status;
                record.EOS_reasons = model.Position_Information.EOS_reasons;

                record.Employee_ProfileId = model.Position_Information.Employee_ProfileId;
                var Employee_ProfileId = int.Parse(model.Position_Information.Employee_ProfileId);
                record.Code = model.Position_Information.Code;
                record.job_descId = model.Position_Information.job_descId;
                var job_descId = int.Parse(model.Position_Information.job_descId);
                record.job_title_cards = dbcontext.job_title_cards.FirstOrDefault(m => m.ID == job_descId);

                // var SlotdescId = int.Parse(model.Position_Information.SlotdescId);
                //   var t = dbcontext.Position_Information.Select(a => a.SlotdescId);
                if (model.Position_Information.SlotdescId == "0")
                {
                    ////remove 
                    if (record.SlotdescId != "0")
                    {
                        var IDslot = int.Parse(record.SlotdescId);
                        var old_slot = dbcontext.Slots.FirstOrDefault(m => m.ID == IDslot);
                        old_slot.Employee_Profile = null;
                        old_slot.EmployeeID = null;
                        old_slot.EmployeeName = null;
                        dbcontext.SaveChanges();
                        var job = dbcontext.job_title_cards.FirstOrDefault(m => m.ID == old_slot.job_title_cards.ID);
                        job.number_hired = job.number_hired - 1;
                        job.number_vacant = job.number_vacant + 1;
                        dbcontext.SaveChanges();
                    }
                }
                else
                {
                    var IDslot = int.Parse(model.Position_Information.SlotdescId);
                    var new_slot = dbcontext.Slots.FirstOrDefault(m => m.ID == IDslot);
                    if (new_slot.EmployeeID == null)
                    {
                        ///remove from old
                        if (record.SlotdescId != "0")
                        {
                            var old_IDslot = int.Parse(record.SlotdescId);
                            var old_slot = dbcontext.Slots.FirstOrDefault(m => m.ID == old_IDslot);
                            old_slot.Employee_Profile = null;
                            old_slot.EmployeeID = null;
                            old_slot.EmployeeName = null;
                            dbcontext.SaveChanges();
                            var jobb = dbcontext.job_title_cards.FirstOrDefault(m => m.ID == old_slot.job_title_cards.ID);
                            jobb.number_hired = jobb.number_hired - 1;
                            jobb.number_vacant = jobb.number_vacant + 1;
                            dbcontext.SaveChanges();
                        }
                        //else if(record.SlotdescId != "0")
                        //{
                        //    var old_IDslot = int.Parse(record.SlotdescId);
                        //    var old_slot = dbcontext.Slots.FirstOrDefault(m => m.ID == old_IDslot);
                        //    old_slot.Employee_Profile = null;
                        //    dbcontext.SaveChanges();
                        //}
                        /////add to new
                        new_slot.Employee_Profile = emp;
                        new_slot.EmployeeName = emp.Full_Name;
                        new_slot.EmployeeID = emp.ID.ToString();
                        var job = dbcontext.job_title_cards.FirstOrDefault(m => m.ID == new_slot.job_title_cards.ID);
                        job.number_hired = job.number_hired + 1;
                        job.number_vacant = job.number_vacant - 1;
                        dbcontext.SaveChanges();
                    }

                }
                record.SlotdescId = model.Position_Information.SlotdescId;

                //    record.job_title_cards = dbcontext.job_title_cards.FirstOrDefault(m => m.ID == SlotdescId);
                record.Default_location_descId = model.Position_Information.Default_location_descId;
                var Default_location_descId = int.Parse(model.Position_Information.Default_location_descId);
                record.work_location = dbcontext.work_location.FirstOrDefault(m => m.ID == Default_location_descId);
                record.Location_descId = model.Position_Information.Location_descId;
                var Location_descId = int.Parse(model.Position_Information.Location_descId);
                record.work_location = dbcontext.work_location.FirstOrDefault(m => m.ID == Location_descId);
                record.Job_level_gradeId = model.Position_Information.Job_level_gradeId;
                var Job_level_gradeId = int.Parse(model.Position_Information.Job_level_gradeId);
                record.Job_level_grade = dbcontext.Job_level_gradee.FirstOrDefault(m => m.ID == Job_level_gradeId);
                record.Organization_ChartId = model.Position_Information.Organization_ChartId;
                var Organization_ChartId = int.Parse(model.Position_Information.Organization_ChartId);
                record.Organization_Chart = dbcontext.Organization_Chart.FirstOrDefault(m => m.ID == Organization_ChartId);
                dbcontext.SaveChanges();

                //    var prof = int.Parse(model.Position_Transaction_Information.Employee_ProfileId);
                //       var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == prof);
                var information = dbcontext.Position_Transaction_Information.FirstOrDefault(m => m.ID == model.Position_Transaction_Information.ID);

                information.Position_transaction = model.Position_Transaction_Information.Position_transaction;
                information.Position_transaction_no = model.Position_Transaction_Information.Position_transaction_no;
                information.Transaction_Type = model.Position_Transaction_Information.Transaction_Type;
                information.Fixed_basic_salary_by = model.Position_Transaction_Information.Fixed_basic_salary_by;
                information.Activity_number = model.Position_Transaction_Information.Activity_number;
                information.Position_transaction = model.Position_Transaction_Information.Position_transaction;
                information.Memo_number = model.Position_Transaction_Information.Memo_number;
                information.Resolution_number = model.Position_Transaction_Information.Resolution_number;
                information.Approved_by = model.Position_Transaction_Information.Approved_by;
                information.Recommended_by = model.Position_Transaction_Information.Recommended_by;
                information.Approved_date = model.Position_Transaction_Information.Approved_date;
                information.Memo_date = model.Position_Transaction_Information.Memo_date;
                information.Resolution_date = model.Position_Transaction_Information.Resolution_date;
                dbcontext.SaveChanges();

                if (command == "Submit")
                {
                    return RedirectToAction("edit", "Employee_Profile", new { id = int.Parse(record.Employee_ProfileId) });
                }
                return RedirectToAction("index", new { id = model.Position_Information.Employee_ProfileId });
            }
            catch (DbUpdateException e)
            {
                TempData["Message"] = HR.Resource.Basic.thiscodeIsalreadyexists;
                return View(model);
            }
            catch (Exception e)
            { return View(model); }
        }
        public ActionResult Details(int emp_id)
        {
            try
            {
                var mo = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == emp_id);
                  var position = mo.Employee_Positions_Profile.FirstOrDefault(m=>m.Primary_Position==true);
                
                if (mo==null)
                {
                    var mes = HR.Resource.Personnel.Thisemployeenothaveprimaryposition;
                    return RedirectToAction("index", "personnel_transaction",new { mess = mes });
                }
                var id = mo.ID;
                ViewBag.job_desc = dbcontext.job_title_cards.ToList().Select(m => new { Code = m.Code + "------[" + m.name + ']', ID = m.ID });
                ViewBag.job_slot_desc = dbcontext.job_title_cards.ToList().Select(m => new { Code = m.num_slots + "------[" + m.name + ']', ID = m.ID });
                ViewBag.Default_location = dbcontext.work_location.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.location_desc = dbcontext.work_location.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Job_level_grade = dbcontext.job_level_setup.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Organization_Chart = dbcontext.Organization_Chart.ToList().Select(m => new { Code = m.Code + "------[" + m.unit_Description + ']', ID = m.ID });
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                //     var position = dbcontext.Position_Information.FirstOrDefault(a => a.ID == id);
                var record = position;

                var record2 = record.Position_Transaction_Information; /* dbcontext.Position_Transaction_Information.FirstOrDefault(m => m.ID == id);*/
                var vm = new Employee_Positions_Profile_VM { Position_Information = record, Position_Transaction_Information = record2 };
                if (vm != null)
                {
                    return View(vm);
                }
                else
                {
                    TempData["Message"] = HR.Resource.Basic.thereisnodata;
                    return RedirectToAction("index", "personnel_transaction");
                }
            }
            catch
            (Exception e)
            { return RedirectToAction("index", "personnel_transaction"); }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                var Position_Information = dbcontext.Position_Information.FirstOrDefault(m => m.ID == id);
                var Position_Transaction_Information = dbcontext.Position_Transaction_Information.FirstOrDefault(m => m.ID == id);

                ViewBag.idemp = Position_Information.Employee_Profile.ID.ToString();

                if (Position_Information != null)
                { return View(Position_Information); }
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
            var Position_Information = dbcontext.Position_Information.FirstOrDefault(m => m.ID == id);
            var Position_Transaction_Information = dbcontext.Position_Transaction_Information.FirstOrDefault(m => m.ID == id);
            var solt = dbcontext.Slots.FirstOrDefault(m => m.Employee_Profile.ID == Position_Information.ID);

            ViewBag.idemp = Position_Information.Employee_Profile.ID.ToString();

            try
            {
                var old_slot = dbcontext.Slots.FirstOrDefault(m => m.EmployeeID == Position_Information.Employee_Profile.ID.ToString());
                if (old_slot != null && old_slot.EmployeeID!=null)
                {
                    old_slot.Employee_Profile = null;
                    old_slot.EmployeeID = null;
                    old_slot.EmployeeName = null;
                    dbcontext.SaveChanges();
                }
                dbcontext.Position_Transaction_Information.Remove(Position_Transaction_Information);
                dbcontext.Position_Information.Remove(Position_Information);
                dbcontext.SaveChanges();
                return RedirectToAction("index", new { id = Position_Information.Employee_ProfileId });
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = HR.Resource.Basic.youcannotdeletethisRow;
                return View(Position_Information);
            }
            catch (Exception e)
            {
                return View();
            }
        }
    }
}