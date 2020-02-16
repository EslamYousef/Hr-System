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
                var oi = int.Parse(model.worklocationid);
                model.work_location = dbcontext.work_location.FirstOrDefault(m => m.ID == oi);
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

                ViewBag.unit_type = dbcontext.Organization_Unit_Type.ToList().Select(m => new { Code = m.Code + "--[" + m.Name + ']', ID = m.ID });
                var parent = dbcontext.Organization_Chart.Where(m=>m.ID!=model.ID&&m.parent!=id).ToList();
                ViewBag.parenttt = parent.ToList().Select(m => new { Code = m.Code + "--[" + m.unit_Description + ']', ID = m.ID });
                
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

                ViewBag.unit_type = dbcontext.Organization_Unit_Type.ToList().Select(m => new { Code = m.Code + "--[" + m.Name + ']', ID = m.ID });
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
                var oi = int.Parse(record.worklocationid);
                model.work_location = dbcontext.work_location.FirstOrDefault(m => m.ID == oi);
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
                dbcontext.SaveChanges();
                return RedirectToAction("index");
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
            return View(list);
        }
        public JsonResult location(string id)
        {
            var ID = int.Parse(id);
            var model = dbcontext.work_location.FirstOrDefault(m => m.ID == ID);
            return Json(model);

        }
        

    }
}