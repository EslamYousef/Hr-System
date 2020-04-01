using HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Reposatory.Evalutions.IReposatory
{
    interface IEvalutionQuestionandAnswer
    {
        EvaluationQuestionsandanswers Find(int ID);
        bool AddOne(EvaluationQuestionsandanswers model);
        bool AddList(List<EvaluationQuestionsandanswers> model);
        List<EvaluationQuestionsandanswers> GetAll();
        bool Remove(int id);
        bool Editone(EvaluationQuestionsandanswers model);
        List<EvaluationQuestionsandanswers> GetAll2();
    }
}
