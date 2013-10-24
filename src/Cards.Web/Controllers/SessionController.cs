using Cards.Core;
using Cards.Extensions.SSO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Cards.Web.Models;

namespace Cards.Web.Controllers
{
    public class SessionController : Controller
    {

        IAuthenticationProvider _provider = null;
        public IAuthenticationProvider Provider 
        {
            get
            {
                if (_provider == null)
                {
                    _provider = new FacebookAuthProvider();
                }

                return _provider;
            }
        }

        public ActionResult Create(string access_token)
        {
            if (!string.IsNullOrEmpty(access_token))
            {
                //TODO: move this to session
                var context = Provider.Authenticate(new TokenCredentials() { Token = access_token });

                if (context.IsAuthenticated)
                {
                    if (Account.VerifyUser(context.Identifier) != null)
                    {
                        FormsAuthentication.SetAuthCookie(context.Identifier, false);

                        var data = new FacebookUserProfile(context.Data);
                        Session["Profile"] = data;

                        return RedirectToAction("index", "areas");
                    }
                }

                return RedirectToAction("index", "home");
            }

            return View();
        }

        public ActionResult Callback()
        {
            return View();
        }

        public ActionResult Delete()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("index", "home");
        }

    }
}
