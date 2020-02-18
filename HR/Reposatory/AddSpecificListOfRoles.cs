using HR.Areas.suberAdmin.Models;
using HR.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace HR.Reposatory
{
    public class AddSpecificListOfRoles: IAddSpecificListOfRoles
    {
        private readonly ApplicationDbContext context;
   
        public AddSpecificListOfRoles(ApplicationDbContext newcontext)
        {
            context = newcontext;
        }
        public Roles Find(int ID)
        {
            try
            {
                var model = context.AddSpecificListOfRoles.Find(ID);
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool AddOne(Roles model)
        {
            try
            {
                context.AddSpecificListOfRoles.Add(model);
                context.SaveChanges();
                return true;
            }
            catch(DbUpdateException)
            {
                return false;
            }
            catch(Exception)
            {
                return false;
            }
        }
        public bool AddList(List<Roles> model)
        {
            try
            {
                context.AddSpecificListOfRoles.AddRange(model);
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
        public List<Roles> GetAll()
        {
            try
            {
                var model = context.AddSpecificListOfRoles.ToList();
                return model;
            }
            catch(Exception)
            {
                return null;
            }
        }
        public bool Remove(Roles model)
        {
            try
            {
                context.AddSpecificListOfRoles.Remove(model);
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