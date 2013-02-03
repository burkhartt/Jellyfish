using System.Web;
using Domain.Models;
using Web.Models;

namespace Web.Extensions {
    public static class HttpContextExtensions {
        public static AccountView Account(this HttpContextBase httpContextBase) {
            return (AccountView) httpContextBase.Items["Account"];
        }

        public static Site Site(this HttpContextBase httpContextBase) {
            return (Site) httpContextBase.Items["Site"];
        }
    }
}