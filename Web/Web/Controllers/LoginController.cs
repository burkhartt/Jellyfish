using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Web.Authentication;
using Web.Events;
using Web.FacebookAuthentication;
using Web.Models;
using Web.Repositories;

namespace Web.Controllers {
    public class LoginController : Controller {
        private readonly IAccountRepository accountRepository;
        private readonly IAuthenticator authenticator;
        private readonly IEventBus eventBus;
        private readonly IFacebookDataRepository facebookDataRepository;

        public LoginController(IAuthenticator authenticator, IAccountRepository accountRepository, IEventBus eventBus,
                               IFacebookDataRepository facebookDataRepository) {
            this.authenticator = authenticator;
            this.accountRepository = accountRepository;
            this.eventBus = eventBus;
            this.facebookDataRepository = facebookDataRepository;
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

        public ActionResult FacebookLogin() {
            var stateId = facebookDataRepository.GenerateNewStateId();
            return
                new RedirectResult("https://www.facebook.com/dialog/oauth?client_id=" +
                                   WebConfigurationManager.AppSettings["FacebookAppId"] + "&redirect_uri=" +
                                   Url.Action("FacebookLoginCallback", "Login", null, "http") + "&state=" + stateId);
        }

        public ActionResult AllAccounts() {
            return View(accountRepository.All());
        }

        public ActionResult AutoLogin(Guid id) {
            eventBus.Send(new AccountSuccessfullyAuthenticatedEvent {Id = id});
            return Redirect(FormsAuthentication.GetRedirectUrl(id.ToString(), false));
        }

        public ActionResult FacebookLoginCallback(string code) {
            facebookDataRepository.SaveUserAuthenticationCode(code);
            var request =
                WebRequest.Create("https://graph.facebook.com/oauth/access_token?client_id=" +
                                  WebConfigurationManager.AppSettings["FacebookAppId"] + "&redirect_uri=" +
                                  Url.Action("FacebookLoginCallback", "Login", null, "http") + "&client_secret=" +
                                  WebConfigurationManager.AppSettings["FacebookSecret"] + "&code=" +
                                  facebookDataRepository.GetUserAuthenticationCode());

            using (var reader = new StreamReader(request.GetResponse().GetResponseStream(), Encoding.ASCII)) {
                var result = reader.ReadToEnd();
                var queryStringParameters = HttpUtility.ParseQueryString(result);
                facebookDataRepository.SaveAccessToken(queryStringParameters["access_token"]);
            }

            var request2 =
                WebRequest.Create("https://graph.facebook.com/me?access_token=" +
                                  facebookDataRepository.GetAccessToken());

            using (var reader = new StreamReader(request2.GetResponse().GetResponseStream(), Encoding.ASCII)) {
                var result = reader.ReadToEnd();
                var facebookUserData = JsonConvert.DeserializeObject<FacebookUserData>(result);
                eventBus.Send(new FacebookLoginEvent { FacebookId = facebookUserData.Id });
            }

            return RedirectToAction("Index", "Home");
        }
    }
}