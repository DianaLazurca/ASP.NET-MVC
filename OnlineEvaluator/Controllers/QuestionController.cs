using OnlineEvaluator.Models;
using OnlineEvaluator.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineEvaluator.Controllers
{
    public class QuestionController : Controller
    {
        // GET: Question
        public ActionResult Index()
        {
            return View();
        }

        // GET: Question/Details/5
        public ActionResult Details(int id)
        {
            Question question = QuestionRepository.GetQuestionById(id);

            if (question != null)
            {
                return Json(question, JsonRequestBehavior.AllowGet);
            }

            return new HttpStatusCodeResult(404);
        }

        // GET: Question/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Question/Create
        [HttpPost]
        public ActionResult Create(Question newQuestion)
        {
            try
            {
                Question savedQuestion = QuestionRepository.AddNewQuestion(newQuestion);

                if (savedQuestion != null)
                {
                    return Json(new { status = 201, id = savedQuestion.Id, text = savedQuestion.Text });
                }
                else
                {
                    return new HttpStatusCodeResult(400, "Error when adding a new question");
                }

            }
            catch
            {
                return new HttpStatusCodeResult(400, "Error when adding a new question");
            }
        }

        // GET: Question/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Question/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Question editedQuestion)
        {
           
                // TODO: Add update logic here
                bool result = QuestionRepository.EditQuestion(id, editedQuestion);
                if (result == true)
                {
                    return new HttpStatusCodeResult(200);
                }

                return new HttpStatusCodeResult(400);
            
        }

        // GET: Question/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Question/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                if (QuestionRepository.RemoveQUestionById(id))
                {
                    return new HttpStatusCodeResult(200);
                }
                return new HttpStatusCodeResult(404);
            }
            catch
            {
                return new HttpStatusCodeResult(404);
            }
        }
    }
}
