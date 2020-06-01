using HR.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HR.Models.Infra;
using HR.Models.ViewModel;
using HR.Models.CardPayroll;
using Antlr.Runtime;
using HR.Models.SetupPayroll;


namespace HR.Controllers
{
    [Authorize]
    public class Employee_Financial_ContractController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Employee_Financial_Contract
        public ActionResult Index(string id)
        {
            var ID = int.Parse(id);
            var new_model = dbcontext.Employee_Financial_Contract_Header.Where(m => m.Employee_Profile.ID == ID).ToList();
            ViewBag.idemp = id;
            return View(new_model);
        }
        public ActionResult Create(string id)
        {
            ViewBag.salaryitem = dbcontext.salary_code.Where(a => a.SourceEntry == 1).ToList().Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + ']', ID = m.ID });

            ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.idemp = id;
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel);
            var model = dbcontext.Employee_Financial_Contract_Header.ToList();
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
            DateTime statis2 = DateTime.Now;
            var ID = int.Parse(id);
            var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == ID);
            var x = new Employee_Financial_Contract_Header { Contract_Number = stru.Structure_Code + count, Employee_Code = emp.ID.ToString(), From_Date = statis2, To_Date = statis2, IsActive = true, Employee_Profile = emp };
            return View(x);

        }
        [HttpPost]
        public ActionResult Create(Employee_Financial_Contract_Header model, string command, FormCollection form)
        {
            try
            {
                //var prof = int.Parse(model.Employee_Profile.ID);
                var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == model.Employee_Profile.ID);

                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.salaryitem = dbcontext.salary_code.Where(a => a.SourceEntry == 1).ToList().Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + ']', ID = m.ID });
                var EmpObj = dbcontext.Employee_Profile.FirstOrDefault(a => a.ID == model.Employee_Profile.ID);
                Employee_Financial_Contract_Header record = new Employee_Financial_Contract_Header();
                var empid = EmpObj.Code + "------" + EmpObj.Name;
                record.Employee_Code = model.Employee_Code == null ? model.Employee_Code = EmpObj.ID.ToString() : model.Employee_Code;
                ViewBag.idemp = model.Employee_Code;
                record.Employee_Profile = EmpObj;
                if (ModelState.IsValid)
                {
                    var list = dbcontext.Employee_Financial_Contract_Header.Where(a => a.Employee_Code == emp.ID.ToString()).ToList();

                    //var list = dbcontext.Employee_Financial_Contract_Header.ToList();
                    if (list.Count() == 0)
                    {
                        record.IsActive = true;
                    }
                    else
                    {
                        var te = list.Where(m => m.IsActive == true);
                        foreach (var item in te)
                        {
                            item.IsActive = false;
                        }
                        record.IsActive = true;
                    }
                    dbcontext.SaveChanges();
                    record.Contract_Number = model.Contract_Number;
                    record.From_Date = model.From_Date;
                    record.To_Date = model.To_Date;
                    if (model.From_Date > model.To_Date)
                    {
                        TempData["Message"] = HR.Resource.Personnel.FromdatebiggerTodate;
                        return View(model);
                    }
                    var Header = dbcontext.Employee_Financial_Contract_Header.Add(record);
                    dbcontext.SaveChanges();
                    ///////////////////////////////////////////
                    var codeid = form["codeid"].Split(',');
                    var SalaryDes = form["SalaryDes"].Split(',');
                    var TypeE = form["TypeE"].Split(',');
                    var ValueType = form["ValueType"].Split(',');
                    var DefaultValue = form["DefaultValue"].Split(',');
                    //var Extendedco = form["Extendedco"].Split(',');
                    //var Extendedna = form["Extendedna"].Split(',');

                    for (var i = 0; i < codeid.Length; i++)
                    {
                        if (codeid[i] != "")
                        {
                            var new_details = new Employee_Financial_Contract_Detail { Contract_Number = Header.ID.ToString(), Created_By = User.Identity.Name, Created_Date = DateTime.Now.Date, SalaryCodeID = codeid[i], SalaryCodeValue = double.Parse(DefaultValue[i]), Salarycodedescription = SalaryDes[i], Type = TypeE[i], ValueType = ValueType[i]/*, ExtendedFields_Code = Extendedco[i], ExtendedFields_Desc = Extendedna[i] */};
                            dbcontext.Employee_Financial_Contract_Detail.Add(new_details);
                            dbcontext.SaveChanges();
                        }
                    }

                    if (command == "Submit")
                    {
                        return RedirectToAction("edit", "Employee_Profile", new { id = int.Parse(record.Employee_Code) });
                    }
                    return RedirectToAction("Index", new { id = model.Employee_Code });
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
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.salaryitem = dbcontext.salary_code.Where(a => a.SourceEntry == 1).ToList().Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + ']', ID = m.ID });

                var old_model = dbcontext.Employee_Financial_Contract_Header.FirstOrDefault(m => m.ID == id);
                var emp = old_model.Employee_Profile;

                var headera = new VMFin {Employee_Financial_Contract_Header = old_model };

                ViewBag.idemp = old_model.Employee_Profile.ID.ToString();
                var old_details = dbcontext.Employee_Financial_Contract_Detail.Where(m => m.Contract_Number == old_model.ID.ToString()).ToList();
                var new_model = new VMFin { Employee_Financial_Contract_Detail = old_details, Employee_Financial_Contract_Header = old_model };
                return View(new_model);
            }
            catch (Exception e)
            { return View(); }
        }
        [HttpPost]
        public ActionResult Edit(VMFin model, string command, FormCollection form)
        {
            try
            {
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.salaryitem = dbcontext.salary_code.Where(a => a.SourceEntry == 1).ToList().Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + ']', ID = m.ID });
                //   var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == prof);
                var record = dbcontext.Employee_Financial_Contract_Header.FirstOrDefault(m => m.ID == model.Employee_Financial_Contract_Header.ID);
                var emp = record.Employee_Profile;
                ViewBag.idemp = model.Employee_Financial_Contract_Header.Contract_Number;

                var list = dbcontext.Employee_Financial_Contract_Header.Where(a => a.IsActive == true).ToList();
              
                if (list != null)
                {
                    for (int i = 0; i < list.Count(); i++)
                    {
                        list[i].IsActive = false;
                    }
                    record.IsActive = true;
                }
                dbcontext.SaveChanges();
                record.Contract_Number = model.Employee_Financial_Contract_Header.Contract_Number;
                record.From_Date = model.Employee_Financial_Contract_Header.From_Date;
                record.To_Date = model.Employee_Financial_Contract_Header.To_Date;
                //if (model.Employee_Financial_Contract_Header.From_Date > model.Employee_Financial_Contract_Header.To_Date)
                //{
                //    TempData["Message"] = HR.Resource.Personnel.FromdatebiggerTodate;
                //    return View(model);
                //}
                dbcontext.SaveChanges();
                ///////////////////////////////////////////
                ///
                var update_details = dbcontext.Employee_Financial_Contract_Detail.Where(m => m.Contract_Number == record.ID.ToString()).ToList();
                dbcontext.Employee_Financial_Contract_Detail.RemoveRange(update_details);
                dbcontext.SaveChanges();

                var codeid = form["codeid"].Split(',');
                var SalaryDes = form["SalaryDes"].Split(',');
                var TypeE = form["TypeE"].Split(',');
                var ValueType = form["ValueType"].Split(',');
                var DefaultValue = form["DefaultValue"].Split(',');
                var Extendedco = form["Extendedco"].Split(',');
                var Extendedna = form["Extendedna"].Split(',');

                for (var i = 0; i < codeid.Length; i++)
                {
                    if (codeid[i] != "")
                    {
                        var new_details = new Employee_Financial_Contract_Detail { Contract_Number = record.ID.ToString(), Created_By = User.Identity.Name, Created_Date = DateTime.Now.Date, SalaryCodeID = codeid[i], SalaryCodeValue = double.Parse(DefaultValue[i]), Salarycodedescription = SalaryDes[i], Type = TypeE[i], ValueType = ValueType[i], ExtendedFields_Code = Extendedco[i], ExtendedFields_Desc = Extendedna[i] };
                        dbcontext.Employee_Financial_Contract_Detail.Add(new_details);
                        dbcontext.SaveChanges();
                    }
                }

                if (command == "Submit")
                {
                    return RedirectToAction("edit", "Employee_Profile", new { id = int.Parse(record.Employee_Code) });
                }
                return RedirectToAction("Index", new { id = record.Employee_Code });
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
                var Employee_Financial = dbcontext.Employee_Financial_Contract_Header.FirstOrDefault(m => m.ID == id);
                //var Employee_FinancialDe = dbcontext.Employee_Financial_Contract_Detail.FirstOrDefault(m => m.ID == id);

                ViewBag.idemp = Employee_Financial.Employee_Profile.ID.ToString();

                if (Employee_Financial != null)
                { return View(Employee_Financial); }
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
        public ActionResult Deletemethod(string id)
        {
            var ID = int.Parse(id);
            var Employee_Financial = dbcontext.Employee_Financial_Contract_Header.FirstOrDefault(m => m.ID == ID);
            var details = dbcontext.Employee_Financial_Contract_Detail.Where(m => m.Contract_Number == id);

            ViewBag.idemp = Employee_Financial.Employee_Profile.ID.ToString();
            try
            {
                dbcontext.Employee_Financial_Contract_Detail.RemoveRange(details);
                dbcontext.SaveChanges();
                dbcontext.Employee_Financial_Contract_Header.Remove(Employee_Financial);
                dbcontext.SaveChanges();
                return RedirectToAction("index", new { id = Employee_Financial.Employee_Code });
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = HR.Resource.Basic.youcannotdeletethisRow;
                return View(Employee_Financial);
            }
            catch (Exception e)
            {
                return View();
            }
        }
        //public class VMFin
        //{
        //    public Employee_Financial_Contract_Header Header { get; set; }
        //    public List<Employee_Financial_Contract_Detail> Employee_Financial_Contract_Detail { get; set; }
        //}
        //public class FinHeaders
        //{
        //    public Employee_Financial_Contract_Header Employee_Financial_Contract_Headeraa { get; set; }
        //}
        }
}