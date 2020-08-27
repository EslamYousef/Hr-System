using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HR.Models;
using System.Data.Entity.Infrastructure;
using HR.Models.Infra;
using HR.Models.ViewModel;
using Microsoft.AspNet.Identity;
using HR.Models.All_Table_Commitee_Resolution;

namespace HR.Controllers
{
    [Authorize(Roles = "Admin,Recuirtment,RecuirtmentCards,Committe Resolution Rec")]
    public class Committe_Resolution_RecuirtmentController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Committe_Resolution_Recuirtment
        public ActionResult Index()
        {
            var new_model = dbcontext.Committe_Resolution_Recuirtment.ToList();
            return View(new_model);
        }
        public ActionResult Create(string id)
        {

            ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });

            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Recuirtment);
            var model = dbcontext.Committe_Resolution_Recuirtment.ToList();
            var count = 0;
            if (model.Count() == 0)
            {
                count = 1;
            }
            else
            {
                var te = model.LastOrDefault().ID;
                count = te + 1;
            }

            var Statis = DateTime.Now;

            var CommitteResolutionRecuirtment = new Committe_Resolution_Recuirtment { Code = stru.Structure_Code + count.ToString() ,Committe_Resolution_Date=Statis,Committe_Year=DateTime.Today.Year};
            return View(CommitteResolutionRecuirtment);

        }
        [HttpPost]
        public ActionResult Create(FormCollection form, Committe_Resolution_Recuirtment model, string Command)
        {
            try
            {
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                if (ModelState.IsValid)
                {
                    var emp_profile = form["Employee_profile_No2"].Split(char.Parse(","));
                    var Emp_name = form["Employee_name"].Split(char.Parse(","));
                    var Head = form["Is_Head"].Split(char.Parse(","));
                    var items = new List<Append_Committe_Member>();
                    for (var i = 0; i < emp_profile.Count(); i++)
                    {
                        if (emp_profile[i] != "" && Emp_name[i] != "" && Head[i] != "")
                        {
                            items.Add(new Append_Committe_Member { Is_Committe_Head = Head[i], Employee_profile = emp_profile[i], Employee_Name = Emp_name[i] });
                        }
                    }
                    if (items.Count() > 0)
                    {
                        var add_items = dbcontext.Append_Committe_Member.AddRange(items);
                        dbcontext.SaveChanges();
                        /////////////////////////////////////
                        var username = User.Identity.GetUserName();
                       
                        var Date = Convert.ToDateTime("1/1/1900");
                        var s = new status { statu = check_status.created, created_by = username, Type = Models.Infra.Type.Committe_Resolution_Recuirtment , approved_bydate = Date, cancaled_bydate = Date, created_bydate = DateTime.Now.Date, Rejected_bydate = Date, return_to_reviewdate = Date };
                        var st = dbcontext.status.Add(s);
                        dbcontext.SaveChanges();
                        ///////////////////////////////
                        var benfit = new Committe_Resolution_Recuirtment {check_status= check_status.created, statID=s.ID, Append_Committe_Member = add_items.ToList(), Code = model.Code, Committe_Usage = model.Committe_Usage, Committe_Location = model.Committe_Location, Committe_Resolution_Date = model.Committe_Resolution_Date, Committe_Year = model.Committe_Year, Committe_Resolution_Status = model.Committe_Resolution_Status, Committe_Type = model.Committe_Type, Committe_Conclusion = model.Committe_Conclusion };
                          var record =  dbcontext.Committe_Resolution_Recuirtment.Add(benfit);
                        dbcontext.SaveChanges();
                        //if (Command == "Submit5" && record.Committe_Usage == Committe_Usage.Personnel)
                        //{
                        //    TempData["Message"] = HR.Resource.Personnel.YoumustchooseTestFromCommitteUsage;
                        //    return View(model);
                        //}
                        //if ((Command == "Submit" && record.Committe_Usage == Committe_Usage.Test) || (Command == "Submit2" && record.Committe_Usage == Committe_Usage.Test) || (Command == "Submit3" && record.Committe_Usage == Committe_Usage.Test) || (Command == "Submit4" && record.Committe_Usage == Committe_Usage.Test))
                        //{
                        //    TempData["Message"] = HR.Resource.Personnel.YoumustchoosePersonnelFromCommitteUsage;
                        //    return View(model);
                        //}


                        if (Command == "Submit" && record.Committe_Usage == Committe_Usage.Personnel)
                        {
                            return RedirectToAction("Create", "Commitee_Agenda", new { id = record.ID });
                        }
                        if (Command == "Submit2" && record.Committe_Usage == Committe_Usage.Personnel)
                        {
                            return RedirectToAction("Create", "Out_Organization", new { id = record.ID });
                        }
                        if (Command == "Submit3" && record.Committe_Usage == Committe_Usage.Personnel)
                        {
                            return RedirectToAction("Create", "In_Organization", new { id = record.ID });
                        }
                        if (Command == "Submit4" && record.Committe_Usage == Committe_Usage.Personnel) 
                        {
                            return RedirectToAction("Create", "Committe_Activities", new { id = record.ID });
                        }
                        if (Command == "Submit5" && record.Committe_Usage == Committe_Usage.Test)
                        {
                            return RedirectToAction("Create", "Linked_to_Testing", new { id = record.ID });
                        }
                    }
                    
                    dbcontext.SaveChanges();
                   
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
        public ActionResult Edit(string id)

        {
            try
            {
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                var ID = int.Parse(id);
                var record = dbcontext.Committe_Resolution_Recuirtment.FirstOrDefault(m => m.ID == ID);
                if (record != null)
                {
                        return View(record);
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
        public ActionResult Edit(Committe_Resolution_Recuirtment model, string Command, FormCollection form)
        {
            try
            {

                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                var record = dbcontext.Committe_Resolution_Recuirtment.FirstOrDefault(m => m.ID == model.ID);
                record.Code = model.Code;
                record.Committe_Usage = model.Committe_Usage;
                record.Committe_Location = model.Committe_Location;
                record.Committe_Resolution_Date = model.Committe_Resolution_Date;
                record.Committe_Year = model.Committe_Year;
                record.Committe_Resolution_Status = model.Committe_Resolution_Status;
                record.Committe_Type = model.Committe_Type;
                record.Committe_Conclusion = model.Committe_Conclusion;
                dbcontext.Append_Committe_Member.RemoveRange(record.Append_Committe_Member);
                dbcontext.SaveChanges();

                /////////////////add new Append_beneficiary_Family////////
                var emp_profile = form["Employee_profile_No2"].Split(char.Parse(","));
                var Emp_name = form["Employee_name"].Split(char.Parse(","));
                var Head = form["Is_Head"].Split(char.Parse(","));
                var items = new List<Append_Committe_Member>();
                for (var i = 0; i < emp_profile.Count(); i++)
                {
                    if (emp_profile[i] != "" && Emp_name[i] != "" && Head[i] != "")
                    {
                        items.Add(new Append_Committe_Member { Is_Committe_Head = Head[i], Employee_profile = emp_profile[i], Employee_Name = Emp_name[i] });
                    }
                }
                if (items.Count() > 0)
                {
                    var add_items = dbcontext.Append_Committe_Member.AddRange(items);
                    dbcontext.SaveChanges();
                    record.Append_Committe_Member = add_items.ToList();
                    /////////////////////////////////////
                    //var username = User.Identity.GetUserName();
                    //var Date = Convert.ToDateTime("1/1/1900");
                    //var s = new status { statu = check_status.created, created_by = username, Type = Models.Infra.Type.Committe_Resolution_Recuirtment, approved_bydate = Date, cancaled_bydate = Date, created_bydate = DateTime.Now.Date, Rejected_bydate = Date, return_to_reviewdate = Date };
                    //var st = dbcontext.status.Add(s);
                    //dbcontext.SaveChanges();
                    ///////////////////////////////
                    //var benfit = new Committe_Resolution_Recuirtment { /*check_status = check_status.created, statID = s.ID,*/ Append_Committe_Member = add_items.ToList(), Code = model.Code, Committe_Usage = model.Committe_Usage, Committe_Location = model.Committe_Location, Committe_Resolution_Date = model.Committe_Resolution_Date, Committe_Year = model.Committe_Year, Committe_Resolution_Status = model.Committe_Resolution_Status, Committe_Type = model.Committe_Type, Committe_Conclusion = model.Committe_Conclusion };
                    dbcontext.SaveChanges();

                    var Agenda = dbcontext.Commitee_Agenda.FirstOrDefault(a => a.Committe_Resolution_RecuirtmentId == model.ID);
                    var Out_Organization = dbcontext.Out_Organization.FirstOrDefault(a => a.Committe_Resolution_RecuirtmentId == model.ID);
                    var In_Organization = dbcontext.In_Organization.FirstOrDefault(a => a.Committe_Resolution_RecuirtmentId == model.ID);
                    var Committe_Activities = dbcontext.Committe_Activities.FirstOrDefault(a => a.Committe_Resolution_RecuirtmentId == model.ID);
                    var Linked_to_Testing = dbcontext.Linked_to_Testing.FirstOrDefault(a => a.Committe_Resolution_RecuirtmentId == model.ID);
                    //if (Command == "Submit5" && record.Committe_Usage == Committe_Usage.Personnel)
                    //{
                    //    TempData["Message"] = HR.Resource.Personnel.YoumustchooseTestFromCommitteUsage;
                    //    return View();
                    //}
                    //if ((Command == "Submit" && record.Committe_Usage == Committe_Usage.Test) || (Command == "Submit2" && record.Committe_Usage == Committe_Usage.Test) || (Command == "Submit3" && record.Committe_Usage == Committe_Usage.Test) || (Command == "Submit4" && record.Committe_Usage == Committe_Usage.Test))
                    //{
                    //    TempData["Message"] = HR.Resource.Personnel.YoumustchoosePersonnelFromCommitteUsage;
                    //    return View();
                    //}
                    if (Agenda != null && Command == "Submit")
                    {
                            return RedirectToAction("Edit", "Commitee_Agenda", new { id = record.ID });
                    }
                    else if (Agenda == null && Command == "Submit")
                    {
                        return RedirectToAction("Create", "Commitee_Agenda", new { id = record.ID });
                    }
                    if (Out_Organization != null && Command == "Submit2")
                    {
                            return RedirectToAction("Edit", "Out_Organization", new { id = record.ID });
                     }
                    else if (Out_Organization == null && Command == "Submit2")

                    {
                        return RedirectToAction("Create", "Out_Organization", new { id = record.ID });
                    }
                    if (In_Organization != null && Command == "Submit3")
                    {
                        return RedirectToAction("Edit", "In_Organization", new { id = record.ID });
                    }
                    else if (In_Organization == null && Command == "Submit3")

                    {
                        return RedirectToAction("Create", "In_Organization", new { id = record.ID });
                    }
                    if (Committe_Activities != null && Command == "Submit4")
                    {
                        return RedirectToAction("Edit", "Committe_Activities", new { id = record.ID });
                    }
                    else if (Committe_Activities == null && Command == "Submit4")

                    {
                        return RedirectToAction("Create", "Committe_Activities", new { id = record.ID });
                    }
                    if (Linked_to_Testing != null && Command == "Submit5")
                    {
                        return RedirectToAction("Edit", "Linked_to_Testing", new { id = record.ID });
                    }
                    else if (Linked_to_Testing == null && Command == "Submit5")

                    {
                        return RedirectToAction("Create", "Linked_to_Testing", new { id = record.ID });
                    }

                }
                dbcontext.SaveChanges();
                
                return RedirectToAction("Index");

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
        public ActionResult Delete(int id)
        {
            try
            {
                var record = dbcontext.Committe_Resolution_Recuirtment.FirstOrDefault(m => m.ID == id);
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
            var record = dbcontext.Committe_Resolution_Recuirtment.FirstOrDefault(m => m.ID == id);
            try
            {
                dbcontext.Committe_Resolution_Recuirtment.Remove(record);
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
                return View();
            }
        }
        public ActionResult status(string id)
        {
            try
            {
                var ID = int.Parse(id);
                var model = dbcontext.Committe_Resolution_Recuirtment.FirstOrDefault(m => m.ID == ID);
                var st = dbcontext.status.FirstOrDefault(m => m.ID == model.statID);
                ViewBag.statue = dbcontext.status.ToList().Select(m => new { code = m.approved_by });
                var my_model = new employeestate { status = st, empid = ID };
                return View(my_model);
            }
            catch (Exception e)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult status(employeestate model)
        {
            var sta = dbcontext.status.FirstOrDefault(m => m.ID == model.status.ID);
            var record = dbcontext.Committe_Resolution_Recuirtment.FirstOrDefault(m => m.ID == model.empid);
            var committe = dbcontext.Committe_Resolution_Recuirtment.Select(a => a.Committe_Resolution_Status).ToList();
            if (model.check_status == check_status.Approved)
            {
                sta.approved_by = User.Identity.GetUserName();
                sta.approved_bydate = model.status.approved_bydate;
                record.check_status = check_status.Approved;
                record.name_state = nameof(check_status.Approved);
                record.Committe_Resolution_Status= Committe_Resolution_Status.Approved; 
                dbcontext.SaveChanges();
            }
         
            else if (model.check_status == check_status.Rejected)
            {
                sta.Rejected_by = User.Identity.GetUserName();
                sta.Rejected_bydate = model.status.Rejected_bydate;
                record.check_status = check_status.Rejected;
                record.name_state = nameof(check_status.Rejected);
                record.Committe_Resolution_Status = Committe_Resolution_Status.Rejected;
                dbcontext.SaveChanges();
            }
            else if (model.check_status == check_status.Return_To_Review)
            {
                sta.return_to_reviewby = User.Identity.GetUserName();
                sta.return_to_reviewdate = model.status.return_to_reviewdate;
                record.check_status = check_status.Return_To_Review;
                record.name_state = nameof(check_status.Return_To_Review);
                record.Committe_Resolution_Status = Committe_Resolution_Status.Canceled;
                dbcontext.SaveChanges();
            }
            return RedirectToAction("index");
        }
    }
}