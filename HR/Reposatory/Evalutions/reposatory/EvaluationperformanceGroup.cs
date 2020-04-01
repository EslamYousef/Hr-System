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
        public PerformanceEvaluationGroup Editone(PerformanceEvaluationGroup model)
        {
            try
            {
                var record = context.PerformanceEvaluationGroup.FirstOrDefault(m => m.ID == model.ID);
                record.Description = model.Description;
                record.Name = model.Name;
                context.SaveChanges();
                return record;
            }
            catch (DbUpdateException)
            {
                return null;
            }
            catch (Exception)
            {
                return null;
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
        public List<PerformanceEvaluationGroupEvaluationElements> getfromManytoMantTable(int id)
        {
            try
            {
               var elemetns= context.PerformanceEvaluationGroupEvaluationElements.Where(m=>m.PerformanceEvaluationGroupID==id).ToList();
                return elemetns;
            }
            catch (Exception) { return null; }
        }
        public bool addManytoMantquestions(Questions_Performance model)
        {
            try
            {
                context.Questions_Performance.Add(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception e) { return false; }
        }
        public List<Questions_Performance> getfromManytoMantquestions(int id)
        {
            try
            {
             context.Configuration.ProxyCreationEnabled = false;
                var elemetns = context.Questions_Performance.Where(m => m.PerformanceEvaluationGroupID == id).ToList();
                return elemetns;
            }
            catch (Exception) { return null; }
        }
        public bool removeTableQUES(List<Questions_Performance> model)
        {
            try
            {
                context.Questions_Performance.RemoveRange(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception) { return false; }
        }
        public bool removeManytomanyTable(List<PerformanceEvaluationGroupEvaluationElements> model)
        {
            try
            {
                context.PerformanceEvaluationGroupEvaluationElements.RemoveRange(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception) { return false; }
        }
    }
}