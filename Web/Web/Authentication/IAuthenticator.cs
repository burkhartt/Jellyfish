using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using Web.Database;
using Web.Models;
using Web.Repositories;

namespace Web.Authentication {
    public interface IAuthenticator {
        bool Authenticate(string emailAddress, string password);
    }

    public class Authenticator : IAuthenticator {
        private readonly IAccountRepository accountRepository;

        public Authenticator(IAccountRepository accountRepository) {
            this.accountRepository = accountRepository;
        }

        public bool Authenticate(string emailAddress, string password) {
            var account = accountRepository.GetByEmailAddressAndPassword(emailAddress, password);

            if (account == null){
                return false;
            }

            var serializer = new JavaScriptSerializer();
            string userData = serializer.Serialize(account);

            var authTicket = new FormsAuthenticationTicket(1, account.Id.ToString(), DateTime.Now, DateTime.Now.AddMinutes(60), false, userData);
            var encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            
            HttpContext.Current.Response.Cookies.Add(authCookie);
            return true;
        }
    }        

    public class Principal : GenericPrincipal {
        public Principal(IIdentity identity, Account account) : base(identity, new string[]{}) {
            Account = account;
        }

        public string Name { get; private set; }
        public string AuthenticationType { get; private set; }
        public bool IsAuthenticated { get; private set; }
        public Account Account { get; private set; }
    }
}