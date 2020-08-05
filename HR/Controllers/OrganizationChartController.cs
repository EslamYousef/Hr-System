using HR.Models;
using HR.Models.Infra;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers
{
    [Authorize]
    public class OrganizationChartController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: OrganizationChart
        public ActionResult Index()
        {
            var model = dbcontext.Organization_Chart.Where(m=>m.parent=="0").ToList();
            return View(model);
        }
        public ActionResult Create_node()
        {
            try
            {
                ViewBag.unit_type = dbcontext.Organization_Unit_Type.ToList().Select(m => new { Code = m.Code + "--[" + m.Name + ']', ID = m.ID });
                ViewBag.location = dbcontext.work_location.ToList().Select(m => new { Code = m.Code + "--[" + m.Name + ']', ID = m.ID });
                ViewBag.empl=dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "->" + m.Name , ID = m.ID });
                ViewBag.cost = dbcontext.CostCenter.ToList().Select(m => new { Code = m.CostCenterCode + "->" + m.CostCenterDesc, ID = m.ID });
                ViewBag.shift = dbcontext.Shift_setup.ToList().Select(m => new { Code = m.Code + "->" + m.Name, ID = m.ID });
                var parent = dbcontext.Organization_Chart.ToList();
                ViewBag.parenttt = parent.Select(m => new { Code = m.Code + "--[" + m.unit_Description + ']', ID = m.ID });
                var model = new Organization_Chart();
                if (parent.Count()==0)
                {
                    model.master_node = "Y";
                }
                else
                {
                    model.master_node = "N";

                }
               
                model.unit_status = check_status.created;
                model.number_of_direct_positions = 0;
                
                //////
                
                var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Organization).Structure_Code;
                var modell = dbcontext.Organization_Chart.ToList();
                if (modell.Count() == 0)
                {
                    model.Code = stru + "1";
                }
                else
                {
                    model.Code = stru + (modell.LastOrDefault().ID + 1).ToString();
                }
                /////
                return View(model);
            }
            catch(Exception e)
            {
                return RedirectToAction("index");
            }
        }

        [HttpPost]
        public ActionResult Create_node(Organization_Chart model)
        {
            try
            {
                ViewBag.unit_type = dbcontext.Organization_Unit_Type.ToList().Select(m => new { Code =m.Code + "--[" + m.Name + ']', ID = m.ID });
                ViewBag.location = dbcontext.work_location.ToList().Select(m => new { Code = m.Code + "--[" + m.Name + ']', ID = m.ID });
                ViewBag.empl = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "->" + m.Name, ID = m.ID });
                ViewBag.cost = dbcontext.CostCenter.ToList().Select(m => new { Code = m.CostCenterCode + "->" + m.CostCenterDesc, ID = m.ID });
                ViewBag.shift = dbcontext.Shift_setup.ToList().Select(m => new { Code = m.Code + "->" + m.Name, ID = m.ID });

                var parent = dbcontext.Organization_Chart.ToList();
                ViewBag.parenttt = parent.ToList().Select(m => new { Code = m.Code + "--[" + m.unit_Description + ']', ID = m.ID });
                model.Childs = new List<Organization_Chart>();
                if(model.master_node=="N")
                {
                    var IDd = int.Parse(model.parent);
                    var parentt = dbcontext.Organization_Chart.FirstOrDefault(m => m.ID == IDd);
                    model.parent=parentt.ID.ToString();
                  
                }
                else
                {
                    model.parent = "0";
                }
                model.unit_type_code = dbcontext.Organization_Unit_Type.FirstOrDefault(m => m.ID == model.unit_type_codeID);
                if (model.worklocationid != null&&model.worklocationid!="0")
                {
                    var oi = int.Parse(model.worklocationid);
                    model.work_location = dbcontext.work_location.FirstOrDefault(m => m.ID == oi);
                }
                
                if (model.Employee_ProfileID == 0) model.Employee_ProfileID = null;
                var new_node = dbcontext.Organization_Chart.Add(model);
                dbcontext.SaveChanges();
                if(new_node.parent!="0")
                {
                    var ID = int.Parse(new_node.parent);
                    var par = dbcontext.Organization_Chart.FirstOrDefault(m => m.ID == ID);
                    par.Childs.Add(new_node);
                    dbcontext.SaveChanges();
                }
              return  RedirectToAction("index");
            }
            catch (DbUpdateException)
            {
                TempData["Message"] =HR.Resource.organ.thiscodeIsalreadyexists;
                return View(model);
            }
            catch (Exception e)
            {
                return View();
            }
        }

        public ActionResult edit_node(string id)
        {
            try
            {
                var ID = int.Parse(id);
                var model = dbcontext.Organization_Chart.FirstOrDefault(m => m.ID == ID);
                ViewBag.location = dbcontext.work_location.ToList().Select(m => new { Code = m.Code + "--[" + m.Name + ']', ID = m.ID });
                ViewBag.empl = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "->" + m.Name, ID = m.ID });
                ViewBag.unit_type = dbcontext.Organization_Unit_Type.ToList().Select(m => new { Code = m.Code + "--[" + m.Name + ']', ID = m.ID });
                var parent = dbcontext.Organization_Chart.Where(m=>m.ID!=model.ID&&m.parent!=id).ToList();
                ViewBag.parenttt = parent.ToList().Select(m => new { Code = m.Code + "--[" + m.unit_Description + ']', ID = m.ID });
                ViewBag.cost = dbcontext.CostCenter.ToList().Select(m => new { Code = m.CostCenterCode + "->" + m.CostCenterDesc, ID = m.ID });
                ViewBag.shift = dbcontext.Shift_setup.ToList().Select(m => new { Code = m.Code + "->" + m.Name, ID = m.ID });

                return View(model);
            }
            catch(Exception e)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult edit_node(Organization_Chart record)
        {
            try
            {
                var model = dbcontext.Organization_Chart.FirstOrDefault(m => m.ID == record.ID);
                ViewBag.location = dbcontext.work_location.ToList().Select(m => new { Code = m.Code + "--[" + m.Name + ']', ID = m.ID });
                ViewBag.empl = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "->" + m.Name, ID = m.ID });

                ViewBag.unit_type = dbcontext.Organization_Unit_Type.ToList().Select(m => new { Code = m.Code + "--[" + m.Name + ']', ID = m.ID });
                ViewBag.cost = dbcontext.CostCenter.ToList().Select(m => new { Code = m.CostCenterCode + "->" + m.CostCenterDesc, ID = m.ID });
                ViewBag.shift = dbcontext.Shift_setup.ToList().Select(m => new { Code = m.Code + "->" + m.Name, ID = m.ID });

                var parentt = dbcontext.Organization_Chart.Where(m => m.ID != model.ID && m.parent != model.ID.ToString()).ToList();
                ViewBag.parenttt = parentt.ToList().Select(m => new { Code = m.Code + "--[" + m.unit_Description + ']', ID = m.ID });

                if (model.master_node == "N")
                {
                    var IDd = int.Parse(record.parent);
                    var pparent = dbcontext.Organization_Chart.FirstOrDefault(m => m.ID == IDd);
                    model.parent=pparent.ID.ToString();
                    pparent.Childs.Add(model);
                    //foreach(var item in model.Childs)
                    //{
                    //    pparent.Childs.Add(item);
                    //    dbcontext.SaveChanges();
                    //}
                   
                }
                else
                {
                    model.parent = "0";
                }
                model.unit_type_codeID = model.unit_type_codeID;
                model.unit_type_code = dbcontext.Organization_Unit_Type.FirstOrDefault(m => m.ID == record.unit_type_codeID);
                if (record.worklocationid != null && record.worklocationid != "0")
                {
                    var oi = int.Parse(record.worklocationid);
                    model.work_location = dbcontext.work_location.FirstOrDefault(m => m.ID == oi);
                }
                model.alter_unit_Description = record.alter_unit_Description;
               // model.Code = record.Code;
                model.master_node = record.master_node;
                model.worklocationid = record.worklocationid;
                model.number_of_direct_positions = record.number_of_direct_positions;
                model.refrence_number = record.refrence_number;
                model.unit_Description = record.unit_Description;
                model.unit_mail = record.unit_mail;
                model.unit_status = record.unit_status;
                model.User_unit_code = record.User_unit_code;
                model.cost_center_id = record.cost_center_id;
                model.shift_code_id = record.shift_code_id;
                if (model.Employee_ProfileID == 0) model.Employee_ProfileID = null;
                else model.Employee_ProfileID = record.Employee_ProfileID;
                dbcontext.SaveChanges();
                return RedirectToAction("Details",new { id = model.ID.ToString() });
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = HR.Resource.organ.thiscodeIsalreadyexists;
                return View(record);
            }
            catch (Exception e)
            {
                return View(record);
            }
        }

        public ActionResult Delete(string id)
        {
            try
            {
                var ID = int.Parse(id);
                var model = dbcontext.Organization_Chart.FirstOrDefault(m => m.ID == ID);
                return View(model);
            }
            catch(Exception e)
            {
                return View();
            }

        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult method_delete(string id)
        {
            var ID = int.Parse(id);
            var model = dbcontext.Organization_Chart.FirstOrDefault(m => m.ID == ID);
            try
            {
                ViewBag.unit_type = dbcontext.Organization_Unit_Type.ToList().Select(m => new { Code = m.Code + "--[" + m.Name + ']', ID = m.ID });
                var parentt = dbcontext.Organization_Chart.Where(m => m.ID != model.ID && m.parent != model.ID.ToString()).ToList();
                ViewBag.location = dbcontext.work_location.ToList().Select(m => new { Code = m.Code + "--[" + m.Name + ']', ID = m.ID });
                ViewBag.parenttt = parentt.ToList().Select(m => new { Code = m.Code + "--[" + m.unit_Description + ']', ID = m.ID });
                var child_noeds = dbcontext.Organization_Chart.Where(m => m.parent == id).ToList();
                if(child_noeds.Count()>0)
                {
                    TempData["Message"] = HR.Resource.organ.youcannotdeletethisRow;

                    ViewBag.location = dbcontext.work_location.ToList().Select(m => new { Code = m.Code + "--[" + m.Name + ']', ID = m.ID });
                    ViewBag.empl = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "->" + m.Name, ID = m.ID });

                    ViewBag.unit_type = dbcontext.Organization_Unit_Type.ToList().Select(m => new { Code = m.Code + "--[" + m.Name + ']', ID = m.ID });
                    var parenttt = dbcontext.Organization_Chart.Where(m => m.ID != model.ID && m.parent != model.ID.ToString()).ToList();
                    ViewBag.parenttt = parenttt.ToList().Select(m => new { Code = m.Code + "--[" + m.unit_Description + ']', ID = m.ID });
                    var modell = dbcontext.Organization_Chart.FirstOrDefault(m => m.ID == ID);
                   return View("Details", modell);
                }
                else
                {
                    dbcontext.Organization_Chart.Remove(model);
                    dbcontext.SaveChanges();
                }
                return RedirectToAction("index");
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = HR.Resource.organ.youcannotdeletethisRow;
                var modell = dbcontext.Organization_Chart.FirstOrDefault(m => m.ID == ID);
                ViewBag.unit_type = dbcontext.Organization_Unit_Type.ToList().Select(m => new { Code = m.Code + "--[" + m.Name + ']', ID = m.ID });
                var parentt = dbcontext.Organization_Chart.Where(m => m.ID != model.ID && m.parent != model.ID.ToString()).ToList();
                ViewBag.parenttt = parentt.ToList().Select(m => new { Code = m.Code + "--[" + m.unit_Description + ']', ID = m.ID });
                return View("Details",modell);
            }
            catch (Exception e)
            {
                return View(model);
            }
        }

      public ActionResult Details(string id)
        {
            var ID = int.Parse(id);
            var model = dbcontext.Organization_Chart.FirstOrDefault(m => m.ID == ID);
            ViewBag.location = dbcontext.work_location.ToList().Select(m => new { Code = m.Code + "--[" + m.Name + ']', ID = m.ID });
            ViewBag.empl = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "->" + m.Name, ID = m.ID });
            ViewBag.cost = dbcontext.CostCenter.ToList().Select(m => new { Code = m.CostCenterCode + "->" + m.CostCenterDesc, ID = m.ID });
            ViewBag.shift = dbcontext.Shift_setup.ToList().Select(m => new { Code = m.Code + "->" + m.Name, ID = m.ID });

            ViewBag.unit_type = dbcontext.Organization_Unit_Type.ToList().Select(m => new { Code = m.Code + "--[" + m.Name + ']', ID = m.ID });
            var parentt = dbcontext.Organization_Chart.Where(m => m.ID != model.ID && m.parent != model.ID.ToString()).ToList();
            ViewBag.parenttt = parentt.ToList().Select(m => new { Code = m.Code + "--[" + m.unit_Description + ']', ID = m.ID });

            return View(model);
        }

        public ActionResult chart_node(string id)
        {
            var ID = int.Parse(id);
            var model = dbcontext.Organization_Chart.FirstOrDefault(m => m.ID == ID);
            var list = new List<Organization_Chart>();
            list.Add(model);
            ViewBag.io = id;
            return View(list);
        }
        public JsonResult location(string id)
        {
            var ID = int.Parse(id);
            var model = dbcontext.work_location.FirstOrDefault(m => m.ID == ID);
            return Json(model);

        }
        [HttpPost]
        public JsonResult ge(int id)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            var li = new List<node>();
            if (id != 0)
                {
                
                var first_level = dbcontext.Organization_Chart.Where(m=>m.ID==id || m.parent==id.ToString()).ToList();
                var list = new List<Organization_Chart>();
                list.AddRange(first_level);
                var len = 0;
                while (true)
                {
                    len = first_level.Count();
                   
                    for(var iff=0;iff<first_level.Count;iff++)
                    {
                        var t = first_level[iff].ID.ToString();
                        list.AddRange(dbcontext.Organization_Chart.Where(m => m.parent ==t).ToList());
                        list = list.Distinct().ToList();
                    }
                    first_level.Clear();
                    first_level.AddRange(list);
                    if(first_level.Count() == len)
                    {
                        break;
                    }
                    
                }

                    first_level.FirstOrDefault(m => m.ID == id).parent = "0";
            
                foreach(var item in first_level)
                {
                    var unit = dbcontext.Organization_Unit_Type.FirstOrDefault(m => m.ID == item.unit_type_codeID);
                    var tt = dbcontext.Organization_Unit_Schema.FirstOrDefault(m => m.ID==unit.Organization_Unit_SchemaID);
                        var col= tt.color;
                    if (col != null)
                    {
                        item.unit_mail = col;
                    }
                    else
                        item.unit_mail = "0000";
                    li.Add(new node { ID = item.ID, parent = item.parent, unit_Description = item.unit_Description, color = item.unit_mail });
                }
                return Json(li);
            }

            var i = dbcontext.Organization_Chart.ToList();
            foreach (var item in i)
            {
                var unit = dbcontext.Organization_Unit_Type.FirstOrDefault(m => m.ID == item.unit_type_codeID);
                var tt = dbcontext.Organization_Unit_Schema.FirstOrDefault(m => m.ID == unit.Organization_Unit_SchemaID);
                var col = tt.color;
                if (col != null)
                {
                    item.unit_mail = col;
                }
                else
                    item.unit_mail = "0000";
                li.Add(new node { ID = item.ID, parent = item.parent, unit_Description = item.unit_Description, color = item.unit_mail });
            }
            return Json(li);
        }
        public ActionResult Index1()
        {
                return View();
            
        }
        public ActionResult Index2(int id)
        {
            ViewBag.io1 = id;
            return View();

        }
        public class node
        {
            public int ID { get; set; }
            public string parent { get; set; }
            public string unit_Description { get; set; }
            public string color { get; set; }
        }
    }
}