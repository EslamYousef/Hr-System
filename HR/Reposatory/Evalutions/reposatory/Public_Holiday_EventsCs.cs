using HR.Models;
using HR.Reposatory.Evalutions.IReposatory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Reposatory.Evalutions.reposatory
{
    public class Public_Holiday_EventsCs : IPublic_Holiday_Events
    {
        private readonly ApplicationDbContext db;

        public Public_Holiday_EventsCs(ApplicationDbContext newContext)
        {
            db = newContext;
        }

        public Public_Holiday_Events Find(int ID)
        {
            try
            {
                var model = db.Public_Holiday_Events.Find(ID);
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}