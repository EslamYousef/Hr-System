using HR.Models;
using HR.Reposatory.Evalutions.IReposatory;

using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Reposatory.Evalutions.reposatory
{
    public class EvalutionElements: IEvalutionElements
    {
        private readonly ApplicationDbContext context;

        public EvalutionElements(ApplicationDbContext newcontext)
        {
            context = newcontext;
        }
        public EvaluationElements AddOne(EvaluationElements model)
        {
            try
            {
              var record=  context.EvaluationElements.Add(model);
                context.SaveChanges();
                return record;
            }
            catch (DbUpdateException)
            {
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool Editone(FormCollection form , EvaluationElements model)
        {
            try
            {
                var record = context.EvaluationElements.FirstOrDefault(m => m.ID == model.ID);
                record.Description = model.Description;
                record.Name = model.Name;
                record.defaultDegree = model.defaultDegree;
                if (record.with_competencies == true)
                {
                    var evaluationAndCompetition = context.Evalution_and_competencies.Where(m => m.EvaluationElementsID == model.ID);
                    context.Evalution_and_competencies.RemoveRange(evaluationAndCompetition);
                    context.SaveChanges();
                  
                }
                record.with_competencies = model.with_competencies; context.SaveChanges();
                if (model.with_competencies == true)
                {
                    var compID = form["IDval"].Split(',');
                    var compDegree = form["degree"].Split(',');
                    for (var i = 0; i < compID.Count(); i++)
                    {
                        if (compID[i] != "")
                        {
                            var id = int.Parse(compID[i]); 
                            var comp = context.EvaluationElementCompetenies.FirstOrDefault(m =>m.ID == id);
                            var elementAndComp = new Evalution_and_competencies
                            {
                                Default_degree = double.Parse(compDegree[i]),
                                EvaluationElementCompeteniesID = comp.ID,
                                EvaluationElementsID = record.ID,
                            };
                            var eva_comp = addavandcomp(elementAndComp);
                        }
                    }
                }
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        //public bool AddList(List<EvaluationGrade> model)
        //{
        //    try
        //    {
        //        context.EvaluationGrade.AddRange(model);
        //        context.SaveChanges();
        //        return true;
        //    }
        //    catch (DbUpdateException)
        //    {
        //        return false;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
        public EvaluationElements Find(int ID)
        {
            try
            {
                var model = context.EvaluationElements.Find(ID);
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool Remove(int id)
        {
            try
            {
                var model = context.EvaluationElements.FirstOrDefault(m => m.ID == id);
                if (model.with_competencies == true)
                {
                    var evaluationAndCompetition = context.Evalution_and_competencies.Where(m => m.EvaluationElementsID == model.ID);
                    context.Evalution_and_competencies.RemoveRange(evaluationAndCompetition);
                    context.SaveChanges();
                    context.EvaluationElements.Remove(model);
                    context.SaveChanges();
                }
                else
                {
                    context.EvaluationElements.Remove(model);
                    context.SaveChanges();
                }
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<EvaluationElements> GetAll()
        {
            try
            {
                var model = context.EvaluationElements.ToList();
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Evalution_and_competencies addavandcomp(Evalution_and_competencies model)
        {
            try
            {
                var record = context.Evalution_and_competencies.Add(model);
                context.SaveChanges();
                return record;
            }
            catch (Exception e) { return null; }
        }
        public void get()
        {
            
                var record = context.Evalution_and_competencies.ToList();
                
            
        }
    }
}