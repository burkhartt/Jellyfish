using System;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using Web.Database;

namespace Web.Authentication {
    public interface IAuthenticator {
        bool Authenticate(string emailAddress, string password);
    }

    public class Authenticator : IAuthenticator {
        private readonly IDatabase database;

        public Authenticator(IDatabase database) {
            this.database = database;
        }

        public bool Authenticate(string emailAddress, string password) {
            if (!IsAuthenticated(emailAddress, password)) {
                return false;
            }

            var roles = GetRoles(emailAddress);
            var authTicket = new FormsAuthenticationTicket(1, emailAddress, DateTime.Now, DateTime.Now.AddMinutes(60), false, roles);
            var encryptedTicket = FormsAuthentication.Encrypt(authTicket);            
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            HttpContext.Current.Response.Cookies.Add(authCookie);
            return true;
        }

        private bool IsAuthenticated(string emailAddress, string password) {
            if (string.IsNullOrEmpty(emailAddress) || string.IsNullOrEmpty(password)) {
                return false;
            }

            var result = database.GetTheDatabase().Account.FindByEmailAddressAndPassword(EmailAddress: emailAddress, Password: password);
            return result != null;
        }

        private string GetRoles(string emailAddress) {
            // Lookup code omitted for clarity
            // This code would typically look up the role list from a database
            // table.
            // If the user was being authenticated against Active Directory,
            // the Security groups and/or distribution lists that the user
            // belongs to may be used instead

            // This GetRoles method returns a pipe delimited string containing
            // roles rather than returning an array, because the string format
            // is convenient for storing in the authentication ticket /
            // cookie, as user data
            return "Senior Manager|Manager|Employee";
        }
    }

    public class Identity : IIdentity {
        public string Name { get; private set; }
        public string AuthenticationType { get; private set; }
        public bool IsAuthenticated { get; private set; }
    }
}