using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Domain.Models.Goals;
using Web.Extensions;

namespace Web.Helpers {
    public static class HttpContextHtmlHelpers {
        public static IEnumerable<Group> Groups(this HtmlHelper helper) {
            var httpContext = new HttpContextWrapper(HttpContext.Current);
            return httpContext.Groups();
        }

        public static IEnumerable<Goal> Goals(this HtmlHelper helper) {
            var httpContext = new HttpContextWrapper(HttpContext.Current);
            return httpContext.Goals();
        }
    }
}