using System.Web.Mvc;
using System.Web.Security;

namespace Web.Filters {
    public class AuthorizedAttribute : ActionFilterAttribute {
        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated) {
                FormsAuthentication.RedirectToLoginPage();
            }
            base.OnActionExecuting(filterContext);
        }
    }
}