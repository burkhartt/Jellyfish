using System;
using Domain.Repositories;
using Entities;
using Events;
using Events.Accounts;
using Events.Bus;
using Events.Events;
using Events.Handler;

namespace Denormalizers {
    public class AuthenticationDenormalizer : IHandleDomainEvents<AccountSuccessfullyAuthenticatedEvent>,
                                              IHandleDomainEvents<FacebookLoginEvent> {
        private readonly IAccountRepository accountRepository;
        private readonly IAccountSessionRepository accountSessionRepository;
        private readonly IEventBus eventBus;

        public AuthenticationDenormalizer(IAccountRepository accountRepository, IAccountSessionRepository accountSessionRepository, IEventBus eventBus) {
            this.accountRepository = accountRepository;
            this.accountSessionRepository = accountSessionRepository;
            this.eventBus = eventBus;
        }

        public void Handle(AccountSuccessfullyAuthenticatedEvent @event) {
            UpdateAuthenticationInformation(@event.Id);
        }        

        public void Handle(FacebookLoginEvent @event) {
            var account = accountRepository.GetByFacebookId(@event.FacebookId);
            if (account == null) {
                eventBus.Send(new FacebookAccountCreatedEvent { FacebookId = @event.FacebookId, Id = Guid.NewGuid() });
            }
            
            account = accountRepository.GetByFacebookId(@event.FacebookId);
            UpdateAuthenticationInformation(account.Id);
        }

        private void UpdateAuthenticationInformation(Guid id) {
            accountSessionRepository.SetCurrentId(id);            
        }
    }
}