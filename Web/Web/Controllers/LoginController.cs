using System.Web.Mvc;
using System.Web.Security;
using Web.Authentication;
using Web.Models;

namespace Web.Controllers {
    public class LoginController : Controller {
        private readonly IAuthenticator authenticator;

        public LoginController(IAuthenticator authenticator) {
            this.authenticator = authenticator;
        }

        public ActionResult Index() {
            return View(new LoginModel());
        }

        [HttpPost]
        public ActionResult Index(LoginModel loginModel) {
            if (!ModelState.IsValid) {
                return View(loginModel);
            }
            
            if (!authenticator.Authenticate(loginModel.EmailAddress, loginModel.Password)) {
                return RedirectToAction("Index", new { ErrorMessage = "Unable to authenticate", ReturnUrl = Request.QueryString["ReturnUrl"] });
            }

            return Redirect(FormsAuthentication.GetRedirectUrl(loginModel.EmailAddress, false));
        }
    }
}