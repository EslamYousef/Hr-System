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
    [Authorize(Roles = "Admin,TM,TMSetup,Exit Permission Types")]
    public class ExitpermissiontypeController : BaseController
    {
        // GET: Exitpermissiontype
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        public ActionResult Index()
        {
            var model = dbcontext.Exit_permission_type.ToList();
            return View(model);
        }
        public ActionResult Create()
        {
            try
            {
                ViewBag.shift = dbcontext.Shift_setup.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.salary_code = dbcontext.salary_code.ToList().Select(m => new { Code = m.SalaryCodeID + "------[" + m.SalaryCodeDesc + ']', ID = m.ID });

                var record = new Exit_permission_type();
                var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel).Structure_Code;
                var model = dbcontext.Exit_permission_type.ToList();
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
        public ActionResult Create(Exit_permission_type model, FormCollection record)
        {
            try
            {
                ViewBag.salary_code = dbcontext.salary_code.ToList().Select(m => new { Code = m.SalaryCodeID + "------[" + m.SalaryCodeDesc + ']', ID = m.ID });
                ViewBag.shift = dbcontext.Shift_setup.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                //if (ModelState.IsValid)
                //{
                var s = record["from"].Split(',');
                var e = record["to"].Split(',');
                model.from = Convert.ToDateTime(s[0]).TimeOfDay;
                model.to = Convert.ToDateTime(e[0]).TimeOfDay;

                if (model.SalaryCode == 0 )
                {
                    ModelState.AddModelError("", HR.Resource.Personnel.SalaryCodemustenter);
                    return View(model);
                }

                dbcontext.Exit_permission_type.Add(model);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
                //}
                //return View(model);
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
                ViewBag.salary_code = dbcontext.salary_code.ToList().Select(m => new { Code = m.SalaryCodeID + "------[" + m.SalaryCodeDesc + ']', ID = m.ID });
                ViewBag.shift = dbcontext.Shift_setup.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                var model = dbcontext.Exit_permission_type.FirstOrDefault(m => m.ID == id);
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult Edit(Exit_permission_type model, FormCollection form)
        {
            try
            {
                ViewBag.salary_code = dbcontext.salary_code.ToList().Select(m => new { Code = m.SalaryCodeID + "------[" + m.SalaryCodeDesc + ']', ID = m.ID });
                ViewBag.shift = dbcontext.Shift_setup.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                var record = dbcontext.Exit_permission_type.FirstOrDefault(m => m.ID == model.ID);
                if (model.SalaryCode == 0)
                {
                    ModelState.AddModelError("", HR.Resource.Personnel.SalaryCodemustenter);
                    return View(model);
                }
                var s = form["from"].Split(',');
                var e = form["to"].Split(',');
                record.from = Convert.ToDateTime(s[0]).TimeOfDay;
                record.to = Convert.ToDateTime(e[0]).TimeOfDay;
                record.Name = model.Name;
                record.Description = model.Description;
                record.ShiftdaystatusID = model.ShiftdaystatusID;
                record.integrate_with_leaves = model.integrate_with_leaves;
                record.integrate_with_payroll = model.integrate_with_payroll;
                record.SalaryCode = model.SalaryCode;
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (Exception)
            {
                return View(model);
            }
        }
        public ActionResult Deltet_method(int id)
        {
            try
            {
                var record = dbcontext.Exit_permission_type.FirstOrDefault(m => m.ID == id);
                return View(record);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        [ActionName("Deltet_method")]
        public ActionResult delete(int id)
        {
            var record = dbcontext.Exit_permission_type.FirstOrDefault(m => m.ID == id);

            try
            {
                dbcontext.Exit_permission_type.Remove(record);
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