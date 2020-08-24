using HR.Models;
using HR.Models.Infra;
using HR.Models.Training.setup;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers.training.setup
{
    [Authorize(Roles = "Admin,talent,talentSetup,Training plan_setup")]
    public class TrainingTypeController : BaseController
    {
        // GET: TrainingType
        ApplicationDbContext dbcontext = new ApplicationDbContext();

        public ActionResult Index()
        {
            var model = dbcontext.TrainingType_Header.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            //////
            var modell = new TrainingType_Header();

            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Talent_Development).Structure_Code;
            var model = dbcontext.TrainingType_Header.ToList();
            if (model.Count() == 0)
            {
                modell.TrainingType_Code = stru + "1";
            }
            else
            {
                modell.TrainingType_Code = stru + (model.LastOrDefault().ID + 1).ToString();
            }
            ViewBag.sub_group = dbcontext.Course_SubGroups.ToList().Select(m => new { Code = m.SubGroup_Code + "-[" + m.SubGroup_Desc + ']', ID = m.ID });
            ViewBag.cost = dbcontext.CostElement.ToList().Select(m => new { Code = m.CostElement_Code + "-[" + m.CostElement_Desc + ']', ID = m.ID });

            modell.Max_No_Attended_Group_Of_Cources = false;
            modell.No_Attended_Group_Of_Cources = 0;
            var add_model = new VM { TrainingType_Header = modell, Budget = Budget.No, traning_place = traning_place.Local };
            
            /////
            return View(add_model);
        }
        [HttpPost]
        public ActionResult Create(VM model,FormCollection form)
        {
            try
            {
                var max = form["check_A"].Split(',');
                if (max.Length == 1)
                {
                    model.TrainingType_Header.No_Attended_Group_Of_Cources = 0;
                    model.TrainingType_Header.Max_No_Attended_Group_Of_Cources = false;
                }
                else
                {

                    model.TrainingType_Header.Max_No_Attended_Group_Of_Cources = true;

                }
                if (ModelState.IsValid)
                {
                    ViewBag.sub_group = dbcontext.Course_SubGroups.ToList().Select(m => new { Code = m.SubGroup_Code + "-[" + m.SubGroup_Desc + ']', ID = m.ID });
                    ViewBag.cost = dbcontext.CostElement.ToList().Select(m => new { Code = m.CostElement_Code + "-[" + m.CostElement_Desc + ']', ID = m.ID });

                    model.TrainingType_Header.Budget = Convert.ToInt16(model.Budget.GetHashCode());
                    model.TrainingType_Header.TrainingPlace = Convert.ToInt16(model.traning_place.GetHashCode());
                  
                    
                    model.TrainingType_Header.Created_By = User.Identity.Name;
                    model.TrainingType_Header.Created_Date = DateTime.Now.Date;
                   var Header= dbcontext.TrainingType_Header.Add(model.TrainingType_Header);
                    dbcontext.SaveChanges();

                    //================================
                    var cost_id = form["cost_id"].Split(',');
                    for (var i = 0; i < cost_id.Length; i++)
                    {
                        if (cost_id[i] != "")
                        {
                            var ID = int.Parse(cost_id[i]);
                            var cost = dbcontext.CostElement.FirstOrDefault(m => m.ID == ID);
                            var Detail = new TrainingType_Detail { cost_des = cost.CostElement_Code + "-" + cost.CostElement_Desc, cost_ID = cost.ID, TrainingType_Code = Header.TrainingType_Code, CostElement_Code = cost.CostElement_Code, Created_By = User.Identity.Name, Created_Date = DateTime.Now.Date };
                            dbcontext.TrainingType_Detail.Add(Detail);
                            dbcontext.SaveChanges();
                        }
                    }
                    //================================

                    return RedirectToAction("Index");
                }
                else
                {
                    return View(model);
                }
            }
            catch (DbUpdateException e)
            {
                TempData["Message"] = HR.Resource.Basic.thiscodeIsalreadyexists;
                return View(model);
            }
            catch (Exception e)
            {
                return View(model);
            }
        }
        public ActionResult Edit(int id)
        {
            try
            {
                ViewBag.sub_group = dbcontext.Course_SubGroups.ToList().Select(m => new { Code = m.SubGroup_Code + "-[" + m.SubGroup_Desc + ']', ID = m.ID });
                ViewBag.cost = dbcontext.CostElement.ToList().Select(m => new { Code = m.CostElement_Code + "-[" + m.CostElement_Desc + ']', ID = m.ID });

                var record = dbcontext.TrainingType_Header.FirstOrDefault(m => m.ID == id);
                if (record != null)
                {
                    var edit_model = new VM { Budget=(Budget)record.Budget,traning_place=(traning_place)record.TrainingPlace,TrainingType_Header=record};
                    return View(edit_model);
                }
                else
                {
                    TempData["Message"] = HR.Resource.Basic.thereisnodata;
                    return View();
                }
            }
            catch
            (Exception e)
            { return View(); }
        }
        [HttpPost]
        public ActionResult Edit(VM model,FormCollection form)
        {
            try
            {
                ViewBag.sub_group = dbcontext.Course_SubGroups.ToList().Select(m => new { Code = m.SubGroup_Code + "-[" + m.SubGroup_Desc + ']', ID = m.ID });
                ViewBag.cost = dbcontext.CostElement.ToList().Select(m => new { Code = m.CostElement_Code + "-[" + m.CostElement_Desc + ']', ID = m.ID });

                var edit = dbcontext.TrainingType_Header.FirstOrDefault(m => m.ID == model.TrainingType_Header.ID);
                edit.TrainingType_Desc = model.TrainingType_Header.TrainingType_Desc;
                edit.TrainingType_AltDesc = model.TrainingType_Header.TrainingType_AltDesc;
               
                edit.SubGroup_Code = model.TrainingType_Header.SubGroup_Code;
                edit.Budget = Convert.ToInt16(model.Budget.GetHashCode());
                edit.TrainingPlace = Convert.ToInt16(model.traning_place.GetHashCode());
                var max = form["check_A"].Split(',');
                if (max.Length == 1)
                {
                    edit.No_Attended_Group_Of_Cources = 0;
                    edit.Max_No_Attended_Group_Of_Cources = false;
                    model.TrainingType_Header.No_Attended_Group_Of_Cources = 0;
                    model.TrainingType_Header.Max_No_Attended_Group_Of_Cources = false;
                }
                else
                {
                    edit.No_Attended_Group_Of_Cources = model.TrainingType_Header.No_Attended_Group_Of_Cources;
                    edit.Max_No_Attended_Group_Of_Cources = true;
                    model.TrainingType_Header.Max_No_Attended_Group_Of_Cources = true;

                }
                edit.Modified_By = User.Identity.Name;
                edit.Modified_Date = DateTime.Now.Date;
                dbcontext.SaveChanges();
                //===================================
                var detail = dbcontext.TrainingType_Detail.Where(m => m.TrainingType_Code == edit.TrainingType_Code).ToList();
                dbcontext.TrainingType_Detail.RemoveRange(detail);
                dbcontext.SaveChanges();
                //===================================
                var cost_id = form["cost_id"].Split(',');
                for (var i = 0; i < cost_id.Length; i++)
                {
                    if (cost_id[i] != "")
                    {
                        var ID = int.Parse(cost_id[i]);
                        var cost = dbcontext.CostElement.FirstOrDefault(m => m.ID == ID);
                        var Detail = new TrainingType_Detail { cost_des = cost.CostElement_Code + "-" + cost.CostElement_Desc, cost_ID = cost.ID, TrainingType_Code = edit.TrainingType_Code, CostElement_Code = cost.CostElement_Code, Created_By = User.Identity.Name, Created_Date = DateTime.Now.Date };
                        dbcontext.TrainingType_Detail.Add(Detail);
                        dbcontext.SaveChanges();
                    }
                }
                //===================================
                return RedirectToAction("index");
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = HR.Resource.Basic.thiscodeIsalreadyexists;
                return View(model);
            }
            catch (Exception e)
            { return View(model); }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                var record = dbcontext.TrainingType_Header.FirstOrDefault(m => m.ID == id);
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
            var record = dbcontext.TrainingType_Header.FirstOrDefault(m => m.ID == id);
            var details = dbcontext.TrainingType_Detail.Where(m => m.TrainingType_Code == record.TrainingType_Code).ToList();
            try
            {
                dbcontext.TrainingType_Header.Remove(record);
                dbcontext.TrainingType_Detail.RemoveRange(details);
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
                return View(record);
            }
        }
        //=======================ajax=============
        public JsonResult get_details_cost(string code)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            var details = dbcontext.TrainingType_Detail.Where(m => m.TrainingType_Code == code).ToList();
            return Json(details);

        }
    }
    public class VM
    {
        public TrainingType_Header TrainingType_Header { get; set; }
        public traning_place traning_place { get; set; }
        public Budget Budget { get; set; }
    }
    public enum traning_place
    {
        Local=0,
        Abroad=1
    }
    public enum Budget
    {
        Yes = 0,
        No = 1
    }
}