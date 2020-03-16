using HR.Models;
using HR.Reposatory.Evalutions.IReposatory;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace HR.Reposatory.Evalutions.reposatory
{
    public class EvalutionType: IEvalutionType
    {
        private readonly ApplicationDbContext context;

        public EvalutionType(ApplicationDbContext newcontext)
        {
            context = newcontext;
        }
        public EvaluationType Find(int ID)
        {
            try
            {
                var model = context.EvaluationType.Find(ID);
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool AddOne(EvaluationType model)
        {
            try
            {
                context.EvaluationType.Add(model);
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
        public bool AddList(List<EvaluationType> model)
        {
            try
            {
                context.EvaluationType.AddRange(model);
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
        public List<EvaluationType> GetAll()
        {
            try
            {
                var model = context.EvaluationType.ToList();
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
                var model = context.EvaluationType.FirstOrDefault(m => m.ID == id);
                context.EvaluationType.Remove(model);
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

        public bool Editone(EvaluationType model)
        {
            try
            {
                var record = context.EvaluationType.FirstOrDefault(m => m.ID == model.ID);
                record.Description = model.Description;
                record.Periods = model.Periods;
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
        public EvaluationType Find2(int ID)
        {
            try
            {
                context.Configuration.ProxyCreationEnabled = false;
                var model = context.EvaluationType.Find(ID);
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}