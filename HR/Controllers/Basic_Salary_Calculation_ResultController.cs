using HR.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HR.Models.Infra;
using HR.Models.Application;

namespace HR.Controllers
{
    [Authorize(Roles = "Admin,Recuirtment,RecuirtmentCards,Applications Rec")]
    public class Basic_Salary_Calculation_ResultController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Basic_Salary_Calculation_Result
        public ActionResult Create(string id)
        {
            var ID = int.Parse(id);
            var App = dbcontext.Application.FirstOrDefault(a => a.ID == ID).Applicant_ProfileId;
            var AppId = int.Parse(App);
            ViewBag.ApplicationApp = dbcontext.Applicant_Profile.FirstOrDefault(a => a.ID == AppId).Full_Name;
            ViewBag.ApplicationCode = dbcontext.Application.FirstOrDefault(a => a.ID == ID).Code;
            ViewBag.idemp = id;
            DateTime statis = DateTime.Now;
            var com = dbcontext.Application.FirstOrDefault(m => m.ID == ID);
            var Basic_Salary_Calculation_Result = new Basic_Salary_Calculation_Result { Application = com, ID = com.ID,Basic_Salary_A = 0 , Insurance_Basic_Salary = 0 , Insurance_Variable_Salary = 0 , Other_Allowance = 0 , Start_Basic_Salary = 0 , Total_Basic_Salary = 0 , Total_Execluded_Allowances = 0 , Total_Included_Allowances = 0 , Total_Remuneration = 0 };
            return View(Basic_Salary_Calculation_Result);
        }
        [HttpPost]
        public ActionResult Create(Basic_Salary_Calculation_Result model, string Command, string id2)
        {
            try
            {
                ViewBag.idemp = id2;
                //if (ModelState.IsValid)
                //{
                Basic_Salary_Calculation_Result record = new Basic_Salary_Calculation_Result();
                //record.Code = model.Code;
                record.ApplicantId = int.Parse(id2);
                record.Start_Basic_Salary = model.Start_Basic_Salary;
                record.Basic_Salary_A = model.Basic_Salary_A;
                record.Other_Allowance = model.Other_Allowance;
                record.Total_Included_Allowances = model.Total_Included_Allowances;
                record.Total_Execluded_Allowances = model.Total_Execluded_Allowances;
                record.Total_Basic_Salary = model.Total_Basic_Salary;
                record.Total_Remuneration = model.Total_Remuneration;
                record.Insurance_Basic_Salary = model.Insurance_Basic_Salary;
                record.Insurance_Variable_Salary = model.Insurance_Variable_Salary;
                dbcontext.Basic_Salary_Calculation_Result.Add(record);
                dbcontext.SaveChanges();
                if (Command == "Submit")
                {
                    return RedirectToAction("Edit", "Application", new { id = id2 });
                }
                //}
                return RedirectToAction("Index", "Application", new { id = id2 });
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
                var ID = int.Parse(id);
                var App = dbcontext.Application.FirstOrDefault(a => a.ID == ID).Applicant_ProfileId;
                var AppId = int.Parse(App);
                ViewBag.ApplicationApp = dbcontext.Applicant_Profile.FirstOrDefault(a => a.ID == AppId).Full_Name;
                ViewBag.ApplicationCode = dbcontext.Application.FirstOrDefault(a => a.ID == ID).Code;

                var record = dbcontext.Basic_Salary_Calculation_Result.FirstOrDefault(m => m.ApplicantId == ID);

                if (record != null)
                {
                    ViewBag.idemp = ID;
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
        public ActionResult Edit(Basic_Salary_Calculation_Result model, string Command, string id2)
        {
            try
            {
                ViewBag.idemp = id2;
                var ID = int.Parse(id2);
                var record = dbcontext.Basic_Salary_Calculation_Result.FirstOrDefault(m => m.ApplicantId == ID);

                record.ApplicantId = int.Parse(id2);
                record.Start_Basic_Salary = model.Start_Basic_Salary;
                record.Basic_Salary_A = model.Basic_Salary_A;
                record.Other_Allowance = model.Other_Allowance;
                record.Total_Included_Allowances = model.Total_Included_Allowances;
                record.Total_Execluded_Allowances = model.Total_Execluded_Allowances;
                record.Total_Basic_Salary = model.Total_Basic_Salary;
                record.Total_Remuneration = model.Total_Remuneration;
                record.Insurance_Basic_Salary = model.Insurance_Basic_Salary;
                record.Insurance_Variable_Salary = model.Insurance_Variable_Salary;
                dbcontext.SaveChanges();
                if (Command == "Submit")
                {
                    return RedirectToAction("Edit", "Application", new { id = id2 });
                }
                //}
                return RedirectToAction("Index", "Application", new { id = id2 });
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
    }
}