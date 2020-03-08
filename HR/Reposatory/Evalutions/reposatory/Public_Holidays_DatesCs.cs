using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HR.Models;
using HR.Reposatory.Evalutions.IReposatory;
using System.Data.Entity.Infrastructure;

namespace HR.Reposatory.Evalutions.reposatory
{
    public class Public_Holidays_DatesCs : IPublic_Holidays_Dates
    {
        private readonly ApplicationDbContext db;
        public Public_Holidays_DatesCs(ApplicationDbContext newContext)
        {
            db = newContext;
        }
        public bool AddList(List<Public_Holidays_Dates> model)
        {
            try
            {
                db.Public_Holidays_Dates.AddRange(model);
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

        public bool AddOne(Public_Holidays_Dates model)
        {
            try
            {
                db.Public_Holidays_Dates.Add(model);
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

        public bool EditOne(Public_Holidays_Dates model)
        {
            try
            {
                var recode = db.Public_Holidays_Dates.FirstOrDefault(a => a.ID == model.ID);
                recode.Holidays_Code = model.Holidays_Code;
                recode.Holiday_description = model.Holiday_description;
                recode.Holiday_alternative_description = model.Holiday_alternative_description;
                recode.Holidaysyear = model.Holidaysyear;
                recode.PublicHolidayEventsId = model.PublicHolidayEventsId;
                //recode.HolidayEventDescriptioned = model.HolidayEventDescriptioned;
                //recode.HolidayEventDescription = model.HolidayEventDescription;
                //recode.Fromdate = model.Fromdate;
                //recode.Todate = model.Todate;
                //recode.Notes = model.Notes;
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

        public Public_Holidays_Dates Find(int ID)
        {
            try
            {
                var model = db.Public_Holidays_Dates.Find(ID);
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Public_Holidays_Dates> GetAll()
        {
            try
            {
                var model = db.Public_Holidays_Dates.ToList();
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
                var model = db.Public_Holidays_Dates.FirstOrDefault(a => a.ID == id);
                db.Public_Holidays_Dates.Remove(model);
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