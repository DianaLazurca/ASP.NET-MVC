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
    public class DemoController : Controller
    {
        // GET: Demo

        public ViewResult Demo(int domainId, int duration)
        {
            TestService testService = new TestService();
          //  List<int> selectedSubdomanins = testService.GenerateRandom10SubdomainsFromDomain(domainId).ToList();
            List<Question> allQuestions = testService.GenerateRandom10QuestionsFromDomain(domainId).ToList();

            if (allQuestions != null && allQuestions.Count() > 0)
            {
                Test test = new Test
                {
                    DomainId = domainId,
                    Duration = duration,
                    Domain = DomainRepository.GetDomainById(domainId),
                    Questions = allQuestions,
                    Name = "Random Test",
                    CreationDate = DateTime.Now
                };

                return View(test);
            }
            else
            {
                return View();
            }

          
        }

        public ActionResult GenerateTestDemo(int domainId, int duration, List<Int32> selectedSubdomains)
        {
            try
            {
                TestService testService = new TestService();
                List<Question> allQuestions = testService.GenerateTestListOfQuestions(selectedSubdomains);


                if (allQuestions != null && allQuestions.Count() > 0)
                {
                    Test test = new Test
                    {
                        DomainId = domainId,
                        Duration = duration,
                        Domain = DomainRepository.GetDomainById(domainId),
                        Questions = allQuestions,
                        Name = "Random Test",
                        CreationDate = DateTime.Now
                    };

                    return Json(test);
                }
                else
                {
                    return new HttpStatusCodeResult(404);
                }
            }
            catch
            {
                return new HttpStatusCodeResult(404);
            }

        }
    }
}