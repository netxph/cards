using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cards.Web.Controllers
{

    [Authorize]
    public class CardsController : Controller
    {
        public ActionResult Edit(int id = 0)
        {
            ViewBag.CardID = id;

            return View();
        }

    }
}
