using Antlr.Runtime;
using HR.Models;
using HR.Models.Infra;
using HR.Models.SetupPayroll;
using HR.Models.TransactionsPayroll;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml;
using System.IO;
using Microsoft.Office.Interop.Excel;
using HR.Models.ViewModel;
using System.Runtime.InteropServices;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using HR.Models.Time_management;

namespace HR.Controllers
{
    [Authorize]
    public class ImportFromExcel_TimeManagementController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: ImportFromExcel_TimeManagement
        public ActionResult ViewExecl()
        {
            var AllData = TempData["list"];
            ViewBag.Data = AllData;
            return View(ViewBag.Data);
        }
        [HttpPost]
        public ActionResult ViewExecl(Employee_Shift_schedule model, FormCollection form)
        {
            try
            {
                var AllData = ViewBag.Data;
                var EmpCode = form["EmployeeCode"].Split(',');
                var EmpName = form["EmployeeName"].Split(',');
                var Date = form["Date"].Split(',');
                var In = form["In"].Split(',');
                var Out = form["Out"].Split(',');
                
                for (var co = 0; co < EmpCode.Length; co++)
                {
                    var Code = EmpCode[co];
                    var ChCode = dbcontext.Employee_Profile.Where(a => a.Active == true).FirstOrDefault(a => a.Code == Code);
                    if (ChCode == null)
                    {
                        TempData["Message"] = "Not found this data " + Code;
                        return RedirectToAction("Index", "Employee_Payroll_Transactions");
                    }
                }
                //for (var na = 0; na < EmpName.Length; na++)
                //{
                //    var Name = EmpName[na];
                //    var ChName = dbcontext.Employee_Profile.Where(a => a.Active == true).FirstOrDefault(a => a.Name == Name);
                //    if (ChName == null)
                //    {
                //        TempData["Message"] = "Not found this data " + Name;
                //        return RedirectToAction("Index", "Employee_Payroll_Transactions");
                //    }
                //}
                
                for (var c = 0; c < EmpCode.Length; c++)
                {
                    if (EmpCode[c] != "")
                    {
                        var CoEmp = EmpCode[c];
                        var NaEmp = EmpName[c];
                        var ChName = dbcontext.Employee_Profile.Where(a => a.Active == true).FirstOrDefault(a => a.Code == CoEmp && a.Name == NaEmp);
                        if (ChName == null)
                        {
                            TempData["Message"] = "Not found this data " + CoEmp + " " + NaEmp;
                            return RedirectToAction("Index", "Employee_Payroll_Transactions");
                        }

                        Employee_Shift_schedule newdata = new Employee_Shift_schedule();
                        var Employee_Shift_schedule = dbcontext.Employee_Shift_schedule.ToList();
                        var Stru = dbcontext.StructureModels.FirstOrDefault(a => a.All_Models == ChModels.Payroll).Structure_Code;
                        if (Employee_Shift_schedule.Count() == 0)
                        {
                            newdata.Code = Stru + "1";
                        }
                        else
                        {
                            newdata.Code = Stru + (Employee_Shift_schedule.LastOrDefault().ID + 1).ToString();
                        }
                        //newdata.Employee_Code = CoEmp;
                        //newdata.SalaryCodeID = SalaCode;
                        //newdata.TransactionValue = 0;
                        //newdata.TransactionDate = DateTime.Parse(Transaction[c]);
                        //newdata.TransactionYear = DateTime.Parse(Transaction[c]).Year;
                        //newdata.TransactionMonth = DateTime.Parse(Transaction[c]).Month;
                        //newdata.EffectiveDate = DateTime.Parse(Effective[c]);
                        //newdata.EffectiveYear = DateTime.Parse(Effective[c]).Year;
                        //newdata.EffectiveMonth = DateTime.Parse(Effective[c]).Month;
                        //newdata.Created_By = User.Identity.Name;
                        //newdata.Created_Date = DateTime.Now.Date;
                        //newdata.CreatedBy = User.Identity.Name;
                        //newdata.CreatedDate = DateTime.Now.Date;
                        //DateTime statis2 = Convert.ToDateTime("1/1/1900");
                        //newdata.ReportAsReadyDate = statis2;
                        //newdata.ApprovedDate = statis2;
                        //newdata.RejectedDate = statis2;
                        //newdata.CanceledDate = statis2;
                        //newdata.CompletedDate = statis2;
                        //newdata.Modified_Date = statis2;

                        //newdata.check_status = HR.Models.Infra.check_status.created;
                        //var username = User.Identity.GetUserName();
                        //var Date = Convert.ToDateTime("1/1/1900");
                        //var s = new status { statu = HR.Models.Infra.check_status.created, created_by = username, Type = Models.Infra.Type.Employee_Payroll_Transactions, approved_bydate = Date, cancaled_bydate = Date, created_bydate = DateTime.Now.Date, Rejected_bydate = Date, return_to_reviewdate = Date };
                        //var st = dbcontext.status.Add(s);
                        //dbcontext.SaveChanges();
                        //newdata.statID = s.ID;

                        //dbcontext.Employee_Payroll_Transactions.Add(newdata);
                        //dbcontext.SaveChanges();
                    }
                }
                return RedirectToAction("Index", "Employee_Payroll_Transactions");
            }
            catch (Exception e)
            {
                TempData["Message"] = "Not found this data";
                return RedirectToAction("Index", "Employee_Payroll_Transactions");
            }

        }

