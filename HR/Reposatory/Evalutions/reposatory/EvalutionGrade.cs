using HR.Models;
using HR.Reposatory.Evalutions.IReposatory;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace HR.Reposatory.Evalutions.reposatory
{
    public class EvalutionGrade: IEvalutionGrade
    {
        private readonly ApplicationDbContext context;

        public EvalutionGrade(ApplicationDbContext newcontext)
        {
            context = newcontext;
        }
        public EvaluationGrade Find(int ID)
        {
            try
            {
                var model = context.EvaluationGrade.Find(ID);
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool AddOne(EvaluationGrade model)
        {
            try
            {
                context.EvaluationGrade.Add(model);
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
        public bool AddList(List<EvaluationGrade> model)
        {
            try
            {
                context.EvaluationGrade.AddRange(model);
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
        public List<EvaluationGrade> GetAll()
        {
            try
            {
                var model = context.EvaluationGrade.ToList();
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
                var model = context.EvaluationGrade.FirstOrDefault(m => m.ID == id);
                context.EvaluationGrade.Remove(model);
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
        
        public bool Editone(EvaluationGrade model)
        {
            try
            {
                var record = context.EvaluationGrade.FirstOrDefault(m => m.ID == model.ID);
                record.Description = model.Description;
                record.Decision_Type = model.Decision_Type;
                record.FromScore = model.FromScore;
                record.ToScore = model.ToScore;
                record.Name = model.Name;
                context.SaveChanges();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
            catch (Exception )
            {
                return false;
            }
        }
    }
}