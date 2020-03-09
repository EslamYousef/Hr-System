using HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Reposatory.Evalutions.IReposatory
{
    interface IEvaluationperformanceGroup
    {
        PerformanceEvaluationGroup Find(int ID);
        PerformanceEvaluationGroup AddOne(PerformanceEvaluationGroup model);
        bool AddList(List<PerformanceEvaluationGroup> model);
        List<PerformanceEvaluationGroup> GetAll();
        bool Remove(int id);
        bool Editone(PerformanceEvaluationGroup model);
    }
}
