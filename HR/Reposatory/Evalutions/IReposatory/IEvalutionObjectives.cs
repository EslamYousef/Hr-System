using HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Reposatory.Evalutions.IReposatory
{
    interface IEvalutionObjectives
    {
        EvaluationObjectives Find(int ID);
        bool AddOne(EvaluationObjectives model);
        bool AddList(List<EvaluationObjectives> model);
        List<EvaluationObjectives> GetAll();
        bool Remove(int id);
        bool Editone(EvaluationObjectives model);
    }
}
