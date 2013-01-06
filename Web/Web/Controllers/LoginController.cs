using System;
using System.Web.Mvc;
using System.Web.Security;
using Web.Authentication;
using Web.Events;
using Web.Models;
using Web.Repositories;

namespace Web.Controllers {
    public class LoginController : Controller {
        private readonly IAuthenticator authenticator;
        private readonly IAccountRepository accountRepository;
        private readonly IEventBus eventBus;

        public LoginController(IAuthenticator authenticator, IAccountRepository accountRepository, IEventBus eventBus) {
            this.authenticator = authenticator;
            this.accountRepository = accountRepository;
            this.eventBus = eventBus;
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
                return View(loginModel);
            }

            return Redirect(FormsAuthentication.GetRedirectUrl(loginModel.EmailAddress, false));
        }

        public ActionResult AllAccounts() {
            return View(accountRepository.All());
        }

        public ActionResult AutoLogin(Guid id) {
            eventBus.Send(new AccountSuccessfullyAuthenticatedEvent { Id = id });
            return Redirect(FormsAuthentication.GetRedirectUrl(id.ToString(), false));
        }
    }
}