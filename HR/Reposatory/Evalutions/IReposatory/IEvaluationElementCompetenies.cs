using HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Reposatory.Evalutions.IReposatory
{
    interface IEvaluationElementCompetenies
    {
        EvaluationElementCompetenies Find(int ID);
        bool AddOne(EvaluationElementCompetenies model);
        bool AddList(List<EvaluationElementCompetenies> model);
        List<EvaluationElementCompetenies> GetAll();
        bool Remove(int id);
        bool Editone(EvaluationElementCompetenies model);
    }
}
