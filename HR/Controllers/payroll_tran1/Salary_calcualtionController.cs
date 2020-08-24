using HR.Models;
using HR.Models.payroll_trans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers.payroll_tran1
{
    public class Salary_calcualtionController : Controller
    {
        // GET: Salary_calcualtion
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        public ActionResult Index()
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
                var sal = new salaryVM { Month = DateTime.Now.Month, Year = DateTime.Now.Year };
               
                /////
                return View(sal);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        public ActionResult index(salaryVM model,FormCollection form)
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

           
                var list_emp = new List<salaryVM>();
                var ID_emp = form["ID_emp"].Split(',');
                var Payroll_Result_Header = new List<Payroll_Calculation_Result_Header>();
                foreach (var item in ID_emp)
                {
                  
                    if (item != "")
                    {
                        var ID = int.Parse(item);
                        var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == ID&&m.Active==true);
                        var EFFECTIVE_month = model.Month;
                        var effrctive_year = model.Year;
                       
                        Payroll_Result_Header.Add(new Payroll_Calculation_Result_Header { Employee_Code = emp.ID.ToString(), EffectiveMonth =(short) EFFECTIVE_month, EffectiveYear =(short) effrctive_year,NetAmount=0,OriginalNetAmount=0,RoundedAmount=0,ArrearAmount=0,IsClosed=false,IsDeserved=false,IsExpended=false,ExpendedGLTransactionNumber=false,Created_By=User.Identity.Name,Created_Date=DateTime.Now.Date });
                      
                      
                        ////////////////////////////loan//////////////////////
                        //var loan_requests = dbcontext.LoanRequest.Where(m => m.EmployeeID == ID.ToString()).ToList();
                        //foreach(var item2 in loan_requests)
                        //{

                        //    var installment = dbcontext.LoanInstallment.Where(m => m.LoanRequestNumber == item2.LoanRequestNumber && m.InstallmentMonth == model.Month && m.InstallmentYear == model.Year&&m.IsActive==true&&m.IsPaid==false).ToList();
                        //    foreach(var item3 in installment)
                        //    {
                        //        Deduction = Deduction + item3.UnpaidAmount;
                        //    }
                        //}
                        ///////////////////////////////////transaction//////////

                        ///////////////////////////////////
                        //list_emp.Add( new salaryVM { emp_id=emp.ID,Month=model.Month,Year=model.Year,emp=emp.Code+"--"+emp.Full_Name,total=earning-Deduction});
                        //TempData["salary"] = list_emp;
                        //return RedirectToAction("view_salary_emp");
                    }

                }
                var Payroll_Result = new List<Payroll_Calculation_Result>();
                foreach (var item in Payroll_Result_Header)
                {
                    bool flag = false;
                    double? Deduction = 0.0;
                    double? earning = 0.0;
                    //////////////////////////finicial contract/////////
                    var finicial_contract_header = dbcontext.Employee_Financial_Contract_Header.FirstOrDefault(m => m.IsActive == true && m.Employee_Code == item.Employee_Code);
                    if (finicial_contract_header.From_Date.Value.Year < model.Year && finicial_contract_header.To_Date.Value.Year > model.Year)
                    {
                        flag = true;
                    }
                    else if ((finicial_contract_header.From_Date.Value.Year == model.Year && finicial_contract_header.To_Date.Value.Year == model.Year)
                        && (finicial_contract_header.From_Date.Value.Month <= model.Month && finicial_contract_header.To_Date.Value.Month >= model.Month))
                    {
                        flag = true;
                    }
                    else if ((finicial_contract_header.From_Date.Value.Year == model.Year && finicial_contract_header.To_Date.Value.Year > model.Year)
                             && (finicial_contract_header.From_Date.Value.Month <= model.Month))
                    {
                        flag = true;
                    }
                    if ((finicial_contract_header.To_Date.Value.Year == model.Year && finicial_contract_header.From_Date.Value.Year < model.Year)
                            && (finicial_contract_header.To_Date.Value.Month >= model.Month))
                    {
                        flag = true;
                    }
                    if (flag)
                    {
                        var fincial_contract_details = dbcontext.Employee_Financial_Contract_Detail.Where(m => m.Contract_Number == finicial_contract_header.ID.ToString()).ToList();
                        foreach (var item4 in fincial_contract_details)
                        {
                           
                            var salary_id=int.Parse(item4.SalaryCodeID);
                            var salary_code = dbcontext.salary_code.FirstOrDefault(m => m.ID == salary_id);
                         
                            Payroll_Result.Add(new Payroll_Calculation_Result { TransactionNumber=item4.Contract_Number,Employee_Code=item.Employee_Code,SalaryCodeID=item4.SalaryCodeID,FormulaCode=salary_code.FormulaCode,TransactionDate=item4.Created_Date,EffectiveDate=item4.Created_Date,TransactionMonth=(short?)item4.Created_Date.Value.Month, TransactionYear = (short?)item4.Created_Date.Value.Year, EffectiveMonth = (short?)item4.Created_Date.Value.Month, EffectiveYear = (short?)item4.Created_Date.Value.Year,
                                                                               AffectToNetAmount = salary_code.AffectToNetAmount,
                                TransactionValue =item4.SalaryCodeValue,SourceEntry=0,Created_By=User.Identity.Name,Created_Date=DateTime.Now,
                                                                                 });
                            if (item4.Type == "Earning")
                            {
                                earning = earning + item4.SalaryCodeValue;
                            }
                            else if (item4.Type == "Deduction")
                            {
                                Deduction = Deduction + item4.SalaryCodeValue;
                            }
                        }
                    }


                    //=======================================================================================================
                    var transations = dbcontext.Employee_Payroll_Transactions.Where(m => m.status.statu == Models.Infra.check_status.Approved&&m.Employee_Code==item.ID.ToString()).ToList();
                    foreach(var item4 in transations)
                    {
                        var salary_id = int.Parse(item4.SalaryCodeID);
                        var salary_code = dbcontext.salary_code.FirstOrDefault(m => m.ID == salary_id);
                        Payroll_Result.Add(new Payroll_Calculation_Result
                        {
                            TransactionNumber = item4.TransactionNumber,
                            Employee_Code = item.Employee_Code,
                            SalaryCodeID = item4.SalaryCodeID,
                            FormulaCode = salary_code.FormulaCode,
                            TransactionDate = item4.TransactionDate,
                            EffectiveDate = item4.EffectiveDate,
                            TransactionMonth = (short?)item4.TransactionDate.Value.Month,
                            TransactionYear = (short?)item4.TransactionDate.Value.Year,
                            EffectiveMonth = (short?)item4.EffectiveDate.Value.Month,
                            EffectiveYear = (short?)item4.EffectiveDate.Value.Year,
                            AffectToNetAmount = salary_code.AffectToNetAmount,
                            TransactionValue = item4.TransactionValue,
                            SourceDocumentType=(short?)item4.SourceDocumentType,
                            SourceDocumentRefrence= item4.SourceDocumentRefrence.ToString(),
                            SourceDocumentDescription=item4.SourceDocumentDescription,
                            
                            SourceEntry = 0,
                            Created_By = User.Identity.Name,
                            Created_Date = DateTime.Now,
                        });

                    }
                }
                //////////
                return RedirectToAction("index");

            }
            catch (Exception e)
            {
                return View(model);

            }
        }
        public ActionResult view_salary_emp()
        {
            var model = TempData["salary"];
            return View(model);
        }
        public ActionResult preview(int id,int month,int year,double? total)
        {
            try
            {
                var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == id && m.Active == true);

                var prev = new preview { emp_id=id,Month=month,Year=year,total=total,emp=emp.Code+"--"+emp.Full_Name};
                var ear = new List<string>();
                var deduc = new List<string>();
                
                bool flag = false;
                //////////////////////////finicial contract/////////
                var finicial_contract_header = dbcontext.Employee_Financial_Contract_Header.FirstOrDefault(m => m.IsActive == true && m.Employee_Code == emp.Code);
                if (finicial_contract_header.From_Date.Value.Year < year && finicial_contract_header.To_Date.Value.Year > year)
                {
                    flag = true;
                }
                else if ((finicial_contract_header.From_Date.Value.Year ==year && finicial_contract_header.To_Date.Value.Year == year)
                    && (finicial_contract_header.From_Date.Value.Month <= month && finicial_contract_header.To_Date.Value.Month >= month))
                {
                    flag = true;
                }
                else if ((finicial_contract_header.From_Date.Value.Year == year && finicial_contract_header.To_Date.Value.Year > year)
                         && (finicial_contract_header.From_Date.Value.Month <= month))
                {
                    flag = true;
                }
                if ((finicial_contract_header.To_Date.Value.Year == year && finicial_contract_header.From_Date.Value.Year < year)
                        && (finicial_contract_header.To_Date.Value.Month >= month))
                {
                    flag = true;
                }
                if (flag)
                {
                    var fincial_contract_details = dbcontext.Employee_Financial_Contract_Detail.Where(m => m.Contract_Number == finicial_contract_header.ID.ToString()).ToList();
                    foreach (var item4 in fincial_contract_details)
                    {
                        if (item4.Type == "Earning")
                        {

                            
                            ear.Add(item4.Salarycodedescription + " -- " + item4.SalaryCodeValue);

                        }
                        else if (item4.Type == "Deduction")
                        {
                            deduc.Add(item4.Salarycodedescription+" -- " + item4.SalaryCodeValue);
                         
                        }
                    }
                }

                //////////////////////////loan//////////////////////
                var loan_requests = dbcontext.LoanRequest.Where(m => m.EmployeeID == id.ToString()).ToList();
                foreach (var item2 in loan_requests)
                {

                    var installment = dbcontext.LoanInstallment.Where(m => m.LoanRequestNumber == item2.LoanRequestNumber && m.InstallmentMonth == month && m.InstallmentYear ==year && m.IsActive == true && m.IsPaid == false).ToList();
                    foreach (var item3 in installment)
                    {
                        deduc.Add("Loan -- " + item3.UnpaidAmount);
                     }
                }
                ////////////////////transaction/////////////////////

                ////////////////////////////////////////////////////
                prev.earning = ear;prev.deduction = deduc;
                return View(prev);
            }
            catch(Exception)
            {
                return RedirectToAction("Index");
            }
        }


    }
    public class salaryVM
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public int emp_id { get; set; }
        public string emp { get; set; }
        public double? total{get;set;}
    }
    public class preview
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public int emp_id { get; set; }
        public string emp { get; set; }
        public double? total { get; set; }
        public List<string> earning { get; set; }
        public List<string> deduction { get; set; }


    }
}