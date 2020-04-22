using HR.Models;
using HR.Models.Infra;
using HR.Models.Time_management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static HR.Models.Time_management.Enum_ex;

namespace HR.Controllers
{
    public class TM_casesController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: TM_cases
        public ActionResult Index()
        {
            //////////////////////////////
            var cases = dbcontext.CasesTM.Where(m => m.Time_management_conditional_setupID == null);
            dbcontext.CasesTM.RemoveRange(cases);
            dbcontext.SaveChanges();
            //////////////////////////////
            var model = dbcontext.Time_management_conditional_setup.ToList();
            return View(model);
        }
        public ActionResult create()
        {
            try
            {
                ViewBag.nu = 1;
                //////////////////////////////
                var cases = dbcontext.CasesTM.Where(m => m.Time_management_conditional_setupID == null);
                dbcontext.CasesTM.RemoveRange(cases);
                dbcontext.SaveChanges();
                //////////////////////////////

                var record_TM = new HR.Models.Time_management.Time_management_conditional_setup();
                var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel).Structure_Code;
                var modelll = dbcontext.Time_management_conditional_setup.ToList();
                var Code = "";
                if (modelll.Count() == 0)
                {
                    Code = stru + "1";
                }
                else
                {
                    Code = stru + (modelll.LastOrDefault().ID + 1).ToString();
                }
                record_TM.Code = Code;
           //     var model = new CasesTM();model.number = 0;
                ViewBag.ex = new List<E>();
               
                return View(record_TM);
                
            }
            catch(Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult create(Time_management_conditional_setup model,FormCollection form)
        {
            try
            {
                ///////////////////////////
                var new_tm= dbcontext.Time_management_conditional_setup.Add(model);
                dbcontext.SaveChanges();
                var cases = dbcontext.CasesTM.Where(m => m.Time_management_conditional_setupID == null).ToList();
                foreach(var item in cases)
                {
                    item.Time_management_conditional_setupID = new_tm.ID;
                }
                dbcontext.SaveChanges();    
                ///////////////////////////
                return  RedirectToAction("index");
            }
            catch(Exception)
            {
                return View(model);
            }

            //var ex = form["exxxxxxx"].Split(' ');
            //var em = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == 1);
            //for (var i=0;i<ex.Length;i++)
            //{
            //    if (ex[i] != "")
            //    {
            //        var Case = (E)Enum.Parse(typeof(HR.Models.Time_management.Enum_ex.E), ex[i]);
            //        var val = Case.GetHashCode();
            //        if(val==0)
            //        {
            //            var operation = (E)Enum.Parse(typeof(HR.Models.Time_management.Enum_ex.E), ex[i+1]);
            //            if(operation==E.greater_than)
            //            {

            //            }
            //           else if (operation == E.younger_than)
            //            {

            //            }
            //           else if (operation == E.equal)
            //            {

            //            }
            //            else if (operation == E.not_equal)
            //            {

            //            }
            //        }
            //        else if (val == 1)
            //        {
            //            var operation = (E)Enum.Parse(typeof(HR.Models.Time_management.Enum_ex.E), ex[i + 1]);
            //            if (operation == E.greater_than)
            //            {

            //            }
            //            else if (operation == E.younger_than)
            //            {

            //            }
            //            else if (operation == E.equal)
            //            {

            //            }
            //            else if (operation == E.not_equal)
            //            {

            //            }
            //        }


            //    }
          
        }


        public ActionResult Edit(int id)
        {
            try
            {
                //////////////////////////////
                var cases = dbcontext.CasesTM.Where(m => m.Time_management_conditional_setupID == null);
                dbcontext.CasesTM.RemoveRange(cases);
                dbcontext.SaveChanges();
                //////////////////////////////
                var record = dbcontext.Time_management_conditional_setup.FirstOrDefault(m => m.ID == id);
                var number=1;
                var Case = dbcontext.CasesTM.Where(m => m.Time_management_conditional_setupID == record.ID).ToList();
                if(Case.Count>0)
                {
                    number = (Case.OrderBy(m => m.number).LastOrDefault().number)+1;
                }
                ViewBag.num = number;
                return View(record);
            }
            catch(Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult Edit(Time_management_conditional_setup model)
        {
            var record = dbcontext.Time_management_conditional_setup.FirstOrDefault(m => m.ID == model.ID);
            try
            {
                var number = 1;
                var Case = dbcontext.CasesTM.Where(m => m.Time_management_conditional_setupID == record.ID).ToList();
                if (Case.Count > 0)
                {
                    number = (Case.OrderBy(m => m.number).LastOrDefault().number) + 1;
                }
                ViewBag.num = number;
                //////////
                /////////
                record.Name = model.Name;
                record.Description = model.Description;
                dbcontext.SaveChanges();
                ///////
                ///////
                var cases = dbcontext.CasesTM.Where(m => m.Time_management_conditional_setupID == null).ToList();
                foreach (var item in cases)
                {
                    item.Time_management_conditional_setupID = record.ID;
                }
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (Exception)
            {
                return View(record);
            }
        }

        public ActionResult delete(int id)
        {
            try
            {
                var model = dbcontext.Time_management_conditional_setup.FirstOrDefault(m => m.ID == id);
                return View(model);

            }
            catch(Exception)
            {
                return RedirectToAction("index");

            }

        }
        [HttpPost]
        [ActionName("delete")]
        public ActionResult delete_method(int id)
        {
            var model = dbcontext.Time_management_conditional_setup.FirstOrDefault(m => m.ID == id);
            var cases = dbcontext.CasesTM.Where(m => m.Time_management_conditional_setupID == model.ID).ToList();
            try
            {
                dbcontext.CasesTM.RemoveRange(cases);
                dbcontext.SaveChanges();
                dbcontext.Time_management_conditional_setup.Remove(model);
                 dbcontext.SaveChanges();
                 return RedirectToAction("index");

            }
            catch (Exception)
            {
                return View(model);

            }

        }

        public JsonResult Add_New_Case(string Case,string num,string desc,string Return,int type)
        {
          
                dbcontext.Configuration.ProxyCreationEnabled = false;
            try
            {
                var N = int.Parse(num);
                if (type != 0)///edit
                {
                    var my_case = dbcontext.CasesTM.FirstOrDefault(m => m.number == N && m.Time_management_conditional_setupID == type);
                    if (my_case != null) ///edit old case 
                    {
                        my_case.Description = desc;
                        my_case.EXeprission = Case;
                        my_case.Return_ex = Return;
                        dbcontext.SaveChanges();
                        ////////
                        var Num = 0;
                        var all_cases = dbcontext.CasesTM.Where(m => m.Time_management_conditional_setupID == null || m.Time_management_conditional_setupID == type).OrderBy(m => m.number).ToList();
                        if (all_cases.Count()>0)
                        {
                            Num = all_cases.LastOrDefault().number + 1;
                        }
                        var new_case = new CasesTM { number = Num };
                        var model = new CaseVM { flag = 2, Case = new_case };
                        return Json(model);
                    }
                    else ///edit new case
                    {
                        var my_case2 = dbcontext.CasesTM.FirstOrDefault(m => m.number == N && m.Time_management_conditional_setupID == null);
                        if (my_case2 != null)
                        {


                            my_case2.Description = desc;
                            my_case2.EXeprission = Case;
                            my_case2.Return_ex = Return;
                            dbcontext.SaveChanges();
                            var Num = 0;
                            var all_cases = dbcontext.CasesTM.Where(m => m.Time_management_conditional_setupID == null || m.Time_management_conditional_setupID == type).OrderBy(m => m.number).ToList();
                            if (all_cases.Count() > 0)
                            {
                                Num = all_cases.LastOrDefault().number + 1;
                            }
                            var new_case = new CasesTM { number = Num };
                            var model = new CaseVM { flag = 2, Case = new_case };
                            return Json(model);
                        }
                        else ///add new case in edit
                        {
                            var new_case = new CasesTM() { EXeprission = Case, number = int.Parse(num), Description = desc, Return_ex = Return };
                            var new_Case = dbcontext.CasesTM.Add(new_case);
                            dbcontext.SaveChanges();
                            var model = new CaseVM { flag = 1, Case = new_Case };
                            return Json(model);
                        }
                    }
                }
                else ///create
                {
                    var edit_case = dbcontext.CasesTM.FirstOrDefault(m => m.number == N && m.Time_management_conditional_setupID == null);
                    if (edit_case != null) ///edit case in create
                    {
                        edit_case.Description = desc;              
                        edit_case.EXeprission = Case;
                        edit_case.Return_ex = Return;
                        dbcontext.SaveChanges();
                        /////
                        var Num = 0;
                        var all_cases = dbcontext.CasesTM.Where(m => m.Time_management_conditional_setupID == null).OrderBy(m => m.number).ToList();
                        if (all_cases.Count() > 0)
                        {
                            Num = all_cases.LastOrDefault().number + 1;
                        }
                        var new_case = new CasesTM { number = Num };
                        var model = new CaseVM { flag = 2, Case = new_case };
                        return Json(model);

                    }
                    else/////add new case in create
                    {
                        var new_case = new CasesTM() { EXeprission = Case, number = int.Parse(num), Description = desc, Return_ex = Return };
                        var new_Case = dbcontext.CasesTM.Add(new_case);
                        dbcontext.SaveChanges();
                        var model = new CaseVM { flag = 1, Case = new_Case };
                        return Json(model);
                    }
                }
            }

            catch (Exception)
            {
                var model = new CaseVM { flag = 0 };
                return Json(false);
            }
            
        }
        public JsonResult remove(int id)
        {
            try
            {
                var Case = dbcontext.CasesTM.FirstOrDefault(m => m.ID == id);
                dbcontext.CasesTM.Remove(Case);
                dbcontext.SaveChanges();
                return Json(true);
            }
            catch(Exception)
            {
                return Json(false);
            }
        }
        public JsonResult moveup(string id,string prev,int type)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            var ID1 = int.Parse(id);
            var current = dbcontext.CasesTM.FirstOrDefault(m => m.ID == ID1);
            var ID2 = int.Parse(prev);
            var PREV = dbcontext.CasesTM.FirstOrDefault(m => m.ID == ID2);
            var current_num = current.number;
            current.number = PREV.number;
            PREV.number = current_num;
            dbcontext.SaveChanges();
            if (type == 0)
            {
                var all_cases = dbcontext.CasesTM.Where(m => m.Time_management_conditional_setupID == null).ToList();
                if(all_cases.Count>0)
                {
                    all_cases = all_cases.OrderBy(m => m.number).ToList();
                    return Json(all_cases);
                }
                return Json(all_cases);
            }
            else if(type != 0)
            {
                var all_cases = dbcontext.CasesTM.Where(m => m.Time_management_conditional_setupID == null || m.Time_management_conditional_setupID == type).ToList();
                if (all_cases.Count > 0)
                {
                    all_cases= all_cases.OrderBy(m => m.number).ToList();
                    return Json(all_cases);
                }
                return Json(all_cases);

            }
            else
            {
                return Json(false);
            }

        }
        public JsonResult movedown(string id, string next,int type)
        {

            dbcontext.Configuration.ProxyCreationEnabled = false;
            var ID1 = int.Parse(id);
            var current = dbcontext.CasesTM.FirstOrDefault(m => m.ID == ID1);
            var ID2 = int.Parse(next);
            var NEXT = dbcontext.CasesTM.FirstOrDefault(m => m.ID == ID2);
            var current_num = current.number;
            current.number = NEXT.number;
            NEXT.number = current_num;
            dbcontext.SaveChanges();
            if (type == 0)
            {
                var all_cases = dbcontext.CasesTM.Where(m => m.Time_management_conditional_setupID == null).ToList();
                if (all_cases.Count > 0)
                {
                    all_cases = all_cases.OrderBy(m => m.number).ToList();
                    return Json(all_cases);
                }
                return Json(all_cases);
            }
            else if (type != 0)
            {
                var all_cases = dbcontext.CasesTM.Where(m => m.Time_management_conditional_setupID == null || m.Time_management_conditional_setupID == type).ToList();
                if (all_cases.Count > 0)
                {
                    all_cases = all_cases.OrderBy(m => m.number).ToList();
                    return Json(all_cases);
                }
                return Json(all_cases);

            }
            else
            {
                return Json(false);
            }
        }
        public JsonResult check_head(string MY_Case,string Return)
        {
            try
            {
                String[] and_or = { "AND", "OR" };
                String[] AND = { "," };
                var sub_cases = MY_Case.Split(and_or, StringSplitOptions.RemoveEmptyEntries);
                var Retur = Return.Split(AND, StringSplitOptions.RemoveEmptyEntries);
                bool flag1 = true;
                if (sub_cases.Length == 1)
                {
                    var flag = first_check(MY_Case);
                    if (flag == false)
                    {
                        //// check_exprission_Faild So we not make check on return exprission
                        return Json(false);
                    }
                    else
                    {

                        //// check_exprission_success So we will make check on return exprission
                        ////now check_return
                        if (Retur.Length == 1)
                        {
                            var re_falg = Return_check(Retur[0]);
                            if (re_falg) { return Json(true); }
                            else { return Json(false); }
                        }
                        else if (Retur.Length > 1)
                        {
                            foreach (var item in Retur)
                            {
                                var re_falg = Return_check(item);
                                if (!re_falg) { return Json(false); }
                            }
                            return Json(true);
                        }
                        else
                        {
                            return Json(false);
                        }
                    }
                }
                
                else
                {
                    foreach (var item in sub_cases)
                    {
                        var flag2 = first_check(item);
                        if (flag2 == false)
                        {
                            flag1 = false;
                            ////check_exprission_Faild So we not make check on return exprission
                            return Json(false);
                        }
                    }
                    if (flag1)
                    {
                        //// check_exprission_success So we will make check on return exprission
                        ////now check_return
                        if (Retur.Length == 1)
                        {
                            var re_falg = Return_check(Retur[0]);
                            if (re_falg) { return Json(true); }
                            else { return Json(false); }
                        }
                        else if (Retur.Length > 1)
                        {

                            foreach (var item in Retur)
                            {
                                var re_falg = Return_check(item);
                                if (!re_falg) { return Json(false); }
                            }
                            return Json(true);
                        }
                        else
                        {
                            return Json(false);
                        }
                    }
                    else { return Json(false); }
                }
            }
            catch(Exception)
            {
                    ////check_error
                    return Json(false);
                
            }
        }
        public bool Return_check(string return_case)
        {
            try
            {
                char[] arr = { ' ' };
                var A = return_case.Split(arr, StringSplitOptions.RemoveEmptyEntries);
                var flag_test = true;
                foreach (var item in A)
                {
                    var flag = last_check(item);
                    if (!flag)
                    {
                        flag_test = false;
                        return false;
                    }
                    
                }
                if (flag_test)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                //if (A[0][0] == '%' && A[1] == "/Equal")
                //{
                //    var new_string = "";
                //    for (var i = 2; i < A.Length ; i++)
                //    {
                //        new_string += A[i] + " ";
                //    }
                //    var flag = last_check(new_string);
                //    if (flag) { return true; }
                //    else { return false; }
                //}


            }
            catch(Exception)
            {
                return false;
            }
        }
        public bool first_check(string sub_case)
        {
            try
            {
                char[] arr = { ' ' };
                var A = sub_case.Split(arr, StringSplitOptions.RemoveEmptyEntries);
                if (A[0][0] == '%' && A[1][0] == '/' && A[2] == "(" && A.Last() == ")")
                {
                    var new_string = "";
                    for (var i = 3; i <= A.Length - 2; i++)
                    {
                        new_string += A[i] + " ";
                    }
                    var flag = last_check(new_string);
                    if (flag) { return true; }
                    else { return false; }
                }
                else { return false; }
            }
            catch(Exception)
            {
                return false;
            }
            
        }
        public bool last_check(string sub_case)
        {
            try
            {
                char[] arr = { ' ' };
                var A = sub_case.Split(arr, StringSplitOptions.RemoveEmptyEntries);
                if(A.Length==1)
                {
                    if (sub_case[0] == '%') //////اسم بس 
                    {
                        return true;
                    }
                    else if (sub_case[0] == 'ْ'||sub_case[0]== '<' || sub_case[0] == '>' || sub_case[0] == '*' ) ///////رقم
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
               else if (A[0][0] == '%' && A[1][0] == '/' && A[2] == "(" && A.Last() == ")")  ///جمله جديده
                {
                    var flag = first_check(sub_case);
                    if (flag) { return true; }
                    else { return false; }
                }
                else   //////////////////////////////////////////////////////////////////////////////عمليات حسابيه مع اسم
                {
                    bool f = true;
                    for (var i = 0; i < A.Length ; i += 2)
                    {
                        if (A[i][0] == '%' )  ///location plus number
                        {
                            if ((i + 1) < A.Length)
                            {
                                if (A[i + 1][0] == '$')
                                {
                                    f = true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return true;
                            }
                        }
                        else if (A[i][0] == 'ْ' || A[i][0] == '<' || sub_case[0] == '>' || sub_case[0] == '*') ///number plus location 
                        {
                            if ((i + 1) < A.Length)
                            {
                                if (A[i + 1][0] == '$')
                                {
                                    f = true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return true;
                            }
                        }
                        else
                        {
                            return false;
                        }

                    }
                    return f;
                }
            }
            catch (Exception)
            {
                
                    return false;
                
            }

        }
        public JsonResult retun_case(int id)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            var Case = dbcontext.CasesTM.FirstOrDefault(m => m.ID == id);
            return Json(Case);
        }
        public JsonResult getL2(int id)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            try
            {
                var list = new List<type_look>();
                if (id==-4)
                {
                    list.Add(new type_look { ID=1,name=HR.Resource.Basic.Country});
                    list.Add(new type_look { ID = 2, name = HR.Resource.Basic.Region });
                    list.Add(new type_look { ID = 3, name = HR.Resource.Basic.State });
                    list.Add(new type_look { ID = 4, name = HR.Resource.Basic.County });
                    list.Add(new type_look { ID = 5, name = HR.Resource.Basic.City });
                    list.Add(new type_look { ID = 6, name = HR.Resource.Basic.Postal_code });
                    list.Add(new type_look { ID = 7, name = HR.Resource.Basic.Nationality });
                    list.Add(new type_look { ID = 8, name = HR.Resource.Basic.religion });
                    list.Add(new type_look { ID = 9, name = HR.Resource.Basic.educationcategory });
                    list.Add(new type_look { ID = 10, name = HR.Resource.Basic.educationlevel });
                    list.Add(new type_look { ID = 11, name = HR.Resource.Basic.MainEducatebody });
                    list.Add(new type_look { ID = 12, name = HR.Resource.Basic.qulificationsubprovider });
                    list.Add(new type_look { ID = 13, name = HR.Resource.Basic.Name_of_educational_qualification });
                    list.Add(new type_look { ID = -99, name = HR.Resource.Basic.qulification_specialty });
                    list.Add(new type_look { ID = -98, name = HR.Resource.Basic.degree });
                    list.Add(new type_look { ID = 14, name =HR.Resource.Basic.milirtyservicestatus });
                    list.Add(new type_look { ID = 15, name = HR.Resource.Basic.militaryservicerank });
                    list.Add(new type_look { ID = 16, name = HR.Resource.Basic.document_group });
                    list.Add(new type_look { ID = 17, name = HR.Resource.Basic.documents });
                    list.Add(new type_look { ID = 18, name = HR.Resource.Basic.contactmethod });
                    list.Add(new type_look { ID = 19, name = HR.Resource.Basic.rejectionresons });
                    list.Add(new type_look { ID = 20, name = HR.Resource.Basic.externalcompnay });
                    list.Add(new type_look { ID = 21, name = HR.Resource.Basic.check_type });
                    list.Add(new type_look { ID = 22, name = HR.Resource.Basic.airports });
                    list.Add(new type_look { ID = 23, name = HR.Resource.Basic.sponsers });
                    list.Add(new type_look { ID = 24, name = HR.Resource.Basic.currency });
                    return Json(list);
                }
                else if(id==-5)
                {
                    list.Add(new type_look { ID = 25, name = HR.Resource.organ.AuthorityType });
                    list.Add(new type_look { ID = 26, name = HR.Resource.organ.Authority });
                    list.Add(new type_look { ID = 27, name = HR.Resource.organ.Skillgroup });
                    list.Add(new type_look { ID = 28, name = HR.Resource.organ.Skill });
                    list.Add(new type_look { ID = 29, name = HR.Resource.organ.risks });
                    list.Add(new type_look { ID = 30, name = HR.Resource.organ.RisksType });
                    list.Add(new type_look { ID = 31, name = HR.Resource.organ.Job_level_class__ID });
                    list.Add(new type_look { ID = 32, name = HR.Resource.organ.Job_level_gradeI__D });
                    list.Add(new type_look { ID = 33, name = HR.Resource.Basic.jobtitle });
                    list.Add(new type_look { ID = 34, name = HR.Resource.organ.jobtitleSubclass });
                    list.Add(new type_look { ID = 35, name = HR.Resource.organ.BudgetclassId });
                    list.Add(new type_look { ID = 36, name = HR.Resource.organ.Budgetclassitems });
                    list.Add(new type_look { ID = 37, name = HR.Resource.organ.OrganizationUnitSchema });
                    list.Add(new type_look { ID = 38, name = HR.Resource.organ.OrganizationUnitLevel });
                    list.Add(new type_look { ID = 39, name = HR.Resource.organ.Organizationunit });
                    list.Add(new type_look { ID = 40, name = HR.Resource.Basic.requirments });
                    list.Add(new type_look { ID = 41, name = HR.Resource.organ.Experiencegroup });
                    list.Add(new type_look { ID = 42, name = HR.Resource.organ.responsibllities });
                    list.Add(new type_look { ID = 43, name = HR.Resource.organ.worklocationid });
                    return Json(list);
                }
                else if (id == -6)
                {
                    list.Add(new type_look { ID = 44, name = HR.Resource.Basic.EOSquestion });
                    list.Add(new type_look { ID = 45, name = HR.Resource.Basic.EOSgroups });
                    list.Add(new type_look { ID = 46, name = HR.Resource.Basic.checklistitem });
                    list.Add(new type_look { ID = 47, name = HR.Resource.Basic.checklistitemgroup });
                    list.Add(new type_look { ID = 48, name =HR.Resource.pers_2.Evaluationelement });
                    list.Add(new type_look { ID = 49, name = HR.Resource.pers_2.EvaluationElementCompetenies });
                    list.Add(new type_look { ID = 50, name = HR.Resource.pers_2.Evaluationgrade });
                    list.Add(new type_look { ID = 51, name = HR.Resource.pers_2.Evaluationtype });
                    list.Add(new type_look { ID = 52, name = HR.Resource.pers_2.evaluitionobjective });

                    list.Add(new type_look { ID = 53, name = HR.Resource.Personnel.Subscribed });
                    list.Add(new type_look { ID = 54, name = HR.Resource.Personnel.ContractType});
                    list.Add(new type_look { ID = 55, name = HR.Resource.Basic.employeerecordtype });
                    //list.Add(new type_look { ID = 56, name = HR.Resource.Basic.retirementlevelssetup });
                    list.Add(new type_look { ID = 57, name = HR.Resource.Basic.employeeprofilegroup });

                    return Json(list);
                }
                else if(id==-7)
                {
                    list.Add(new type_look { ID = 58, name = HR.Resource.Basic.comit });
                    list.Add(new type_look { ID = 59, name = HR.Resource.Basic.checklistitem });
                    return Json(list);
                }
                else
                {
                    return Json(false);
                }
            }
            catch(Exception)
            {
                return Json(false);
            }
        }
        public JsonResult getL3(int id)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            try
            {
                
                if(id==1)
                {
                    var list = dbcontext.Country.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if(id==2)
                {
                    var list = dbcontext.Area.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if (id == 3)
                {
                    var list = dbcontext.the_states.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if (id == 4)
                {
                    var list = dbcontext.Territories.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if (id == 5)
                {
                    var list = dbcontext.cities.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if (id == 6)
                {
                    var list = dbcontext.postcode.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if (id == 7)
                {
                    var list = dbcontext.Nationality.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if (id == 8)
                {
                    var list = dbcontext.Religion.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if (id == 9)
                {
                    var list = dbcontext.Educate_category.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if (id == 10)
                {
                    var list = dbcontext.Educate_Title.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if (id == 11)
                {
                    var list = dbcontext.Main_Educate_body.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if (id == 12)
                {
                    var list = dbcontext.Sub_educational_body.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if (id == 13)
                {
                    var list = dbcontext.Name_of_educational_qualification.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if (id == -99)
                {
                    var list = dbcontext.Qualification_Major.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if (id == -98)
                {
                    var list = dbcontext.GradeEducate.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if (id == 14)
                {
                    var list = dbcontext.Military_Service_Status.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if (id == 15)
                {
                    var list = dbcontext.Military_Service_Rank.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if (id == 16)
                {
                    var list = dbcontext.Document_Group.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if (id == 17)
                {
                    var list = dbcontext.Documents.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if (id == 18)
                {
                    var list = dbcontext.Contact_methods.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if (id == 19)
                {
                    var list = dbcontext.Rejection_Reasons.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if (id == 20)
                {
                    var list = dbcontext.External_compaines.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if (id == 21)
                {
                    var list = dbcontext.Checktype.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if (id == 22)
                {
                    var list = dbcontext.Air_ports.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if (id == 23)
                {
                    var list = dbcontext.Sponsor.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if (id == 24)
                {
                    var list = dbcontext.Currency.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if (id == 25)
                {
                    var list = dbcontext.Authority_Type.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if (id == 26)
                {
                    var list = dbcontext.Authorities.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if (id == 27)
                {
                    var list = dbcontext.Skill_group.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if (id == 28)
                {
                    var list = dbcontext.Skill.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if (id == 29)
                {
                    var list = dbcontext.Risks.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if(id==30)
                {
                    var list = dbcontext.Risks_Type.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if (id == 31)
                {
                    var list = dbcontext.Job_level_class.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if (id == 32)
                {
                    var list = dbcontext.Job_level_gradee.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if (id == 33)
                {
                    var list = dbcontext.Job_title_class.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if (id == 34)
                {
                    var list = dbcontext.Job_title_sub_class.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if (id == 35)
                {
                    var list = dbcontext.Budget_class.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if (id == 36)
                {
                    var list = dbcontext.Budget_class_items.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if (id == 37)
                {
                    var list = dbcontext.Organization_Unit_Schema.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if (id == 38)
                {
                    var list = dbcontext.Organization_Unit_Level.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if (id == 39)
                {
                    var list = dbcontext.Organization_Unit_Type.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if (id == 40)
                {
                    var list = dbcontext.Requirments.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if (id == 41)
                {
                    var list = dbcontext.Experience_group.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if (id == 42)
                {
                    var list = dbcontext.Responsibilities.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if (id == 43)
                {
                    var list = dbcontext.work_location.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if (id == 44)
                {
                    var list = dbcontext.Definition_of_EOS_Interview_Questions.ToList().Select(m => new { name = m.Question_Description, ID = m.ID });
                    return Json(list);
                }
                else if (id == 45)
                {
                    var list = dbcontext.EOS_Interview_Questions_Groups.ToList().Select(m => new { name = m.Description_of, ID = m.ID });
                    return Json(list);
                }
                else if (id == 46)
                {
                    var list = dbcontext.Check_Lists_Items.ToList().Select(m => new { name = m.Description, ID = m.ID });
                    return Json(list);
                   
                }
                else if (id == 47)
                {
                    var list = dbcontext.Check_List_Item_Groups.ToList().Select(m => new { name = m.Description_Group, ID = m.ID });
                    return Json(list);
                }
                else if (id == 46)
                {
                    var list = dbcontext.EvaluationElements.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if (id == 49)
                {
                    var list = dbcontext.EvaluationElementCompetenies.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if (id == 50)
                {
                    var list = dbcontext.EvaluationGrade.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if (id == 51)
                {
                    var list = dbcontext.EvaluationType.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if (id == 52)
                {
                    var list = dbcontext.EvaluationObjectives.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if (id == 53)
                {
                    var list = dbcontext.Subscription_Syndicate.ToList().Select(m => new { name = m.Subscription_Description, ID = m.ID });
                    return Json(list);
                }
                else if (id == 54)
                {
                    var list = dbcontext.Contract_Type.ToList().Select(m => new { name = m.Contract_Type_Description, ID = m.ID });
                    return Json(list);
                }
                else if (id == 55)
                {
                    var list = dbcontext.Employee_records.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if (id == 57)
                {
                    var list = dbcontext.Employee_Profile_Groups.ToList().Select(m => new { name = m.Group_Description, ID = m.ID });
                    return Json(list);
                }
                else if (id == 58)
                {
                    var list = dbcontext.Committe_subjects.ToList().Select(m => new { name = m.Name, ID = m.ID });
                    return Json(list);
                }
                else if (id == 59)
                {
                    var list = dbcontext.Check_Lists_Items.ToList().Select(m => new { name = m.Description, ID = m.ID });
                    return Json(list);
                }

                else
                {
                    return Json(false);
                }
            }
            catch(Exception)
            {
                return Json(false);
            }
        }
        public JsonResult get_all_cases(int id)
        {
            try
            {
                dbcontext.Configuration.ProxyCreationEnabled = false;
                var Cases = dbcontext.CasesTM.Where(m => m.Time_management_conditional_setupID == id).ToList();
                if(Cases.Count>0)
                {
                   return Json( Cases.OrderBy(m => m.number).ToList());
                }
                else
                {
                    return Json(false);
                }
            }
            catch(Exception)
            { 
               return Json(false);
            }
        }

    }
    public class type_look
    {
        public int ID { get; set; }
        public string name { get; set; }
    }
    public class CaseVM
    {
        public int flag { get; set; }
        public CasesTM Case { get; set; }
    }
}