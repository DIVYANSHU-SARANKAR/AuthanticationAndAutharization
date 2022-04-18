using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoMVCApp.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(Roles ="student")]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles ="student")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}