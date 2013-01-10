using Web.Events;
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
}