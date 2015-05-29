using OnlineEvaluator.Models;
using OnlineEvaluator.Repositories;
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
            if (evaluation != null)
            {
                return View(evaluation);
            }
            else
            {
                return new HttpStatusCodeResult(404);
            }
            
        }
    }
}