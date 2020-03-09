using HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Reposatory.Evalutions.IReposatory
{
    interface IAppend_Public_Holidays_Dates
    {
        Append_Public_Holidays_Dates Find(int ID);
        List<Append_Public_Holidays_Dates> GetAll();
        Append_Public_Holidays_Dates AddOne(Append_Public_Holidays_Dates model);
        bool AddList(List<Append_Public_Holidays_Dates> model);
        bool EditOne(Append_Public_Holidays_Dates model);
        bool Remove(int id);
    }
}
