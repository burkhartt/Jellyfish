using System;
using System.Web;
using System.Web.Security;
using Web.Events;
using Web.Events.Entity;
using Web.Models;
using Web.Repositories;

namespace Web.Denormalizers {
    public class AuthenticationDenormalizer : IHandleEvents<AccountSuccessfullyAuthenticatedEvent>,
                                              IHandleEvents<EntityUpdatedEvent<Account>>,
                                              IHandleEvents<FacebookLoginEvent> {
        private readonly IAccountRepository accountRepository;
        private readonly IAccountSessionRepository accountSessionRepository;

        public AuthenticationDenormalizer(IAccountRepository accountRepository, IAccountSessionRepository accountSessionRepository) {
            this.accountRepository = accountRepository;
            this.accountSessionRepository = accountSessionRepository;
        }

        public void Handle(AccountSuccessfullyAuthenticatedEvent @event) {
            UpdateAuthenticationInformation(@event.Id);
        }

        public void Handle(EntityUpdatedEvent<Account> @event) {
            UpdateAuthenticationInformation(@event.Entity.Id);
        }

        public void Handle(FacebookLoginEvent @event) {
            var account = accountRepository.GetByFacebookId(@event.FacebookId);
            if (account == null) {
                accountRepository.Create(new FacebookAccount(@event.FacebookId));
            }
            
            account = accountRepository.GetByFacebookId(@event.FacebookId);
            UpdateAuthenticationInformation(account.Id);
        }

        private void UpdateAuthenticationInformation(Guid id) {
            accountSessionRepository.SetCurrentId(id);            
        }
    }
}