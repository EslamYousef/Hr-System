using HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Reposatory.Evalutions.IReposatory
{
    interface IEvalutionGrade
    {
        EvaluationGrade Find(int ID);
        bool AddOne(EvaluationGrade model);
        bool AddList(List<EvaluationGrade> model);
        List<EvaluationGrade> GetAll();
        bool Remove(int id);
        bool Editone(EvaluationGrade model);
    }
}
