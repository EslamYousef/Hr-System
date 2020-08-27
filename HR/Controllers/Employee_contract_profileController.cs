using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HR.Models;
using System.Data.Entity.Infrastructure;
using HR.Models.Infra;
using HR.Models.ViewModel;
namespace HR.Controllers
{
    [Authorize(Roles = "Admin,personnel,personnelCards,Employee Profile")]
    public class Employee_contract_profileController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Employee_contract_profile
        public ActionResult Index(string id)
        {
            var ID = int.Parse(id);
            var new_model = dbcontext.Employee_contract_profile.Where(m => m.Employee_Profile.ID == ID).ToList();
            ViewBag.idemp = id;
            return View(new_model);
        }
        public ActionResult Create(string id)
        {
            ViewBag.To_Airport = dbcontext.Air_ports.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.From_Airport = dbcontext.Air_ports.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.Contract_Type = dbcontext.Contract_Type.ToList().Select(m => new { Code = m.Contract_Type_Code + "------[" + m.Contract_Type_Description + ']', ID = m.ID });
            ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.Approved_date = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.idemp = id;
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel);
            var model = dbcontext.Employee_contract_profile.ToList();
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
            var Employee_contract_profile = new Employee_contract_profile { Code = stru.Structure_Code + count, Employee_ProfileId = id, Approved_date = statis2, Contract_start_date = statis2, Contract_end_date = statis2, Medical_date = statis2 };
   
