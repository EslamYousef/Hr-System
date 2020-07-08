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
    public class ShiftdaystatusController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Shiftdaystatus
        public ActionResult Index()
        {
            var model = dbcontext.Shiftdaystatus.ToList();
            return View(model);
        }
        public ActionResult Create()
        {
            try
            {
                ViewBag.shift = dbcontext.Shift_setup.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.salary_code = dbcontext.salary_code.ToList().Select(m => new { Code = m.SalaryCodeID + "------[" + m.SalaryCodeDesc + ']', ID = m.ID });

                var req = dbcontext.Shiftdaystatus.ToList();
                var model = new Shiftdaystatus();
                var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel).Structure_Code;
                string number;
                if (req.Count > 0)
                {
                    number = stru + (req.LastOrDefault().ID + 1).ToString();
                }
                else
                {
                    number = stru + 1;
                }
                model.Code = number; model.Disable_Editing = false;
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult Create(Shiftdaystatus model, string command ,FormCollection form)
        {
            try
            {
                ViewBag.shift = dbcontext.Shift_setup.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.salary_code = dbcontext.salary_code.ToList().Select(m => new { Code = m.SalaryCodeID + "------[" + m.SalaryCodeDesc + ']', ID = m.ID });

                model.Color = form["color"].Split(',')[0];
                var codeid = form["codeid"].Split(',');
                var SalaryDes = form["SalaryDes"].Split(',');
                var DefaultValue = form["DefaultValue"].Split(',');
                var mo=  dbcontext.Shiftdaystatus.Add(model);
                dbcontext.SaveChanges();
                for (var i = 0; i < codeid.Length; i++)
                {
                    if (codeid[i] != "")
                    {
                        var new_details = new ShiftdaystatusDetials { ShiftdaystatusId = mo.ID.ToString(), Created_By = User.Identity.Name, Created_Date = DateTime.Now.Date, PayrollItemCode = codeid[i], DefaultValue = int.Parse(DefaultValue[i]), PayrollItemDescription = SalaryDes[i]};
                        dbcontext.ShiftdaystatusDetials.Add(new_details);
                        dbcontext.SaveChanges();
                    }
                }

                if (command == "submit2")
                {
                    return RedirectToAction("Link", new { id = mo.ID });
                }
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
                ViewBag.shift = dbcontext.Shift_setup.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.salary_code = dbcontext.salary_code.ToList().Select(m => new { Code = m.SalaryCodeID + "------[" + m.SalaryCodeDesc + ']', ID = m.ID });

                var model = dbcontext.Shiftdaystatus.FirstOrDefault(m => m.ID == id);
                var old_details = dbcontext.ShiftdaystatusDetials.Where(m => m.ShiftdaystatusId == model.ID.ToString()).ToList();
                var new_model = new ShiftVMs { ShiftdaystatusDetials = old_details, Shiftdaystatus = model };

                return View(new_model);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult Edit(ShiftVMs model, string command, FormCollection form)
        {

            try
            {
                ViewBag.shift = dbcontext.Shift_setup.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.salary_code = dbcontext.salary_code.ToList().Select(m => new { Code = m.SalaryCodeID + "------[" + m.SalaryCodeDesc + ']', ID = m.ID });

                var original_model = dbcontext.Shiftdaystatus.FirstOrDefault(m => m.ID == model.Shiftdaystatus.ID);
                original_model.Description = model.Shiftdaystatus.Description;
                original_model.Name = model.Shiftdaystatus.Name;
                original_model.Alias = model.Shiftdaystatus.Alias;
                original_model.Shift_setupID = model.Shiftdaystatus.Shift_setupID;
                original_model.Color = form["color"].Split(',')[0];
                original_model.Disable_Editing = model.Shiftdaystatus.Disable_Editing;
                dbcontext.SaveChanges();
                ///////////delete//////////
                var update_details = dbcontext.ShiftdaystatusDetials.Where(m => m.ShiftdaystatusId == original_model.ID.ToString()).ToList();
                dbcontext.ShiftdaystatusDetials.RemoveRange(update_details);
                dbcontext.SaveChanges();
                var codeid = form["codeid"].Split(',');
                var SalaryDes = form["SalaryDes"].Split(',');
                var DefaultValue = form["DefaultValue"].Split(',');
                ///////////////////add///////

                for (var i = 0; i < codeid.Length; i++)
                {
                    if (codeid[i] != "")
                    {
                        var new_details = new ShiftdaystatusDetials { ShiftdaystatusId = original_model.ID.ToString(), Created_By = User.Identity.Name, Created_Date = DateTime.Now.Date, PayrollItemCode = codeid[i], DefaultValue = int.Parse(DefaultValue[i]), PayrollItemDescription = SalaryDes[i] };
                        dbcontext.ShiftdaystatusDetials.Add(new_details);
                        dbcontext.SaveChanges();
                    }
                }

                if (command == "submit2")
                {
                    return RedirectToAction("Link", new { id = original_model.ID });
                }
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
                var model = dbcontext.Shiftdaystatus.FirstOrDefault(m => m.ID == id);
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        [ActionName("Deltet_method")]
        public ActionResult Deltet(int id)
        {
            var model = dbcontext.Shiftdaystatus.FirstOrDefault(m => m.ID == id);
            try
            {
                var link = dbcontext.Day_Status_Linkedto_Location.Where(m => m.ShiftdaystatusID == id);
                dbcontext.Day_Status_Linkedto_Location.RemoveRange(link);
                dbcontext.SaveChanges();
                dbcontext.Shiftdaystatus.Remove(model);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (Exception)
            {
                return View(model);
            }
        }
        public ActionResult Link(int id)
        {
            ViewBag.id2 = id;
            var  modell = dbcontext.Shiftdaystatus.FirstOrDefault(m => m.ID == id);
            try
            {
                var all_unit = dbcontext.work_location.ToList();             
                var  mm = new List<linkVM>();
                foreach (var team in all_unit)
                {
                    if (modell.Day_Status_Linkedto_Location.Any(ma => ma.work_locationID == team.ID))
                    {
                        mm.Add(new linkVM {work_location=team, check = true});
                    }
                    else
                    {
                        mm.Add(new linkVM { work_location = team, check = false });
                    }
                }
                return View(mm);
            }
            catch (Exception e)
            {
                return RedirectToAction("edit",new { id=modell.ID});
            }
        }
        [HttpPost]

        public ActionResult Link(FormCollection reco, int id)
        {
            try
            {
                ViewBag.id2 = id;
                var modell = dbcontext.Shiftdaystatus.FirstOrDefault(m => m.ID == id);
                var all_unit = dbcontext.work_location.ToList();
                var mm = new List<linkVM>();
                var old_link = dbcontext.Day_Status_Linkedto_Location.Where(m => m.ShiftdaystatusID == modell.ID).ToList();
                dbcontext.Day_Status_Linkedto_Location.RemoveRange(old_link);
                dbcontext.SaveChanges();
                if (reco.AllKeys.Count() > 0)
                {
                    var sel = reco["Selected"].Split(',');
                    foreach (var item in sel)
                    {
                        
                            var worklocation_id = int.Parse(item);
                            var new_link = new Day_Status_Linkedto_Location { ShiftdaystatusID = modell.ID, work_locationID = worklocation_id };
                            dbcontext.Day_Status_Linkedto_Location.Add(new_link);
                            dbcontext.SaveChanges();
                        
                    }
                }
                return RedirectToAction("edit",new { id=modell.ID});
            }
            catch (Exception e)
            {
                return View(reco);
            }
        }
    }
    public class ShiftVMs
    {
        public Shiftdaystatus Shiftdaystatus { get; set; }
        public List<ShiftdaystatusDetials> ShiftdaystatusDetials { get; set; }
    }

    public class linkVM
    {
        public bool check { get; set; }
        public work_location work_location { get; set; }
    }
}