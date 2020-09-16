using HR.Models;
using HR.Models.penalities.setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers
{
    public class penality_reportController : Controller
    {
        // GET: penality_report
        ApplicationDbContext dbcontext = new ApplicationDbContext();

        [Authorize(Roles = "Admin,personnel,personnelReport")]
        public ActionResult penlaity_report_co()
        {
            try
            {
               
                ViewBag.emp = dbcontext.Employee_Profile.Where(m=>m.Active==true).Select(m => new { code = m.Code +"->"+m.Full_Name, id = m.ID });
                ViewBag.punis = dbcontext.Discipline_Punishment.ToList().Select(m => new { Code = "" + m.Punishment_Code + "--[" + m.Punishment_Desc + ']', ID = m.ID }).ToList();
                var M = new PVM ();
                return View(M);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult penlaity_report_co(PVM mydata,FormCollection form)
        {
            try
            {
                ViewBag.emp = dbcontext.Employee_Profile.Where(m => m.Active == true).Select(m => new { code = m.Code + "->" + m.Full_Name, id = m.ID });
                ViewBag.punis = dbcontext.Discipline_Punishment.ToList().Select(m => new { Code = "" + m.Punishment_Code + "--[" + m.Punishment_Desc + ']', ID = m.ID }).ToList();
                var all_request = dbcontext.Discipline_PunishmentTransaction.ToList();
                var list_show = new List<repo_show>();

                //=======================================
                if (mydata.trans_Date_from != null || mydata.trans_Date_to != null)
                {
                    if (mydata.trans_Date_from > mydata.trans_Date_to)
                    {
                        TempData["Message"] = "Error in request date";
                        return View(mydata);
                    }
                    else
                    {
                        var puni = all_request.Where(m => m.Transaction_Date >= mydata.trans_Date_from && m.Transaction_Date <= mydata.trans_Date_to).ToList();
                        foreach (var item in puni)
                        {
                            var all_details = dbcontext.Discipline_PunishmentTransaction_Detail.Where(m => m.Transaction_Number == item.Transaction_Number).ToList();
                            foreach (var it in all_details)
                            {
                                if (check(list_show,it))
                                {
                                    var new_add = new repo_show { Discipline_PunishmentTransaction_Detail = it, number = item.Transaction_Number, emp_name = item.em, event_date = (DateTime)item.Event_Date, tran_date = (DateTime)item.Transaction_Date, status = item.stat.statu.ToString() };
                                    list_show.Add(new_add);
                                }
                            }
                        }
                    }
                }
                if (mydata.event_Date_from != null || mydata.event_Date_to != null)
                {
                    if (mydata.event_Date_from > mydata.event_Date_to)
                    {
                        TempData["Message"] = "Error in event date";
                        return View(mydata);
                    }
                    else
                    {
                       var puni= all_request.Where(m => m.Event_Date >= mydata.event_Date_from && m.Event_Date <= mydata.event_Date_to).ToList();
                        foreach (var item in puni)
                        {
                            var all_details = dbcontext.Discipline_PunishmentTransaction_Detail.Where(m => m.Transaction_Number == item.Transaction_Number).ToList();
                            foreach (var it in all_details)
                            {
                                if (check(list_show, it))
                                {
                                    var new_add = new repo_show { Discipline_PunishmentTransaction_Detail = it, number = item.Transaction_Number, emp_name = item.em, event_date = (DateTime)item.Event_Date, tran_date = (DateTime)item.Transaction_Date, status = item.stat.statu.ToString() };
                                    list_show.Add(new_add);
                                }
                            }
                        }

                    }
                }
                if (mydata.rest_date_from != null || mydata.rest_date_to != null)
                {
                    if (mydata.rest_date_from > mydata.rest_date_to)
                    {
                        TempData["Message"] = "Error in reset date";
                        return View(mydata);
                    }
                    else
                    {
                        foreach (var item in all_request)
                        {
                            var all_details = dbcontext.Discipline_PunishmentTransaction_Detail.Where(m => m.Transaction_Number == item.Transaction_Number).ToList();
                            foreach(var i in all_details)
                            {
                                if (i.Punishment_RestDate >= mydata.rest_date_from && i.Punishment_RestDate <= mydata.rest_date_from)
                                {
                                    if (check(list_show, i))
                                    {
                                        var new_add = new repo_show { Discipline_PunishmentTransaction_Detail = i, number = item.Transaction_Number, emp_name = item.em, event_date = (DateTime)item.Event_Date, tran_date = (DateTime)item.Transaction_Date, status = item.stat.statu.ToString() };
                                        list_show.Add(new_add);
                                    }
                                }
                            }
                          
                        }
                    }
                }
                //=======================================
                var E = form["E"].Split(char.Parse(","));
                var P = form["P"].Split(char.Parse(","));
                var S = form["S"].Split(char.Parse(","));
                var List = form["List_Display"].Split(char.Parse(","));

                var flag1 = new Boolean[8];
               
                for (var i = 0; i < 8; i++)
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
                foreach (var item in E)
                {
                    if (item != "")
                    {
                        var pun = all_request.Where(m => m.Employee_Code ==item).ToList();
                        foreach (var item2 in pun)
                        {
                            var all_details = dbcontext.Discipline_PunishmentTransaction_Detail.Where(m => m.Transaction_Number == item2.Transaction_Number).ToList();
                            foreach (var i in all_details)
                            {
                                if (check(list_show, i))
                                {
                                    var w_add = new repo_show { Discipline_PunishmentTransaction_Detail = i, number = item2.Transaction_Number, emp_name = item2.em, event_date = (DateTime)item2.Event_Date, tran_date = (DateTime)item2.Transaction_Date, status = item2.stat.statu.ToString() };

                                    list_show.Add(w_add);
                                }
                            }

                        }
                    }
                }
                foreach (var item in S)
                {
                    if (item != "")
                    {
                        var em_id = int.Parse(item);
                        var pun = all_request.Where(m => m.stat.statu == (Models.Infra.check_status) em_id).ToList();
                        foreach (var item2 in pun)
                        {
                            var all_details = dbcontext.Discipline_PunishmentTransaction_Detail.Where(m => m.Transaction_Number == item2.Transaction_Number).ToList();
                            foreach (var i in all_details)
                            {
                                if (check(list_show, i))
                                {
                                    var w_add = new repo_show { Discipline_PunishmentTransaction_Detail = i, number = item2.Transaction_Number, emp_name = item2.em, event_date = (DateTime)item2.Event_Date, tran_date = (DateTime)item2.Transaction_Date, status = item2.stat.statu.ToString() };

                                    list_show.Add(w_add);
                                }
                            }

                        }
                    }
                }
                foreach (var item in P)
                {
                    if (item != "")
                    {
                        
                        foreach (var item2 in all_request)
                        {
                            var all_details = dbcontext.Discipline_PunishmentTransaction_Detail.Where(m => m.Transaction_Number == item2.Transaction_Number).ToList();
                            foreach (var i in all_details)
                            {
                                if (i.Punishment_Code == item)
                                {
                                    if (check(list_show, i))
                                    {
                                        var w_add = new repo_show { Discipline_PunishmentTransaction_Detail = i, number = item2.Transaction_Number, emp_name = item2.em, event_date = (DateTime)item2.Event_Date, tran_date = (DateTime)item2.Transaction_Date, status = item2.stat.statu.ToString() };

                                        list_show.Add(w_add);
                                    }
                                }
                            }

                        }
                    }
                }


                var my_repo = new report_3 { flag = flag1, details_pun = list_show };
                return View("show_report", my_repo);

            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        public ActionResult show_report(REPORT_2 Report_)
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
        public bool check(List<repo_show> items, Discipline_PunishmentTransaction_Detail adding_item)
        {
            foreach (var i in items)
            {
                if (i.Discipline_PunishmentTransaction_Detail.ID == adding_item.ID)
                {
                    return false;
                }
            }
            return true;
        }
    }
    public class PVM
    {
        public DateTime trans_Date_from { get; set; }
        public DateTime trans_Date_to { get; set; }
        public DateTime event_Date_from { get; set; }
        public DateTime event_Date_to { get; set; }
        public DateTime rest_date_from { get; set; }
        public DateTime rest_date_to { get; set; }
    }
    public class repo_show
    {
        public Discipline_PunishmentTransaction_Detail Discipline_PunishmentTransaction_Detail { get; set; }
        public string number { get; set; }
        public DateTime tran_date { get; set; }
        public DateTime event_date { get; set; }
        public string emp_name { get; set; }
        public string status { get; set; }

    }
    public class report_3
    {
        public Boolean[] flag { get; set; }
        public List<repo_show> details_pun { get; set; }
    }
}