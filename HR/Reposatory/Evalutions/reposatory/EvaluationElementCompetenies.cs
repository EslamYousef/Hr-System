using HR.Models;
using HR.Reposatory.Evalutions.IReposatory;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace HR.Reposatory.Evalutions.reposatory
{
    public class EvaluationElementCompetenies: IEvaluationElementCompetenies
    {
        private readonly ApplicationDbContext context;

        public EvaluationElementCompetenies(ApplicationDbContext newcontext)
        {
            context = newcontext;
        }
        public HR.Models.EvaluationElementCompetenies Find(int ID)
        {
            try
            {
                var model = context.EvaluationElementCompetenies.Find(ID);
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool AddOne(HR.Models.EvaluationElementCompetenies model)
        {
            try
            {
                context.EvaluationElementCompetenies.Add(model);
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
        public bool AddList(List<HR.Models.EvaluationElementCompetenies> model)
        {
            try
            {
                context.EvaluationElementCompetenies.AddRange(model);
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
        public List<HR.Models.EvaluationElementCompetenies> GetAll()
        {
            try
            {
                var model = context.EvaluationElementCompetenies.ToList();
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
                var model = context.EvaluationElementCompetenies.FirstOrDefault(m => m.ID == id);
                context.EvaluationElementCompetenies.Remove(model);
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

        public bool Editone(HR.Models.EvaluationElementCompetenies model)
        {
            try
            {
                var record = context.EvaluationElementCompetenies.FirstOrDefault(m => m.ID ==model.ID);
                record.Name = model.Name;
                record.Description = model.Description;
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