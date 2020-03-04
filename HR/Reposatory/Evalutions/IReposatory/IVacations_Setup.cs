using HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Reposatory.Evalutions.IReposatory
{
    interface IVacations_Setup
    {
        Vacations_Setup Find(int ID);
        bool AddOne(Vacations_Setup model);
        bool AddList(List<Vacations_Setup> model);
        List<Vacations_Setup> GetAll();
        bool Remove(Vacations_Setup model);
    }
}
