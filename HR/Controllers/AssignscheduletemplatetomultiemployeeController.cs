using Antlr.Runtime;
using HR.Models;
using HR.Models.Infra;
using HR.Models.SetupPayroll;
using HR.Models.Time_management;
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

namespace HR.Controllers
{
    [Authorize]
    public class AssignscheduletemplatetomultiemployeeController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Assignscheduletemplatetomultiemployee
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            try
            {
                List<SelectListItem> items = new List<SelectListItem>();
                items.Insert(0, (new SelectListItem
                {
                    Text = "All employee",
                    Value = "1",
                }));
                items.Insert(1, (new SelectListItem
                {
                    Text = "unit",
                    Value = "2",
                }));
                items.Insert(2, (new SelectListItem
                {
                    Text = "nationality",
                    Value = "3",
                }));
                items.Insert(3, (new SelectListItem
                {
                    Text = "Work location",
                    Value = "4",
                }));
                items.Insert(4, (new SelectListItem
                {
                    Text = "Cost center",
                    Value = "5",

                }));
                items.Insert(5, (new SelectListItem
                {
                    Text = "Cadre level",
                    Value = "6",

                }));
                ViewBag.Object = new SelectList(items, "Value", "Text");
                ViewBag.Template = dbcontext.Template.ToList().Select(m => new { Code = m.TemplateCode + "-[" + m.TemplateDescription + ']', ID = m.ID });

                var model = new Template();

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult Create(Template model, FormCollection form ,DateTime? fromDDD , string toD)
        {
            try
            {
                List<SelectListItem> items = new List<SelectListItem>();
                items.Insert(0, (new SelectListItem
                {
                    Text = "All employee",
                    Value = "1",
                }));
                items.Insert(1, (new SelectListItem
                {
                    Text = "unit",
                    Value = "2",
                }));
                items.Insert(2, (new SelectListItem
                {
                    Text = "nationality",
                    Value = "3",
                }));
                items.Insert(3, (new SelectListItem
                {
                    Text = "Work location",
                    Value = "4",
                }));
                items.Insert(4, (new SelectListItem
                {
                    Text = "Cost center",
                    Value = "5",

                }));
                items.Insert(5, (new SelectListItem
                {
                    Text = "Cadre level",
                    Value = "6",

                }));
                ViewBag.Object = new SelectList(items, "Value", "Text");

                var ID_emp = form["ID_emp"].Split(',');
                var codeids = form["codeids"].Split(',');
                var Description = form["TempDe"].Split(',');
                var DescriptionAl = form["TempDeAl"].Split(',');
                //var fromD = form["fromDDD"].Split(',');
                //var toDs = form["toD"].Split(',');
                var code = codeids[0];
      var Shiftscheduletemplate =   dbcontext.Shiftscheduletemplate.Where(a => a.TemplateCode_Shifts == code).ToList();

                for (int e = 0; e < ID_emp.Count(); e++)
                {
                    if (ID_emp[e] != "")
                    {
                        var id = int.Parse(ID_emp[e]);
                        Employee_Shift_schedule new_Records = new Employee_Shift_schedule();

                        dbcontext.Configuration.ProxyCreationEnabled = false;
                        var num_of_sch = dbcontext.Employee_Shift_schedule.Where(m => m.Employee_ProfileID == id).ToList();
                        var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == id);
                        var codes = "";
                        if (num_of_sch.Count == 0)
                        {
                            codes = emp.Code + "_SCH_1";
                        }
                        else
                        {
                            codes = emp.Code + "_SCH_" + (num_of_sch.Count() + 1);
                        }
                        new_Records.Code = codes;
                        var emps = int.Parse(ID_emp[e]);
                        new_Records.Employee_ProfileID = emps;
                        var Des = Description[0];
                        new_Records.Name = Des;
                        var DesAl = DescriptionAl[0];
                        new_Records.Description = DesAl;
                        new_Records.From_date = model.From_date;
                        new_Records.To_date = model.To_date;
                        var Headers = dbcontext.Employee_Shift_schedule.Add(new_Records);
                        dbcontext.SaveChanges();

                        for (var i = 0; i < Shiftscheduletemplate.Count(); i++)
                        {

                            Schedule_Details new_Record = new Schedule_Details();
                            new_Record.Employee_Shift_scheduleID = Headers.ID;
                            new_Record.From = Shiftscheduletemplate[i].From;
                            new_Record.To = Shiftscheduletemplate[i].To;
                            new_Record.From_date = Shiftscheduletemplate[i].From_date;
                            new_Record.To_date = Shiftscheduletemplate[i].To_date;
                            new_Record.ShiftdaystatusID = Shiftscheduletemplate[i].ShiftdaystatusID;
                            new_Record.Shift_setupID = Shiftscheduletemplate[i].Shift_setupID;

                            var Header = dbcontext.Schedule_Details.Add(new_Record);
                            dbcontext.SaveChanges();
                        }
                    }
                }
                ////////////////
               
                return RedirectToAction("index", "EmployeeShift");
            }
            catch (Exception e)
            {
                return View(model);
            }
        }

    }
}