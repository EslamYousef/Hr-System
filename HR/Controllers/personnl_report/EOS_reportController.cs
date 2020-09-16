using HR.Models;
using HR.Models.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers.personnl_report
{
    public class EOS_reportController : BaseController
    {
        // GET: EOS_report
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin,personnel,personnelReport")]
        public ActionResult EOS_report_co()
        {
            try
            {
                var model = new eos_report { EOS_type = new EOS_type(), date_of_eos_from = DateTime.Now.Date, date_of_eos_to = DateTime.Now.Date, have_alone = false, notice_period = 0, request_date_from = DateTime.Now, request_date_to = DateTime.Now, transaferred_to_payroll = false };
                var employee = dbcontext.Employee_Profile.ToList();
                ViewBag.interview = dbcontext.EOS_Interview_Questions_Groups.ToList().Select(m => new { Code = "" + m.Questions_Group_Code + "-----[" + m.Description_of + ']', ID = m.ID }).ToList();
                ViewBag.check_list = dbcontext.Check_List_Item_Groups.ToList().Select(m => new { Code = "" + m.Group_Code + "-----[" + m.Description_Group + ']', ID = m.ID }).ToList();
                ViewBag.emp = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Full_Name + ']', ID = m.ID });
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult EOS_report_co(eos_report mydata ,FormCollection form)
        {
            try
            {
                var employee = dbcontext.Employee_Profile.ToList();
                ViewBag.interview = dbcontext.EOS_Interview_Questions_Groups.ToList().Select(m => new { Code = "" + m.Questions_Group_Code + "-----[" + m.Description_of + ']', ID = m.ID }).ToList();
                ViewBag.check_list = dbcontext.Check_List_Item_Groups.ToList().Select(m => new { Code = "" + m.Group_Code + "-----[" + m.Description_Group + ']', ID = m.ID }).ToList();
                ViewBag.emp = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Full_Name + ']', ID = m.ID });

                var all_request = dbcontext.EOS_Request.ToList();
                var my_report = new List<EOS_Request>();
                
                if(mydata.request_date_from!=null || mydata.request_date_to != null)
                {
                    if(mydata.request_date_from>mydata.request_date_to)
                    {
                        TempData["Message"] = "Error in request date";
                        return View(mydata);
                    }
                    else
                    {
                        my_report.AddRange(all_request.Where(m => m.Requset_date >= mydata.request_date_from && m.Requset_date <= mydata.request_date_to).ToList());
                    }
                }
                if (mydata.date_of_eos_from != null || mydata.date_of_eos_to != null)
                {
                    if (mydata.date_of_eos_from > mydata.date_of_eos_to)
                    {
                        TempData["Message"] = "Error in Eos date";
                        return View(mydata);
                    }
                    else
                    {
                        var E_d = all_request.Where(m => Convert.ToDateTime(m.eos_Date) >= mydata.date_of_eos_from && Convert.ToDateTime(m.eos_Date) <= mydata.date_of_eos_to).ToList();
                        foreach(var item in E_d)
                        {
                            if (!my_report.Contains(item))
                            {
                                my_report.Add(item);
                            }
                        }
                       

                    }
                }
                if(mydata.notice_period>=0)
                {
                    var E_d1 = all_request.Where(m => m.Notice_period == mydata.notice_period).ToList();
                    foreach (var item in E_d1)
                    {
                        if (!my_report.Contains(item))
                        {
                            my_report.Add(item);
                        }
                    }
                }
                if (mydata.notice_period >= 0)
                {
                    var E_d2 = all_request.Where(m => m.Notice_period == mydata.notice_period).ToList();
                    foreach (var item in E_d2)
                    {
                        if (!my_report.Contains(item))
                        {
                            my_report.Add(item);
                        }
                    }
                }
                
                    var E_d3 = all_request.Where(m => m.are_the_employee_has_a_loan_or_advanced == mydata.have_alone).ToList();
                    foreach (var item in E_d3)
                    {
                        if (!my_report.Contains(item))
                        {
                            my_report.Add(item);
                        }
                    }
                var E_d4 = all_request.Where(m => m.are_the_settlement_transferred_to_payroll == mydata.transaferred_to_payroll).ToList();
                foreach (var item in E_d4)
                {
                    if (!my_report.Contains(item))
                    {
                        my_report.Add(item);
                    }
                }
                //=======================================
                var E = form["em"].Split(char.Parse(","));
                var I = form["interV"].Split(char.Parse(","));
                var C = form["checkL"].Split(char.Parse(","));
                var E_T = form["eosT"].Split(char.Parse(","));
                var S = form["status"].Split(char.Parse(","));
                var List = form["List_Display"].Split(char.Parse(","));
                var flag1 = new Boolean[11];
                for (var i = 0; i < 11; i++)
                {
                    flag1[i] = false;
                }
                foreach (var item in List)
                {
                    if (item != "")
                    {
                        var index = int.Parse(item);
                        flag1[index] = true;
                    }
                }
                foreach(var item in E)
                {
                    if(item!="")
                    {
                        var em_id = int.Parse(item);
                        var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == em_id);
                        var E_d_ = all_request.Where(m => m.Employee == emp).ToList();
                        foreach (var item2 in E_d_)
                        {
                            if (!my_report.Contains(item2))
                            {
                                my_report.Add(item2);
                            }
                        }
                    }
                }
                foreach (var item in  I)
                {
                    if (item != "")
                    {
                        var em_id = int.Parse(item);
                        var emp = dbcontext.EOS_Interview_Questions_Groups.FirstOrDefault(m => m.ID == em_id);
                        var E_d_2 = all_request.Where(m => m.EOS_group == emp).ToList();
                        foreach (var item2 in E_d_2)
                        {
                            if (!my_report.Contains(item2))
                            {
                                my_report.Add(item2);
                            }
                        }
                    }
                }
                foreach (var item in C)
                {
                    if (item != "")
                    {
                        var em_id = int.Parse(item);
                        var emp = dbcontext.Check_List_Item_Groups.FirstOrDefault(m => m.ID == em_id);
                        var E_d_2 = all_request.Where(m => m.Check_List_Item_Groups == emp).ToList();
                        foreach (var item2 in E_d_2)
                        {
                            if (!my_report.Contains(item2))
                            {
                                my_report.Add(item2);
                            }
                        }
                    }
                }
                foreach (var item in E_T)
                {
                    if (item != "")
                    {
                        var em_id = int.Parse(item);
                        var so = all_request.Where(m => m.EOS_type == (EOS_type)em_id).ToList();
                        
                        foreach (var item2 in so)
                        {
                            if (!my_report.Contains(item2))
                            {
                                my_report.Add(item2);
                            }
                        }
                    }
                }
                foreach (var item in S)
                {
                    if (item != "")
                    {
                        var em_id = int.Parse(item);
                        var so = all_request.Where(m => m.status.statu == (check_status)em_id).ToList();

                        foreach (var item2 in so)
                        {
                            if (!my_report.Contains(item2))
                            {
                                my_report.Add(item2);
                            }
                        }
                    }
                }
                var my_repo = new REPORT { flag=flag1,my_list=my_report};
                return View("show_report", my_repo);

            }
            catch (Exception)
            {
                return RedirectToAction("index");   
            }
        }
        public ActionResult show_report(REPORT Report_)
        {
            if(Report_!=null)
            {
                return View(Report_);
            }
            else
            {
                return RedirectToAction("index", "Home");
            }
        }
        


    }
        public class eos_report
        {
            public DateTime request_date_from { get; set; }
            public DateTime request_date_to { get; set; }
            public DateTime date_of_eos_from { get; set; }
            public DateTime date_of_eos_to { get; set; }
            public int notice_period { get; set; }
            public bool have_alone { get; set; }
            public bool transaferred_to_payroll { get; set; }
            public EOS_type EOS_type { get; set; }

        }
    public class REPORT
    {
        public List<EOS_Request> my_list { get; set; }
        public Boolean[] flag { get; set; }
    }
    }
   