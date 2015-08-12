using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using EdmundsMvc.Models;

namespace EdmundsMvc.Controllers
{
    public class WizardController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Wizard
        public async Task<ActionResult> _One(string n)
        {
            return View();
        }

        public async Task<ActionResult> _Two()
        {
            return View();
        }
    }
}
