using Database;
using Domain.Repositories;
using Entities;
using Events;
using Events.Handler;

namespace Denormalizers {
    public class AccountDenormalizer : IHandleDomainEvents<FacebookFriendAccountRetrievedEvent>,
                                       IHandleDomainEvents<FacebookAccountCreatedEvent>,
    IHandleDomainEvents<FacebookFriendListImportedEvent> {
        private readonly IAccountRepository accountRepository;
        private readonly IDatabase database;

        public AccountDenormalizer(IAccountRepository accountRepository, IDatabase database) {
            this.accountRepository = accountRepository;
            this.database = database;
        }

        public void Handle(FacebookAccountCreatedEvent @event) {
            if (!accountRepository.FindById(@event.Id).IsNull()) {
                return;
            }

            accountRepository.Create(new Account
            {
                AccountConfirmed = true,
                FacebookId = @event.FacebookId,
                Id = @event.Id
            });
        }

        public void Handle(FacebookFriendAccountRetrievedEvent @event) {
            if (!accountRepository.FindById(@event.Id).IsNull()) {
                return;
            }
            accountRepository.Create(new Account {
                AccountConfirmed = false,
                FacebookId = @event.FacebookId,
                FirstName = @event.FirstName,
                Id = @event.Id,
                LastName = @event.LastName,
                Picture = @event.Picture
            });
        }

        public void Handle(FacebookFriendListImportedEvent @event) {
            database.GetTheDatabase().Account.UpdateById(Id: @event.Id, FacebookFriendListImported: @event.EventDate);
        }
    }
}