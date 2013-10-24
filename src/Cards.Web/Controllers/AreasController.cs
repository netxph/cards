using Cards.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cards.Web.Models;

namespace Cards.Web.Controllers
{
    [Authorize]
    public class AreasController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(int id = 0)
        {
            ViewBag.AreaID = id;

            return View();
        }

    }
}
