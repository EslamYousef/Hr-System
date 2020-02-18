using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HR.Models;
using System.Data.Entity.Infrastructure;
using HR.Models.Infra;
namespace HR.Controllers
{
    [Authorize]
    public class Definition_of_EOS_Interview_QuestionsController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Definition_of_EOS_Interview_Questions
      public ActionResult Index()
        {
            var model = dbcontext.Definition_of_EOS_Interview_Questions.ToList();
            return View(model);
        }
        public ActionResult Create()
        {
           
            var o= dbcontext.EOS_Interview_Questions_Groups.ToList().Select(m => new { Code ="" +m.Questions_Group_Code + "-----[" + m.Description_of + ']', ID = m.ID }).ToList();
            ViewBag.EOS_Interview_Questions_Groups = o;
            if (o==null || o.Count()  == 0)
            {

                TempData["Message"] = HR.Resource.Personnel.EnterdatafirstonEOSInterviewQuestionsGroups; 
                var modelll = dbcontext.Definition_of_EOS_Interview_Questions.ToList();
                return View("index",modelll);
    
            }
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel);
            var model = dbcontext.Definition_of_EOS_Interview_Questions.ToList();
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
                var modell = new Definition_of_EOS_Interview_Questions {Question_Code = stru.Structure_Code + count };
                return View(modell);
            
        }
        [HttpPost]
        public ActionResult Create(Definition_of_EOS_Interview_Questions model, string command)
        {
            try
            {
                ViewBag.EOS_Interview_Questions_Groups = dbcontext.EOS_Interview_Questions_Groups.ToList().Select(m => new { Code = "" + m.Questions_Group_Code + "-----[" + m.Description_of + ']', ID = m.ID });

                if (ModelState.IsValid)
                {
                    if (model.Question_GroupId == "0" || model.Question_GroupId == null)
                    {
                        ModelState.AddModelError("", HR.Resource.Personnel.EOSInterviewQuestionsGroupsCodemustenter);
                        return View(model);
                    }
                    Definition_of_EOS_Interview_Questions record = new Definition_of_EOS_Interview_Questions();
                    record.Question_Code = model.Question_Code;
                    record.Question_Description = model.Question_Description;
                    record.Description = model.Description;             
                    record.Question_GroupId = model.Question_GroupId;
                    var Question_GroupId = int.Parse(model.Question_GroupId);
                    var group= dbcontext.EOS_Interview_Questions_Groups.FirstOrDefault(m => m.ID == Question_GroupId);
                    record.EOS_Interview_Questions_Groups = group;
                   var ques= dbcontext.Definition_of_EOS_Interview_Questions.Add(record);
                    dbcontext.SaveChanges();
                    group.questions.Add(ques);
                    dbcontext.SaveChanges();
                    //////add new question to eos///////
                    var ESO = dbcontext.EOS_Request.Where(m => m.EOS_group.ID == Question_GroupId);
                    foreach(var item in ESO)
                    {
                        var interview = new Answer_EOS();
                        interview.question = ques;
                        interview.answer = "";
                        interview.Notes = "";
                        interview.EOS = item;
                        var inter = dbcontext.Answer_EOS.Add(interview);
                     
                       
                    }
                    dbcontext.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(model);
                }
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = HR.Resource.Basic.thiscodeIsalreadyexists;
                return View(model);
            }
            catch (Exception e)
            {
                return View(model);
            }

        }



        public ActionResult Edit(int id)
        {
            try
            {
                ViewBag.EOS_Interview_Questions_Groups = dbcontext.EOS_Interview_Questions_Groups.ToList().Select(m => new { Code = "" + m.Questions_Group_Code + "-----[" + m.Description_of + ']', ID = m.ID });
                var record = dbcontext.Definition_of_EOS_Interview_Questions.FirstOrDefault(m => m.ID == id);
                if (record != null)
                { return View(record); }
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
        public ActionResult Edit(Definition_of_EOS_Interview_Questions model)
        {
            try
            {
                ViewBag.EOS_Interview_Questions_Groups = dbcontext.EOS_Interview_Questions_Groups.ToList().Select(m => new { Code = "" + m.Questions_Group_Code + "-----[" + m.Description_of + ']', ID = m.ID });
                if (model.Question_Code == "0" || model.Question_Code == null)
                {
                    ModelState.AddModelError("", HR.Resource.Personnel.EOSInterviewQuestionsGroupsCodemustenter);
                    return View(model);
                }
                var record = dbcontext.Definition_of_EOS_Interview_Questions.FirstOrDefault(m => m.ID == model.ID);
                record.Question_Code = model.Question_Code;
                record.Question_Description = model.Question_Description;
                record.Description = model.Description;
                record.Question_GroupId = model.Question_GroupId;
                var Question_GroupId = int.Parse(model.Question_GroupId);
                var old_group = dbcontext.EOS_Interview_Questions_Groups.FirstOrDefault(m => m.ID == model.EOS_Interview_Questions_Groups.ID);
                old_group.questions.Remove(record);
                dbcontext.SaveChanges();
                var new_group = dbcontext.EOS_Interview_Questions_Groups.FirstOrDefault(m => m.ID == Question_GroupId);
                record.EOS_Interview_Questions_Groups = new_group;
                dbcontext.SaveChanges();
                new_group.questions.Add(record);
                dbcontext.SaveChanges();

                ////
                //var answer_eos = dbcontext.Answer_EOS.Where(m => m.question.ID == record.ID);
                //foreach(var item in answer_eos)
                //{
                //    item.
                //}
                ////
                return RedirectToAction("index");
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = HR.Resource.Basic.thiscodeIsalreadyexists;
                return View(model);
            }
            catch (Exception e)
            { return View(model); }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                var record = dbcontext.Definition_of_EOS_Interview_Questions.FirstOrDefault(m => m.ID == id);
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
            var record = dbcontext.Definition_of_EOS_Interview_Questions.FirstOrDefault(m => m.ID == id);
           // var group = record.EOS_Interview_Questions_Groups;
           // group.questions.Remove(record);
           // dbcontext.SaveChanges();
            try
            {
                ////remove question from EOS interview////
                var answerEOS = dbcontext.Answer_EOS.Where(m => m.question.ID == record.ID);
                dbcontext.Answer_EOS.RemoveRange(answerEOS);
                dbcontext.SaveChanges();
                /////
                dbcontext.Definition_of_EOS_Interview_Questions.Remove(record);
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
                return View(record);
            }
        }


    }
}