using HR.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HR.Reposatory.Evalutions.IReposatory
{
    interface IEvalutionElements
    {
        EvaluationElements Find(int ID);
        EvaluationElements AddOne(EvaluationElements model);
        //bool AddList(List<EvaluationElements> model);
       List<EvaluationElements> GetAll();
        bool Remove(int id);
        bool Editone(FormCollection form, EvaluationElements model);
        Evalution_and_competencies addavandcomp(Evalution_and_competencies model);
        void get();
       List <Evalution_and_competencies> find_evaandcomp(int id);
        EvaluationElements Find2(int ID);
    }
}
