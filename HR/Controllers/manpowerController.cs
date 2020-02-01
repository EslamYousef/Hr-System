using HR.Models;
using HR.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers
{
    public class manpowerController : Controller
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: manpower
        public ActionResult Index()
        {
            var model = dbcontext.man_power.ToList();
            return View(model);
        }
        public ActionResult Create()
        {
            try
            {
                ViewBag.organ = dbcontext.Organization_Chart.ToList().Select(m => new { Code = m.Code + "------[" + m.unit_Description + ']', ID = m.ID }); ;
                ViewBag.cadre = new job_level_setup();
                var model = new man_power();
                model.from_year = 1900;model.to_year = 1901;
                var VM = new manpowerVM {man_power=model,selected_organ=0 };
                return View(VM);
            }
            catch(Exception e)
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Create(manpowerVM model, FormCollection Form)
        {
            try
            {
                ViewBag.organ = dbcontext.Organization_Chart.ToList();
                ViewBag.cadre = new job_level_setup();
                var check_date = dbcontext.man_power.FirstOrDefault(m => m.from_year == model.man_power.from_year);
                if(check_date!=null)
                {
                    TempData["Message"] = "this year have already man power you can add your items by enter to edit action and add it  ";
                    return RedirectToAction("index");
                }
                else
                {
                    var manpoer = new man_power();
                    manpoer.organization = dbcontext.Organization_Chart.FirstOrDefault(m => m.ID == model.selected_organ);
                    manpoer.from_year = model.man_power.from_year;
                    manpoer.to_year = model.man_power.to_year;


                    var levelcode = Form["levelcode"].Split(char.Parse(","));
                    var levelname = Form["levelname"].Split(char.Parse(","));
                    var count = Form["count"].Split(char.Parse(","));
                    
                    var q1 = Form["q1"].Split(char.Parse(","));
                    var new_job = Form["new"].Split(char.Parse(","));
                    var q2 = Form["q2"].Split(char.Parse(","));
                    var q3 = Form["q3"].Split(char.Parse(","));
                    var q4 = Form["q4"].Split(char.Parse(","));
                    var id = Form["levelID"].Split(char.Parse(","));
                    var ss = new List<items_man_power>();
                   
                    for (var i= 0; i < q1.Count(); i++)
                    {

                        if (q1[i] != "")
                        {
                            var IDlevel = int.Parse(id[i]);
                            var le = dbcontext.job_level_setup.FirstOrDefault(m => m.ID == IDlevel);

                            var item_de = new items_man_power
                            {
                                cadre_code=le,
                                current_jobs=int.Parse(count[i]),
                                new_jobs= int.Parse(new_job[i]),
                                quarter1= int.Parse(q1[i]),
                                quarter2= int.Parse(q2[i]),
                                quarter3= int.Parse(q3[i]),
                                quarter4= int.Parse(q3[i]),
                                
                            };

                            var item = dbcontext.items_man_power.Add(item_de);
                            dbcontext.SaveChanges();
                            ss.Add(item);
                        }
                    }
                    manpoer.items_man_power = ss;
                    dbcontext.man_power.Add(manpoer);
                    dbcontext.SaveChanges();
                    return RedirectToAction("index");
                }
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = "error";
                return View(model);
            }
            catch (Exception e)
            {
                return View(model);
            }
        }

        public ActionResult edit(string id)


        {
            try
            {
                ViewBag.organ = dbcontext.Organization_Chart.ToList().Select(m => new { Code = m.Code + "------[" + m.unit_Description + ']', ID = m.ID }); ;
                ViewBag.cadre = new job_level_setup();
                int ID = int.Parse(id);
                var model = dbcontext.man_power.FirstOrDefault(m => m.ID ==ID);
                var items = dbcontext.items_man_power.Where(m => m.man_power.ID == model.ID).ToList();
                model.items_man_power = items;
                var VM = new manpowerVM { man_power = model, selected_organ = model.organization.ID };
                return View(VM);
            }
            catch (Exception e)
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult edit(manpowerVM model, FormCollection Form)
        {
            try
            {
                ViewBag.organ = dbcontext.Organization_Chart.ToList();
                ViewBag.cadre = new job_level_setup();
                //var check_date = dbcontext.man_power.FirstOrDefault(m => m.from_year == model.man_power.from_year);
                //if (check_date != null)
                //{
                //    TempData["Message"] = "this year have already man power you can add your items by enter to edit action and add it  ";
                //    return RedirectToAction("index");
                //}
               
                    var manpoer = dbcontext.man_power.FirstOrDefault(m=>m.ID==model.man_power.ID);
                    manpoer.organization = dbcontext.Organization_Chart.FirstOrDefault(m => m.ID == model.selected_organ);



                var items = manpoer.items_man_power;
                //manpoer.items_man_power = null;
                //dbcontext.SaveChanges();
                dbcontext.items_man_power.RemoveRange(items);
                dbcontext.SaveChanges();
                var levelcode = Form["levelcode"].Split(char.Parse(","));
                var levelname = Form["levelname"].Split(char.Parse(","));
                var count = Form["count"].Split(char.Parse(","));

                var q1 = Form["q1"].Split(char.Parse(","));
                var new_job = Form["new"].Split(char.Parse(","));
                var q2 = Form["q2"].Split(char.Parse(","));
                var q3 = Form["q3"].Split(char.Parse(","));
                var q4 = Form["q4"].Split(char.Parse(","));
                var id = Form["levelID"].Split(char.Parse(","));
                var ss = new List<items_man_power>();

                    for (var i = 0; i < q1.Count(); i++)
                    {

                        if (q1[i] != "")
                        {
                            var IDlevel = int.Parse(id[i]);
                            var le = dbcontext.job_level_setup.FirstOrDefault(m => m.ID == IDlevel);

                            var item_de = new items_man_power
                            {
                                cadre_code = le,
                                current_jobs = int.Parse(count[i]),
                                new_jobs = int.Parse(new_job[i]),
                                quarter1 = int.Parse(q1[i]),
                                quarter2 = int.Parse(q2[i]),
                                quarter3 = int.Parse(q3[i]),
                                quarter4 = int.Parse(q3[i]),

                            };

                            var item = dbcontext.items_man_power.Add(item_de);
                            dbcontext.SaveChanges();
                            ss.Add(item);
                        }
                    }
                    manpoer.items_man_power = ss;
                   
                    dbcontext.SaveChanges();
                    return RedirectToAction("index");
                
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = "error";
                return View(model);
            }
            catch (Exception e)
            {
                return View(model);
            }
        }
        public ActionResult delete(string id)
        {
            try
            {
                var ID = int.Parse(id);
                var model = dbcontext.man_power.FirstOrDefault(m => m.ID == ID);
                return View(model);
            }
            catch (Exception e)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        [ActionName("delete")]
        public ActionResult method_delete(string id)
        {
            var ID = int.Parse(id);
            var model = dbcontext.man_power.FirstOrDefault(m => m.ID == ID);

            try
            {
                dbcontext.items_man_power.RemoveRange(model.items_man_power);
                dbcontext.SaveChanges();
                dbcontext.man_power.Remove(model);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (DbUpdateException e)
            {
                TempData["Message"] = "You can't delete beacause it have child";
                return View(model);
            }
            catch (Exception e)
            {
                return View(model);
            }
        }
    }
}