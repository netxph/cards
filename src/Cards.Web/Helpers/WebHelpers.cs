using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

    }
}