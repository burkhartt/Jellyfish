using Domain.Repositories;
using Entities;
using Events;
using Events.Handler;

namespace Denormalizers {
    public class AccountDenormalizer : IHandleDomainEvents<FacebookFriendAccountRetrievedEvent> {
        private readonly IAccountRepository accountRepository;

        public AccountDenormalizer(IAccountRepository accountRepository) {
            this.accountRepository = accountRepository;
        }

        public void Handle(FacebookFriendAccountRetrievedEvent @event) {
            accountRepository.Create(new Account { AccountConfirmed = true, FacebookId = @event.FacebookId, FirstName = @event.FirstName, Id = @event.Id, LastName = @event.LastName, Picture = @event.Picture });
        }
    }
}