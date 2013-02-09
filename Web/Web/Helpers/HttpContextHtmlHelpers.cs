using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Domain.Models.Goals;
using Entities;
using Web.Extensions;

namespace Web.Helpers {
    public static class HttpContextHtmlHelpers {
        public static IEnumerable<Group> Groups(this HtmlHelper helper) {
            return GetHttpContext().Groups();
        }

        public static IEnumerable<Goal> Goals(this HtmlHelper helper) {
            return GetHttpContext().Goals();
        }

        public static IEnumerable<Account> GroupMembers(this HtmlHelper helper) {
            return GetHttpContext().GroupMembers();
        }

        private static HttpContextWrapper GetHttpContext() {
            return new HttpContextWrapper(HttpContext.Current);
        }
    }
}