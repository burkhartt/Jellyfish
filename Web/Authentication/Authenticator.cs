using Domain.Repositories;
using Events.Accounts;
using Events.Bus;

namespace Authentication {
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