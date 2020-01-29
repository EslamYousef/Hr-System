using HR.Models;
using HR.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers
{
    public class OrganizationInquiryController : Controller
    {
        // GET: OrganizationInquiry
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        public ActionResult View_jobs()
        {
            try
            {
                var model = dbcontext.job_title_cards.ToList();
                List<string> empID;
                List<Job_viewVM> Vm = new List<Job_viewVM>();
                foreach (var item in model)
                {
                    if(item.number_hired>0)
                    {
                         empID = item.Slots.Where(m => m.EmployeeID != null || m.EmployeeID != "0").ToList().Select(m => m.EmployeeID).ToList();
                    }
                    else
                    {
                         empID = new List<string>();
                    }
                    Vm.Add(new Job_viewVM { ID=empID,jobs=item});
                }
                return View(Vm);
            }
            catch(Exception e)
            {
                return View();
            }
        }
        public ActionResult organization_view_related_to_jobs() 
        {
            try
            {
                var list = dbcontext.Organization_Chart.ToList();
                var i =0;
                if (list.Count() > 0) { i = list.LastOrDefault().ID+1; }
                foreach(var item1 in list)
                {
                  
                    var job_node = new List<Organization_Chart>();
                    var job = new Organization_Chart();
                    
                    var jobs = dbcontext.job_title_cards.Where(m => m.Organization_unit_codeID == item1.ID.ToString());
                    
                    foreach(var item2 in jobs)
                    {
                        job_node.Add(new Organization_Chart { ID = i, Childs=new List<Organization_Chart>(), unit_Description =item2.name + " ,Hired [" + item2.number_hired + " ]   ,vacant[ " + item2.number_vacant + " ]"});
                        i++;
                    }
                    if (jobs.Count() > 0)
                    {
                        job.ID = i++;
                        job.unit_Description = "jobs";
                        job.Childs = job_node;
                        item1.Childs.Add(job);

                    }
                }
               
                List<Organization_Chart> ll = new List<Organization_Chart>();
                if (list.Count() > 0)
                { ll.Add(list.FirstOrDefault(m => m.parent == "0")); }
                return View(ll);
            }
            catch(Exception e)
            {
                return View();
            }
        }
        public ActionResult special_organization_view_related_to_jobs(string id )
        {
            try
            {
                var list = dbcontext.Organization_Chart.ToList();
                var i = 0;
                if (list.Count() > 0) { i = list.LastOrDefault().ID + 1; }
                foreach (var item1 in list)
                {

                    var job_node = new List<Organization_Chart>();
                    var job = new Organization_Chart();

                    var jobs = dbcontext.job_title_cards.Where(m => m.Organization_unit_codeID == item1.ID.ToString());

                    foreach (var item2 in jobs)
                    {
                        job_node.Add(new Organization_Chart { ID = i, Childs = new List<Organization_Chart>(), unit_Description = item2.name + " ,Hired [" + item2.number_hired + " ]   ,vacant[ " + item2.number_vacant + " ]" });
                        i++;
                    }
                    if (jobs.Count() > 0)
                    {
                        job.ID = i++;
                        job.unit_Description = "jobs";
                        job.Childs = job_node;
                        item1.Childs.Add(job);

                    }
                }
                List<Organization_Chart> ll = new List<Organization_Chart>();
                int ooo = int.Parse(id);
                ll.Add(list.FirstOrDefault(m => m.ID == ooo));
                return View("organization_view_related_to_jobs", ll);
            }
            catch (Exception e)
            {
                return View();
            }
        }

    }
}