using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineEvaluator.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        [HttpPost]
        public ActionResult GenerateTest(List<Int32> selectedSubdomains)
        {
            try
            {
                return Json(selectedSubdomains, JsonRequestBehavior.DenyGet);
            }
            catch
            {
                return new HttpStatusCodeResult(404);
            }
            
        }
    }
}