using HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers
{
    public class man_power_reportController : BaseController
    {
        // GET: man_power_report
        ApplicationDbContext dbcontext = new ApplicationDbContext();
     
        [Authorize(Roles = "Admin,personnel,personnelReport")]
        public ActionResult man_power_report_co()
        {
            try
            {
                var all_man_power = dbcontext.man_power.ToList();
                ViewBag.years = all_man_power.Select(m =>new {code= m.from_year,id=m.from_year });
                ViewBag.organ = dbcontext.Organization_Chart.ToList().Select(m => new { Code = m.Code + "-[" + m.unit_Description + ']', ID = m.ID }); ;
                ViewBag.setup = dbcontext.job_level_setup.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID }); ;
                ViewBag.sub = dbcontext.Job_title_sub_class.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID }); ;
                ViewBag.job_t = dbcontext.job_title_cards.ToList().Select(m => new { Code = m.Code + "-[" + m.name + ']', ID = m.ID }); ;

                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult man_power_report_co(FormCollection form)
        {
            try
            {
                var all_man_power = dbcontext.man_power.ToList();
                ViewBag.years = all_man_power.Select(m => new { code = m.from_year, id = m.from_year });
                ViewBag.organ = dbcontext.Organization_Chart.ToList().Select(m => new { Code = m.Code + "-[" + m.unit_Description + ']', ID = m.ID }); ;
                ViewBag.setup = dbcontext.job_level_setup.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID }); ;
                ViewBag.sub = dbcontext.Job_title_sub_class.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID }); ;
                ViewBag.job_t = dbcontext.job_title_cards.ToList().Select(m => new { Code = m.Code + "-[" + m.name + ']', ID = m.ID }); ;

                var my_report = new List<man_power>();
                
                //=======================================
                var E = form["year_1"].Split(char.Parse(","));
                var I = form["organization_"].Split(char.Parse(","));
                var C = form["job_"].Split(char.Parse(","));
                var E_T = form["joblevel_"].Split(char.Parse(","));
                var S = form["jobtitle_"].Split(char.Parse(","));
                var List = form["List_Display"].Split(char.Parse(","));
                var flag1 = new Boolean[7];
                var list_show = new List<show_re>();
                for (var i = 0; i < 7; i++)
                {
                    flag1[i] = false;
                }
                foreach (var item in List)
                {
                    if (item != "")
                    {
                        var index = int.Parse(item);
                        flag1[index] = true;
                    }
                }
                foreach (var item in E)
                {
                    if (item != "")
                    {
                        var em_id = int.Parse(item);
                        var man1 = all_man_power.Where(m=>m.from_year==em_id).ToList();
                        
                        foreach (var item1 in man1)
                        {
                            foreach(var i in item1.items_man_power)
                            {
                                var SD = new show_re { item = i, year = item1.from_year, organization = item1.Organization_Chart.unit_Description };
                                
                                if (check(list_show, SD))
                                {
                                    list_show.Add(SD);
                                }
                            }
                           
                            
                        }
                    }
                }

                foreach (var item in I)
                {
                    if (item != "")
                    {
                        var em_id = int.Parse(item);
                        var man2 = all_man_power.Where(m => m.Organization_ChartID == em_id).ToList();

                        foreach (var item2 in man2)
                        {


                            foreach (var i in item2.items_man_power)
                            {
                                var SD = new show_re { item = i, year = item2.from_year, organization = item2.Organization_Chart.unit_Description };


                                if (check(list_show, SD))
                                {
                                    list_show.Add(SD);
                                }


                            }
                        }
                    }
                }
                
                
                    foreach (var item2 in C)
                    {
                        if (item2 != "")
                        {
                            var em_id = int.Parse(item2);
                            var man3 = dbcontext.items_man_power.Where(m => m.job_title_cardsID == em_id).ToList();
                            foreach (var item3 in man3)
                            {
                                        var SD = new show_re { item = item3, year = item3.man_power.from_year, organization = item3.man_power.Organization_Chart.unit_Description };

                                if (check(list_show, SD))
                                {
                                    list_show.Add(SD);
                                }
                            }
                        }
                    }
                foreach (var item4 in E_T)
                {
                    if (item4 != "")
                    {
                        var em_id = int.Parse(item4);
                        var man3 = dbcontext.items_man_power.Where(m => m.job_level_setupID == em_id).ToList();
                        foreach (var item3 in man3)
                        {
                            var SD = new show_re { item = item3, year = item3.man_power.from_year, organization = item3.man_power.Organization_Chart.unit_Description };

                                if (check(list_show, SD))
                                {
                                    list_show.Add(SD);
                                }
                            }
                    }
                }
                foreach (var item5 in S)
                {
                    if (item5 != "")
                    {
                        var em_id = int.Parse(item5);
                        var man3 = dbcontext.items_man_power.Where(m => m.Job_title_sub_classID == em_id).ToList();
                        foreach (var item3 in man3)
                        {
                            var SD = new show_re { item = item3, year = item3.man_power.from_year, organization = item3.man_power.Organization_Chart.unit_Description };

                                if (check(list_show, SD))
                                {
                                    list_show.Add(SD);
                                }
                            }
                    }
                }
               
                var my_repo = new REPORT_2 { flag = flag1, man_power_items = list_show };
                return View("show_report", my_repo);

            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        public ActionResult show_report(REPORT_2  Report_)
        {
            if (Report_ != null)
            {
                return View(Report_);
            }
            else
            {
                return RedirectToAction("index", "Home");
            }
        }
        public bool check(List<show_re> items, show_re adding_item)
        {
            foreach(var i in items)
            {
                    if (i.item.id == adding_item.item.id)
                    {
                        return false;
                    }   
            }
            return true;
        }

    }
    public class REPORT_2
    {
        public List<show_re> man_power_items { get; set; }
       
        public Boolean[] flag { get; set; }
    }
    public class show_re
    {
        public items_man_power item { get; set; }
        public int year { get; set; }
        public string organization { get; set; }
    }
}