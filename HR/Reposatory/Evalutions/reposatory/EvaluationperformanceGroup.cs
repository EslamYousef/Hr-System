using HR.Models;
using HR.Reposatory.Evalutions.IReposatory;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace HR.Reposatory.Evalutions.reposatory
{
    public class EvaluationperformanceGroup: IEvaluationperformanceGroup
    {
        private readonly ApplicationDbContext context;

        public EvaluationperformanceGroup(ApplicationDbContext newcontext)
        {
            context = newcontext;
        }
        public PerformanceEvaluationGroup Find(int ID)
        {
            try
            {
                var model = context.PerformanceEvaluationGroup.Find(ID);
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public PerformanceEvaluationGroup AddOne(PerformanceEvaluationGroup model)
        {
            try
            {
              var obj=  context.PerformanceEvaluationGroup.Add(model);
                        context.SaveChanges();
                return obj;
            }
            catch (DbUpdateException)
            {
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public bool AddList(List<PerformanceEvaluationGroup> model)
        {
            try
            {
                context.PerformanceEvaluationGroup.AddRange(model);
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
        public List<PerformanceEvaluationGroup> GetAll()
        {
            try
            {
                var model = context.PerformanceEvaluationGroup.ToList();
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
                var model = context.PerformanceEvaluationGroup.FirstOrDefault(m => m.ID == id);
                context.PerformanceEvaluationGroup.Remove(model);
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
        public bool Editone(PerformanceEvaluationGroup model)
        {
            try
            {
                var record = context.PerformanceEvaluationGroup.FirstOrDefault(m => m.ID == model.ID);
              
                record.EvaluationQuestionsandanswers = null;

                record.Description = model.Description;
                record.Name = model.Name;
              
                record.EvaluationQuestionsandanswers = model.EvaluationQuestionsandanswers;
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
        public bool addManytoMantTable(PerformanceEvaluationGroupEvaluationElements model)
        {
            try
            {
                context.PerformanceEvaluationGroupEvaluationElements.Add(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception) { return false; }
        }
        public bool addManytoMantquestions(Questions_Performance model)
        {
            try
            {
                context.Questions_Performance.Add(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception) { return false; }
        }
    }
}