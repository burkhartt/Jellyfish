using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers {
    public class LoginController : Controller {
        public ActionResult Index() {
            return View(new LoginModel());
        }
    }
}