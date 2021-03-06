﻿using HR.Models;
using HR.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers
{
    
    public class OrganizationInquiryController : BaseController
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
                         empID = item.Slots.Where(m => m.EmployeeID != null && m.EmployeeID != "0").ToList().Select(m => m.EmployeeID).ToList();
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
        [Authorize(Roles = "Admin,Organization,OrganizationInquiry,organization reloated to jobs")]
        public ActionResult organization_view_related_to_jobs() 
        {
            try
            {
                var list = dbcontext.Organization_Chart.ToList();
                var i =0;
                var j = 0;
                if (list.Count() > 0) { i = list.LastOrDefault().ID+1; }
                foreach(var item1 in list)
                {
                  
                    var job_node = new List<Organization_Chart>();
                    var job = new Organization_Chart();
                    
                    var jobs = dbcontext.job_title_cards.Where(m => m.Organization_unit_codeID == item1.ID.ToString());
                    
                    foreach(var item2 in jobs.ToList())
                    {
                        var list_slot_details = new List<Organization_Chart>();
                        var slots = dbcontext.Slots.Where(m => m.job_title_cards.ID == item2.ID).ToList();
                        if (slots != null)
                        {
                            foreach (var item3 in slots)
                            {
                                j++;
                                if (item3.EmployeeID == null||item3.EmployeeID=="0")
                                {
                                    list_slot_details.Add(new Organization_Chart { Childs = new List<Organization_Chart>(), ID = (j + 1000), unit_Description = item3.slot_description + " , " + item3.check_status+" , "+item3.slot_type + " , " + "[ Free Slot ]" });

                                }
                                else
                                {
                                    list_slot_details.Add(new Organization_Chart { Childs = new List<Organization_Chart>(), ID = (j + 1000), unit_Description = item3.slot_description + " , " + item3.check_status  + " , " + item3.slot_type+" ,  [ "+ item3.EmployeeID + "->" + item3.EmployeeName +" ] "});

                                }
                            }
                        }   
                        job_node.Add(new Organization_Chart { ID = i, Childs= list_slot_details, unit_Description =item2.name + " ,Hired [" + item2.number_hired + " ]   ,vacant[ " + item2.number_vacant + " ]"});
                        i++;
                    }
                    if (jobs.Count() > 0)
                    {
                        job.ID = i++;
                        job.unit_Description = HR.Resource.organ.jobs;
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
                        job.unit_Description = HR.Resource.organ.jobs;
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
        [Authorize(Roles = "Admin,Organization,OrganizationInquiry,view jobs")]
        public ActionResult organization_view_related_to_jobs2()
        {
            var master_node = new List<Organization_Chart>();
            try
            {
                var job_node = new List<Organization_Chart>();
                var list = dbcontext.Organization_Chart.ToList();
                var i = 0;
                var j = 0;
                if (list.Count() > 0)
                { i = list.LastOrDefault().ID + 1; }
                var jobs = dbcontext.job_title_cards.ToList();

                foreach (var item in jobs)
                {
                    var list_slot_details = new List<Organization_Chart>();
                    if (item.Slots != null)
                    {
                        foreach (var item3 in item.Slots)
                        {
                            j++;
                            if (item3.EmployeeID == null || item3.EmployeeID == "0")
                            {
                                list_slot_details.Add(new Organization_Chart { Childs = new List<Organization_Chart>(), ID = (j + 1000), unit_Description = item3.slot_description + " , " + item3.check_status + " , " + item3.slot_type + " , " + "[ Free Slot ]" });

                            }
                            else
                            {
                                list_slot_details.Add(new Organization_Chart { Childs = new List<Organization_Chart>(), ID = (j + 1000), unit_Description = item3.slot_description + " , " + item3.check_status + " , " + item3.slot_type + " ,  [ " + item3.EmployeeID + "->" + item3.EmployeeName + " ] " });

                            }
                        }
                      
                    }
                    job_node.Add(new Organization_Chart { ID = i, parent = "1", Childs = list_slot_details, unit_Description = item.name + " ,Hired [" + item.number_hired + " ]   ,vacant[ " + item.number_vacant + " ]" });
                    i++;
                }
               
                master_node.Add(new Organization_Chart{unit_Description="ARADO",Childs=job_node,ID=0});
                return View("organization_view_related_to_jobs",master_node);
            }
            catch (Exception e)
            {
                return View("organization_view_related_to_jobs", master_node);
            }


          
           
        }

    }
}