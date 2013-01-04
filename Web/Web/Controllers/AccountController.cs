using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers {
    public class AccountController : Controller {
        public ActionResult Create() {
            return View(new CreateAccountModel());
        }
    }
}