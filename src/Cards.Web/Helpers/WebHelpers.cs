using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cards.Web.Models;

namespace Cards.Web.Helpers
{
    public static class WebHelpers
    {

        public static string GetRoot(this UrlHelper url)
        {
            var root = HttpContext.Current.Request.ApplicationPath;

            if (!root.EndsWith("/"))
            {
                root = root + "/";
            }

            return root;
        }

        public static bool IsAuthenticated
        {
            get { return HttpContext.Current.User.Identity.IsAuthenticated; }
        }

        public static FacebookUserProfile CurrentUser
        {
            get { return HttpContext.Current.Session["Profile"] as FacebookUserProfile; }
        }

        public static string ProfilePicture 
        {
            get { return string.Format("https://graph.facebook.com/{0}/picture", CurrentUser.Id); }
        }

        public static string FullName
        { 
            get { return CurrentUser.Name ; }
        }

    }
}