using System;
using Domain.Models.Accounts;
using Domain.Repositories;
using Events.Accounts;
using Events.Entities;
using Events.Events;
using Events.Handler;

namespace Denormalizers {
    public class AuthenticationDenormalizer : IHandleDomainEvents<AccountSuccessfullyAuthenticatedEvent>,
                                              IHandleDomainEvents<EntityUpdatedEvent<Account>>,
                                              IHandleDomainEvents<FacebookLoginEvent> {
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