using HR.Models;
using HR.Reposatory.Evalutions.IReposatory;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace HR.Reposatory.Evalutions.reposatory
{
    public class EvalutionObjectives: IEvalutionObjectives
    {
        private readonly ApplicationDbContext context;

        public EvalutionObjectives(ApplicationDbContext newcontext)
        {
            context = newcontext;
        }
        public EvaluationObjectives Find(int ID)
        {
            try
            {
                var model = context.EvaluationObjectives.Find(ID);
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool AddOne(EvaluationObjectives model)
        {
            try
            {
                context.EvaluationObjectives.Add(model);
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
        public bool AddList(List<EvaluationObjectives> model)
        {
            try
            {
                context.EvaluationObjectives.AddRange(model);
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
        public List<EvaluationObjectives> GetAll()
        {
            try
            {
                var model = context.EvaluationObjectives.ToList();
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
                var model = context.EvaluationObjectives.FirstOrDefault(m => m.ID == id);
                context.EvaluationObjectives.Remove(model);
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

        public bool Editone(EvaluationObjectives model)
        {
            try
            {
                var record = context.EvaluationObjectives.FirstOrDefault(m => m.ID == model.ID);
                record.Description = model.Description;
                record.Name = model.Name;
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