using HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Reposatory.Evalutions.IReposatory
{
    interface IEvalutonplan
    {
        EvaluationPlan Find(int ID);
        EvaluationPlan AddOne(EvaluationPlan model);
        bool AddList(List<EvaluationPlan> model);
        List<EvaluationPlan> GetAll();
        bool Remove(int id);
        EvaluationPlan Editone(EvaluationPlan model);
        PlaneSchedule AddOneschedule(PlaneSchedule model);
        bool reomveplanescedule(int id);
        List<PlaneSchedule> findplanescedule(int id);
    }
}
