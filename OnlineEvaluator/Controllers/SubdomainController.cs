﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineEvaluator.Repositories;
using OnlineEvaluator.Models;

namespace OnlineEvaluator.Controllers
{
    public class SubdomainController : Controller
    {
        // GET: Subdomain
        public ActionResult Index()
        {
            return View();
        }

        // GET: Subdomain/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Subdomain/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Subdomain/Create
        [HttpPost]
        public ActionResult Create(int domainId, string subdomainName)
        {
            try
            {
                Subdomain subdomain = SubdomainRepository.AddSubdomain(domainId, subdomainName);
                if (subdomain != null)
                {
                    return Json(new { status = 201, id = subdomain.Id });
                }
                else
                {
                    return new HttpStatusCodeResult(409);
                }
            }
            catch
            {
                return new HttpStatusCodeResult(409);
            }
        }

        // GET: Subdomain/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Subdomain/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, string name, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                if (SubdomainRepository.EditSubdomainName(id, name))
                {
                    return new HttpStatusCodeResult(200);
                }
                else
                {
                    return new HttpStatusCodeResult(400);
                }
            }
            catch
            {
                return new HttpStatusCodeResult(400);
            }
        }

        // GET: Subdomain/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        public JsonResult GetQuestions(int id)
        {
            return Json(SubdomainRepository.GetQuestionsForSubdomainById(id), JsonRequestBehavior.AllowGet);
        }

        // POST: Subdomain/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                if (SubdomainRepository.RemoveSubdomainById(id))
                {
                    return new HttpStatusCodeResult(200);
                }
                else
                {
                    return new HttpStatusCodeResult(400);
                }

            }
            catch
            {
                return new HttpStatusCodeResult(400);
            }
        }
    }
}
