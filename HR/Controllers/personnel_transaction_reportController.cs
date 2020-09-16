using HR.Models;
using HR.Models.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers
{
    public class personnel_transaction_reportController : BaseController
    {
        // GET: personnel_transaction_report
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin,personnel,personnelReport")]
        public ActionResult personnel_transaction()
        {
            try
            {
                var model = new data_report { EOS_reson = new EOS_type(),trans_type=new transaction_type() };

                ViewBag.empll = dbcontext.Employee_Profile.Where(m => m.Active == true).ToList().Select(m => new { Code = m.Code + "------[" + m.Full_Name + ']', ID = m.ID });
                ViewBag.job_desc = dbcontext.job_title_cards.ToList().Select(m => new { Code = m.Code + "------[" + m.name + ']', ID = m.ID });
                ViewBag.location_desc = dbcontext.work_location.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.cadre = dbcontext.job_level_setup.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Organization_Chart = dbcontext.Organization_Chart.ToList().Select(m => new { Code = m.Code + "------[" + m.unit_Description + ']', ID = m.ID });
                ViewBag.cost = dbcontext.CostCenter.ToList().Select(m => new { Code = m.CostCenterCode + "->" + m.CostCenterDesc, ID = m.ID });
                ViewBag.shift = dbcontext.Shift_setup.ToList().Select(m => new { Code = m.Code + "->" + m.Name, ID = m.ID });

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult personnel_transaction(data_report mydata, FormCollection form)
        {
            try
            {
                ViewBag.empll = dbcontext.Employee_Profile.Where(m => m.Active == true).ToList().Select(m => new { Code = m.Code + "------[" + m.Full_Name + ']', ID = m.ID });
                ViewBag.job_desc = dbcontext.job_title_cards.ToList().Select(m => new { Code = m.Code + "------[" + m.name + ']', ID = m.ID });
                ViewBag.location_desc = dbcontext.work_location.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.cadre = dbcontext.job_level_setup.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Organization_Chart = dbcontext.Organization_Chart.ToList().Select(m => new { Code = m.Code + "------[" + m.unit_Description + ']', ID = m.ID });
                ViewBag.cost = dbcontext.CostCenter.ToList().Select(m => new { Code = m.CostCenterCode + "->" + m.CostCenterDesc, ID = m.ID });
                ViewBag.shift = dbcontext.Shift_setup.ToList().Select(m => new { Code = m.Code + "->" + m.Name, ID = m.ID });


                var all_request = dbcontext.personnel_transaction.ToList();
                var my_report = new List<personnel_transaction>();

                if (mydata.trans_date_from != null || mydata.trans_date_to != null)
                {
                    if (mydata.trans_date_from > mydata.trans_date_to)
                    {
                        TempData["Message"] = "Error in request date";
                        return View(mydata);
                    }
                    else
                    {
                        my_report.AddRange(all_request.Where(m => m.transaction_date >= mydata.trans_date_from && m.transaction_date <= mydata.trans_date_to).ToList());
                    }
                }
                if (mydata.effective_date_from != null || mydata.effective_date_to != null)
                {
                    if (mydata.effective_date_from > mydata.effective_date_to)
                    {
                        TempData["Message"] = "Error in effective date";
                        return View(mydata);
                    }
                    else
                    {
                        var E_d = all_request.Where(m => Convert.ToDateTime(m.Effective_date) >= mydata.effective_date_from && Convert.ToDateTime(m.Effective_date) <= mydata.effective_date_to).ToList();
                        foreach (var item in E_d)
                        {
                            if (!my_report.Contains(item))
                            {
                                my_report.Add(item);
                            }
                        }


                    }
                }
                if (mydata.end_of_Service_date_from != null || mydata.end_of_Service_date_to != null)
                {
                    if (mydata.end_of_Service_date_from > mydata.end_of_Service_date_to)
                    {
                        TempData["Message"] = "Error in end of Service date";
                        return View(mydata);
                    }
                    else
                    {
                        var E_d = all_request.Where(m => Convert.ToDateTime(m.End_of_service_date) >= mydata.end_of_Service_date_from && Convert.ToDateTime(m.End_of_service_date) <= mydata.end_of_Service_date_to).ToList();
                        foreach (var item in E_d)
                        {
                            if (!my_report.Contains(item))
                            {
                                my_report.Add(item);
                            }
                        }


                    }
                }
                if (mydata.last_working_date_from != null || mydata.last_working_date_to != null)
                {
                    if (mydata.last_working_date_from > mydata.last_working_date_to)
                    {
                        TempData["Message"] = "Error in last working date";
                        return View(mydata);
                    }
                    else
                    {
                        var E_d = all_request.Where(m => Convert.ToDateTime(m.Last_working_date) >= mydata.last_working_date_from && Convert.ToDateTime(m.Last_working_date) <= mydata.last_working_date_to).ToList();
                        foreach (var item in E_d)
                        {
                            if (!my_report.Contains(item))
                            {
                                my_report.Add(item);
                            }
                        }


                    }
                }



                
                //=======================================
                var Employee = form["em"].Split(char.Parse(","));
                var Status = form["status"].Split(char.Parse(","));
                var job = form["job_"].Split(char.Parse(","));
                var loaction = form["loaction_"].Split(char.Parse(","));
                var cadre = form["cadre_"].Split(char.Parse(","));
                var unit = form["unit_"].Split(char.Parse(","));
                var Cost = form["cost_"].Split(char.Parse(","));
                var Shift = form["shift_"].Split(char.Parse(","));
                var Eos_reson = form["EOS_reson"].Split(char.Parse(","));
                var Transaction_type = form["trans_type"].Split(char.Parse(","));
                var List = form["List_Display"].Split(char.Parse(","));

                var flag1 = new Boolean[12];
                for (var i = 0; i < 12; i++)
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
                foreach (var item in Employee)
                {
                    if (item != "")
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
                foreach (var item in job)
                {
                    if (item != "")
                    {
                        var em_id = int.Parse(item);
                        var emp = dbcontext.job_title_cards.FirstOrDefault(m => m.ID == em_id);
                        var E_d_2 = all_request.Where(m => m.job_title_cards == emp).ToList();
                        foreach (var item2 in E_d_2)
                        {
                            if (!my_report.Contains(item2))
                            {
                                my_report.Add(item2);
                            }
                        }
                    }
                }
                foreach (var item in loaction)
                {
                    if (item != "")
                    {
                        var em_id = int.Parse(item);
                        var emp = dbcontext.work_location.FirstOrDefault(m => m.ID == em_id);
                        var E_d_2 = all_request.Where(m => m.work_location == emp).ToList();
                        foreach (var item2 in E_d_2)
                        {
                            if (!my_report.Contains(item2))
                            {
                                my_report.Add(item2);
                            }
                        }
                    }
                }
                foreach (var item in cadre)
                {
                    if (item != "")
                    {
                        var em_id = int.Parse(item);
                        var emp = dbcontext.job_level_setup.FirstOrDefault(m => m.ID == em_id);
                        var E_d_2 = all_request.Where(m => m.job_level_setup == emp).ToList();
                        foreach (var item2 in E_d_2)
                        {
                            if (!my_report.Contains(item2))
                            {
                                my_report.Add(item2);
                            }
                        }
                    }
                }
                foreach (var item in unit)
                {
                    if (item != "")
                    {
                        var em_id = int.Parse(item);
                        var emp = dbcontext.Organization_Chart.FirstOrDefault(m => m.ID == em_id);
                        var E_d_2 = all_request.Where(m => m.Organization_ChartId == emp.ID.ToString()).ToList();
                        foreach (var item2 in E_d_2)
                        {
                            if (!my_report.Contains(item2))
                            {
                                my_report.Add(item2);
                            }
                        }
                    }
                }
                foreach (var item in Cost)
                {
                    if (item != "")
                    {
                        var em_id = int.Parse(item);
                        var emp = dbcontext.CostCenter.FirstOrDefault(m => m.ID == em_id);
                        var E_d_2 = all_request.Where(m => m.cost_center_id == emp.ID).ToList();
                        foreach (var item2 in E_d_2)
                        {
                            if (!my_report.Contains(item2))
                            {
                                my_report.Add(item2);
                            }
                        }
                    }
                }
                foreach (var item in Shift)
                {
                    if (item != "")
                    {
                        var em_id = int.Parse(item);
                        var emp = dbcontext.Shift_setup.FirstOrDefault(m => m.ID == em_id);
                        var E_d_2 = all_request.Where(m => m.shift_id == emp.ID).ToList();
                        foreach (var item2 in E_d_2)
                        {
                            if (!my_report.Contains(item2))
                            {
                                my_report.Add(item2);
                            }
                        }
                    }
                }
                foreach (var item in Transaction_type)
                {
                    if (item != "")
                    {
                        var em_id = int.Parse(item);
                        var so = all_request.Where(m => m.Transaction_type == (transaction_type)em_id).ToList();

                        foreach (var item2 in so)
                        {
                            if (!my_report.Contains(item2))
                            {
                                my_report.Add(item2);
                            }
                        }
                    }
                }
                foreach (var item in Eos_reson)
                {
                    if (item != "")
                    {
                        var em_id = int.Parse(item);
                        var so = all_request.Where(m => m.EOS_reasons == (EOS_type)em_id).ToList();

                        foreach (var item2 in so)
                        {
                            if (!my_report.Contains(item2))
                            {
                                my_report.Add(item2);
                            }
                        }
                    }
                }
                foreach (var item in Status)
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
                var my_repo = new report_tran { flag = flag1, my_list = my_report };
                return View("show_report", my_repo);

            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        public ActionResult show_report(report_tran Report_)
        {
            if (Report_ != null)
            {
                return View(Report_);
            }
            else
            {
                return RedirectToAction("index", "Home");
            }
        }

    }
    public class data_report
    {
        public DateTime trans_date_from { get; set; }

        public DateTime trans_date_to { get; set; }

        public DateTime effective_date_from { get; set; }

        public DateTime effective_date_to { get; set; }
        public DateTime end_of_Service_date_from { get; set; }

        public DateTime end_of_Service_date_to { get; set; }
        public DateTime last_working_date_from { get; set; }

        public DateTime last_working_date_to { get; set; }
        public transaction_type trans_type { get; set; }
        public EOS_type EOS_reson { get; set; }

    }
    public class report_tran
    {
        public List<personnel_transaction> my_list { get; set; }
        public Boolean[] flag { get; set; }
    }
}