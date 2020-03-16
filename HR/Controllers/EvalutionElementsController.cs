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
    public class EvalutionElementsController : Controller
    {
        // GET: EvalutionElements
        private readonly IEvalutionElements reposatoryelement;
        private readonly IEvaluationElementCompetenies reposatorycomp;
        private readonly IStructure reposatorystructure;
        public EvalutionElementsController()
        {
            reposatoryelement = new EvalutionElements(new Models.ApplicationDbContext());
            reposatorycomp = new HR.Reposatory.Evalutions.reposatory.EvaluationElementCompetenies(new Models.ApplicationDbContext());

            reposatorystructure = new Structure(new Models.ApplicationDbContext());
        }
        public ActionResult Index()
        {
            try
            {
                var list = reposatoryelement.GetAll();
                if (list != null) { return View(list); }
                else { TempData["Message"] = HR.Resource.pers_2.Faild; return View(); }
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
                var ALLList = reposatoryelement.GetAll();
                var model = new EvaluationElements();
                if (ALLList.Count() == 0)
                {
                    model.Code = stru + "1";
                }
                else
                {
                    model.Code = stru + (ALLList.LastOrDefault().ID + 1).ToString();
                }
                ViewBag.competitions = reposatorycomp.GetAll().Select(m=>new{Code=m.Code+"-->"+m.Name,ID=m.ID});
                model.defaultDegree = 0;
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public ActionResult Create(FormCollection form, EvaluationElements model)
        {
            try
            {
                ViewBag.competitions = reposatorycomp.GetAll().Select(m => new { Code = m.Code + "-->" + m.Name, ID = m.ID });

                if (ModelState.IsValid)
                {
                    var record = reposatoryelement.AddOne(model);
                    if (record!=null)
                    {
                        if (model.with_competencies)
                        {
                                var compID = form["ID"].Split(',');
                                var compDegree = form["degree"].Split(',');
                                for (var i = 0; i < compID.Count(); i++)
                                {
                                    if (compID[i] != "")
                                    {
                                        var comp = reposatorycomp.Find(int.Parse(compID[i]));
                                        var elementAndComp = new Evalution_and_competencies
                                        {
                                            Default_degree = double.Parse(compDegree[i]),
                                            EvaluationElementCompeteniesID = comp.ID,
                                            EvaluationElementsID = record.ID,
                                        };
                                        var eva_comp = reposatoryelement.addavandcomp(elementAndComp);
                                    }

                                }
                        }
                      
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
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(int id)
        {
            try
            {
                ViewBag.competitions = reposatorycomp.GetAll().Select(m => new { Code = m.Code + "-->" + m.Name, ID = m.ID });
                var model = reposatoryelement.Find2(id);
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
        public ActionResult Edit(FormCollection form, EvaluationElements model)
        {
            try
            {
                ViewBag.competitions = reposatorycomp.GetAll().Select(m => new { Code = m.Code + "-->" + m.Name, ID = m.ID });
                model.Evalution_and_competencies = reposatoryelement.Find2(model.ID).Evalution_and_competencies;
                if (ModelState.IsValid)
                {
                    var flag = reposatoryelement.Editone(form,model);
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
            catch (Exception)
            {
                TempData["Message"] = HR.Resource.pers_2.Faild;
                return View(model);
            }
        }
        public ActionResult Delete(int id)
        {
            try
            {

                var model = reposatoryelement.Find(id);
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

                var flag = reposatoryelement.Remove(id);
                if (flag) { TempData["Message"] = HR.Resource.pers_2.removesuccessfully; return RedirectToAction("index"); }
                else
                {
                    TempData["Message"] = HR.Resource.pers_2.Faild;
                    return View("index");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }
        public JsonResult getcomp(int id)
        {
            try {
             
                return Json(reposatorycomp.Find(id));
            }
            catch (Exception) { return Json(false); }
        }
    }
}