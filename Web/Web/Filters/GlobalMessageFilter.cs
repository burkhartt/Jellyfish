#region

using System.Web.Mvc;
using Utilities;
using Web.Models;

#endregion

namespace Web.Filters {
    public class GlobalMessageFilter : IActionFilter {
        public void OnActionExecuting(ActionExecutingContext filterContext) {}

        public void OnActionExecuted(ActionExecutedContext filterContext) {
            if (!(filterContext.Result is RedirectToRouteResult)) {
                return;
            }

            var successMessage = filterContext.Result.CastAs<RedirectToRouteResult>().RouteValues["SuccessMessage"];
            if (successMessage != null) {
                filterContext.Controller.TempData["SuccessMessage"] = new SuccessMessage {
                    Message = (string) successMessage
                };
            }

            var errorMessage = filterContext.Result.CastAs<RedirectToRouteResult>().RouteValues["ErrorMessage"];
            if (errorMessage != null) {
                filterContext.Controller.TempData["ErrorMessage"] = new ErrorMessage {
                    Message = (string) errorMessage
                };
            }
        }
    }
}