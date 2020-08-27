using HR.Models;
using HR.Models.Infra;
using HR.Models.Time_management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers
{
    [Authorize(Roles = "Admin,TM,TMSetup,Exit Permission Reasons")]
    public class ExitPermissionReasonController : BaseController
    {
        // GET: ExitPermissionReason
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        public ActionResult Index()
        {
            var model = dbcontext.Exit_Permission_Reason.ToList();
            return View(model);
        }
        public ActionResult Create()
        {
            try
            {
                var record = new Exit_Permission_Reason();
                var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel).Structure_Code;
                var model = dbcontext.Exit_Permission_Reason.ToList();
                if (model.Count() == 0)
                {
                    record.Code = stru + "1";
                }
                else
                {
                    record.Code = stru + (model.LastOrDefault().ID + 1).ToString();
                }
                return View(record);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult Create(Exit_Permission_Reason model)
        {
            try
            {
                dbcontext.Exit_Permission_Reason.Add(model);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (Exception)
            {
                return View(model);
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                var model = dbcontext.Exit_Permission_Reason.FirstOrDefault(m => m.ID == id);
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult Edit(Exit_Permission_Reason model)
        {
            try
            {
                var record = dbcontext.Exit_Permission_Reason.FirstOrDefault(m => m.ID == model.ID);
                record.Name = model.Name;
                record.Description = model.Description;
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (Exception)
            {
                return View(model);
            }
        }
        public ActionResult delete(int id)
        {
            try
            {
                var record = dbcontext.Exit_Permission_Reason.FirstOrDefault(m => m.ID == id);
                return View(record);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        [ActionName("delete")]
        public ActionResult delete_method(int id)
        {
            var record = dbcontext.Exit_Permission_Reason.FirstOrDefault(m => m.ID == id);

            try
            {
                dbcontext.Exit_Permission_Reason.Remove(record);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (Exception)
            {
                return View(record);
            }
        }
    }
}