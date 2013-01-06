using System;
using System.Security.Principal;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using Web.Events;
using Web.Models;
using Web.Repositories;

namespace Web.Authentication {
    public interface IAuthenticator {
        bool Authenticate(string emailAddress, string password);
    }

    public class Authenticator : IAuthenticator {
        private readonly IAccountRepository accountRepository;
        private readonly IEventBus eventBus;

        public Authenticator(IAccountRepository accountRepository, IEventBus eventBus) {
            this.accountRepository = accountRepository;
            this.eventBus = eventBus;
        }

        public bool Authenticate(string emailAddress, string password) {
            var account = accountRepository.GetByEmailAddressAndPassword(emailAddress, password);

            if (account == null) {
                return false;
            }

            eventBus.Send(new AccountSuccessfullyAuthenticatedEvent { Id = account.Id });

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