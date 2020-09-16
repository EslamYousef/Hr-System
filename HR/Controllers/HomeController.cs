using HR.Models;
using HR.Models.NOtification;
using HR.Models.user;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace HR.Controllers
{
    [Authorize]
    [HandleError(View = "Error")]
    public class HomeController : BaseController
    {
   

        public ActionResult Index()

        {

            return View();

        }
        public ActionResult ChangeLanguage(string lang)
        {
            new Langmanger().SetLanguage(lang);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult setting()
        {
            return View();
        }

        public ActionResult GetAllBasicModule()
        {

            return View();

        }
        //public JsonResult GetNotificationContacts()
        //{
        //    ApplicationDbContext CONTEXT = new ApplicationDbContext();
        //    CONTEXT.Configuration.ProxyCreationEnabled = false;

        //    var notificationRegisterTime = Session["LastUpdated"] != null ? Convert.ToDateTime(Session["LastUpdated"]) : DateTime.Now;
        //    NotificationComponent NC = new NotificationComponent();
        //    var list = NC.GetContacts(notificationRegisterTime).Select(m => new { ID = m.ID });
        //    //update session here for get only new added contacts (notification)
        //    Session["LastUpdate"] = DateTime.Now;
        //    return new JsonResult { Data = list, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //}




        public ActionResult alert_inbox()

        {
            ApplicationDbContext CONTEXT = new ApplicationDbContext();

            try
            {
                var user_id = User.Identity.GetUserId();
                var all_alert = CONTEXT.Alert_inbox.Where(m => m.send_to_user_id == user_id).ToList();
                
                foreach (var item in all_alert)
                {
                    try
                    {
                        item.send_from_user_id = CONTEXT.Users.FirstOrDefault(m => m.Id == item.send_from_user_id).UserName;
                    }
                    catch(Exception)
                    {
                        continue;
                    }
                    }
                return View(all_alert.OrderBy(m => m.Read));
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }



        }

        
        public JsonResult MARK_RED(int id)
        {
            try
            {
                ApplicationDbContext dbcontext = new ApplicationDbContext();
                var a = dbcontext.Alert_inbox.FirstOrDefault(m => m.ID == id);
                if (a != null)
                {
                    a.Read = true;
                    dbcontext.SaveChanges();
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
            catch (Exception)
            {
                return Json(false);
            }


        }
        public JsonResult MARK_all_as_RED(string userid)
        {
            try
            {
                ApplicationDbContext dbcontext = new ApplicationDbContext();
                var a = dbcontext.Alert_inbox.Where(m => m.send_to_user_id == userid &&m.Read==false).ToList();
                if (a.Count()>0)
                {
                    foreach(var item in a)
                    {
                        item.Read = true;
                        dbcontext.SaveChanges();
                       
                    }
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
            catch (Exception)
            {
                return Json(false);
            }


        }
    }
    public class check
    {
       static public HR.Models.user.notifications check_alert(string screen_name, HR.Models.user.Action action, type_field type_field)
        {
            ApplicationDbContext dbcontext = new ApplicationDbContext();
            var C = dbcontext.notifications.FirstOrDefault(m => m.Form == screen_name && m.Action == action && m.type_field == type_field);
            if (C != null)
            {
                return C;
            }
            else
            {
                return null;
            }
        }
    }

}