        public ActionResult ImportTransactionEntryFromExcel()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ImportTransactionEntryFromExcel(HttpPostedFileBase MyItem)
        {
            try
            {
                if (MyItem == null)
                {
                }
                else if (MyItem.FileName != null)
                {
                    string folderpath = Server.MapPath("~/TimeManagementTransactionEntryFromExcel/");
                    Directory.CreateDirectory(folderpath);
                    string FilePath = folderpath;
                    string filename = MyItem.FileName + Guid.NewGuid() + Path.GetExtension(MyItem.FileName);
                    string p = FilePath + "/" + filename;
                    MyItem.SaveAs(p);
                    var FileCombine = Path.Combine(p);
                    var Listt = ReadExcelFile(FileCombine);
                    TempData["list"] = Listt;
                    return RedirectToAction("ViewExecl", "ImportFromExcel_TimeManagement");
                }
                return RedirectToAction("index");
            }
            catch (Exception e)
            {
                return View();
            }
        }

        public List<EmpVM> ReadExcelFile(string FilePath)
        {
            var xlApp = new Application();
            var xlWorkBook = xlApp.Workbooks.Open(FilePath, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            var xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);
            var range = xlWorkSheet.UsedRange;
            var ExcelList = GetExcelList((range.Rows as Range).Value2);
            return ExcelList;
        }
        public object GetExcelList(object[,] TwoDArray)
        {
            var empList = new List<EmpVM>();

            //Getting the no of rows of 2d array 
            int NoOfRows = TwoDArray.GetLength(0);
            //Getting the no of columns of the 2d array
            int NoOfColumns = TwoDArray.GetLength(1);

            for (int R = 2; R <= NoOfRows; R++)
            {
                for (int C = 1; C <= NoOfColumns; C++)
                {
                    var emp = new EmpVM();
                    emp.EmployeeCode = TwoDArray[R, C].ToString();
                    C++;
                    emp.EmployeeName = TwoDArray[R, C].ToString();
                    C++;
                    var tra = TwoDArray[R, C].ToString();
                    double d = double.Parse(tra);
                    DateTime conv = DateTime.FromOADate(d);
                    emp.Date = conv;
                    C++;

                    var In = TwoDArray[R, C].ToString();
                    double DIn = double.Parse(In);
                    DateTime ConIn = DateTime.FromOADate(DIn);
                    emp.In = ConIn.TimeOfDay;
                  
                    C++;
                    var Out = TwoDArray[R, C].ToString();
                    double DOut = double.Parse(Out);
                    DateTime ConOut = DateTime.FromOADate(DOut);
                    emp.Out = ConOut.TimeOfDay;
                    
                    empList.Add(emp);
                }
            }
            return empList;
        }

        public class EmpVM
        {
            public string EmployeeCode { get; set; }
            public string EmployeeName { get; set; }
            public DateTime Date { get; set; }
            public TimeSpan In { get; set; }
            public TimeSpan Out { get; set; }
          

        }

    }
}