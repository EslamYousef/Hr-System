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
                ViewBag.jobs = new SelectList(Alljobs.Select(m => new { Code = m.Code + "------[" + m.name + ']', ID = m.ID }), "ID", "Code");
                ViewBag.codes = new SelectList(Alljobs.Select(m=>new { m.Code}));
                ViewBag.names = new SelectList(Alljobs.Select(m => new { m.name }));
                ViewBag.nationality = new SelectList(dbcontext.Nationality.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID }),"ID", "Code");
                ViewBag.parent = new SelectList(Alljobs.Select(m => new { Code = m.Code + "------[" + m.name + ']', ID = m.ID }),"ID","name");
                ViewBag.sub_class = new SelectList(dbcontext.Job_title_sub_class.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID }),"ID", "Code");
                ViewBag.chart = new SelectList(dbcontext.Organization_Chart.ToList().Select(m => new { Code = m.Code + "------[" + m.unit_Description + ']', ID = m.ID }),"ID", "Code");
                ViewBag.job_setup =new SelectList(dbcontext.job_level_setup.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID }),"ID","Code");
                return View();
            }
            catch(Exception)
            {
                return RedirectToAction("index");
            }
        }

    }
}