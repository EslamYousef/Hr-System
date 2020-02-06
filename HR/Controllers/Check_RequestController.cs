using HR.Models;
using HR.Models.Infra;
using HR.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers
{
    [Authorize]
    public class Check_RequestController : BaseController
    {
        // GET: Check_Request
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        public ActionResult index()
        {
            var all_requests = dbcontext.check_Request.ToList();
            var all_statues = dbcontext.check_request_change_status.ToList();
                var model=  from a in all_requests
                        join b in all_statues on a.check_request_change_statusID equals b.ID.ToString()
                        select new Check_Request_VM
                        {
                           check_Request=a,check_request_change_status=b
                        };
            return View(model);
        }
        
        public ActionResult history(string id)
        {
            try
            {
                var ID = int.Parse(id);
                var request = dbcontext.check_Request.FirstOrDefault(m=>m.ID==ID);
                var status_id = int.Parse(request.check_request_change_statusID);
                var all_statues = dbcontext.check_request_change_status.FirstOrDefault(m=>m.ID== status_id);
                if(all_statues.selected_signby>0)
                {
                    all_statues.Sign_by = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == all_statues.selected_signby);
                }
                if(all_statues.selected_directedto > 0)
                {
                    all_statues.Directed_to = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == all_statues.selected_directedto);

                }
                var model = new Check_Request_VM
                            {
                                check_Request = request,
                                check_request_change_status = all_statues
                };
                return View(model);
            }
            catch (Exception e)
            {
                return View();
            }
        }
        public ActionResult Create()
        {
            ViewBag.employee = dbcontext.Employee_Profile.ToList().Select(m => new { Code = "" + m.Code + "-----[" + m.Full_Name + ']', ID = m.ID }).ToList();

            ViewBag.Type = dbcontext.Checktype.ToList().Select(m=>new { Code=m.Code+"-----"+m.Name,ID=m.ID});
            ViewBag.check_request_status = dbcontext.check_request_status.ToList().Select(m => new { Code = m.Code + "-----" + m.Name, ID = m.ID });
            var req = dbcontext.check_Request.ToList();
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Basic).Structure_Code;
            string number;
            if(req.Count>0)
            {
                number = stru+ (req.LastOrDefault().ID + 1).ToString();
            }
            else
            {
                number = stru+1;
            }
            var check_Request_model = new check_Request { Request_number=number, check_number=0.ToString(),Request_date=DateTime.Now.Date,Check_Due_date=DateTime.Now.Date,month=1,year=1990,amount=0.00};
            var check_request_change_status = new check_request_change_status { Date=DateTime.Now.Date,selected_directedto=0,selected_signby=0};
            var model = new Check_Request_VM {  check_Request= check_Request_model, check_request_change_status= check_request_change_status };
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(Check_Request_VM model)
        {
            try
            {
                ViewBag.employee = dbcontext.Employee_Profile.ToList().Select(m => new { Code = "" + m.Code + "-----[" + m.Full_Name + ']', ID = m.ID }).ToList();

                ViewBag.Type = dbcontext.Checktype.ToList().Select(m => new { Code = m.Code + "-----" + m.Name, ID = m.ID });
                ViewBag.check_request_status = dbcontext.check_request_status.ToList().Select(m => new { Code = m.Code + "-----" + m.Name, ID = m.ID });
                if (model.check_Request.check_number == 0.ToString()||model.check_Request.check_number==null)
                {
                    TempData["Message"] = "You Must enter check number status";
                    return View(model);
                }
               var check_request_change_status_record = new check_request_change_status();
                if(model.check_request_change_status.selected_signby>0)
                {
                    check_request_change_status_record.selected_signby = model.check_request_change_status.selected_signby;
                    check_request_change_status_record.Sign_by = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == model.check_request_change_status.selected_signby);
                }
                else
                {
                    check_request_change_status_record.Sign_by = null;
                }
                if (model.check_request_change_status.selected_directedto > 0)
                {
                    check_request_change_status_record.selected_directedto = model.check_request_change_status.selected_directedto;
                    check_request_change_status_record.Directed_to = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == model.check_request_change_status.selected_directedto);
                }
                else
                {
                    check_request_change_status_record.Directed_to = null;
                }
                check_request_change_status_record.Date = model.check_request_change_status.Date;
                check_request_change_status_record.Comment = model.check_request_change_status.Comment;
                check_request_change_status_record.check_request_statusID = model.check_request_change_status.check_request_statusID;
                var ID =int.Parse( model.check_request_change_status.check_request_statusID);
                check_request_change_status_record.check_Request = dbcontext.check_request_status.FirstOrDefault(m => m.ID == ID);
                var check_request_change_status=dbcontext.check_request_change_status.Add(check_request_change_status_record);
                dbcontext.SaveChanges();
                if (check_request_change_status != null)
                {
                    var check_request_record = new check_Request();
                    check_request_record.Request_date = model.check_Request.Request_date;
                    check_request_record.month = model.check_Request.month;
                    check_request_record.year = model.check_Request.year;
                    check_request_record.amount = model.check_Request.amount;
                    check_request_record.check_number = model.check_Request.check_number;
                    check_request_record.Check_Due_date = model.check_Request.Check_Due_date;
                    check_request_record.Description = model.check_Request.Description;
                    check_request_record.Comment = model.check_Request.Comment;
                    check_request_record.Request_number = model.check_Request.Request_number;
                    var ID_ = int.Parse(model.check_Request.ChecktypeID);
                    check_request_record.ChecktypeID = model.check_Request.ChecktypeID;
                    check_request_record.Checktype = dbcontext.Checktype.FirstOrDefault(m => m.ID == ID_);
                    check_request_record.check_request_change_statusID = check_request_change_status.ID.ToString();
                    check_request_record.check_request_change_status = check_request_change_status;
                    dbcontext.check_Request.Add(check_request_record);
                    dbcontext.SaveChanges();
                    return RedirectToAction("index");
                }
                else
                {
                    TempData["Message"] = "You Must enter check request statu";
                    return View(model);
                }
            }
            catch (Exception e)
            {
                return View();
            }
           
        }
        public ActionResult Edit(string id)
        {
            try
            {
                ViewBag.employee = dbcontext.Employee_Profile.ToList().Select(m => new { Code = "" + m.Code + "-----[" + m.Full_Name + ']', ID = m.ID }).ToList();

                ViewBag.Type = dbcontext.Checktype.ToList().Select(m => new { Code = m.Code + "-----" + m.Name, ID = m.ID });
                ViewBag.check_request_status = dbcontext.check_request_status.ToList().Select(m => new { Code = m.Code + "-----" + m.Name, ID = m.ID });

                var ID = int.Parse(id);
                var request = dbcontext.check_Request.FirstOrDefault(m => m.ID == ID);
                var statue_ID = int.Parse(request.check_request_change_statusID);
                var request_statue = dbcontext.check_request_change_status.FirstOrDefault(m => m.ID == statue_ID);
                var model = new Check_Request_VM { check_Request = request, check_request_change_status = request_statue };
                return View(model);
            }
            catch(Exception e)
            { return View(); }
        }
        [HttpPost]
        public ActionResult Edit(Check_Request_VM model)
        {
            try
            {
                ViewBag.employee = dbcontext.Employee_Profile.ToList().Select(m => new { Code = "" + m.Code + "-----[" + m.Full_Name + ']', ID = m.ID }).ToList();

                ViewBag.Type = dbcontext.Checktype.ToList().Select(m => new { Code = m.Code + "-----" + m.Name, ID = m.ID });
                ViewBag.check_request_status = dbcontext.check_request_status.ToList().Select(m => new { Code = m.Code + "-----" + m.Name, ID = m.ID });
                if (model.check_Request.check_number == 0.ToString() || model.check_Request.check_number == null)
                {
                    TempData["Message"] = "You Must enter check number";
                    return View(model);
                }
                var check_request_change_status_record = dbcontext.check_request_change_status.FirstOrDefault(m=>m.ID==model.check_request_change_status.ID);
                check_request_change_status_record.Date = model.check_request_change_status.Date;
                check_request_change_status_record.Comment = model.check_request_change_status.Comment;
                if (model.check_request_change_status.selected_signby > 0)
                {
                    check_request_change_status_record.selected_signby = model.check_request_change_status.selected_signby;
                    check_request_change_status_record.Sign_by = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == model.check_request_change_status.selected_signby);
                }
                else
                {
                    check_request_change_status_record.selected_signby = 0;
                    check_request_change_status_record.Sign_by = null;
                }
                if (model.check_request_change_status.selected_directedto > 0)
                {
                    check_request_change_status_record.selected_directedto = model.check_request_change_status.selected_directedto;
                    check_request_change_status_record.Directed_to = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == model.check_request_change_status.selected_directedto);
                }
                else
                {
                    check_request_change_status_record.selected_directedto = 0;
                    check_request_change_status_record.Directed_to = null;
                }
                check_request_change_status_record.check_request_statusID = model.check_request_change_status.check_request_statusID;
                var ID = int.Parse(model.check_request_change_status.check_request_statusID);
                check_request_change_status_record.check_Request = dbcontext.check_request_status.FirstOrDefault(m => m.ID == ID);
                //var check_request_change_status = dbcontext.check_request_change_status.Add(check_request_change_status_record);
                dbcontext.SaveChanges();
                
                    var check_request_record = dbcontext.check_Request.FirstOrDefault(m=>m.ID==model.check_Request.ID);
                    check_request_record.Request_date = model.check_Request.Request_date;
                    check_request_record.month = model.check_Request.month;
                    check_request_record.year = model.check_Request.year;
                    check_request_record.amount = model.check_Request.amount;
                    check_request_record.check_number = model.check_Request.check_number;
                    check_request_record.Check_Due_date = model.check_Request.Check_Due_date;
                    check_request_record.Description = model.check_Request.Description;
                    check_request_record.Comment = model.check_Request.Comment;
                    check_request_record.Request_number = model.check_Request.Request_number;
                    var ID_ = int.Parse(model.check_Request.ChecktypeID);
                    check_request_record.ChecktypeID = model.check_Request.ChecktypeID;
                    check_request_record.Checktype = dbcontext.Checktype.FirstOrDefault(m => m.ID == ID_);
                    //check_request_record.check_request_change_statusID = check_request_change_status.ID.ToString();
                    //check_request_record.check_request_change_status = check_request_change_status;
                    //dbcontext.check_Request.Add(check_request_record);
                    dbcontext.SaveChanges();
                    return RedirectToAction("index");
                
                
            }
            catch (Exception e)
            {
                return View(model);
            }

        }
        public ActionResult Delete(string id)
        {
            try
            {
                ViewBag.Type = dbcontext.Checktype.ToList().Select(m => new { Code = m.Code + "-----" + m.Name, ID = m.ID });
                ViewBag.check_request_status = dbcontext.check_request_status.ToList().Select(m => new { Code = m.Code + "-----" + m.Name, ID = m.ID });
                var ID = int.Parse(id);
                var request = dbcontext.check_Request.FirstOrDefault(m => m.ID == ID);
                var statue_ID = int.Parse(request.check_request_change_statusID);
                var request_statue = dbcontext.check_request_change_status.FirstOrDefault(m => m.ID == statue_ID);
                var model = new Check_Request_VM { check_Request = request, check_request_change_status = request_statue };
                if (model != null)
                {
                    return View(model);
                }
                else
                {
                    TempData["Message"]= "there is no Check_Request";
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
        public ActionResult Deletemethod(string id)
        {
            try
            {
                ViewBag.Type = dbcontext.Checktype.ToList().Select(m => new { Code = m.Code + "-----" + m.Name, ID = m.ID });
                ViewBag.check_request_status = dbcontext.check_request_status.ToList().Select(m => new { Code = m.Code + "-----" + m.Name, ID = m.ID });
                var ID = int.Parse(id);
                var request = dbcontext.check_Request.FirstOrDefault(m => m.ID == ID);
                var statue_ID = int.Parse(request.check_request_change_statusID);
                var request_statue = dbcontext.check_request_change_status.FirstOrDefault(m => m.ID == statue_ID);
                var model = new Check_Request_VM { check_Request = request, check_request_change_status = request_statue };

                dbcontext.check_Request.Remove(request);
                dbcontext.check_request_change_status.Remove(request_statue);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (Exception e)
            {

                return View();
            }
        }





    }
}