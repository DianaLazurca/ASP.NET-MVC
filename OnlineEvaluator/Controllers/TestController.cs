using OnlineEvaluator.Models;
using OnlineEvaluator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using OnlineEvaluator.Repositories;

namespace OnlineEvaluator.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        [HttpPost]
        public ActionResult GenerateTest(int domainId, int duration, List<Int32> selectedSubdomains)
        {
            try
            {
                TestService  testService = new TestService();
                List<Question> allQuestions = testService.GenerateTestListOfQuestions(selectedSubdomains);

                if (allQuestions != null && allQuestions.Count() > 0) 
                {
                    Test test = new Test
                    {
                        DomainId = domainId,
                        Duration = duration,
                        Questions = allQuestions
                    };

                    test = TestRepository.AddTest(test);

                    if (test != null && test.Id != 0)
                    {
                        Evaluation evaluation = new Evaluation 
                        {
                            TestId = test.Id,
                            ApplicationUserId = User.Identity.GetUserId(),
                            StartDate = DateTime.Now
                        };

                        evaluation = EvaluationRepository.AddEvaluation(evaluation);
                        if (evaluation != null && evaluation.Id != 0)
                        {
                            return Json(new {evaluationId = evaluation.Id});
                        }
                        else
                        {
                            return new HttpStatusCodeResult(404);
                        }
                    }
                    else
                    {
                        return new HttpStatusCodeResult(404);
                    }
                }
                else
                {
                    return new HttpStatusCodeResult(404);
                }


                //return Json(selectedSubdomains, JsonRequestBehavior.DenyGet);
            }
            catch
            {
                return new HttpStatusCodeResult(404);
            }
            
        }
    }
}