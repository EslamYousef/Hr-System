using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using System.Threading;
using HR.Models;
using Microsoft.AspNet.Identity;

namespace HR.Controllers
{
    [HandleError]
    [Authorize]
    public class BaseController : Controller
    {
        public BaseController()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var date = context.date_alert_daily.ToList();
            if (date.Count == 0)
            {
                context.date_alert_daily.Add(new Models.user.date_alert_daily { date = DateTime.Now.Date });
                context.SaveChanges();
            }
            if (date.Count > 0)
            {
                var emp = context.Employee_Profile.ToList();

                
                var expirebefore = context.notifications.FirstOrDefault(m => m.type_field == Models.user.type_field.date_field && m.Field == "expire date" && m.Action == Models.user.Action.before_due_Date);
                var expire_after = context.notifications.FirstOrDefault(m => m.type_field == Models.user.type_field.date_field && m.Field == "expire date" && m.Action == Models.user.Action.after_due_Date);

                var issue_date_before = context.notifications.FirstOrDefault(m => m.type_field == Models.user.type_field.date_field && m.Field == "issue date" && m.Action == Models.user.Action.before_due_Date);
                var issue_date_after = context.notifications.FirstOrDefault(m => m.type_field == Models.user.type_field.date_field && m.Field == "issue date" && m.Action == Models.user.Action.after_due_Date);

                var contract_end_date_before = context.notifications.FirstOrDefault(m => m.type_field == Models.user.type_field.date_field && m.Field == "contract end date" && m.Action == Models.user.Action.before_due_Date);
                var contract_end_date_after = context.notifications.FirstOrDefault(m => m.type_field == Models.user.type_field.date_field && m.Field == "contract end date" && m.Action == Models.user.Action.after_due_Date);

                while (date[0].date.Date <= DateTime.Now.Date)
                {
                    var date_ = date[0].date;
                    //=========================================================================
                    if (contract_end_date_before != null)
                    {
                        var contract_end_datebefore_list = new List<Employee_Profile>();
                        foreach (var i in emp)
                        {
                            var contract = context.Employee_contract_profile.FirstOrDefault(m => m.Active == true && m.Employee_ProfileId == i.ID.ToString());
                            if(contract!=null)
                            {
                                if ((contract.Contract_end_date.Date - date_.Date).Days == contract_end_date_before.number)
                                {
                                    contract_end_datebefore_list.Add(i);
                                }
                            }
                           
                        }
                        foreach (var item in contract_end_datebefore_list)
                        {
                            var check = context.Alert_inbox.FirstOrDefault(m => m.check_name == "contract end date before" + item.ID);
                            if (check == null)
                            {

                                context.Alert_inbox.Add(new Models.user.Alert_inbox { check_name = "contract end date before" + item.ID, send_from_user_id = item.Full_Name, send_to_user_id = contract_end_date_before.send_to_ID_user, title = contract_end_date_before.Subject, Subject = contract_end_date_before.Message, until = contract_end_date_before.until });
                                context.SaveChanges();
                            }
                        }
                    }
                    //================================================================================
                    if (contract_end_date_after != null)
                    {

                        var contract_end_datebefore_list = new List<Employee_Profile>();
                        foreach (var i in emp)
                        {
                            var contract = context.Employee_contract_profile.FirstOrDefault(m => m.Active == true && m.Employee_ProfileId == i.ID.ToString());
                            if (contract != null)
                            {
                                if ((date_.Date - contract.Contract_end_date.Date).Days == contract_end_date_after.number)
                                {
                                    contract_end_datebefore_list.Add(i);
                                }
                            }
                        }
                        foreach (var item in contract_end_datebefore_list)
                        {
                            var check = context.Alert_inbox.FirstOrDefault(m => m.check_name == "contract end date after" + item.ID);
                            if (check == null)
                            {

                                context.Alert_inbox.Add(new Models.user.Alert_inbox { check_name = "contract end date after" + item.ID, send_from_user_id = item.Full_Name, send_to_user_id = contract_end_date_after.send_to_ID_user, title = contract_end_date_after.Subject, Subject = contract_end_date_after.Message, until = contract_end_date_after.until });
                                context.SaveChanges();
                            }
                        }
                    }
                    //=========================================================================
                    if (issue_date_before != null)
                    {
                        var contract_end_datebefore_list = new List<Employee_Profile>();
                        foreach (var i in emp)
                        {
                            if ((i.Issue_date.Date - date_.Date).Days == issue_date_before.number)
                            {
                                contract_end_datebefore_list.Add(i);
                            }
                        }
                        foreach (var item in contract_end_datebefore_list)
                        {
                            var check = context.Alert_inbox.FirstOrDefault(m => m.check_name == "issue date before" + item.ID);
                            if (check == null)
                            {

                                context.Alert_inbox.Add(new Models.user.Alert_inbox { check_name = "issue date before" + item.ID, send_from_user_id = item.Full_Name, send_to_user_id = issue_date_before.send_to_ID_user, title = issue_date_before.Subject, Subject = issue_date_before.Message, until = issue_date_before.until });
                                context.SaveChanges();
                            }
                        }
                    }
                    
                    //==============================================================================
                    if (issue_date_after != null)
                    {
                       
                        var contract_end_datebefore_list = new List<Employee_Profile>();
                        foreach (var i in emp)
                        {
                            if ((date_.Date - i.Issue_date.Date).Days == issue_date_after.number)
                            {
                                contract_end_datebefore_list.Add(i);
                            }
                        }
                        foreach (var item in contract_end_datebefore_list)
                        {
                            var check = context.Alert_inbox.FirstOrDefault(m => m.check_name == "issue date after" + item.ID);
                            if (check == null)
                            {

                                context.Alert_inbox.Add(new Models.user.Alert_inbox { check_name = "issue date after" + item.ID, send_from_user_id = item.Full_Name, send_to_user_id = issue_date_after.send_to_ID_user, title = issue_date_after.Subject, Subject = issue_date_after.Message, until = issue_date_after.until });
                                context.SaveChanges();
                            }
                        }
                    }
                    //=========================================================================
                    if (expirebefore != null)
                    {
                        var contract_end_datebefore_list = new List<Employee_Profile>();
                          foreach (var i in emp)
                        {
                            if ((i.Expire_date.Date - date_.Date).Days == expirebefore.number)
                            {
                                contract_end_datebefore_list.Add(i);
                            }
                        }
                        foreach (var item in contract_end_datebefore_list)
                        {
                            var check = context.Alert_inbox.FirstOrDefault(m => m.check_name == "expire date before" + item.ID);
                            if (check == null)
                            {

                                context.Alert_inbox.Add(new Models.user.Alert_inbox { check_name = "expire date before" + item.ID, send_from_user_id = item.Full_Name, send_to_user_id = expirebefore.send_to_ID_user, title = expirebefore.Subject, Subject = expirebefore.Message, until = expirebefore.until });
                                context.SaveChanges();
                            }
                        }
                    }
                    //==============================================================================
                    if (expire_after != null)
                    {
                      
                        var contract_end_datebefore_list = new List<Employee_Profile>();
                        foreach (var i in emp)
                        {
                            if ((date_.Date - i.Expire_date.Date).Days == expire_after.number)
                            {
                                contract_end_datebefore_list.Add(i);
                            }
                        }
                        foreach (var item in contract_end_datebefore_list)
                        {
                            var check = context.Alert_inbox.FirstOrDefault(m => m.check_name == "expire date after" + item.ID);
                            if (check == null)
                            {

                                context.Alert_inbox.Add(new Models.user.Alert_inbox { check_name = "expire date after" + item.ID, send_from_user_id = item.Full_Name, send_to_user_id = expire_after.send_to_ID_user, title = expire_after.Subject, Subject = expire_after.Message, until = expire_after.until });
                                context.SaveChanges();
                            }
                        }
                    }
                    date[0].date = date[0].date.AddDays(1);
                }

                date[0].date = DateTime.Now;
                context.SaveChanges();
            }
        }
        // GET: Base
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["lang"] != null)
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Session["lang"].ToString());
            }
            else
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

            }
        }
      


    }
}