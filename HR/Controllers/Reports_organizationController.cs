using HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers
{
    public class Reports_organizationController : Controller
    {
        // GET: Reports_organization
        ApplicationDbContext dbcontext = new ApplicationDbContext(); 
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult job_title()
        {
            try
            {
                var Alljobs = dbcontext.job_title_cards.ToList();
                ViewBag.jobs = Alljobs.Select(m => new { Code = m.Code + "------[" + m.name + ']', ID = m.ID });
                ViewBag.codes = Alljobs.Select(m => new { m.Code });
                ViewBag.names = Alljobs.Select(m => new { m.name });
                ViewBag.nationality = dbcontext.Nationality.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.parent = Alljobs.Select(m => new { Code = m.Code + "------[" + m.name + ']', ID = m.ID });
                ViewBag.sub_class = dbcontext.Job_title_sub_class.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.chart = dbcontext.Organization_Chart.ToList().Select(m => new { Code = m.Code + "------[" + m.unit_Description + ']', ID = m.ID });
                ViewBag.job_setup =dbcontext.Nationality.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                return View();
            }
            catch(Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult job_title(FormCollection model)
        {
            try
            {
                var nationality = model["nationality"].Split(char.Parse(","));
                var job_setup = model["job_setup"].Split(char.Parse(","));
                var name = model["name"].Split(char.Parse(","));
                var working_system = model["working_system"].Split(char.Parse(","));
                var chart = model["chart"].Split(char.Parse(","));
                var parent = model["parent"].Split(char.Parse(","));
                var sub_class = model["sub_class"].Split(char.Parse(","));
                var parmanet = model["parmanet"].Split(char.Parse(","));
                var validity = model["validity"].Split(char.Parse(","));
                var List_Display = model["List_Display"].Split(char.Parse(","));


                return View();
            }
            catch(Exception)
            {
                return View();
            }
        }
        
    }
}