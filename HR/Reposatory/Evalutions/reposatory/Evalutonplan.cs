using HR.Models;
using HR.Reposatory.Evalutions.IReposatory;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace HR.Reposatory.Evalutions.reposatory
{
    public class Evalutonplan: IEvalutonplan
    {
        private readonly ApplicationDbContext context;

        public Evalutonplan(ApplicationDbContext newcontext)
        {
            context = newcontext;
        }
        public EvaluationPlan Find(int ID)
        {
            try
            {
                var model = context.EvaluationPlan.Find(ID);
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public EvaluationPlan AddOne(EvaluationPlan model)
        {
            try
            {
               var obj= context.EvaluationPlan.Add(model);
                context.SaveChanges();
            
                //==================================================================================
                return obj;
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
        public bool AddList(List<EvaluationPlan> model)
        {
            try
            {
                context.EvaluationPlan.AddRange(model);
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
        public List<EvaluationPlan> GetAll()
        {
            try
            {
                var model = context.EvaluationPlan.ToList();
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
                var model = context.EvaluationPlan.FirstOrDefault(m => m.ID == id);
                context.EvaluationPlan.Remove(model);
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

        public EvaluationPlan Editone(EvaluationPlan model)
        {
            try
            {
                var record = context.EvaluationPlan.FirstOrDefault(m => m.ID == model.ID);
                record.Description = model.Description;
                record.Year = model.Year;
                record.Name = model.Name;
                record.previous_apprisal_to_review = model.previous_apprisal_to_review;
             
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

        public PlaneSchedule AddOneschedule(PlaneSchedule model)
        {
            try
            {
                var obj = context.PlaneSchedule.Add(model);
                context.SaveChanges();
                return obj;
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
        public bool reomveplanescedule(int id)
        {
            try
            {

                var obj = context.PlaneSchedule.Where(m=>m.EvaluationPlanID==id);
                context.PlaneSchedule.RemoveRange(obj);
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
        public List<PlaneSchedule> findplanescedule(int id)
        {
            try
            {
                context.Configuration.ProxyCreationEnabled = false;
                var obj = context.PlaneSchedule.Where(m => m.EvaluationPlanID == id).ToList();
                return obj;
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
    }
}