using HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Reposatory.Evalutions.IReposatory
{
    interface IPublic_Holiday_Events
    {
        Public_Holiday_Events Find(int ID);
    }
}
