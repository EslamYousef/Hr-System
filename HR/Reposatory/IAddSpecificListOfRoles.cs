using HR.Areas.suberAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Reposatory
{
    public interface IAddSpecificListOfRoles
    {
        Roles Find(int ID);
        bool AddOne(Roles model);
        bool AddList(List<Roles> model);
        List<Roles> GetAll();
        bool Remove(Roles model);
    }
}
