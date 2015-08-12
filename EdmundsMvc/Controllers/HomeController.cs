using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EdmundsMvc.Models;

namespace EdmundsMvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string age)
        {
            return View();
        }
    }
}