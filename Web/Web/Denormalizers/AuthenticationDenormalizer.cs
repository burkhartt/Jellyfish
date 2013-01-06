using System;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using Web.Events;
using Web.Repositories;

namespace Web.Denormalizers {
    public class AuthenticationDenormalizer : IHandleEvents<AccountSuccessfullyAuthenticatedEvent> {
        private readonly IAccountRepository accountRepository;

        public AuthenticationDenormalizer(IAccountRepository accountRepository) {
            this.accountRepository = accountRepository;
        }

        public void Handle(AccountSuccessfullyAuthenticatedEvent @event) {
            var account = accountRepository.FindById(@event.Id);

            var serializer = new JavaScriptSerializer();
            var userData = serializer.Serialize(account);

            var authTicket = new FormsAuthenticationTicket(1, account.Id.ToString(), DateTime.Now, DateTime.Now.AddMinutes(60), false, userData);
            var encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

            HttpContext.Current.Response.Cookies.Add(authCookie);
        }
    }
}