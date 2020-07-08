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
    public class EmployeeAnnualandSpecialAllowanceController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: EmployeeAnnualandSpecialAllowance
        public ActionResult Index()
        {
            var Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true).ToList();
            var EmployeeAnnualIncreaseHistory = dbcontext.EmployeeAnnualIncreaseHistory.ToList();
            var model = from a in Employee_Profile
                        join b in EmployeeAnnualIncreaseHistory on a.ID equals int.Parse(b.Employee_Code)
                        orderby b.ID
                        select new EmployeeAnnualIncreaseHistory_VM
                        {
                            fullname = a.Full_Name,
                            EmployeeId = a.ID,
                            IncreaseType = b.IncreaseType,
                            EmployeeAnnualIncreaseHistory = b
                        };
            return View(model);
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

                var EmployeeAnnualIncrease = new EmployeeAnnualIncreaseHistory { AllowncePercentage = 0, Month = 6, Year = 2020 };
                var Employee_BasicSalary = new Employee_BasicSalary_History { Allowances_Percentage = 0 };

                var model = new VM_EmployeeAnnualandSpecial { EmployeeAnnualIncreaseHistory = EmployeeAnnualIncrease, Employee_BasicSalary_History = Employee_BasicSalary, IncreaseType = IncreaseType.AnnualIncrease };

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult Create(VM_EmployeeAnnualandSpecial model, FormCollection form, HttpPostedFileBase MyItem, string SpecialConfirm)
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
                var percentage = form["percentage"].Split(',');
                var money = form["money"].Split(',');

                var SpePer = form["SpePer"].Split(',');
                var SpeAmo = form["SpeAmo"].Split(',');
                if (SpecialConfirm == "Calculate")
                {
                    for (var i = 0; i < ID_emp.Count(); i++)
                    {
                        if (ID_emp[i] != "")
                        {
                            var IDEmp = int.Parse(ID_emp[i]);
                            //int ID2 = int.Parse(IDEmp);
                            var Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true).FirstOrDefault(a => a.ID == IDEmp);
                            var BasicSalary_A = Employee_Profile.BasicSalary_A;


                            Employee_BasicSalary_History new_Record = new Employee_BasicSalary_History();
                            new_Record.Employee_Code = IDEmp.ToString();
                            new_Record.History_Year = model.EmployeeAnnualIncreaseHistory.Year; 
                            new_Record.History_Month = model.EmployeeAnnualIncreaseHistory.Month; 
                            new_Record.BasicSalary_A = BasicSalary_A;
                            if (SpePer[0] == "0")
                            {
                                double per = double.Parse(SpeAmo[0]);
                                new_Record.Allowances_Percentage = 0;
                                new_Record.Allowances_Amount = per;
                                new_Record.BasicSalary_A = BasicSalary_A;
                                new_Record.Total_excluded_Amount = (BasicSalary_A + per);
                                Employee_Profile.BasicSalary_A = Convert.ToDouble(new_Record.Total_excluded_Amount);
                                Employee_Profile.Totalexecludedallowances = Convert.ToDouble(new_Record.Total_excluded_Amount);
                            }
                            else
                            {
                                double per = double.Parse(SpePer[0]);
                                new_Record.Allowances_Percentage = per;
                                var Allowances_Percentage = ((per * BasicSalary_A) / 100);
                                new_Record.Allowances_Amount = 0;
                                new_Record.BasicSalary_A = BasicSalary_A;
                                new_Record.Total_excluded_Amount = (BasicSalary_A + Allowances_Percentage);
                                Employee_Profile.BasicSalary_A = Convert.ToDouble(new_Record.Total_excluded_Amount);
                                Employee_Profile.Totalexecludedallowances = Convert.ToDouble(new_Record.Total_excluded_Amount);
                            }
                      
                        new_Record.Created_By = User.Identity.Name;
                        new_Record.Created_Date = DateTime.Now.Date;
                        var Header = dbcontext.Employee_BasicSalary_History.Add(new_Record);
                        dbcontext.SaveChanges();
                    } 
                    }
                }
                else
                {


                    if (MyItem == null)
                    {
                        foreach (var item in ID_emp)
                        {
                            if (item != "")
                            {
                                var PayrollGeneralSetup = dbcontext.PayrollGeneralSetup.FirstOrDefault().SalaryCodeID_BasicSalary;
                                var PayrollSetup = int.Parse(PayrollGeneralSetup);
                                var salary_code = dbcontext.salary_code.FirstOrDefault(a => a.ID == PayrollSetup).SalaryCodeDesc;
                                var ID = int.Parse(item);
                                var Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true).FirstOrDefault(a => a.ID == ID).ID;
                                var emp = Employee_Profile.ToString();
                                var Employee_Financial_Contract_Header = dbcontext.Employee_Financial_Contract_Header.Where(a => a.IsActive == true).FirstOrDefault(a => a.Employee_Code == emp).ID;
                                var FinancialContract = Employee_Financial_Contract_Header.ToString();
                                var Employee_Financial_Contract_Detail = dbcontext.Employee_Financial_Contract_Detail.Where(a => a.Contract_Number == FinancialContract).FirstOrDefault(a => a.Salarycodedescription == salary_code).SalaryCodeValue;
                                var NewSalaryFinancial = dbcontext.Employee_Financial_Contract_Detail.Where(a => a.Contract_Number == FinancialContract).FirstOrDefault(a => a.Salarycodedescription == salary_code);


                                EmployeeAnnualIncreaseHistory new_Record = new EmployeeAnnualIncreaseHistory();
                                new_Record.Employee_Code = emp.ToString();
                                new_Record.Year = model.EmployeeAnnualIncreaseHistory.Year;
                                new_Record.Month = model.EmployeeAnnualIncreaseHistory.Month;
                                new_Record.IncreaseType = model.IncreaseType.GetHashCode();
                                new_Record.Notes = model.EmployeeAnnualIncreaseHistory.Notes;
                                if (percentage[1] == "0")
                                {
                                    double per = double.Parse(money[1]);
                                    new_Record.AllowncePercentage = per;
                                    new_Record.AllownceAmount = per;
                                    new_Record.OldSalary = Employee_Financial_Contract_Detail;
                                    new_Record.NewSalary = (Employee_Financial_Contract_Detail + per);
                                    NewSalaryFinancial.SalaryCodeValue = new_Record.NewSalary;
                                }
                                else
                                {
                                    double per = double.Parse(percentage[1]);
                                    new_Record.AllowncePercentage = model.EmployeeAnnualIncreaseHistory.AllowncePercentage;
                                    var Percentage = ((per * Employee_Financial_Contract_Detail) / 100);
                                    new_Record.AllownceAmount = Percentage;
                                    new_Record.OldSalary = Employee_Financial_Contract_Detail;
                                    new_Record.NewSalary = (Employee_Financial_Contract_Detail + Percentage);
                                    NewSalaryFinancial.SalaryCodeValue = new_Record.NewSalary;
                                }

                                new_Record.Created_By = User.Identity.Name;
                                new_Record.Created_Date = DateTime.Now.Date;
                                var Header = dbcontext.EmployeeAnnualIncreaseHistory.Add(new_Record);
                                dbcontext.SaveChanges();
                            }
                        }
                    }
                    else if (MyItem.FileName != null)
                    {
                        string folderpath = Server.MapPath("~/EmployeeAnnualFromExcel/");
                        Directory.CreateDirectory(folderpath);
                        string FilePath = folderpath;
                        string filename = MyItem.FileName + Guid.NewGuid() + Path.GetExtension(MyItem.FileName);
                        string p = FilePath + "/" + filename;
                        MyItem.SaveAs(p);
                        var FileCombine = Path.Combine(p);
                        var Listt = ReadExcelFile(FileCombine);
                        TempData["list"] = Listt;
                        return RedirectToAction("ViewExecl", "EmployeeAnnualandSpecialAllowance");
                    }
                }
                ////////////////
                return RedirectToAction("index");
            }
            catch (Exception e)
            {
                return View(model);
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
                    emp.Year = TwoDArray[R, C].ToString();
                    C++;
                    emp.Month = TwoDArray[R, C].ToString();
                    C++;
                    emp.AllowncePercentage = TwoDArray[R, C].ToString();
                    empList.Add(emp);
                }
            }
            return empList;
        }
        public ActionResult ViewExecl()
        {
            var AllData = TempData["list"];
            ViewBag.Data = AllData;
            return View(ViewBag.Data);
        }
        [HttpPost]
        public ActionResult ViewExecl(EmployeeAnnualIncreaseHistory model, FormCollection form)
        {
            try
            {
                var AllData = ViewBag.Data;
                var EmpCode = form["EmployeeCode"].Split(',');
                var EmpName = form["EmployeeName"].Split(',');
                var Year = form["Year"].Split(',');
                var Month = form["Month"].Split(',');
                var AllowncePercentage = form["AllowncePercentage"].Split(',');

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
                for (var na = 0; na < EmpName.Length; na++)
                {
                    var Name = EmpName[na];
                    var ChName = dbcontext.Employee_Profile.Where(a => a.Active == true).FirstOrDefault(a => a.Name == Name);

                    if (ChName == null)
                    {
                        TempData["Message"] = "Not found this data " + Name;
                        return RedirectToAction("Index", "Employee_Payroll_Transactions");
                    }
                }

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
                        var PayrollGeneralSetup = dbcontext.PayrollGeneralSetup.FirstOrDefault().SalaryCodeID_BasicSalary;
                        var PayrollSetup = int.Parse(PayrollGeneralSetup);
                        var salary_code = dbcontext.salary_code.FirstOrDefault(a => a.ID == PayrollSetup).SalaryCodeDesc;

                        var Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true).FirstOrDefault(a => a.Code == CoEmp).ID;
                        var emp = Employee_Profile.ToString();
                        var Employee_Financial_Contract_Header = dbcontext.Employee_Financial_Contract_Header.Where(a => a.IsActive == true).FirstOrDefault(a => a.Employee_Code == emp).ID;
                        var FinancialContract = Employee_Financial_Contract_Header.ToString();
                        var Employee_Financial_Contract_Detail = dbcontext.Employee_Financial_Contract_Detail.Where(a => a.Contract_Number == FinancialContract).FirstOrDefault(a => a.Salarycodedescription == salary_code).SalaryCodeValue;
                        var NewSalaryFinancial = dbcontext.Employee_Financial_Contract_Detail.Where(a => a.Contract_Number == FinancialContract).FirstOrDefault(a => a.Salarycodedescription == salary_code);



                        EmployeeAnnualIncreaseHistory new_Record = new EmployeeAnnualIncreaseHistory();

                        new_Record.Employee_Code = ChName.ID.ToString();
                        short year = short.Parse(Year[c]);
                        new_Record.Year = year;
                        short month = short.Parse(Month[c]);
                        new_Record.Month = month;
                        //new_Record.IncreaseType = model.IncreaseType.GetHashCode();
                        new_Record.Notes = model.Notes;

                        double per = double.Parse(AllowncePercentage[c]);
                        new_Record.AllowncePercentage = per;
                        var Percentage = ((per * Employee_Financial_Contract_Detail) / 100);
                        new_Record.AllownceAmount = Percentage;
                        new_Record.OldSalary = Employee_Financial_Contract_Detail;
                        new_Record.NewSalary = (Employee_Financial_Contract_Detail + Percentage);
                        NewSalaryFinancial.SalaryCodeValue = new_Record.NewSalary;

                        new_Record.Created_By = User.Identity.Name;
                        new_Record.Created_Date = DateTime.Now.Date;

                        dbcontext.EmployeeAnnualIncreaseHistory.Add(new_Record);
                        dbcontext.SaveChanges();
                    }
                }
                return RedirectToAction("Index", "EmployeeAnnualandSpecialAllowance");
            }
            catch (Exception e)
            {
                TempData["Message"] = "Not found this data";
                return RedirectToAction("Index", "EmployeeAnnualandSpecialAllowance");
            }
        }

        public ActionResult ShowEmployeeAnnualIncrease(string id)
        {
            var ID = int.Parse(id);
            //var new_model = dbcontext.EmployeeAnnualIncreaseHistory.Where(m => m.Employee_Profile.ID == ID).ToList();
            ViewBag.idemp = id;

            var Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true && a.ID == ID).ToList();
            var EmployeeAnnualIncreaseHistory = dbcontext.EmployeeAnnualIncreaseHistory.ToList();
            var model = from a in Employee_Profile
                        join b in EmployeeAnnualIncreaseHistory on a.ID equals int.Parse(b.Employee_Code)
                        orderby b.ID
                        select new EmployeeAnnualIncreaseHistory_VM
                        {
                            fullname = a.Full_Name,
                            EmployeeId = a.ID,
                            IncreaseType = b.IncreaseType,
                            EmployeeAnnualIncreaseHistory = b
                        };
            return View(model);
        }
        public ActionResult ShowEmployeeBasicSalaryHistory(string id)
        {
            var ID = int.Parse(id);
            //var new_model = dbcontext.EmployeeAnnualIncreaseHistory.Where(m => m.Employee_Profile.ID == ID).ToList();
            ViewBag.idemp = id;

            var Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true && a.ID == ID).ToList();
            var Employee_BasicSalary_History = dbcontext.Employee_BasicSalary_History.ToList();
            var model = from a in Employee_Profile
                        join b in Employee_BasicSalary_History on a.ID equals int.Parse(b.Employee_Code)
                        orderby b.ID
                        select new EmployeeBasicSalaryHistory_VM
                        {
                            fullname = a.Full_Name,
                            EmployeeId = a.ID,
                            Employee_BasicSalary_History = b
                        };
            return View(model);
        }
        public class EmpVM
        {
            public string EmployeeCode { get; set; }
            public string EmployeeName { get; set; }
            public string Year { get; set; }
            public string Month { get; set; }
            public string AllowncePercentage { get; set; }
        }
        public class VM_EmployeeAnnualandSpecial
        {
            public EmployeeAnnualIncreaseHistory EmployeeAnnualIncreaseHistory { get; set; }
            public Employee_BasicSalary_History Employee_BasicSalary_History { get; set; }
            public IncreaseType IncreaseType { get; set; }

        }
        public enum IncreaseType
        {
            [Display(Name = "Annual Increase")]
            AnnualIncrease = 1,
            [Display(Name = "Promotion Increase")]
            PromotionIncrease = 2,
            [Display(Name = "Other Increase")]
            OtherIncrease = 3,
        }
        public class EmployeeAnnualIncreaseHistory_VM
        {
            public string fullname { get; set; }
            public int EmployeeId { get; set; }
            public int IncreaseType { get; set; }
            public EmployeeAnnualIncreaseHistory EmployeeAnnualIncreaseHistory { get; set; }
        }
        public class EmployeeBasicSalaryHistory_VM
        {
            public string fullname { get; set; }
            public int EmployeeId { get; set; }
            public Employee_BasicSalary_History Employee_BasicSalary_History { get; set; }
        }
    }
}