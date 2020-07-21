using HR.Models;
using HR.Models.Infra;
using HR.Models.Training.transaction;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers.training.Transaction
{
    public class TrainingOpportunityController : BaseController
    {
        // GET: TrainingOpportunity
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        public ActionResult Index()
        {
            var list = dbcontext.TrainingOpportunity_Header.ToList();
            return View(list);
        }
        public ActionResult create()
        {
            try
            {
                ViewBag.type = dbcontext.TrainingType_Header.ToList().Select(m => new { Code = m.TrainingType_Code + "-[" + m.TrainingType_Desc + ']', ID = m.ID });
                ViewBag.currency = dbcontext.Currency.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.classification = dbcontext.CourseClassification.ToList().Select(m => new { Code = m.CourseClassification_Code + "-[" + m.CourseClassification_Desc + ']', ID = m.ID });
                ViewBag.cadre = dbcontext.job_level_setup.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID }).ToList();
                ViewBag.E_level = dbcontext.Educate_Title.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID }).ToList();
                var new_model = new TrainingOpportunity_Header { Year = DateTime.Now.Year ,Total_Estimated_Local_Cost=0,Total_Estimated_Foreign_Cost=0};

                return View(new VM { TrainingOpportunity_Header=new_model,TrainingOpportunity_Detail=new TrainingOpportunity_Detail(),Distributed_Opportunity=new Distributed_Opportunity()});
            }
            catch(Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult create(VM model,FormCollection form)
        {
            ViewBag.type = dbcontext.TrainingType_Header.ToList().Select(m => new { Code = m.TrainingType_Code + "-[" + m.TrainingType_Desc + ']', ID = m.ID });
            ViewBag.currency = dbcontext.Currency.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
            ViewBag.classification = dbcontext.CourseClassification.ToList().Select(m => new { Code = m.CourseClassification_Code + "-[" + m.CourseClassification_Desc + ']', ID = m.ID });
            ViewBag.cadre = dbcontext.job_level_setup.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID }).ToList();
            ViewBag.E_level = dbcontext.Educate_Title.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID }).ToList();

            try
            {
                var status = new status { created_by = User.Identity.Name, created_bydate = DateTime.Now.Date , statu=check_status.created };
                var S=dbcontext.status.Add(status);
                dbcontext.SaveChanges();
                ///======
                model.TrainingOpportunity_Header.CreatedBy = User.Identity.Name;
                model.TrainingOpportunity_Header.CreatedDate = DateTime.Now.Date;
                model.TrainingOpportunity_Header.statusID = S.ID;
                model.TrainingOpportunity_Header.Transaction_Status = (Int16)check_status.created;
                var header=  dbcontext.TrainingOpportunity_Header.Add(model.TrainingOpportunity_Header);
                dbcontext.SaveChanges();
                //=============================================================================
                var Classification = form["Classification__"].Split(',');
                var job = form["job__"].Split(',');
                var education = form["education__"].Split(',');
                var total_emp = form["total_emp__"].Split(',');
                var total_oppo = form["total_oppo__"].Split(',');
                var cosrtperoppo = form["cosrtperoppo__"].Split(',');
                var total_co = form["total_co"].Split(',');
                var FOR = form["FOR"].Split(',');
                var emp_in = form["emp_in__"].Split(',');
                var oppo_in = form["oppo_in__"].Split(',');
                var emo_out = form["emo_out__"].Split(',');
                var oppo_out = form["oppo_out__"].Split(',');
                var emp_in_ = form["emp_in__"].Split(',');
                for (var i = 0; i < Classification.Length; i++)
                {
                    if (Classification[i] != "" || i>0)
                    {
                        var ID_class=0;
                        if (Classification[i]!="")
                        {
                            ID_class = int.Parse(Classification[i]);
                        }
                        
                        var classification = dbcontext.CourseClassification.FirstOrDefault(m => m.ID == ID_class);
                        //====
                        var ID_cadr = 0;
                        if (job[i] != "")
                        {
                            ID_cadr = int.Parse(job[i]);
                        }

                        
                        var cadre = dbcontext.job_level_setup.FirstOrDefault(m => m.ID == ID_cadr);
                        //====
                        var ID_edu = 0;
                        if (education[i] != "")
                        {
                            ID_edu = int.Parse(education[i]);
                        }
                        var edu = dbcontext.Educate_Title.FirstOrDefault(m => m.ID == ID_edu);
                        //====

                        var details = new TrainingOpportunity_Detail();
                        details.headerID = header.ID;
                        details.TrainingType_Code = model.TrainingOpportunity_Header.TrainingType_Code;
                        details.Year = model.TrainingOpportunity_Header.Year;
                        details.Total_Cost = int.Parse(total_co[i]);
                        details.Cost_Per_Opportunity = int.Parse(cosrtperoppo[i]);
                        details.Total_Cost_for = float.Parse(FOR[i]);
                        details.Total_Num_Of_Employee = int.Parse(total_emp[i]);
                        details.Total_Num_Of_Opportunity = int.Parse(total_oppo[i]);
                        details.Created_By = User.Identity.Name;
                        details.Created_Date = DateTime.Now.Date ;



                        if(cadre!=null)
                        {
                            details.Cadre_Code = cadre.ID.ToString();
                            details.card_des = cadre.Code + "-" + cadre.Name;
                        }
                        if(classification!=null)
                        {
                            details.CourseClassification_Code = classification.ID.ToString();
                            details.classifiaction_des = classification.CourseClassification_Code + "-" + classification.CourseClassification_Desc;
                          
                        }
                        dbcontext.TrainingOpportunity_Detail.Add(details);
                        dbcontext.SaveChanges();
                        //========================================
                        var Distributed = new Distributed_Opportunity
                        {
                            headerID = header.ID,
                            TrainingType_Code = model.TrainingOpportunity_Header.TrainingType_Code,
                            Year = model.TrainingOpportunity_Header.Year,
                            InHouse_Number_Of_Opportunity= int.Parse(oppo_in[i]),
                            OutSite_Number_Of_Opportunity= int.Parse(oppo_out[i]),
                            InHouse_To_Be_Trainee = int.Parse(emp_in_[i]),
                            OutSite_To_Be_Trainee = int.Parse(emo_out[i]),
                            Number_Of_Employee = int.Parse(total_emp[i]),
                            Created_By = User.Identity.Name,
                            Created_Date = DateTime.Now.Date
                        };
                        if (cadre != null)
                        {
                            Distributed.Cadre_Code = cadre.ID.ToString();
                            Distributed.card_des = cadre.Code + "-" + cadre.Name;
                        }
                        if (classification != null)
                        {
                            Distributed.CourseClassification_Code = classification.ID.ToString();
                            Distributed.classifiaction_des = classification.CourseClassification_Code + "-" + classification.CourseClassification_Desc;

                        }
                        if(edu!=null)
                        {
                            Distributed.EducationLevel_Code = edu.ID.ToString();
                            Distributed.education_des = edu.Code + "-" + edu.Name;
                           
                        }
                        dbcontext.Distributed_Opportunity.Add(Distributed);
                        dbcontext.SaveChanges();
                    }
                   
                }
                return RedirectToAction("index");
            }
            catch(Exception e)
            {
                return View(model);
            }
        }
        public ActionResult edit(int id)
        {
            try
            {
                ViewBag.type = dbcontext.TrainingType_Header.ToList().Select(m => new { Code = m.TrainingType_Code + "-[" + m.TrainingType_Desc + ']', ID = m.ID });
                ViewBag.currency = dbcontext.Currency.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.classification = dbcontext.CourseClassification.ToList().Select(m => new { Code = m.CourseClassification_Code + "-[" + m.CourseClassification_Desc + ']', ID = m.ID });
                ViewBag.cadre = dbcontext.job_level_setup.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID }).ToList();
                ViewBag.E_level = dbcontext.Educate_Title.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID }).ToList();
                var header = dbcontext.TrainingOpportunity_Header.FirstOrDefault(m => m.ID == id);
                var edit_model = new VM { TrainingOpportunity_Header = header, TrainingOpportunity_Detail = new TrainingOpportunity_Detail(), Distributed_Opportunity = new Distributed_Opportunity() };
                return View(edit_model);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult edit(VM model,FormCollection form)
        {
            try
            {
                ViewBag.type = dbcontext.TrainingType_Header.ToList().Select(m => new { Code = m.TrainingType_Code + "-[" + m.TrainingType_Desc + ']', ID = m.ID });
                ViewBag.currency = dbcontext.Currency.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.classification = dbcontext.CourseClassification.ToList().Select(m => new { Code = m.CourseClassification_Code + "-[" + m.CourseClassification_Desc + ']', ID = m.ID });
                ViewBag.cadre = dbcontext.job_level_setup.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID }).ToList();
                ViewBag.E_level = dbcontext.Educate_Title.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID }).ToList();
                var H_ = dbcontext.TrainingOpportunity_Header.FirstOrDefault(m => m.ID == model.TrainingOpportunity_Header.ID);
                var sta = dbcontext.status.FirstOrDefault(m => m.ID == H_.statusID);
                if (sta.statu == check_status.Approved || sta.statu == check_status.Rejected || sta.statu == check_status.Closed || sta.statu == check_status.Recervied || sta.statu == check_status.Canceled)
                {
                    TempData["message"] = HR.Resource.training.status_message;
                    return RedirectToAction("index");
                }
                //==================== Remove old data
                var detail = dbcontext.TrainingOpportunity_Detail.Where(m => m.headerID == model.TrainingOpportunity_Header.ID).ToList();
                var distributed = dbcontext.Distributed_Opportunity.Where(m => m.headerID == model.TrainingOpportunity_Header.ID).ToList();
                dbcontext.TrainingOpportunity_Detail.RemoveRange(detail);
                dbcontext.Distributed_Opportunity.RemoveRange(distributed);
                dbcontext.SaveChanges();
                //====================updated header
                var header = dbcontext.TrainingOpportunity_Header.FirstOrDefault(m => m.ID == model.TrainingOpportunity_Header.ID);
                header.Modified_By = User.Identity.Name;header.Modified_Date = DateTime.Now.Date;header.Year = model.TrainingOpportunity_Header.Year;
                header.TrainingType_Code = model.TrainingOpportunity_Header.TrainingType_Code;header.Total_Estimated_Local_Cost = model.TrainingOpportunity_Header.Total_Estimated_Local_Cost;
                header.Total_Estimated_Foreign_Cost = model.TrainingOpportunity_Header.Total_Estimated_Foreign_Cost;header.Currency_Code = model.TrainingOpportunity_Header.Currency_Code;
                dbcontext.SaveChanges();
                //======================add new data
                var Classification = form["Classification__"].Split(',');
                var job = form["job__"].Split(',');
                var education = form["education__"].Split(',');
                var total_emp = form["total_emp__"].Split(',');
                var total_oppo = form["total_oppo__"].Split(',');
                var cosrtperoppo = form["cosrtperoppo__"].Split(',');
                var total_co = form["total_co"].Split(',');
                var FOR = form["FOR"].Split(',');
                var emp_in = form["emp_in__"].Split(',');
                var oppo_in = form["oppo_in__"].Split(',');
                var emo_out = form["emo_out__"].Split(',');
                var oppo_out = form["oppo_out__"].Split(',');
                var emp_in_ = form["emp_in__"].Split(',');
                for (var i = 0; i < Classification.Length; i++)
                {
                    if (Classification[i] != "" || i > 0)
                    {
                        var ID_class = 0;
                        if (Classification[i] != "")
                        {
                            ID_class = int.Parse(Classification[i]);
                        }

                        var classification = dbcontext.CourseClassification.FirstOrDefault(m => m.ID == ID_class);
                        //====
                        var ID_cadr = 0;
                        if (job[i] != "")
                        {
                            ID_cadr = int.Parse(job[i]);
                        }


                        var cadre = dbcontext.job_level_setup.FirstOrDefault(m => m.ID == ID_cadr);
                        //====
                        var ID_edu = 0;
                        if (education[i] != "")
                        {
                            ID_edu = int.Parse(education[i]);
                        }
                        var edu = dbcontext.Educate_Title.FirstOrDefault(m => m.ID == ID_edu);
                        //====

                        var details = new TrainingOpportunity_Detail();
                        details.headerID = header.ID;
                        details.TrainingType_Code = model.TrainingOpportunity_Header.TrainingType_Code;
                        details.Year = model.TrainingOpportunity_Header.Year;
                        details.Total_Cost = int.Parse(total_co[i]);
                        details.Cost_Per_Opportunity = int.Parse(cosrtperoppo[i]);
                        details.Total_Cost_for = float.Parse(FOR[i]);
                        details.Total_Num_Of_Employee = int.Parse(total_emp[i]);
                        details.Total_Num_Of_Opportunity = int.Parse(total_oppo[i]);
                        details.Created_By = User.Identity.Name;
                        details.Created_Date = DateTime.Now.Date;



                        if (cadre != null)
                        {
                            details.Cadre_Code = cadre.ID.ToString();
                            details.card_des = cadre.Code + "-" + cadre.Name;
                        }
                        if (classification != null)
                        {
                            details.CourseClassification_Code = classification.ID.ToString();
                            details.classifiaction_des = classification.CourseClassification_Code + "-" + classification.CourseClassification_Desc;

                        }
                        dbcontext.TrainingOpportunity_Detail.Add(details);
                        dbcontext.SaveChanges();
                        //========================================
                        var Distributed = new Distributed_Opportunity
                        {
                            headerID = header.ID,
                            TrainingType_Code = model.TrainingOpportunity_Header.TrainingType_Code,
                            Year = model.TrainingOpportunity_Header.Year,
                            InHouse_Number_Of_Opportunity = int.Parse(oppo_in[i]),
                            OutSite_Number_Of_Opportunity = int.Parse(oppo_out[i]),
                            InHouse_To_Be_Trainee = int.Parse(emp_in_[i]),
                            OutSite_To_Be_Trainee = int.Parse(emo_out[i]),
                            Number_Of_Employee = int.Parse(total_emp[i]),
                            Created_By = User.Identity.Name,
                            Created_Date = DateTime.Now.Date
                        };
                        if (cadre != null)
                        {
                            Distributed.Cadre_Code = cadre.ID.ToString();
                            Distributed.card_des = cadre.Code + "-" + cadre.Name;
                        }
                        if (classification != null)
                        {
                            Distributed.CourseClassification_Code = classification.ID.ToString();
                            Distributed.classifiaction_des = classification.CourseClassification_Code + "-" + classification.CourseClassification_Desc;

                        }
                        if (edu != null)
                        {
                            Distributed.EducationLevel_Code = edu.ID.ToString();
                            Distributed.education_des = edu.Code + "-" + edu.Name;

                        }
                        dbcontext.Distributed_Opportunity.Add(Distributed);
                        dbcontext.SaveChanges();
                    }
                   
                }
                return RedirectToAction("index");
            }
            catch (Exception)
            {
                return View(model);
            }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                var header = dbcontext.TrainingOpportunity_Header.FirstOrDefault(m => m.ID == id);
                 if (header != null)
                { return View(header); }
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
            var header = dbcontext.TrainingOpportunity_Header.FirstOrDefault(m => m.ID == id);
            var H_ = dbcontext.TrainingOpportunity_Header.FirstOrDefault(m => m.ID == header.ID);
            var sta = dbcontext.status.FirstOrDefault(m => m.ID == H_.statusID);
            if (sta.statu == check_status.Approved || sta.statu == check_status.Rejected || sta.statu == check_status.Closed || sta.statu == check_status.Recervied || sta.statu == check_status.Canceled)
            {
                TempData["message"] = HR.Resource.training.status_message;
                return RedirectToAction("index");
            }
            var details = dbcontext.TrainingOpportunity_Detail.Where(m => m.headerID == id).ToList();
            var destr = dbcontext.Distributed_Opportunity.Where(m => m.headerID == id).ToList();
            var status = dbcontext.status.FirstOrDefault(m => m.ID == header.statusID);

            try
            {
                dbcontext.TrainingOpportunity_Header.Remove(header);
                if(details.Count>0)
                    dbcontext.TrainingOpportunity_Detail.RemoveRange(details);
                if (destr.Count > 0)
                    dbcontext.Distributed_Opportunity.RemoveRange(destr);
                dbcontext.status.Remove(status);

                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = HR.Resource.Basic.youcannotdeletethisRow;
                return View(header);
            }
            catch (Exception e)
            {
                return View(header);
            }
        }
        public ActionResult status(int id)
        {
            ViewBag.header_id = id;
            var hearder = dbcontext.TrainingOpportunity_Header.FirstOrDefault(m => m.ID == id);
            var my_model = dbcontext.status.FirstOrDefault(m => m.ID == hearder.statusID);
            if (my_model.approved_by == null)
                my_model.approved_bydate = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString());
            if (my_model.Rejected_by == null)
                my_model.Rejected_bydate = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString());
            if (my_model.return_to_reviewby == null)
                my_model.return_to_reviewdate = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString());
            if (my_model.cancaled_by == null)
                my_model.cancaled_bydate = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString());
            if (my_model.closed_by == null)
                my_model.closed_bydate = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString());
            if (my_model.Recervied_by == null)
                my_model.Recervied_bydate = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString());
            return View(my_model);
        }
        [HttpPost]
        public ActionResult status(status model,int header_id2)
        {
            ViewBag.header_id = header_id2;
            var record = dbcontext.TrainingOpportunity_Header.FirstOrDefault(m => m.ID == header_id2);
            if (model.statu == check_status.Approved)
            {

                var sta = dbcontext.status.FirstOrDefault(m => m.ID == record.statusID);
                sta.approved_by = User.Identity.Name;
                sta.approved_bydate = model.approved_bydate;
                sta.statu = check_status.Approved;
                dbcontext.SaveChanges();

                record.Transaction_Status = (Int16)check_status.Approved;
                dbcontext.SaveChanges();


                
            }
            else if (model.statu == check_status.Closed)
            {
                var sta = dbcontext.status.FirstOrDefault(m => m.ID == record.statusID);

                sta.closed_by = User.Identity.Name;
                sta.closed_bydate = model.closed_bydate;
                sta.statu = check_status.Closed;
                dbcontext.SaveChanges();

                record.Transaction_Status = (Int16)check_status.Closed;
            
                dbcontext.SaveChanges();


            }
            else if (model.statu == check_status.Canceled)
            {
                var sta = dbcontext.status.FirstOrDefault(m => m.ID == record.statusID);

                sta.cancaled_by = User.Identity.Name;
                sta.cancaled_bydate = model.cancaled_bydate;
                sta.statu = check_status.Canceled;
                dbcontext.SaveChanges();
                record.Transaction_Status = (Int16)check_status.Canceled;
              
                dbcontext.SaveChanges();

            }
            else if (model.statu == check_status.Recervied)
            {
                var sta = dbcontext.status.FirstOrDefault(m => m.ID == record.statusID);

                sta.Recervied_by = User.Identity.Name;
                sta.Recervied_bydate = model.Recervied_bydate;
                sta.statu = check_status.Recervied;
                dbcontext.SaveChanges();
                record.Transaction_Status = (Int16)check_status.Recervied;
                
                dbcontext.SaveChanges();


            }
            else if (model.statu == check_status.Rejected)
            {
                var sta = dbcontext.status.FirstOrDefault(m => m.ID == record.statusID);
                sta.Rejected_by = User.Identity.Name;
                sta.Rejected_bydate = model.approved_bydate;
                sta.statu = check_status.Rejected;
                dbcontext.SaveChanges();
                record.Transaction_Status = (Int16)check_status.Rejected;
                dbcontext.SaveChanges();

            }
            else if (model.statu == check_status.Return_To_Review)
            {
                var sta = dbcontext.status.FirstOrDefault(m => m.ID == record.statusID);
                sta.return_to_reviewby = User.Identity.Name;
                sta.return_to_reviewdate = model.approved_bydate;
                sta.statu = check_status.Return_To_Review;
                dbcontext.SaveChanges();
                record.Transaction_Status = (Int16)check_status.Return_To_Review;
                dbcontext.SaveChanges();

            }

            return RedirectToAction("index");
        }

        //===========================================ajax================================
        public JsonResult get_num_emp(int cadre,int educ)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            var emp_edu = dbcontext.Employee_Qualification_Profile.Where(m => m.Educate_TitleId == educ.ToString()).ToList();
            var emp_cadre = dbcontext.Position_Information.Where(m => m.Job_level_gradeId == cadre.ToString()&&m.Primary_Position==true).ToList();
            var employee_E = new List<Employee_Profile>();
            var employee_po = new List<Employee_Profile>();
            var employee = new List<Employee_Profile>();

            ///===================================================
            foreach (var i in emp_edu)
            {
                var ID = int.Parse(i.Employee_ProfileId);
                var e = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == ID);
                if(!employee_E.Contains(e))
                {
                    employee_E.Add(e);
                }
            }

            ///===================================================
            foreach (var i in emp_cadre)
            {
                var ID = int.Parse(i.Employee_ProfileId);
                var e = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == ID);
                if (!employee_po.Contains(e))
                {
                    employee_po.Add(e);
                }
            }
            ///===================================================
            if(employee_E.Count> employee_po.Count)
            {
                foreach(var item in employee_po)
                {
                    if(employee_E.Contains(item) )
                    {
                        if (!employee.Contains(item))
                        {
                            employee.Add(item);
                        }
                    }
                }
            }
            else
            {
                foreach (var item in employee_E)
                {
                    if (employee_po.Contains(item))
                    {
                        if (!employee.Contains(item))
                        {
                            employee.Add(item);
                        }
                    }
                }
            }

            
            return Json(employee.Count());
        }
        public JsonResult getcurrency(int local, int year,int cuID)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            var cur = dbcontext.Exchange_Rate.FirstOrDefault(m => m.Year == year &&m.CurrencyID==cuID);
            var M = new List<months>();
            if (cur != null)
            {
              M = dbcontext.months.Where(m => m.Exchange_RateID == cur.ID).ToList();
            }
            var av = 0.0;
            foreach (var item in M)
            {
                av += item.value;
            }
            av =((float)local)*( av / 12);
            return Json(av);
        }
        public JsonResult getallcontent(int header_id)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            var details = dbcontext.TrainingOpportunity_Detail.Where(m => m.headerID == header_id).ToList();
            var desi = dbcontext.Distributed_Opportunity.Where(m => m.headerID == header_id).ToList();
            var new_data = new List<VM>();
            for(var i=0;i< details.Count();i++)
            {
                new_data.Add(new VM { TrainingOpportunity_Detail = details.ElementAt(i), Distributed_Opportunity = desi.ElementAt(i) });
               
            }
            return Json(new_data);
        }
    }
    public class VM
    {
        public TrainingOpportunity_Header TrainingOpportunity_Header { get; set; }
        public TrainingOpportunity_Detail TrainingOpportunity_Detail { get; set; }
        public Distributed_Opportunity Distributed_Opportunity { get; set; }
    }
}