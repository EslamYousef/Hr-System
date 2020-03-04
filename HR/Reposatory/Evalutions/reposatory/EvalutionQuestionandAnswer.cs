using HR.Models;
using HR.Reposatory.Evalutions.IReposatory;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace HR.Reposatory.Evalutions.reposatory
{
    public class EvalutionQuestionandAnswer: IEvalutionQuestionandAnswer
    {
        private readonly ApplicationDbContext context;

        public EvalutionQuestionandAnswer(ApplicationDbContext newcontext)
        {
            context = newcontext;
        }
        public EvaluationQuestionsandanswers Find(int ID)
        {
            try
            {
                var model = context.EvaluationQuestionsandanswers.Find(ID);
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool AddOne(EvaluationQuestionsandanswers model)
        {
            try
            {
                context.EvaluationQuestionsandanswers.Add(model);
                context.SaveChanges();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool AddList(List<EvaluationQuestionsandanswers> model)
        {
            try
            {
                context.EvaluationQuestionsandanswers.AddRange(model);
                context.SaveChanges();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<EvaluationQuestionsandanswers> GetAll()
        {
            try
            {
                var model = context.EvaluationQuestionsandanswers.ToList();
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool Remove(int id)
        {
            try
            {
                var model = context.EvaluationQuestionsandanswers.FirstOrDefault(m => m.ID == id);
                context.EvaluationQuestionsandanswers.Remove(model);
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Editone(EvaluationQuestionsandanswers model)
        {
            try
            {
                var record = context.EvaluationQuestionsandanswers.FirstOrDefault(m => m.ID == model.ID);
                record.Question = model.Question;
                record.model_answer = model.model_answer;
                context.SaveChanges();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}