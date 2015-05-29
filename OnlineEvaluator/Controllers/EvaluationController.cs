using OnlineEvaluator.Models;
using OnlineEvaluator.Repositories;
using OnlineEvaluator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineEvaluator.Controllers
{
    public class EvaluationController : Controller
    {
        // GET: Evaluation
        public ActionResult TakeTest(int id)
        {
            Evaluation evaluation = EvaluationRepository.GetEvaluationById(id);

            if (evaluation == null)
            {
                return new HttpStatusCodeResult(404);
            }

            int minutesLeft = evaluation.StartDate.AddMinutes(evaluation.Test.Duration).Subtract(DateTime.Now).Minutes;

            if (evaluation != null && evaluation.IsTaken == false && minutesLeft >= 1)
            {
                return View(evaluation);
            }
            else
            {
                return new HttpStatusCodeResult(404);
            }
            
        }

        [HttpPost]
        public ActionResult FinishTest(int id, Evaluation evaluation)
        {
            Evaluation oldEvaluation = EvaluationRepository.GetEvaluationById(id);

            if (oldEvaluation == null)
            {
                return new HttpStatusCodeResult(400);
            }

            int secondsLeft = oldEvaluation.StartDate.AddMinutes(oldEvaluation.Test.Duration).Subtract(DateTime.Now).Seconds;

            if (secondsLeft > -5 && oldEvaluation.IsTaken == false)
            {
                evaluation.IsTaken = true;
                evaluation = EvaluationRepository.UpdateEvaluation(id, evaluation);

                if (evaluation != null)
                {
                    evaluation = EvaluationRepository.GetEvaluationById(evaluation.Id);
                    EvaluationService evaluationService = new EvaluationService();
                    return Json(evaluationService.Evaluate(evaluation), JsonRequestBehavior.DenyGet);
                }
                else
                {
                    return new HttpStatusCodeResult(400);
                }
            }
            else
            {
                return new HttpStatusCodeResult(400);
            }
            

            
        }
    }
}