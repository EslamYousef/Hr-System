using HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers.training.Transaction
{
    public class trainingplanController : Controller
    {
        // GET: trainingplan
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        public ActionResult Index()
        {
            var courses = dbcontext.TrainingPlan_Header.ToList();
            return View(courses);
        }
        //public ActionResult Create()
        //{

        //}
        public ActionResult delete(int id)
        {
            try
            {
                var model = dbcontext.TrainingPlan_Header.FirstOrDefault(m => m.ID == id);
                return View(model);
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
            var plan_header = dbcontext.TrainingPlan_Header.FirstOrDefault(m => m.ID == id);
            var plan_details = dbcontext.TrainingPlan_Detail.Where(m => m.TrainingPlan_HeaderID == plan_header.ID).ToList();
            try
            {
                dbcontext.TrainingPlan_Header.Remove(plan_header);
                dbcontext.TrainingPlan_Detail.RemoveRange(plan_details);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (Exception)
            {
                return View(plan_header);
            }
        }
    }
}