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
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Subdomain/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Subdomain/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
