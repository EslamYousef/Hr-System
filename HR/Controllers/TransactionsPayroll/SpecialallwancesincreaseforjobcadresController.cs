using Antlr.Runtime;
using HR.Models;
using HR.Models.Infra;
using HR.Models.SetupPayroll;
using HR.Models.TransactionsPayroll;
using HR.Models.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers.TransactionsPayroll
{
    [Authorize]
    public class SpecialallwancesincreaseforjobcadresController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Specialallwancesincreaseforjobcadres
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Employee_Payroll_Transactions model, FormCollection form, string[] joblevelgrade, string[] JoblevelClass, string[] JobClass)
        {
            try
            {
                var Month = form["Month"].Split(',');
                var Year = form["Year"].Split(',');
                var percentage = form["percentage"].Split(',');

               
                if (joblevelgrade[0] == "1")
                {
                    var Joblevelgrade = dbcontext.Job_level_gradee.ToList();
                    foreach (var item in Joblevelgrade)
                    {
                        int ID = item.ID;
                        var min_basic_salary = item.min_basic_salary;
                        double year = double.Parse(Year[0]);
                        double month = double.Parse(Month[0]);
                        double Percentage = double.Parse(percentage[0]);
                        var Allowance = (min_basic_salary * Percentage)/100 ;
                        double newsalary = (Allowance + min_basic_salary);
                        var new_details = new special_allowance_job_level_grade { Job_level_gradeID = ID, Year = year ,Month = month ,Percentage = Percentage ,Allowance_amount = Allowance  , pervious_basic = min_basic_salary , new_basic_sallary = newsalary };
                        dbcontext.special_allowance_job_level_grade.Add(new_details);
                        dbcontext.SaveChanges();
                        item.min_basic_salary = newsalary;
                        dbcontext.SaveChanges();
                    }
                }
                if (JoblevelClass[0] == "1")
                {
                    var jobleveclass = dbcontext.Job_level_class.ToList();
                    foreach (var item in jobleveclass)
                    {
                        int ID = item.ID;
                        var min_basic_salary = item.min_basic_salary;
                        double year = double.Parse(Year[0]);
                        double month = double.Parse(Month[0]);
                        double Percentage = double.Parse(percentage[0]);
                        var Allowance = (min_basic_salary * Percentage) / 100;
                        double newsalary = (Allowance + min_basic_salary);
                        var new_details = new special_allowance_job_level_class { Job_level_classID = ID, Year = year, Month = month, Percentage = Percentage, Allowance_amount = Allowance, pervious_basic = min_basic_salary, new_basic_sallary = newsalary };
                        dbcontext.special_allowance_job_level_class.Add(new_details);
                        dbcontext.SaveChanges();
                        item.min_basic_salary = newsalary;
                        dbcontext.SaveChanges();
                    }
                }
                if (JobClass[0] == "1")
                {
                    var joblevelsetup = dbcontext.job_level_setup.ToList();
                    foreach (var item in joblevelsetup)
                    {
                        int ID = item.ID;
                        var min_basic_salary = item.min_basic_salary;
                        double year = double.Parse(Year[0]);
                        double month = double.Parse(Month[0]);
                        double Percentage = double.Parse(percentage[0]);
                        var Allowance = (min_basic_salary * Percentage) / 100;
                        double newsalary = (Allowance + min_basic_salary);
                        var new_details = new special { job_level_setupID = ID, Year = year, Month = month, Percentage = Percentage, Allowance_amount = Allowance, pervious_basic = min_basic_salary, new_basic_sallary = newsalary };
                        dbcontext.special.Add(new_details);
                        dbcontext.SaveChanges();
                        item.min_basic_salary = newsalary;
                        dbcontext.SaveChanges();
                    }
                }

                return RedirectToAction("index");
            }
            catch (Exception e)
            {
                return View(model);
            }
        }

    }
}