using HR.Models;
using HR.Models.Infra;
using HR.Reposatory.Evalutions.IReposatory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Reposatory.Evalutions.reposatory
{
    public class Structure: IStructure
    {
        private readonly ApplicationDbContext dbcontext;
        public Structure(ApplicationDbContext context)
        {
            dbcontext = context;
        }

      public  StructureModels find(ChModels Nameofmodel)
        {
            try
            {
                return dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == Nameofmodel);
            }
            catch(Exception)
            {
                return null;
            }
        }
    }
}