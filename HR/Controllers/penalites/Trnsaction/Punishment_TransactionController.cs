using HR.Models;
using HR.Models.Infra;
using HR.Models.penalities.setup;
using HR.Models.TransactionsPayroll;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers.penalites.Trnsaction
{
    
    public class Punishment_TransactionController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();

        [Authorize(Roles = "Admin,Penalties,PenaltiesTransaction")]
        public ActionResult index()
        {
            var model = dbcontext.Discipline_PunishmentTransaction.ToList();
            //foreach(var item in model)
            //{
            //    var st = dbcontext.status.FirstOrDefault(m => m.ID == item.stat_ID);
            //    item.stat = st;
            //    item.stat.statu = st.statu;
            //}
            return View(model);
        }
        [Authorize(Roles = "Admin,Penalties,PenaltiesTransaction")]
        public ActionResult Create()
        {
            try
            {
                var record = new Discipline_PunishmentTransaction();
                ViewBag.employee = dbcontext.Employee_Profile.Where(m => m.Active == true).ToList().Select(m => new { Code = "" + m.Code + "--[" + m.Full_Name + ']', ID = m.ID }).ToList();
                ViewBag.restoption = dbcontext.Discipline_PunishmentRestOption.ToList().Select(m => new { Code = "" + m.RestOption_Code + "--[" + m.RestOption_Desc + ']', ID = m.ID }).ToList();
                ViewBag.punishment = dbcontext.Discipline_Punishment.ToList().Select(m => new { Code = "" + m.Punishment_Code + "--[" + m.Punishment_Desc + ']', ID = m.ID }).ToList();
                try
                {
                    var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel).Structure_Code;
                    var model = dbcontext.Discipline_PunishmentTransaction.ToList();
                    if (model.Count() == 0)
                    {
                        record.Transaction_Number = stru + "1";
                    }
                    else
                    {
                        record.Transaction_Number = stru + (model.LastOrDefault().ID + 1).ToString();
                    }
                    record.Transaction_Date = DateTime.Now.Date;
                    record.Event_Date = DateTime.Now.Date;
                    record.RestOption_Date = DateTime.Now.Date;
                    record.Custom_Rest = false;

                    return View(record);
                }
                catch (Exception e)
                {
                    return RedirectToAction("index");
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult Create(Discipline_PunishmentTransaction model, FormCollection form)
        {
            try
            {
                var record = new Discipline_PunishmentTransaction();
                ViewBag.employee = dbcontext.Employee_Profile.Where(m => m.Active == true).ToList().Select(m => new { Code = "" + m.Code + "--[" + m.Full_Name + ']', ID = m.ID }).ToList();

                ViewBag.restoption = dbcontext.Discipline_PunishmentRestOption.ToList().Select(m => new { Code = "" + m.RestOption_Code + "--[" + m.RestOption_Desc + ']', ID = m.ID }).ToList();
                ViewBag.punishment = dbcontext.Discipline_Punishment.ToList().Select(m => new { Code = "" + m.Punishment_Code + "--[" + m.Punishment_Desc + ']', ID = m.ID }).ToList();
                //=======================================================================================
                var St_ = new status { created_by = User.Identity.Name, created_bydate = DateTime.Now.Date, statu = check_status.created };
                var status = dbcontext.status.Add(St_);
                dbcontext.SaveChanges();
                //=============================================Header====================================
                var a1 = form["RestOption_Code"].Split(',');
                if (a1.Length == 1)
                {
                    model.RestOption_Code = null;
                    model.Custom_Rest = false;
                }
                else
                {
                    model.Custom_Rest = true;
                    if (a1[1] == "")
                    {
                        return View(model);
                    }
                    else
                    {
                        model.RestOption_Code = a1[1];
                    }
                }

                model.stat_ID = status.ID;
                model.stat = status;
                model.Created_By = User.Identity.Name;
                model.Created_Date = DateTime.Now.Date;
                model.Posted_to_payroll = false;
                if(int.Parse(model.Employee_Code)>0)
                {
                    var ID_emp = int.Parse(model.Employee_Code);
                    var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.Active == true && m.ID ==ID_emp);
                    model.em = emp.Code + "-" + emp.Full_Name;
                }
                var header = dbcontext.Discipline_PunishmentTransaction.Add(model);
                
                dbcontext.SaveChanges();
                //=================================================Details=============================
             
                var punishent_group = form["center_id"].Split(',');
                var Frequency = form["frequency_"].Split(',');
                var penalit = form["Penality_"].Split(',');
                var rest_date = form["rest_D"].Split(',');
                var extra = form["extra"].Split(',');
                var G = form["G"].Split(',');
                for (var i = 0; i < punishent_group.Length; i++)
                {
                    if (punishent_group[i] != "")
                    {
                        var Pun_id = int.Parse(punishent_group[i]);
                        var puni = dbcontext.Discipline_Punishment.FirstOrDefault(m => m.ID == Pun_id);
                        var Pen_id = int.Parse(penalit[i]);
                        var pen = dbcontext.Discipline_PenaltyItem_Header.FirstOrDefault(m => m.ID == Pen_id);
                        var model_details = new Discipline_PunishmentTransaction_Detail {extra_frecuany=int.Parse(extra[i]),guide=Guid.Parse(G[i]), Punishment_Code = puni.ID.ToString(), punis_des = puni.Punishment_Code + '-' + puni.Punishment_Desc, PenaltyItem_Code = pen.ID.ToString(), penal_des = pen.PenaltyItem_Code + '-' + pen.PenaltyItem_Desc, Punishment_Frequency = (short)int.Parse(Frequency[i]), Punishment_RestDate = Convert.ToDateTime(rest_date[i]), Transaction_Number = header.Transaction_Number, Created_By = User.Identity.Name, Created_Date = DateTime.Now.Date };
                        dbcontext.Discipline_PunishmentTransaction_Detail.Add(model_details);
                        dbcontext.SaveChanges();
                    }
                }
                //===================================================
                return RedirectToAction("index");
            }
            catch (Exception e)
            {
                return View(model);
            }
        }

        [Authorize(Roles = "Admin,Penalties,PenaltiesTransaction")]
        public ActionResult edit(int id)
        {
            try
            {
                ViewBag.employee = dbcontext.Employee_Profile.Where(m => m.Active == true).ToList().Select(m => new { Code = "" + m.Code + "--[" + m.Full_Name + ']', ID = m.ID }).ToList();
                ViewBag.restoption = dbcontext.Discipline_PunishmentRestOption.ToList().Select(m => new { Code = "" + m.RestOption_Code + "--[" + m.RestOption_Desc + ']', ID = m.ID }).ToList();
                ViewBag.punishment = dbcontext.Discipline_Punishment.ToList().Select(m => new { Code = "" + m.Punishment_Code + "--[" + m.Punishment_Desc + ']', ID = m.ID }).ToList();

                var puni_tran = dbcontext.Discipline_PunishmentTransaction.FirstOrDefault(m => m.ID == id);
                return View(puni_tran);

            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult edit(Discipline_PunishmentTransaction model, FormCollection form)
        {
            try
            {
                ViewBag.employee = dbcontext.Employee_Profile.Where(m => m.Active == true).ToList().Select(m => new { Code = "" + m.Code + "--[" + m.Full_Name + ']', ID = m.ID }).ToList();
                ViewBag.restoption = dbcontext.Discipline_PunishmentRestOption.ToList().Select(m => new { Code = "" + m.RestOption_Code + "--[" + m.RestOption_Desc + ']', ID = m.ID }).ToList();
                ViewBag.punishment = dbcontext.Discipline_Punishment.ToList().Select(m => new { Code = "" + m.Punishment_Code + "--[" + m.Punishment_Desc + ']', ID = m.ID }).ToList();
                var puni_tran = dbcontext.Discipline_PunishmentTransaction.FirstOrDefault(m => m.ID == model.ID);

                var sta = dbcontext.status.FirstOrDefault(m => m.ID == puni_tran.stat_ID);

                if (sta.statu == check_status.Approved || sta.statu == check_status.Rejected || sta.statu == check_status.Closed || sta.statu == check_status.Recervied || sta.statu == check_status.Canceled)
                {

                    TempData["message"] = HR.Resource.training.status_message;
                    return RedirectToAction("index");
                }

             
                //=================================================================================================
                puni_tran.Transaction_Date = model.Transaction_Date;
                puni_tran.Event_Date = model.Event_Date;
                puni_tran.Employee_Code = model.Employee_Code;
                puni_tran.Transaction_Statement = model.Transaction_Statement;
                if (int.Parse(model.Employee_Code) > 0)
                {
                    var ID_emp = int.Parse(model.Employee_Code);
                    var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.Active == true && m.ID == ID_emp);
                    model.em = emp.Code + "-" + emp.Full_Name;
                }
                else
                {
                    return View(model);
                }
                var a1 = form["RestOption_Code"].Split(',');
                if (a1.Length == 1)
                {
                    model.RestOption_Code = null;
                    model.Custom_Rest = false;

                    puni_tran.RestOption_Code = null;
                    puni_tran.Custom_Rest = false;
                }
                else
                {
                    model.Custom_Rest = true;
                    if (a1[1] == "")
                    {
                        return View(model);
                    }
                    else
                    {
                        model.RestOption_Code = a1[1];
                        puni_tran.RestOption_Code = a1[1];
                    }
                }
                puni_tran.Modified_By = User.Identity.Name;
                puni_tran.Modified_Date = DateTime.Now.Date;
                dbcontext.SaveChanges();
                //====================================================================================================
                var details = dbcontext.Discipline_PunishmentTransaction_Detail.Where(m => m.Transaction_Number == model.Transaction_Number).ToList();
                if(details.Count>0)
                {
                    dbcontext.Discipline_PunishmentTransaction_Detail.RemoveRange(details);
                    dbcontext.SaveChanges();
                }
                //=====================================================================================================
                //=================================================Details=============================================
                
                var punishent_group = form["center_id"].Split(',');
                var Frequency = form["frequency_"].Split(',');
                var penalit = form["Penality_"].Split(',');
                var rest_date = form["rest_D"].Split(',');
                var extra = form["extra"].Split(',');
                var G = form["G"].Split(',');
                for (var i = 0; i < punishent_group.Length; i++)
                {
                    if (punishent_group[i] != "")
                    {
                        var Pun_id = int.Parse(punishent_group[i]);
                        var puni = dbcontext.Discipline_Punishment.FirstOrDefault(m => m.ID == Pun_id);
                        var Pen_id = int.Parse(penalit[i]);
                        var pen = dbcontext.Discipline_PenaltyItem_Header.FirstOrDefault(m => m.ID == Pen_id);
                        var model_details = new Discipline_PunishmentTransaction_Detail { extra_frecuany = int.Parse(extra[i]),guide =Guid.Parse(G[i]), Punishment_Code = puni.ID.ToString(), punis_des = puni.Punishment_Code + '-' + puni.Punishment_Desc, PenaltyItem_Code = pen.ID.ToString(), penal_des = pen.PenaltyItem_Code + '-' + pen.PenaltyItem_Desc, Punishment_Frequency = (short)int.Parse(Frequency[i]), Punishment_RestDate = Convert.ToDateTime(rest_date[i]), Transaction_Number = puni_tran.Transaction_Number, Created_By = User.Identity.Name, Created_Date = DateTime.Now.Date };
                        dbcontext.Discipline_PunishmentTransaction_Detail.Add(model_details);
                        dbcontext.SaveChanges();
                    }
                }
                //==========================================================================================================
                return RedirectToAction("index");
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [Authorize(Roles = "Admin,Penalties,PenaltiesTransaction")]
        public ActionResult delete(int id)
        {
            try
            {
               
                var model_header = dbcontext.Discipline_PunishmentTransaction.FirstOrDefault(m => m.ID == id);
                return View(model_header);
            }
            catch (Exception e)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        [ActionName("delete")]
        public ActionResult method_delete(int id)
        {

            var model_header = dbcontext.Discipline_PunishmentTransaction.FirstOrDefault(m => m.ID == id);
            var model_details = dbcontext.Discipline_PunishmentTransaction_Detail.Where(m => m.Transaction_Number == model_header.Transaction_Number).ToList();
            var status = dbcontext.status.FirstOrDefault(m => m.ID == model_header.stat_ID);
            
            
            try
            {
                dbcontext.Discipline_PunishmentTransaction_Detail.RemoveRange(model_details);
                dbcontext.Discipline_PunishmentTransaction.Remove(model_header);
                dbcontext.status.Remove(status);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = HR.Resource.organ.youcannotdeletethisRow;
                return View(model_header);
            }
            catch (Exception e)
            {
                TempData["Message"] = HR.Resource.organ.youcannotdeletethisRow;
                return View(model_header);
            }
        }
        [Authorize(Roles = "Admin,Penalties,PenaltiesProcess")]
        public ActionResult status(int id)
        {
            ViewBag.header_id = id;
            var hearder = dbcontext.Discipline_PunishmentTransaction.FirstOrDefault(m => m.ID == id);
            var my_model = dbcontext.status.FirstOrDefault(m => m.ID == hearder.stat_ID);

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
        public ActionResult status(status model, int header_id2)
        {
            ViewBag.header_id = header_id2;
            var record = dbcontext.Discipline_PunishmentTransaction.FirstOrDefault(m => m.ID == header_id2);
            if (model.statu == check_status.Approved)
            {

                var sta = dbcontext.status.FirstOrDefault(m => m.ID == record.stat_ID);
                sta.approved_by = User.Identity.Name;
                sta.approved_bydate = model.approved_bydate;
                sta.statu = check_status.Approved;
                dbcontext.SaveChanges();
                record.Transaction_Status = (Int16)check_status.Approved;
                dbcontext.SaveChanges();
            }
            else if (model.statu == check_status.Closed)
            {
                var sta = dbcontext.status.FirstOrDefault(m => m.ID == record.stat_ID);

                sta.closed_by = User.Identity.Name;
                sta.closed_bydate = model.closed_bydate;
                sta.statu = check_status.Closed;
                dbcontext.SaveChanges();
                record.Transaction_Status = (Int16)check_status.Closed;
             
                dbcontext.SaveChanges();


            }
            else if (model.statu == check_status.Canceled)
            {
                var sta = dbcontext.status.FirstOrDefault(m => m.ID == record.stat_ID);

                sta.cancaled_by = User.Identity.Name;
                sta.cancaled_bydate = model.cancaled_bydate;
                sta.statu = check_status.Canceled;
                dbcontext.SaveChanges();

                var ALL_DETAILS = dbcontext.Discipline_PunishmentTransaction_Detail.Where(m => m.Transaction_Number == record.Transaction_Number).ToList();
                foreach(var item in ALL_DETAILS)
                {
                   
                    var all_D = dbcontext.Discipline_PunishmentTransaction_Detail.Where(m => m.guide == item.guide&&m.Punishment_Frequency>item.Punishment_Frequency).OrderBy(m=>m.Punishment_Frequency).ToList();
                    item.Punishment_Frequency = 0;
                    dbcontext.SaveChanges();
                    foreach (var item2 in all_D)
                    {
                        if (item2.extra_frecuany <= 4)
                        {
                            item2.extra_frecuany -= 1;
                            item2.Punishment_Frequency -= 1;
                        }
                        else
                        {
                            item2.extra_frecuany -= 1;

                        }
                        dbcontext.SaveChanges();
                    }
                }
                record.Transaction_Status = (Int16)check_status.Canceled;
                dbcontext.SaveChanges();

            }
            else if (model.statu == check_status.Recervied)
            {
                var sta = dbcontext.status.FirstOrDefault(m => m.ID == record.stat_ID);

                sta.Recervied_by = User.Identity.Name;
                sta.Recervied_bydate = model.Recervied_bydate;
                sta.statu = check_status.Recervied;
                dbcontext.SaveChanges();
                record.Transaction_Status = (Int16)check_status.Recervied;
                dbcontext.SaveChanges();


            }
            else if (model.statu == check_status.Rejected)
            {
                var sta = dbcontext.status.FirstOrDefault(m => m.ID == record.stat_ID);
                sta.Rejected_by = User.Identity.Name;
                sta.Rejected_bydate = model.Rejected_bydate;
                sta.statu = check_status.Rejected;
                dbcontext.SaveChanges();
                var ALL_DETAILS = dbcontext.Discipline_PunishmentTransaction_Detail.Where(m => m.Transaction_Number == record.Transaction_Number).ToList();
                foreach (var item in ALL_DETAILS)
                {

                    var all_D = dbcontext.Discipline_PunishmentTransaction_Detail.Where(m => m.guide == item.guide && m.Punishment_Frequency > item.Punishment_Frequency).OrderBy(m => m.Punishment_Frequency).ToList();
                    item.Punishment_Frequency = 0;
                    dbcontext.SaveChanges();
                    foreach (var item2 in all_D)
                    {
                        if (item2.extra_frecuany <= 4)
                        {
                            item2.extra_frecuany -= 1;
                            item2.Punishment_Frequency -= 1;
                        }
                        else
                        {
                            item2.extra_frecuany -= 1;

                        }
                        dbcontext.SaveChanges();
                    }
                }
                record.Transaction_Status = (Int16)check_status.Rejected;
               dbcontext.SaveChanges();

            }
            else if (model.statu == check_status.Return_To_Review)
            {
                var sta = dbcontext.status.FirstOrDefault(m => m.ID == record.stat_ID);
                sta.return_to_reviewby = User.Identity.Name;
                sta.return_to_reviewdate = model.approved_bydate;
                sta.statu = check_status.Return_To_Review;
                dbcontext.SaveChanges();
                record.Transaction_Status = (Int16)check_status.Return_To_Review;
                dbcontext.SaveChanges();

            }

            return RedirectToAction("index");
        }
        //==========================ajax==============================
        public JsonResult get_freq(string employee_id,string puni_id,string date,int rest__)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            var old_transaction_header = dbcontext.Discipline_PunishmentTransaction.Where(m => m.Employee_Code == employee_id).ToList();
            var D = new List<Discipline_PunishmentTransaction_Detail>();
            var gg=new Guid();
            foreach(var item in old_transaction_header)
            {
               
                var old_details = dbcontext.Discipline_PunishmentTransaction_Detail.Where(m =>m.Punishment_Frequency!=0&& m.Transaction_Number == item.Transaction_Number&&m.Punishment_Code==puni_id&&m.Punishment_RestDate>=DateTime.Now).ToList();
                if(old_details.Count>0)
                {
                    gg = old_details[0].guide;
                }
                D.AddRange(old_details);
            }
            var B_id = int.Parse(puni_id);
            var Punishment_group = dbcontext.Discipline_Punishment.FirstOrDefault(m => m.ID == B_id);
            var ITEMS = dbcontext.Discipline_Punishment_Detail.Where(m => m.Punishment_Code == Punishment_group.ID.ToString()).ToList();

            ///=====================================rest=============================================
            var Date = Convert.ToDateTime(date);
            if (rest__==0)
            {

                var res_id = int.Parse(Punishment_group.RestOption_Code);
                var rest = dbcontext.Discipline_PunishmentRestOption.FirstOrDefault(m => m.ID == res_id);
                Date = Date.AddDays((double)rest.RestOption_Days);


            }
            else
            {
                var rest = dbcontext.Discipline_PunishmentRestOption.FirstOrDefault(m => m.ID == rest__);
                Date = Date.AddDays((double)rest.RestOption_Days);

            }

            var my_model = new VM_item { Date = Date };
            //=========================================================================================
            //=======================================items=============================================
            if (D.Count==0)
            {
                var  item=ITEMS.Where(m => m.PenaltyItem_Level == 1).FirstOrDefault();
                my_model.item = item;
                my_model.guide = Guid.NewGuid();

                return Json(my_model);
            }
            else if(D.Count==1)
            {
                var item = ITEMS.Where(m => m.PenaltyItem_Level == 2).FirstOrDefault();
                my_model.item = item;
                my_model.guide = gg;
                my_model.Date = (DateTime)D[0].Punishment_RestDate;
                return Json(my_model);
            }
            else if (D.Count == 2)
            {
                var item = ITEMS.Where(m => m.PenaltyItem_Level == 3).FirstOrDefault();
                my_model.item = item;

                my_model.guide = gg;
                my_model.Date = (DateTime)D[0].Punishment_RestDate;
                return Json(my_model);
            }
            else if (D.Count == 3)
            {
                var item = ITEMS.Where(m => m.PenaltyItem_Level == 4).FirstOrDefault();
                my_model.item = item;

                my_model.guide = gg;
                my_model.Date = (DateTime)D[0].Punishment_RestDate;
                return Json(my_model);
            }
            else
            {
                var item = ITEMS.Where(m => m.PenaltyItem_Level == 4).FirstOrDefault();
                my_model.item = item;

                my_model.guide = gg;
                my_model.extra_frecuany = D.Count + 1;

                my_model.Date = (DateTime)D[0].Punishment_RestDate;
                return Json(my_model);
            }
            return Json(null);
            
        }
        public JsonResult get_all_content(string  trans_number)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            var puni = dbcontext.Discipline_PunishmentTransaction_Detail.Where(m => m.Transaction_Number == trans_number).ToList();
            return Json(puni);
        }


        public ActionResult post_to_poyroll(int? id)
        {
            try
            {
                if (id != null)
                {
                    var punishment = dbcontext.Discipline_PunishmentTransaction.FirstOrDefault(m => m.ID == id);
                    var stats = dbcontext.status.FirstOrDefault(m => m.ID == punishment.stat_ID);
                    if (stats.statu == check_status.Approved &&punishment.Posted_to_payroll==false)
                    {
                        var details = dbcontext.Discipline_PunishmentTransaction_Detail.Where(m => m.Transaction_Number == punishment.Transaction_Number).ToList();
                        foreach (var item in details)
                        {
                            var ID_pen = int.Parse(item.PenaltyItem_Code);
                            var header_penality = dbcontext.Discipline_PenaltyItem_Header.FirstOrDefault(m => m.ID == ID_pen);
                            if (header_penality.LinkToPayroll == true)
                            {
                                //var salary_items = dbcontext.Discipline_PenaltyItem_Detail.Where(m => m.PenaltyItem_Code == header_penality.ID.ToString()).ToList();
                                var Header_group_id = int.Parse(header_penality.CodeGroupID);
                                var Salcode_header = dbcontext.SalaryCodeGroup_Header.FirstOrDefault(a => a.ID == Header_group_id);
                                var Salcode = dbcontext.SalaryCodeGroup_Detail.Where(a => a.CodeGroupID == Salcode_header.CodeGroupID).ToList();

                                foreach (var item2 in Salcode)
                                {
                                    ///////salary item
                                    

                                    var new_Record = new Employee_Payroll_Transactions();
                                    var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Payroll).Structure_Code;
                                    var model_ = dbcontext.Employee_Payroll_Transactions.ToList();
                                    if (model_.Count() == 0)
                                    {
                                        new_Record.TransactionNumber = stru + "1";
                                    }
                                    else
                                    {
                                        new_Record.TransactionNumber = stru + (model_.LastOrDefault().ID + 1).ToString();
                                    }
                                    //============================================================================================
                                    var emp_id = int.Parse(punishment.Employee_Code);
                                    var emp = dbcontext.Employee_Profile.FirstOrDefault(a => a.ID == emp_id);
                                    new_Record.Employee_Code = emp.Code;
                                   
                                    var salary_item = dbcontext.salary_code.FirstOrDefault(a => a.SalaryCodeID == item2.SalaryCodeID);
                                    
                                    new_Record.SalaryCodeID = item2.SalaryCodeID;
                                    //==============================================================================================
                                    new_Record.TransactionDate = punishment.Transaction_Date;
                                    string Transaction = punishment.Transaction_Date.ToString();
                                    new_Record.TransactionMonth = DateTime.Parse(Transaction).Month;
                                    new_Record.TransactionYear = DateTime.Parse(Transaction).Year;
                                    new_Record.EffectiveDate = punishment.Event_Date;
                                    string Effective = punishment.Event_Date.ToString();
                                    new_Record.EffectiveMonth = DateTime.Parse(Effective).Month;
                                    new_Record.EffectiveYear = DateTime.Parse(Effective).Year;
                                    new_Record.TransactionValue = (double?)item2.DefaultValue;


                                   /////    new_Record.JournalName_BatchCode = model.Employee_Payroll_Transactions.JournalName_BatchCode;
                                     new_Record.CreatedDate = DateTime.Now.Date;
                                    new_Record.CreatedBy = User.Identity.Name;

                                    new_Record.SourceDocumentType = Payment_Type_Source_Document.penalites.GetHashCode();
                                    new_Record.SourceDocumentRefrence = punishment.Transaction_Number;
                                    var details_punishment = dbcontext.Discipline_PunishmentTransaction_Detail.FirstOrDefault(m => m.Transaction_Number == punishment.Transaction_Number);
                                    new_Record.SourceDocumentDescription = details_punishment.penal_des;
                                     new_Record.SourceDocumentNotes = details_punishment.penal_des +'/'+ punishment.Transaction_Number;
                                    /////new_Record.TransactionNotes = model.Employee_Payroll_Transactions.TransactionNotes;
                                    // new_Record.CostCenterCode = emp.CostCenterCode;
                                    new_Record.CostCenterCode = "88";
                                      if((bool)salary_item.EnableExtendedFields)
                                    {
                                        new_Record.ExtendedFields_Code = salary_item.ExtendedFields_Code;
                                    }
                                    else
                                    {

                                        new_Record.ExtendedFields_Code = null;
                                    }
                                    //    new_Record.Contract_Number = model.Employee_Payroll_Transactions.Contract_Number;

                                    new_Record.TransactionStatus = check_status.created.GetHashCode();
                                    new_Record.check_status = HR.Models.Infra.check_status.created;
                                    new_Record.name_state = nameof(check_status.created);
                                    var username = User.Identity.Name;
                                    var Date = Convert.ToDateTime("1/1/1900");
                                    var s = new status { statu = HR.Models.Infra.check_status.created, created_by = username, Type = Models.Infra.Type.Employee_Payroll_Transactions, approved_bydate = Date, cancaled_bydate = Date, created_bydate = DateTime.Now.Date, Rejected_bydate = Date, return_to_reviewdate = Date };
                                    var st = dbcontext.status.Add(s);
                                    dbcontext.SaveChanges();
                                    new_Record.statID = s.ID;

                                    var Header = dbcontext.Employee_Payroll_Transactions.Add(new_Record);
                                    dbcontext.SaveChanges();
                                    //=============================================================================================
                                    
                                }
                                
                            }

                        }
                        punishment.PostedBy = User.Identity.Name;
                        punishment.PostedDate = DateTime.Now;
                        punishment.Posted_to_payroll = true;
                        dbcontext.SaveChanges();
                        TempData["Message"] = "post to payroll successfully";
                        return RedirectToAction("index");

                    }
                    else
                    {
                        TempData["Message"] = "post to payroll faild";
                        return RedirectToAction("index");
                    }

                }


                else
                {
                    var punishment = dbcontext.Discipline_PunishmentTransaction.Where(m => m.Posted_to_payroll == false && m.stat.statu == check_status.Approved).ToList();
                    foreach (var item1 in punishment)
                    {
                        var stats = dbcontext.status.FirstOrDefault(m => m.ID == item1.stat_ID);
                        if (stats.statu == check_status.Approved && item1.Posted_to_payroll == false)
                        {
                            var details = dbcontext.Discipline_PunishmentTransaction_Detail.Where(m => m.Transaction_Number == item1.Transaction_Number).ToList();
                            foreach (var item in details)
                            {
                                var ID_pen = int.Parse(item.PenaltyItem_Code);
                                var header_penality = dbcontext.Discipline_PenaltyItem_Header.FirstOrDefault(m => m.ID == ID_pen);
                                if (header_penality.LinkToPayroll == true)
                                {
                                    var Header_group_id = int.Parse(header_penality.CodeGroupID);
                                    var Salcode_header = dbcontext.SalaryCodeGroup_Header.FirstOrDefault(a => a.ID == Header_group_id);
                                    var Salcode = dbcontext.SalaryCodeGroup_Detail.Where(a => a.CodeGroupID == Salcode_header.CodeGroupID).ToList();

                                    foreach (var item2 in Salcode)
                                    {
                                        ///////salary item


                                        var new_Record = new Employee_Payroll_Transactions();
                                        var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Payroll).Structure_Code;
                                        var model_ = dbcontext.Employee_Payroll_Transactions.ToList();
                                        if (model_.Count() == 0)
                                        {
                                            new_Record.TransactionNumber = stru + "1";
                                        }
                                        else
                                        {
                                            new_Record.TransactionNumber = stru + (model_.LastOrDefault().ID + 1).ToString();
                                        }
                                        //============================================================================================
                                        var emp_id = int.Parse(item1.Employee_Code);
                                        var emp = dbcontext.Employee_Profile.FirstOrDefault(a => a.ID == emp_id);
                                        new_Record.Employee_Code = emp.Code;


                                        var salary_item = dbcontext.salary_code.FirstOrDefault(a => a.SalaryCodeID == item2.SalaryCodeID);
                                        new_Record.SalaryCodeID = item2.SalaryCodeID;
                                        //==============================================================================================
                                        new_Record.TransactionDate = item1.Transaction_Date;
                                        string Transaction = item1.Transaction_Date.ToString();
                                        new_Record.TransactionMonth = DateTime.Parse(Transaction).Month;
                                        new_Record.TransactionYear = DateTime.Parse(Transaction).Year;
                                        new_Record.EffectiveDate = item1.Event_Date;
                                        string Effective = item1.Event_Date.ToString();
                                        new_Record.EffectiveMonth = DateTime.Parse(Effective).Month;
                                        new_Record.EffectiveYear = DateTime.Parse(Effective).Year;
                                        new_Record.TransactionValue = (double?)item2.DefaultValue;


                                        /////    new_Record.JournalName_BatchCode = model.Employee_Payroll_Transactions.JournalName_BatchCode;
                                        new_Record.CreatedDate = DateTime.Now.Date;
                                        new_Record.CreatedBy = User.Identity.Name;

                                        new_Record.SourceDocumentType = Payment_Type_Source_Document.penalites.GetHashCode();
                                        new_Record.SourceDocumentRefrence = item1.Transaction_Number;
                                        var details_punishment = dbcontext.Discipline_PunishmentTransaction_Detail.FirstOrDefault(m => m.Transaction_Number == item1.Transaction_Number);
                                        new_Record.SourceDocumentDescription = details_punishment.penal_des;
                                        new_Record.SourceDocumentNotes = details_punishment.penal_des + '/' + item1.Transaction_Number;
                                        /////new_Record.TransactionNotes = model.Employee_Payroll_Transactions.TransactionNotes;
                                        // new_Record.CostCenterCode = emp.CostCenterCode;
                                        new_Record.CostCenterCode = "88";
                                        if ((bool)salary_item.EnableExtendedFields)
                                        {
                                            new_Record.ExtendedFields_Code = salary_item.ExtendedFields_Code;
                                        }
                                        else
                                        {

                                            new_Record.ExtendedFields_Code = null;
                                        }
                                        //    new_Record.Contract_Number = model.Employee_Payroll_Transactions.Contract_Number;

                                        new_Record.TransactionStatus = check_status.created.GetHashCode();
                                        new_Record.check_status = HR.Models.Infra.check_status.created;
                                        new_Record.name_state = nameof(check_status.created);
                                        var username = User.Identity.Name;
                                        var Date = Convert.ToDateTime("1/1/1900");
                                        var s = new status { statu = HR.Models.Infra.check_status.created, created_by = username, Type = Models.Infra.Type.Employee_Payroll_Transactions, approved_bydate = Date, cancaled_bydate = Date, created_bydate = DateTime.Now.Date, Rejected_bydate = Date, return_to_reviewdate = Date };
                                        var st = dbcontext.status.Add(s);
                                        dbcontext.SaveChanges();
                                        new_Record.statID = s.ID;

                                        var Header = dbcontext.Employee_Payroll_Transactions.Add(new_Record);
                                        dbcontext.SaveChanges();
                                        //=============================================================================================

                                    }
                                    item1.PostedBy = User.Identity.Name;
                                    item1.PostedDate = DateTime.Now;
                                    item1.Posted_to_payroll = true;
                                    dbcontext.SaveChanges();
                                  
                                }
                            }
                        }
                    }
                    TempData["Message"] = "post to payroll successfully";
                    return RedirectToAction("index");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
    }
    public class VM_item
    {
        public DateTime Date { get; set; }
        public Discipline_Punishment_Detail item { get; set; }
        public Guid guide { get; set; }

        public int extra_frecuany { get; set; }
    }
}