using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.Models;

namespace HR.Reposatory.Evalutions.IReposatory
{
    interface IPublic_Holidays_Dates
    {
        Public_Holidays_Dates Find(int ID);
        List<Public_Holidays_Dates> GetAll();
        bool AddOne(Public_Holidays_Dates model);
        bool AddList(List<Public_Holidays_Dates> model);
        bool EditOne(Public_Holidays_Dates model);
        bool Remove(int id);
    }
}
