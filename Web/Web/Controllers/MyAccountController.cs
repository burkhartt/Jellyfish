using System.Web.Mvc;
using Web.Filters;

namespace Web.Controllers {
    [Authorized]
    public class MyAccountController : Controller {
        public ActionResult Verified() {
            return View();
        }
    }
}