using HR.Models;
using HR.Models.Infra;
using HR.Models.ViewModel;
using HR.Reposatory.Evalutions.IReposatory;
using HR.Reposatory.Evalutions.reposatory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers
{
    public class PerformanceEvaluationGroupController : Controller
    {
        // GET: PerformanceEvaluationGroup
        private readonly IEvaluationperformanceGroup reposatoryEvaluationPerformance;
        private readonly IStructure reposatorystructure;
        private readonly IEvalutionQuestionandAnswer reposatoryQuestions;
        private readonly IEvalutionElements reposatoryElements;
        private readonly IEvaluationElementCompetenies reposatoryElementComp;
        public PerformanceEvaluationGroupController()
        {
            reposatoryEvaluationPerformance = new EvaluationperformanceGroup(new ApplicationDbContext());
            reposatorystructure = new Structure(new Models.ApplicationDbContext());
            reposatoryQuestions = new EvalutionQuestionandAnswer(new Models.ApplicationDbContext());
            reposatoryElements = new EvalutionElements(new Models.ApplicationDbContext());
            reposatoryElementComp = new HR.Reposatory.Evalutions.reposatory.EvaluationElementCompetenies(new Models.ApplicationDbContext());
        }
        public ActionResult Index()
        {
            try
            {
            var allList= reposatoryEvaluationPerformance.GetAll();
                return View(allList);
            }
            catch(Exception)
            {
                TempData["Message"] = HR.Resource.pers_2.Faild;
                return View();
            }
           
        }
        public ActionResult Create()
        {
            try
            {
                ViewBag.QA = reposatoryQuestions.GetAll().Select(m => new { ID = m.ID, Code = m.Code + "->" + m.Question });
                ViewBag.elements = reposatoryElements.GetAll().Select(m => new { ID = m.ID, Code = m.Code + "->" + m.Name });
                var stru = reposatorystructure.find(ChModels.Personnel).Structure_Code;
                var ALLList = reposatoryEvaluationPerformance.GetAll();
                var model = new PerformanceEvaluationGroup();
                if (ALLList.Count() == 0)
                {
                    model.Code = stru + "1";
                }
                else
                {
                    model.Code = stru + (ALLList.LastOrDefault().ID + 1).ToString();
                }
                return View(model);
            }
            catch (Exception)
            {
                TempData["Message"] = HR.Resource.pers_2.Faild;
                return RedirectToAction("index");
            }
        }
        
        [HttpPost]
        public ActionResult Create(FormCollection form , PerformanceEvaluationGroup model)
        {
            try
            {
                ViewBag.QA = reposatoryQuestions.GetAll().Select(m => new { ID = m.ID, Code = m.Code + "->" + m.Question });
                ViewBag.elements = reposatoryElements.GetAll().Select(m => new { ID = m.ID, Code = m.Code + "->" + m.Name });
                var obj = reposatoryEvaluationPerformance.AddOne(model);
                var ID_Q = form["ID_Q"].Split(',');
                for (var i = 0; i < ID_Q.Count(); i++)
                {
                    if (ID_Q[i] != "")
                    {
                        var id = int.Parse(ID_Q[i]);
                        var q = reposatoryQuestions.Find(id);
                        var F = reposatoryEvaluationPerformance.addManytoMantquestions(new Questions_Performance { EvaluationQuestionsandanswersID = q.ID, PerformanceEvaluationGroupID = obj.ID });

                    }
                }
                var  ID_element =form["ID_Element"].Split(',');
               
                List<int> EvaluationElements = new List<int>();
                    for (var i = 0; i < ID_element.Count(); i++)
                    {
                        if (ID_element[i] != "")
                        {
                            var id = int.Parse(ID_element[i]);
                            var element = reposatoryElements.Find(id);
                            var F= reposatoryEvaluationPerformance.addManytoMantTable(new PerformanceEvaluationGroupEvaluationElements { EvaluationElementsID=element.ID,PerformanceEvaluationGroupID=obj.ID});
                        }
                    }
                    List<int> Q_List = new List<int>();
       
              
               
                if (obj != null) { TempData["Message"] = HR.Resource.pers_2.addedSuccessfully;return RedirectToAction("index"); }
                else { TempData["Message"] = HR.Resource.pers_2.Faild;return View(model);}
            }
            catch (Exception e)
            {
                TempData["Message"] = HR.Resource.pers_2.Faild;
                return View(model);
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                ViewBag.QA = reposatoryQuestions.GetAll().Select(m => new { ID = m.ID, Code = m.Code + "->" + m.Question });
                ViewBag.elements = reposatoryElements.GetAll().Select(m => new { ID = m.ID, Code = m.Code + "->" + m.Name });
                var obj = reposatoryEvaluationPerformance.Find(id);
                foreach(var item in obj.EvaluationQuestionsandanswers)
                {
                    item.EvaluationQuestionsandanswers = reposatoryQuestions.Find(item.EvaluationQuestionsandanswersID);
                }
               
                if (obj!=null) { return View(obj); }
                else { return RedirectToAction("index"); }
            }
            catch (Exception)
            {
                TempData["Message"] = HR.Resource.pers_2.Faild;
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult Edit(FormCollection form,PerformanceEvaluationGroup model)
        {
            try
            {
                ViewBag.QA = reposatoryQuestions.GetAll().Select(m => new { ID = m.ID, Code = m.Code+"->"+m.Question });
                ViewBag.elements = reposatoryElements.GetAll().Select(m => new { ID = m.ID, Code = m.Code+"->"+m.Name });
                var ID_element = form["ID_Element"].Split(',');
                var ID_Q = form["ID_Q"].Split(',');
                List<int> EvaluationElements = new List<int>();
                for (var i = 0; i < ID_element.Count(); i++)
                {
                    if (ID_element[i] != "")
                    {
                        var id = int.Parse(ID_element[i]);
                        var element = reposatoryElements.Find(id);
                        EvaluationElements.Add(element.ID);
                    }
                }
                List<EvaluationQuestionsandanswers> Q_List = new List<EvaluationQuestionsandanswers>();
                //for (var i = 0; i < ID_Q.Count(); i++)
                //{
                //    if (ID_Q[i] != "")
                //    {
                //        var id = int.Parse(ID_Q[i]);
                //        var element = reposatoryQuestions.Find(id);
                //        Q_List.Add(element);
                //    }
                //}
              //  model. = EvaluationElements;
            //    model.EvaluationQuestionsandanswers = Q_List;
                var flag= reposatoryEvaluationPerformance.Editone(model);
                if (flag) { TempData["Message"] = HR.Resource.pers_2.addedSuccessfully; return RedirectToAction("index"); }
                else{ TempData["Message"] = HR.Resource.pers_2.Faild; return View(model); }
               
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
                var obj = reposatoryEvaluationPerformance.Find(id);
                return View(obj);
            }
            catch (Exception) { TempData["Message"] = HR.Resource.pers_2.Faild; return RedirectToAction("index"); }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult method_Delete(int id)
        {
            try
            {
                var obj = reposatoryEvaluationPerformance.Remove(id);
                if (obj) { TempData["Message"] = HR.Resource.organ.delete; return RedirectToAction("index"); }
                else { TempData["Message"] = HR.Resource.pers_2.Faild; return View(); }
            }
            catch (Exception) { TempData["Message"] = HR.Resource.pers_2.Faild; return RedirectToAction("index"); }

        }
        public JsonResult GetElement(int id)
        {
            return Json(reposatoryElements.Find(id));
        }
        public JsonResult GetCompet(int id)
        {
          var obj= reposatoryElements.find_evaandcomp(id);
            var obj2 = reposatoryElementComp.GetAll();
            var eva_And_Comp = from e in obj
                               join d in obj2 on e.EvaluationElementCompeteniesID equals d.ID
                                 select new performancEevaluationVM
                                 {
                                     degeree= e.Default_degree,
                                     code = d.Code,
                                     Desc = d.Description,
                                 };
            var t = eva_And_Comp.ToList();
            return Json(t);
        }
        public JsonResult Questions(int id)
        {
            return Json(reposatoryQuestions.Find(id));
        }



    }
}