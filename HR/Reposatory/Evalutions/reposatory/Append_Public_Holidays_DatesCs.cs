using HR.Reposatory.Evalutions.IReposatory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HR.Models;
using System.Data.Entity.Infrastructure;

namespace HR.Reposatory.Evalutions.reposatory
{
    
    public class Append_Public_Holidays_DatesCs : IAppend_Public_Holidays_Dates
    {
        private readonly ApplicationDbContext db;
        public Append_Public_Holidays_DatesCs(ApplicationDbContext newContext)
        {
            db = newContext;
        }
        public bool AddList(List<Append_Public_Holidays_Dates> model)
        {
            try
            {
           db.Append_Public_Holidays_Dates.AddRange(model);
                db.SaveChanges();
                return true ;
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

        public Append_Public_Holidays_Dates AddOne(Append_Public_Holidays_Dates model)
        {
            try
            {
                var obj = db.Append_Public_Holidays_Dates.Add(model);
                db.SaveChanges();
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

        public bool EditOne(Append_Public_Holidays_Dates model)
        {
            try
            {
                var recode = db.Append_Public_Holidays_Dates.FirstOrDefault(a => a.ID == model.ID);
                recode.Todate = model.Todate;
                recode.Fromdate = model.Fromdate;
                recode.Notes = model.Notes;
                db.SaveChanges();
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

        public Append_Public_Holidays_Dates Find(int ID)
        {
            try
            {
                var model = db.Append_Public_Holidays_Dates.Find(ID);
                return model;
            }
            catch (Exception)
            {

                return null;
                
            }
        }

        public List<Append_Public_Holidays_Dates> GetAll()
        {
            try
            {
                var model = db.Append_Public_Holidays_Dates.ToList();
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
                var model = db.Append_Public_Holidays_Dates.FirstOrDefault(m => m.ID == id);
                db.Append_Public_Holidays_Dates.Remove(model);
                db.SaveChanges();
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