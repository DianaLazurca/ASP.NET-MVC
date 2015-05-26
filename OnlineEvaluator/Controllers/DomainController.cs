﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineEvaluator.Repositories;

namespace OnlineEvaluator.Controllers
{
    public class DomainController : Controller
    {
        // GET: Domain
        public ActionResult Index()
        {
            return View();
        }

        
        public JsonResult GetAllDomains()
        {
            return Json(DBManager.GetAllDomains().ToList(), JsonRequestBehavior.AllowGet);
        }
        // GET: Domain/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Domain/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Domain/Create
        [HttpPost]
        public ActionResult Create(String domainName)
        {
            try
            {
               string message = DBManager.AddNewDomain(domainName);
               
               if (message.Contains("success"))
               {
                   string domainId = message.Substring(7);
                   return Json(new { status = 201, id = domainId });
               } else {
                   throw new Exception();
               }
               
            }
            catch
            {
                return new HttpStatusCodeResult(401, "Error when adding a new domain");
            }
        }

        // GET: Domain/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Domain/Edit/5
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

        // GET: Domain/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Domain/Delete/5
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