using HR.Models;
using HR.Reposatory.Evalutions.IReposatory;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace HR.Reposatory.Evalutions.reposatory
{
    public class Vacations_Setup: IVacations_Setup
    {
        private readonly ApplicationDbContext context;
        public Vacations_Setup(ApplicationDbContext newcontext)
        {
            context = newcontext;
        }
        public Models.Vacations_Setup Find(int ID)
        {
            try
            {
                var model = context.Vacations_Setup.Find(ID);
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool AddOne(Models.Vacations_Setup model)
        {
            try
            {
                context.Vacations_Setup.Add(model);
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

        public bool AddList(List<Models.Vacations_Setup> model)
        {
            try
            {
                context.Vacations_Setup.AddRange(model);
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

        public List<Models.Vacations_Setup> GetAll()
        {
            try
            {
              var model=  context.Vacations_Setup.ToList();
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Remove(Models.Vacations_Setup model)
        {
            try
            {
                context.Vacations_Setup.Remove(model);
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