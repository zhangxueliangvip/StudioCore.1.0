using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnitTestWeb.ServiceReference;

namespace UnitTestWeb.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            LoginPluginClient client = new LoginPluginClient();
           var result= client.DoWork("zxl");
            return View();
        }
    }
}