            return View(Employee_contract_profile);
        }
        [HttpPost]
        public ActionResult Create(Employee_contract_profile model, string command)
        {
            try
            {
                if (model.ContractTypeId == null) { model.ContractTypeId = "0"; }
                if (model.ApprovedbyId == null) { model.ApprovedbyId = "0"; }
                if (model.FromAirpotId == null) { model.FromAirpotId = "0"; }
                if (model.ToAirpotId == null) { model.ToAirpotId = "0"; }

                ViewBag.To_Airport = dbcontext.Air_ports.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.From_Airport = dbcontext.Air_ports.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Contract_Type = dbcontext.Contract_Type.ToList().Select(m => new { Code = m.Contract_Type_Code + "------[" + m.Contract_Type_Description + ']', ID = m.ID });
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Approved_date = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.idemp = model.Employee_ProfileId;

                if (ModelState.IsValid)
                {
                    var con = int.Parse(model.Employee_ProfileId);
                    var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == con);
                    var EmpObj = dbcontext.Employee_Profile.FirstOrDefault(a => a.ID == model.ID);

                    Employee_contract_profile record = new Employee_contract_profile();
                    var list = dbcontext.Employee_contract_profile.ToList();

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
                        //var te = list.LastOrDefault();
                        //te.Active = false;
                        //record.Active = true;
                    }
                    record.Code = model.Code;
               //     record.Contract = model.Contract;
                    record.Employment_type = model.Employment_type;
                    record.Contract_start_date = model.Contract_start_date;
                    record.Contract_end_date = model.Contract_end_date;
                    if (model.Contract_start_date > model.Contract_end_date)
                    {
                        TempData["Message"] = HR.Resource.Personnel.FromdatebiggerTodate;
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
                    record.Days_Balance = model.Days_Balance;
                    record.Days_Per_Period = model.Days_Per_Period;
                    record.Tickets_No = model.Tickets_No;
                    record.Tickets_Per_Period = model.Tickets_Per_Period;
                    record.Tickets_Class_Tpyeemp = model.Tickets_Class_Tpyeemp;
                    record.FromAirpotId = model.FromAirpotId;
                    record.ToAirpotId = model.ToAirpotId;
                    record.Air_ports = model.Air_ports;
                    record.Adult_Tickets_No = model.Adult_Tickets_No;
                    record.Child_Tickets_No = model.Child_Tickets_No;
                    record.Tickets_Class_Tpyefam = model.Tickets_Class_Tpyefam;
                    var empid = EmpObj.Code + "------" + EmpObj.Name;
                    record.Employee_ProfileId = model.Employee_ProfileId == null ? model.Employee_ProfileId = empid : model.Employee_ProfileId;
                    record.Employee_Profile = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == EmpObj.ID);
                    //   record.Employee_ProfileId = model.Employee_ProfileId;
                    //var Employee_ProfileId = int.Parse(model.Employee_ProfileId);
                    //record.Employee_Profile = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == Employee_ProfileId);
                    record.ContractTypeId = model.ContractTypeId;
                    record.ApprovedbyId = model.ApprovedbyId;
         
                    var pos = dbcontext.Employee_contract_profile.Add(record);
                    dbcontext.SaveChanges();

                    if (command == "Submit")
                    {
                        return RedirectToAction("edit", "Employee_Profile", new { id = EmpObj.ID });//id = int.Parse(record.Employee_ProfileId)
                    }
                    return RedirectToAction("Index", new { id = EmpObj.ID });
            }
                else
                {
                return View(model);
            }
            //    return View(model);
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
                ViewBag.To_Airport = dbcontext.Air_ports.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.From_Airport = dbcontext.Air_ports.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Contract_Type = dbcontext.Contract_Type.ToList().Select(m => new { Code = m.Contract_Type_Code + "------[" + m.Contract_Type_Description + ']', ID = m.ID });
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Approved_date = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
               

                var record = dbcontext.Employee_contract_profile.FirstOrDefault(m => m.ID == id);
                ViewBag.idemp = record.Employee_Profile.ID.ToString();
              
              
                if (record != null)
                {
                    return View(record);
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
        public ActionResult Edit(Employee_contract_profile model, string command)
        {
            try
            {
                if (model.ContractTypeId == null) { model.ContractTypeId = "0"; }
                if (model.ApprovedbyId == null) { model.ApprovedbyId = "0"; }
                if (model.FromAirpotId == null) { model.FromAirpotId = "0"; }
                if (model.ToAirpotId == null) { model.ToAirpotId = "0"; }

                ViewBag.To_Airport = dbcontext.Air_ports.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.From_Airport = dbcontext.Air_ports.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Contract_Type = dbcontext.Contract_Type.ToList().Select(m => new { Code = m.Contract_Type_Code + "------[" + m.Contract_Type_Description + ']', ID = m.ID });
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Approved_date = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                var EmpObj = dbcontext.Employee_Profile.FirstOrDefault(a => a.ID == model.Employee_Profile.ID);
              
                //   var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == prof);
                var record = dbcontext.Employee_contract_profile.FirstOrDefault(m => m.ID == model.ID);
                var list = dbcontext.Employee_contract_profile.Where(a => a.Active == true).ToList();
                var empid = EmpObj.Code + "------" + EmpObj.Name;
                record.Employee_ProfileId = model.Employee_ProfileId == null ? model.Employee_ProfileId = EmpObj.ID.ToString() : model.Employee_ProfileId;
                ViewBag.idemp = model.Employee_ProfileId;
                record.Employee_Profile = EmpObj;

                if (list != null)
                {
                    for (int i = 0; i < list.Count(); i++)
                    {
                        list[i].Active = false;
                    }
                    record.Active = true;
                }
                var emp = record.Employee_Profile;
                record.Code = model.Code;
             //   record.Contract = model.Contract;
                record.Employment_type = model.Employment_type;
                record.Contract_start_date = model.Contract_start_date;
                record.Contract_end_date = model.Contract_end_date;
                if (model.Contract_start_date > model.Contract_end_date)
                {
                    TempData["Message"] = HR.Resource.Personnel.FromdatebiggerTodate;
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
                record.Days_Balance = model.Days_Balance;
                record.Days_Per_Period = model.Days_Per_Period;
                record.Tickets_No = model.Tickets_No;
                record.Tickets_Per_Period = model.Tickets_Per_Period;
                record.Tickets_Class_Tpyeemp = model.Tickets_Class_Tpyeemp;
                record.FromAirpotId = model.FromAirpotId;
                record.ToAirpotId = model.ToAirpotId;
                record.Air_ports = model.Air_ports;
                record.Adult_Tickets_No = model.Adult_Tickets_No;
                record.Child_Tickets_No = model.Child_Tickets_No;
                record.Tickets_Class_Tpyefam = model.Tickets_Class_Tpyefam;
               
                record.ContractTypeId = model.ContractTypeId;
                record.ApprovedbyId = model.ApprovedbyId;
                dbcontext.SaveChanges();

                if (command == "Submit")
                {
                    return RedirectToAction("edit", "Employee_Profile", new { id = EmpObj.ID   }); //id = int.Parse(record.Employee_ProfileId)
                }
                return RedirectToAction("index", new { id = EmpObj.ID }); //id = model.Employee_ProfileId 
            }
            catch (DbUpdateException e)
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

                var record = dbcontext.Employee_contract_profile.FirstOrDefault(m => m.ID == id);
                ViewBag.idemp = record.Employee_Profile.ID.ToString();
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

            var record = dbcontext.Employee_contract_profile.FirstOrDefault(m => m.ID == id);
            ViewBag.idemp = record.Employee_Profile.ID.ToString();
            try
            {
                dbcontext.Employee_contract_profile.Remove(record);
                dbcontext.SaveChanges();
                return RedirectToAction("index", new { id = record.Employee_ProfileId });
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = HR.Resource.Basic.youcannotdeletethisRow;
                return View(record);
            }
            catch (Exception e)
            {
                return View();
            }
        }
    }
}