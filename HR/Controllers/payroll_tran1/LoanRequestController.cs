using HR.Models;
using HR.Models.Infra;
using HR.Models.payroll_trans;
using HR.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers.payroll_tran1
{
    public class LoanRequestController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: LoanRequest
        public ActionResult Index()
        {
            var model = dbcontext.LoanRequest.ToList();
            return View(model);
        }
        public ActionResult Create()
        {
            try
            {
                ////loan_request_number
                var New_request = new LoanRequest();
                var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Payroll).Structure_Code;
                var All_requests = dbcontext.LoanRequest.ToList();
                if (All_requests.Count() == 0)
                {
                    New_request.LoanRequestNumber = stru + "1";
                }
                else
                {
                    New_request.LoanRequestNumber = stru + (All_requests.LastOrDefault().ID + 1).ToString();
                }
                ///////
                ViewBag.emp = dbcontext.Employee_Profile.Where(m=>m.Active==true).ToList().Select(m => new { Code = m.Code + "--[" + m.Name + ']', ID = m.ID });
                ViewBag.loan_type = dbcontext.LoanInAdvanceSetup.ToList().Select(m => new { Code = m.LoanTypeCode + "--[" + m.LoanTypeDesc + ']', ID = m.ID });
                New_request.LoanAmount = 0;
                New_request.NumberOfInstallment = 0;
                New_request.TotalPaidAmount = 0;
                New_request.LoanInstallmentAmount = 0;
                New_request.TotalRemainingAmount = 0;
                New_request.NumberOfDeductedInstallments = 1;
                New_request.StartDate = DateTime.Now.Date;
                New_request.EndDate = DateTime.Now.Date;
                //////
                New_request.Created_By = User.Identity.Name;
                New_request.Created_Date = DateTime.Now.Date;
                /////
                New_request.RequestStatus = check_status.created.GetHashCode();
                New_request.check_status = check_status.created.ToString();
                /////
                return View(New_request);
            }
            catch(Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult Create(LoanRequest Model,string command)
        {
            try
            {
                ViewBag.emp = dbcontext.Employee_Profile.Where(m => m.Active == true).ToList().Select(m => new { Code = m.Code + "--[" + m.Name + ']', ID = m.ID });
                ViewBag.loan_type = dbcontext.LoanInAdvanceSetup.ToList().Select(m => new { Code = m.LoanTypeCode + "--[" + m.LoanTypeDesc + ']', ID = m.ID });

                if (Model.NumberOfDeductedInstallments>Model.NumberOfInstallment)
                {
                    return View(Model);
                }
                if(Model.NumberOfInstallment==0)
                {
                    Model.NumberOfInstallment = 1;
                }
                if (Model.NumberOfDeductedInstallments == 0)
                {
                    Model.NumberOfDeductedInstallments = 1;
                }
                if(Model.StartDate==null)
                {
                    return View(Model);
                }
                if (Model.EndDate == null)
                {
                   Model.EndDate=cal_end_date(Model.StartDate, Model.NumberOfInstallment, Model.NumberOfDeductedInstallments);
                }
                Model.LoanInstallmentAmount = Model.LoanAmount / Model.NumberOfInstallment;
                Model.RequestStatus = check_status.created.GetHashCode();
                if(Model.EmployeeID!=null)
                {
                    int ID_emp = int.Parse(Model.EmployeeID);
                    var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == ID_emp);
                    Model.emp_name = emp.Full_Name;
                }
                ///////////////status////////////////////////
               
            
                var Date = Convert.ToDateTime("1/1/1900");
                var s = new status { statu = check_status.created, Type = Models.Infra.Type.employee_record, approved_bydate = Date, cancaled_bydate = Date, created_bydate = DateTime.Now.Date, Rejected_bydate = Date, return_to_reviewdate = Date };
                s.created_by = User.Identity.Name;
                var st = dbcontext.status.Add(s);
                dbcontext.SaveChanges();
                Model.statusID = st.ID;
                //////////////////////////////////////////////


                var loan= dbcontext.LoanRequest.Add(Model);
                dbcontext.SaveChanges();

                var start1 = loan.StartDate;
                var start2 = loan.StartDate;
                var Mon = 1;
                while(start1.Value.CompareTo(Model.EndDate.Value)!=0)
                {
                    Mon++;
                 start1= start1.Value.AddMonths(1);

                    
                }
                var i = 1;
                for (var item=0;item<Mon;item++ )
                {
                   
                    for (var ii = 0; ii < Model.NumberOfDeductedInstallments; ii++)
                    {
                        var loan_installment = new LoanInstallment
                        {
                            Created_By = User.Identity.Name,
                            Created_Date = DateTime.Now.Date,
                            LoanRequestNumber = loan.LoanRequestNumber,
                            InstallmentNumber = i,
                            InstallmentAmount = loan.LoanAmount,
                            IsPaid = false,
                            IsFreeze = false,
                            IsActive = true,
                            IsTransfered = false,
                            PaidAmount = 0,
                            InstallmentMonth = (short)start2.Value.Month,
                            InstallmentYear = (short)start2.Value.Year,
                            UnpaidAmount = loan.LoanInstallmentAmount,
                            InstallmenNotes = null

                        };
                        
                        dbcontext.LoanInstallment.Add(loan_installment);
                        dbcontext.SaveChanges();
                        i++;
                        if(i>loan.NumberOfInstallment)
                        {
                            break;
                        }
                    }
                    start2 = start2.Value.AddMonths(1);

                }


                if (command == "Submit")
                {
                    return RedirectToAction("loan_installment", "LoanRequest", new { number = loan.LoanRequestNumber,id=loan.ID });
                }
                return RedirectToAction("index");
            }
            catch(Exception e)
            {
                return View(Model);
            }
        }
        public ActionResult edit(int id)
        {
            try
            {
                var edit_model = dbcontext.LoanRequest.FirstOrDefault(m => m.ID == id);
                ViewBag.emp = dbcontext.Employee_Profile.Where(m => m.Active == true).ToList().Select(m => new { Code = m.Code + "--[" + m.Name + ']', ID = m.ID });
                ViewBag.loan_type = dbcontext.LoanInAdvanceSetup.ToList().Select(m => new { Code = m.LoanTypeCode + "--[" + m.LoanTypeDesc + ']', ID = m.ID });
                return View(edit_model);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult edit(LoanRequest Model,string command)
        {
            try
            {
                var edit_model = dbcontext.LoanRequest.FirstOrDefault(m => m.ID == Model.ID);
                ViewBag.emp = dbcontext.Employee_Profile.Where(m => m.Active == true).ToList().Select(m => new { Code = m.Code + "--[" + m.Name + ']', ID = m.ID });
                ViewBag.loan_type = dbcontext.LoanInAdvanceSetup.ToList().Select(m => new { Code = m.LoanTypeCode + "--[" + m.LoanTypeDesc + ']', ID = m.ID });
                var sta = dbcontext.status.FirstOrDefault(m => m.ID == edit_model.statusID);
                if (sta.statu == check_status.Approved || sta.statu == check_status.Rejected || sta.statu == check_status.Closed || sta.statu == check_status.Recervied || sta.statu == check_status.Canceled)
                {
                    TempData["message"] = HR.Resource.training.status_message;
                    return RedirectToAction("index");
                }
                if (Model.NumberOfDeductedInstallments > Model.NumberOfInstallment)
                {
                    return View(Model);
                }
                if (Model.NumberOfInstallment == 0)
                {
                    Model.NumberOfInstallment = 1;
                }
                if (Model.NumberOfDeductedInstallments == 0)
                {
                    Model.NumberOfDeductedInstallments = 1;
                }
                if (Model.StartDate == null)
                {
                    return View(Model);
                }
                if (Model.EndDate == null)
                {
                    Model.EndDate = cal_end_date(Model.StartDate, Model.NumberOfInstallment, Model.NumberOfDeductedInstallments);
                }
                edit_model.LoanInstallmentAmount = Model.LoanAmount / Model.NumberOfInstallment;
                edit_model.RequestStatus = check_status.created.GetHashCode();
                edit_model.Modified_By = User.Identity.Name;
                edit_model.Modified_Date = DateTime.Now.Date;


                edit_model.EmployeeID = Model.EmployeeID;
                edit_model.LoanTypeCode = Model.LoanTypeCode;
                edit_model.LoanAmount = Model.LoanAmount;edit_model.NumberOfInstallment = Model.NumberOfInstallment;
                edit_model.TotalPaidAmount = Model.TotalPaidAmount;edit_model.LoanInstallmentAmount = Model.LoanInstallmentAmount;
                edit_model.TotalRemainingAmount = Model.TotalRemainingAmount;edit_model.NumberOfDeductedInstallments = Model.NumberOfDeductedInstallments;
                edit_model.StartDate = Model.StartDate;edit_model.EndDate = Model.EndDate;
                edit_model.TransferTransactionNumber = Model.TransferTransactionNumber;edit_model.TransferredTo = Model.TransferredTo;edit_model.TransferNotes = Model.TransferNotes;
                edit_model.Guarantor1 = Model.Guarantor1;edit_model.Guarantor2 = Model.Guarantor2;
                edit_model.LoanRequestNote = Model.LoanRequestNote;


               
                if (Model.EmployeeID != null)
                {
                    int ID_emp = int.Parse(Model.EmployeeID);
                    var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == ID_emp);
                    edit_model.emp_name = emp.Full_Name;
                }
                dbcontext.SaveChanges();


                var old_install = dbcontext.LoanInstallment.Where(m => m.LoanRequestNumber == edit_model.LoanRequestNumber).ToList();
                dbcontext.LoanInstallment.RemoveRange(old_install);
                dbcontext.SaveChanges();

               
                var start1 = edit_model.StartDate;
                var start2 = edit_model.StartDate;
                var Mon = 1;
                while (start1.Value.CompareTo(Model.EndDate.Value) != 0)
                {
                    Mon++;
                    start1 = start1.Value.AddMonths(1);


                }
                var i = 1;
                for (var item = 0; item < Mon; item++)
                {

                    for (var ii = 0; ii < Model.NumberOfDeductedInstallments; ii++)
                    {
                        var loan_installment = new LoanInstallment
                        {
                            Created_By = User.Identity.Name,
                            Created_Date = DateTime.Now.Date,
                            LoanRequestNumber = edit_model.LoanRequestNumber,
                            InstallmentNumber = i,
                            InstallmentAmount = edit_model.LoanAmount,
                            IsPaid = false,
                            IsFreeze = false,
                            IsActive = true,
                            IsTransfered = false,
                            PaidAmount = 0,
                            InstallmentMonth = (short)start2.Value.Month,
                            InstallmentYear = (short)start2.Value.Year,
                            UnpaidAmount = edit_model.LoanInstallmentAmount,
                            InstallmenNotes = null

                        };

                        dbcontext.LoanInstallment.Add(loan_installment);
                        dbcontext.SaveChanges();
                        i++;
                        if (i > edit_model.NumberOfInstallment)
                        {
                            break;
                        }
                    }
                    start2 = start2.Value.AddMonths(1);

                }
                if (command == "Submit")
                {
                    return RedirectToAction("loan_installment", "LoanRequest", new { number = edit_model.LoanRequestNumber,id=edit_model.ID });
                }
                return RedirectToAction("index");
            }
            catch(Exception)
            {
                return View(Model);
            }
        }
       
        public ActionResult delete(int id)
        {
            try
            {
                var model = dbcontext.LoanRequest.FirstOrDefault(m => m.ID == id);
                return View(model);
            }
            catch(Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        [ActionName("delete")]
        public ActionResult delete_method(int id)
        {
            var model = dbcontext.LoanRequest.FirstOrDefault(m => m.ID == id);
            var sta = dbcontext.status.FirstOrDefault(m => m.ID == model.statusID);
            if (sta.statu == check_status.Approved || sta.statu == check_status.Rejected || sta.statu == check_status.Closed || sta.statu == check_status.Recervied || sta.statu == check_status.Canceled)
            {
                TempData["message"] = HR.Resource.training.status_message;
                return RedirectToAction("index");
            }
            try
            {

               var inst= dbcontext.LoanInstallment.Where(m => m.LoanRequestNumber == model.LoanRequestNumber).ToList();
                dbcontext.LoanInstallment.RemoveRange(inst);
                dbcontext.SaveChanges();
                dbcontext.LoanRequest.Remove(model);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch(Exception)
            {
                return View(model);
            }
        }
        public ActionResult loan_installment(string number,int id)
        {
            try
            {
                ViewBag.id = id;
                var instal = dbcontext.LoanInstallment.Where(m => m.LoanRequestNumber == number).ToList();
                var VM = new List<loan_installmentVM>();
                foreach(var item in instal)
                {
                    VM.Add(new loan_installmentVM { freez = false, LoanInstallment = item });
                }
                return View(VM);
            }
            catch(Exception)
            {
                return RedirectToAction("index");
            }
        }

        [HttpPost]
        public ActionResult loan_installment(FormCollection form,int id)
        {
            try
            {
                ViewBag.id = id;
                var ID_LIST = form["frezz"].Split(',');
                DateTime new_date;
                foreach(var item in ID_LIST)
                {
                    if(item!=" ")
                    {
                        var Id = int.Parse(item);
                        var inst = dbcontext.LoanInstallment.FirstOrDefault(m => m.ID == Id);
                        var loan = dbcontext.LoanInstallment.Where(m => m.LoanRequestNumber == inst.LoanRequestNumber).OrderBy(m => m.InstallmentNumber).ToList();
                        var i = loan.Last().InstallmentNumber;
                        new_date = new DateTime((int)(loan.Last().InstallmentYear), (int)(loan.Last().InstallmentMonth), 1);
                        new_date = new_date.AddMonths(1);
                        inst.IsActive = false;
                        var UN = inst.UnpaidAmount;
                        inst.UnpaidAmount = 0;
                        dbcontext.SaveChanges();

                        i++;

                        var loan_installment = new LoanInstallment
                        {
                            Created_By = User.Identity.Name,
                            Created_Date = DateTime.Now.Date,
                            LoanRequestNumber = inst.LoanRequestNumber,
                            InstallmentNumber = i,
                            InstallmentAmount = inst.InstallmentAmount,
                            IsPaid = false,
                            IsFreeze = false,
                            IsActive = true,
                            IsTransfered = false,
                            PaidAmount = 0,
                            InstallmentMonth = (short)new_date.Month,
                            InstallmentYear = (short)new_date.Year,
                            UnpaidAmount = UN,
                            InstallmenNotes = null

                        };

                        dbcontext.LoanInstallment.Add(loan_installment);
                        dbcontext.SaveChanges();
                    }
                   


                }
                return RedirectToAction("edit",new { id= id });
            }
            catch(Exception)
            {
                return RedirectToAction("index");
            }
        }




        public ActionResult details(int id)
        {
            ViewBag.emp = dbcontext.Employee_Profile.Where(m => m.Active == true).ToList().Select(m => new { Code = m.Code + "--[" + m.Name + ']', ID = m.ID });
            ViewBag.loan_type = dbcontext.LoanInAdvanceSetup.ToList().Select(m => new { Code = m.LoanTypeCode + "--[" + m.LoanTypeDesc + ']', ID = m.ID });

            var loan = dbcontext.LoanRequest.FirstOrDefault(m => m.ID == id);
            return View(loan);
        }
        [HttpPost]
        public ActionResult details(LoanRequest Model, string command)
        {
            ViewBag.emp = dbcontext.Employee_Profile.Where(m => m.Active == true).ToList().Select(m => new { Code = m.Code + "--[" + m.Name + ']', ID = m.ID });
            ViewBag.loan_type = dbcontext.LoanInAdvanceSetup.ToList().Select(m => new { Code = m.LoanTypeCode + "--[" + m.LoanTypeDesc + ']', ID = m.ID });

            if (command == "Submit")
            {
                return RedirectToAction("loan_installment2", "LoanRequest", new { number = Model.LoanRequestNumber, id = Model.ID });
            }
            return RedirectToAction("index");
        }

       
        public ActionResult loan_installment2(string number, int id)
        {
            try
            {
                ViewBag.id = id;
                var instal = dbcontext.LoanInstallment.Where(m => m.LoanRequestNumber == number).ToList();
                var VM = new List<loan_installmentVM>();
                foreach (var item in instal)
                {
                    VM.Add(new loan_installmentVM { freez = false, LoanInstallment = item });
                }
                return View(VM);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult loan_installment2(FormCollection form, int id)
        {
            try
            {
                ViewBag.id = id;
                var ID_LIST = form["frezz"].Split(',');
                DateTime new_date;
                foreach (var item in ID_LIST)
                {
                    if (item != " ")
                    {
                        var Id = int.Parse(item);
                        var inst = dbcontext.LoanInstallment.FirstOrDefault(m => m.ID == Id);
                        var loan = dbcontext.LoanInstallment.Where(m => m.LoanRequestNumber == inst.LoanRequestNumber).OrderBy(m => m.InstallmentNumber).ToList();
                        var i = loan.Last().InstallmentNumber;
                        new_date = new DateTime((int)(loan.Last().InstallmentYear), (int)(loan.Last().InstallmentMonth), 1);
                        new_date = new_date.AddMonths(1);
                        inst.IsActive = false;
                        var UN = inst.UnpaidAmount;
                        inst.UnpaidAmount = 0;
                        dbcontext.SaveChanges();

                        i++;

                        var loan_installment = new LoanInstallment
                        {
                            Created_By = User.Identity.Name,
                            Created_Date = DateTime.Now.Date,
                            LoanRequestNumber = inst.LoanRequestNumber,
                            InstallmentNumber = i,
                            InstallmentAmount = inst.InstallmentAmount,
                            IsPaid = false,
                            IsFreeze = false,
                            IsActive = true,
                            IsTransfered = false,
                            PaidAmount = 0,
                            InstallmentMonth = (short)new_date.Month,
                            InstallmentYear = (short)new_date.Year,
                            UnpaidAmount = UN,
                            InstallmenNotes = null

                        };

                        dbcontext.LoanInstallment.Add(loan_installment);
                        dbcontext.SaveChanges();
                    }



                }
                return RedirectToAction("index");
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }

        public DateTime cal_end_date(DateTime? start,double? num_inst,double? deduction)
        {
            int Num =(int)(num_inst/deduction);
            if(num_inst%deduction>0)
            {
                Num= (int)(num_inst / deduction)+1;
            }
            var end_date = start.Value.AddMonths(Num-1);
            return end_date;
        }
        public JsonResult Getone(DateTime from, DateTime to)
        {
            try
            {
                dbcontext.Configuration.ProxyCreationEnabled = false;
                var req = new List<LoanRequest>();
                    req = dbcontext.LoanRequest.Where(m => DateTime.Compare((DateTime)m.StartDate, from) >= 0 && DateTime.Compare((DateTime)m.EndDate, to) <= 0).ToList();
                foreach(var item in req)
                {
                    item.CanceledBy = item.StartDate.ToString();
                    item.RejectedBy = item.EndDate.ToString();
                }
                return Json(req);
            }
            catch (Exception e)
            {
                return Json(false);
            }
        }
        public JsonResult Getalll()
        {
            try
            {
                dbcontext.Configuration.ProxyCreationEnabled = false;
                var req = new List<LoanRequest>();
                req = dbcontext.LoanRequest.ToList();
                foreach (var item in req)
                {
                    item.CanceledBy = item.StartDate.ToString();
                    item.RejectedBy = item.EndDate.ToString();
                }
                return Json(req);
            }
            catch (Exception e)
            {
                return Json(false);
            }
        }
        ////////////
        public ActionResult Mass_Loan_Request()
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

                ////loan_request_number
                var New_request = new LoanRequest();
                var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Payroll).Structure_Code;
                var All_requests = dbcontext.LoanRequest.ToList();
                if (All_requests.Count() == 0)
                {
                    New_request.LoanRequestNumber = stru + "1";
                }
                else
                {
                    New_request.LoanRequestNumber = stru + (All_requests.LastOrDefault().ID + 1).ToString();
                }
                ///////
                ViewBag.emp = dbcontext.Employee_Profile.Where(m => m.Active == true).ToList().Select(m => new { Code = m.Code + "--[" + m.Name + ']', ID = m.ID });
                ViewBag.loan_type = dbcontext.LoanInAdvanceSetup.ToList().Select(m => new { Code = m.LoanTypeCode + "--[" + m.LoanTypeDesc + ']', ID = m.ID });
                New_request.LoanAmount = 0;
                New_request.NumberOfInstallment = 0;
                New_request.TotalPaidAmount = 0;
                New_request.LoanInstallmentAmount = 0;
                New_request.TotalRemainingAmount = 0;
                New_request.NumberOfDeductedInstallments = 1;
                New_request.StartDate = DateTime.Now.Date;
                New_request.EndDate = DateTime.Now.Date;
                //////
                New_request.Created_By = User.Identity.Name;
                New_request.Created_Date = DateTime.Now.Date;
                /////
                New_request.RequestStatus = check_status.created.GetHashCode();
                New_request.check_status = check_status.created.ToString();
                /////
                return View(New_request);
            }
            catch(Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult Mass_Loan_Request(FormCollection form,LoanRequest Model)
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

                ViewBag.emp = dbcontext.Employee_Profile.Where(m => m.Active == true).ToList().Select(m => new { Code = m.Code + "--[" + m.Name + ']', ID = m.ID });
                ViewBag.loan_type = dbcontext.LoanInAdvanceSetup.ToList().Select(m => new { Code = m.LoanTypeCode + "--[" + m.LoanTypeDesc + ']', ID = m.ID });
                /////////
                var list_emp = new List<Employee_Profile>();
                var ID_emp = form["ID_emp"].Split(',');
                foreach (var item in ID_emp)
                {
                    if (item != "")
                    {
                        var ID = int.Parse(item);
                        var per_em = new per_emp();
                        var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == ID);
                        list_emp.Add(emp);
                    }

                }
                //////////


                if (Model.NumberOfDeductedInstallments > Model.NumberOfInstallment)
                {
                    return View(Model);
                }
                if (list_emp.Count == 0)
                {
                    return View(Model);
                    //int ID_emp = int.Parse(Model.EmployeeID);
                    //var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == ID_emp);
                    //Model.emp_name = emp.Full_Name;
                }

                if (Model.NumberOfInstallment == 0)
                {
                    Model.NumberOfInstallment = 1;
                }
                if (Model.NumberOfDeductedInstallments == 0)
                {
                    Model.NumberOfDeductedInstallments = 1;
                }
                if (Model.StartDate == null)
                {
                    return View(Model);
                }
                if (Model.EndDate == null)
                {
                    Model.EndDate = cal_end_date(Model.StartDate, Model.NumberOfInstallment, Model.NumberOfDeductedInstallments);
                }
                Model.LoanInstallmentAmount = Model.LoanAmount / Model.NumberOfInstallment;
                Model.RequestStatus = check_status.created.GetHashCode();

                ///////////////status////////////////////////


                var Date = Convert.ToDateTime("1/1/1900");
                var s = new status { statu = check_status.created, Type = Models.Infra.Type.employee_record, approved_bydate = Date, cancaled_bydate = Date, created_bydate = DateTime.Now.Date, Rejected_bydate = Date, return_to_reviewdate = Date };
                s.created_by = User.Identity.Name;
                var st = dbcontext.status.Add(s);
                dbcontext.SaveChanges();
                Model.statusID = st.ID;
                //////////////////////////////////////////////


                foreach (var item1 in list_emp)
                {
                    var All_requests = dbcontext.LoanRequest.ToList();
                    var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Payroll).Structure_Code;

                    if (All_requests.Count() == 0)
                    {
                        Model.LoanRequestNumber = stru + "1";
                    }
                    else
                    {
                        Model.LoanRequestNumber = stru + (All_requests.LastOrDefault().ID + 1).ToString();
                    }
                    Model.EmployeeID = item1.ID.ToString();
                    Model.emp_name = item1.Full_Name;
                    var loan = dbcontext.LoanRequest.Add(Model);
                    dbcontext.SaveChanges();
                
               

                var start1 = loan.StartDate;
                var start2 = loan.StartDate;
                var Mon = 1;
                while (start1.Value.CompareTo(Model.EndDate.Value) != 0)
                {
                    Mon++;
                    start1 = start1.Value.AddMonths(1);


                }
                var i = 1;
                    for (var item = 0; item < Mon; item++)
                    {

                        for (var ii = 0; ii < Model.NumberOfDeductedInstallments; ii++)
                        {
                            var loan_installment = new LoanInstallment
                            {
                                Created_By = User.Identity.Name,
                                Created_Date = DateTime.Now.Date,
                                LoanRequestNumber = loan.LoanRequestNumber,
                                InstallmentNumber = i,
                                InstallmentAmount = loan.LoanAmount,
                                IsPaid = false,
                                IsFreeze = false,
                                IsActive = true,
                                IsTransfered = false,
                                PaidAmount = 0,
                                InstallmentMonth = (short)start2.Value.Month,
                                InstallmentYear = (short)start2.Value.Year,
                                UnpaidAmount = loan.LoanInstallmentAmount,
                                InstallmenNotes = null

                            };

                            dbcontext.LoanInstallment.Add(loan_installment);
                            dbcontext.SaveChanges();
                            i++;
                            if (i > loan.NumberOfInstallment)
                            {
                                break;
                            }
                        }
                        start2 = start2.Value.AddMonths(1);
                    }
                }
                return RedirectToAction("index");

            }
            catch (Exception e)
            {
                return View(Model);

            }
           
        }
        public JsonResult worklocation()
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            try
            {
                var list = dbcontext.work_location.ToList().Select(m => new { Code = m.Code + "->" + m.Name, ID = m.ID }).ToList();
                return Json(list);
            }
            catch (Exception)
            {
                return Json(null);
            }
        }
        public JsonResult cadrelevel()
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            try
            {
                var list = dbcontext.job_level_setup.ToList().Select(m => new { Code = m.Code + "->" + m.Name, ID = m.ID }).ToList();
                return Json(list);
            }
            catch (Exception)
            {
                return Json(null);
            }
        }
        public JsonResult costcenter()
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            try
            {
                var list = dbcontext.CostCenter.ToList().Select(m => new { Code = m.CostCenterCode + "->" + m.CostCenterDesc, ID = m.ID }).ToList();
                return Json(list);
            }
            catch (Exception)
            {
                return Json(null);
            }
        }
        public ActionResult status(string id)
        {
            try
            {
                var ID = int.Parse(id);
                var model = dbcontext.LoanRequest.FirstOrDefault(m => m.ID == ID);
                var st = model.status;
                ViewBag.statue = dbcontext.status.ToList().Select(m => new { code = m.approved_by });
                var my_model = new employeestate { status = st, empid = int.Parse(model.EmployeeID), opertion_id = model.ID };
                if (my_model.status.approved_by == null)
                    my_model.status.approved_bydate = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString());
                if (my_model.status.Rejected_by == null)
                    my_model.status.Rejected_bydate = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString());
                if (my_model.status.return_to_reviewby == null)
                    my_model.status.return_to_reviewdate = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString());
                if (my_model.status.cancaled_by == null)
                    my_model.status.cancaled_bydate = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString());
                if (my_model.status.closed_by == null)
                    my_model.status.closed_bydate = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString());
                if (my_model.status.Recervied_by == null)
                    my_model.status.Recervied_bydate = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString());
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
            var record = dbcontext.LoanRequest.FirstOrDefault(m => m.ID == model.opertion_id);
            var sta = record.status;
            if (model.check_status == check_status.Approved)
            {
                sta.approved_by = User.Identity.Name;
                sta.approved_bydate = model.status.approved_bydate;
                sta.statu = check_status.Approved;
                record.check_status = "Approved";
                record.RequestStatus = check_status.Approved.GetHashCode();

                dbcontext.SaveChanges();
                var loan = dbcontext.LoanRequest.FirstOrDefault(m => m.ID == model.opertion_id);
               
            }
            else if (model.check_status == check_status.Closed)
            {
                sta.closed_by = User.Identity.Name;
                sta.closed_bydate = model.status.closed_bydate;
                sta.statu = check_status.Closed;
                record.check_status = "Closed";
                record.RequestStatus = check_status.Closed.GetHashCode();

                dbcontext.SaveChanges();
                var loan = dbcontext.LoanRequest.FirstOrDefault(m => m.ID == model.opertion_id);

            }
            else if (model.check_status == check_status.Canceled)
            {
                sta.cancaled_by = User.Identity.Name;
                sta.cancaled_bydate = model.status.cancaled_bydate;
                sta.statu = check_status.Canceled;
                record.check_status = "Canceled";
                record.RequestStatus = check_status.Canceled.GetHashCode();

                dbcontext.SaveChanges();
                var loan = dbcontext.LoanRequest.FirstOrDefault(m => m.ID == model.opertion_id);

            }
            else if (model.check_status == check_status.Recervied)
            {
                sta.Recervied_by = User.Identity.Name;
                sta.Recervied_bydate = model.status.Recervied_bydate;
                sta.statu = check_status.Recervied;
                record.check_status = "Recervied";
                record.RequestStatus = check_status.Recervied.GetHashCode();

                dbcontext.SaveChanges();
                var loan = dbcontext.LoanRequest.FirstOrDefault(m => m.ID == model.opertion_id);

            }
            else if (model.check_status == check_status.Rejected)
            {
                sta.Rejected_by = User.Identity.Name;
                sta.Rejected_bydate = model.status.Rejected_bydate;
                sta.statu = check_status.Rejected;
                record.check_status = "Rejected";
                record.RequestStatus = check_status.Rejected.GetHashCode();


                dbcontext.SaveChanges();
            }
            else if (model.check_status == check_status.Return_To_Review)
            {
                sta.return_to_reviewby = User.Identity.Name;
                sta.return_to_reviewdate = model.status.return_to_reviewdate;
                sta.statu = check_status.Return_To_Review;
                record.check_status = "RETURN TO REVIEW";
                record.RequestStatus = check_status.Return_To_Review.GetHashCode();


                dbcontext.SaveChanges();
            }

            return RedirectToAction("index");
        }


       
    }
}