using HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Reposatory.Evalutions.IReposatory
{
    interface IEvalutionType
    {
        EvaluationType Find(int ID);
        bool AddOne(EvaluationType model);
        bool AddList(List<EvaluationType> model);
        List<EvaluationType> GetAll();
        bool Remove(int id);
        bool Editone(EvaluationType model);
        EvaluationType Find2(int ID);
    }
}
