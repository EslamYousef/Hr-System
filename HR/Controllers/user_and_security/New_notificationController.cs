using HR.Models;
using HR.Models.user;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers.user_and_security
{
    [Authorize(Roles = "Admin,setting")]
    public class New_notificationController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: New_notification
        public ActionResult Index()
        {
            var noti = dbcontext.notifications.ToList();
            return View(noti);
        }
        public ActionResult create()
        {
            try
            {
             //   var C_ID = User.Identity.GetUserId();
                ViewBag.screen = dbcontext.Screen.Where(m => m.parent_screen == 0).Select(m=>new { ID=m.ID,code=m.name}).ToList();
                ViewBag.users = dbcontext.Users.Where(m => m.active == true).Select(m => new { ID = m.Id, code = m.UserName }).ToList();
                var model = new notifications ();
                return View(model);
            }
            catch(Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult create(notifications model,string feild)
        {
            try
            {
               // var C_ID = User.Identity.GetUserId();
                ViewBag.screen = dbcontext.Screen.Where(m => m.parent_screen == 0).Select(m => new { ID = m.ID, code = m.name }).ToList();
                ViewBag.users = dbcontext.Users.Where(m => m.active == true).Select(m => new { ID = m.Id, code = m.UserName }).ToList();

                if(model.until!=null&&model.until.Value.Year!=0001)
                {
                    if(model.until<DateTime.Now.Date)
                    {
                        TempData["Message"] = "Error in until date";
                        return View(model);
                    }
                }
                if(model.type_field==type_field.date_field|| model.type_field == type_field.normal_field)
                {
                    
                    if(feild == null|| feild=="0")
                    {
                        TempData["Message"] = "you must choose field";
                        return View(model);
                    }
                    else
                    {
                        model.Field = feild;
                    }
                  
                }
                if (model.type_field != type_field.date_field && (model.Action == HR.Models.user.Action.after_due_Date || model.Action == HR.Models.user.Action.before_due_Date))
                {
                    TempData["Message"] = "you can't choose date before or after with normal field";
                    return View(model);
                }
                if (model.type_field == 0 || model.Action == 0)
                {
                    return View(model);
                }
                dbcontext.notifications.Add(model);
                dbcontext.SaveChanges();
                return RedirectToAction("index");

            }
            catch (Exception)
            {
                return View(model);
            }
        }

        public ActionResult edit(int id)
        {
            try
            {
                var C_ID = User.Identity.GetUserId();
                ViewBag.screen = dbcontext.Screen.Where(m => m.parent_screen == 0).Select(m => new { ID = m.ID, code = m.name }).ToList();
                ViewBag.users = dbcontext.Users.Where(m => m.active == true).Select(m => new { ID = m.Id, code = m.UserName }).ToList();
                var model = dbcontext.notifications.FirstOrDefault(m => m.ID == id);
                return View(model);
            }
            catch(Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult edit(notifications model,string feild)
        {
            try
            {
                var C_ID = User.Identity.GetUserId();
                ViewBag.screen = dbcontext.Screen.Where(m => m.parent_screen == 0).Select(m => new { ID = m.ID, code = m.name }).ToList();
                ViewBag.users = dbcontext.Users.Where(m => m.active==true).Select(m => new { ID = m.Id, code = m.UserName }).ToList();
                var edit_model = dbcontext.notifications.FirstOrDefault(m => m.ID == model.ID);

                if (model.until != null && model.until.Value.Year != 0001)
                {
                    if (model.until < DateTime.Now.Date)
                    {
                        TempData["Message"] = "Error in until date";
                        return View(model);
                    }
                }
                if (model.type_field == type_field.date_field || model.type_field == type_field.normal_field)
                {
                    if (feild == null || feild == "0")
                    {
                        TempData["Message"] = "you must choose field";
                        return View(model);
                    }
                    else
                    {
                        edit_model.Field = feild;
                    }

                }
                if (model.type_field != type_field.date_field && (model.Action == HR.Models.user.Action.after_due_Date || model.Action == HR.Models.user.Action.before_due_Date))
                {
                    TempData["Message"] = "you can't choose date before or after with normal field";
                    return View(model);
                }
                if(model.type_field==0||model.Action==0)
                {
                    return View(model);
                }
                edit_model.Action = model.Action;
               // edit_model.Field = model.Field;
                edit_model.Form = model.Form;
                edit_model.Message = model.Message;
                edit_model.send_to_ID_user = model.send_to_ID_user;
                edit_model.Subject = model.Subject;
                edit_model.type_field = model.type_field;
                edit_model.until = model.until;
                edit_model.number = model.number;
                dbcontext.SaveChanges();

                return RedirectToAction("index");
            }
            catch (Exception)
            {
                return View(model);
            }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                var record = dbcontext.notifications.FirstOrDefault(m => m.ID == id);
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = HR.Resource.Basic.thereisnodata;
                    return View();
                }

            }
            catch (Exception e)
            {
                return View();
            }

        }
        [ActionName("Delete")]
        [HttpPost]
        public ActionResult Deletemethod(int id)
        {
            var record = dbcontext.notifications.FirstOrDefault(m => m.ID == id);

            try
            {
                dbcontext.notifications.Remove(record);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = HR.Resource.Basic.youcannotdeletethisRow;
                return View(record);
            }
            catch (Exception e)
            {
                return View();
            }
        }

        public JsonResult get_fields(string  parent)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            var parent_screen = dbcontext.Screen.FirstOrDefault(m => m.name == parent);
            var fields = dbcontext.Screen.Where(m => m.parent_screen == parent_screen.ID).ToList();
            return Json(fields);
        }

    }
}