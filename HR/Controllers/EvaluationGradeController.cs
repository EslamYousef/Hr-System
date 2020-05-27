using HR.Models;
using HR.Models.Infra;
using HR.Reposatory.Evalutions.IReposatory;
using HR.Reposatory.Evalutions.reposatory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers
{
    public class EvaluationGradeController : BaseController
    {
        //  EvaluationGrade
        private readonly IEvalutionGrade reposatoryGrade;
        private readonly IStructure reposatorystructure;
        public EvaluationGradeController()
        {
            reposatoryGrade = new EvalutionGrade( new Models.ApplicationDbContext());
            reposatorystructure = new Structure(new Models.ApplicationDbContext());
        }

        public ActionResult Index()
        {
            try
            {
                var list = reposatoryGrade.GetAll();
                if (list != null) { return View(list); }
                else {TempData["Message"] = HR.Resource.pers_2.Faild;return View();}
            }
            catch (Exception)
            {
                TempData["Message"] = HR.Resource.pers_2.Faild;
                return View();
            }
        }
        public ActionResult Create()
        {
            try
            {

                /////////create code///////
                var stru = reposatorystructure.find(ChModels.Personnel).Structure_Code;
                var ALLList = reposatoryGrade.GetAll();
                var model = new EvaluationGrade();
                if (ALLList.Count() == 0)
                {
                    model.Code = stru + "1";
                }
                else
                {
                    model.Code = stru + (ALLList.LastOrDefault().ID + 1).ToString();
                }
                model.Decision_Type = Decisiontype.Positive;
                return View(model);
            }
            catch(Exception)
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost ]
        public ActionResult Create(EvaluationGrade model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var flag=reposatoryGrade.AddOne(model);
                    if (flag){ TempData["Message"] = HR.Resource.pers_2.addedSuccessfully;
                        return RedirectToAction("index");
                    }
                    else{ TempData["Message"] = HR.Resource.pers_2.Faild;
                          return View(model);
                    }
                }
                else
                {
                    TempData["Message"] = HR.Resource.pers_2.Faild;
                    return View(model);
                }
            }
            catch(Exception)
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(int id)
        {
            try
            {
                var model = reposatoryGrade.Find(id);
                if (model != null) { return View(model); }
                else
                {
                    TempData["Message"] = HR.Resource.pers_2.Faild;
                    return RedirectToAction("index");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public ActionResult Edit(EvaluationGrade model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var flag = reposatoryGrade.Editone(model);
                    if (flag)
                    {
                        TempData["Message"] = HR.Resource.pers_2.addedSuccessfully;
                        return RedirectToAction("index");
                    }
                    else
                    {
                        TempData["Message"] = HR.Resource.pers_2.Faild;
                        return View(model);
                    }
                }
                else
                {

                    TempData["Message"] = HR.Resource.pers_2.Faild;
                    return View(model);
                }
            }
            catch(Exception)
            {
                TempData["Message"] = HR.Resource.pers_2.Faild;
                return View(model);
            }
        }
        public ActionResult Delete(int id)
        {
            try
            {
               
                var model = reposatoryGrade.Find(id);
                if (model != null) { return View(model); }
                else
                {
                    TempData["Message"] = HR.Resource.pers_2.Faild;
                    return RedirectToAction("index");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult Delete_method(int id)
        {
            try
            {

                var flag = reposatoryGrade.Remove(id);
                if (flag) { TempData["Message"] = HR.Resource.pers_2.removesuccessfully ; return RedirectToAction("index"); }
                else
                {
                    TempData["Message"] = HR.Resource.pers_2.Faild;
                    return RedirectToAction("index");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }
    }
}