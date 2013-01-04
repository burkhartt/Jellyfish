using System.Web.Mvc;
using Web.Helpers;
using Web.Models;

namespace Web.Filters {
    public class SuccessMessageFilter : IActionFilter {
        public void OnActionExecuting(ActionExecutingContext filterContext) {
            
        }

        public void OnActionExecuted(ActionExecutedContext filterContext) {
            if (!(filterContext.Result is RedirectToRouteResult)) {
                return;
            }

            var successMessage = filterContext.Result.CastAs<RedirectToRouteResult>().RouteValues["SuccessMessage"];
            if (successMessage != null) {
                filterContext.Controller.TempData["SuccessMessage"] = new SuccessMessage { Message = (string) successMessage };
            }
        }
    }
}