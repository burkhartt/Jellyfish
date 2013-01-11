using Domain.Models.Accounts;
using Domain.Repositories;
using Events.Accounts;
using Events.Bus;
using Events.Friends;
using Events.Handler;

namespace Denormalizers {
    public class AccountDenormalizer : GenericDenormalizer<Account>, IHandleEvents<InviteFriendEvent>,
                                       IHandleEvents<VerifyAccountEvent> {
        private readonly IAccountRepository accountRepository;
        private readonly IEventBus eventBus;

        public AccountDenormalizer(IAccountRepository accountRepository, IEventBus eventBus) : base(accountRepository) {
            this.accountRepository = accountRepository;
            this.eventBus = eventBus;
        }

        public void Handle(InviteFriendEvent @event) {
            var account = new Account {
                EmailAddress = @event.EmailAddress,
                Id = @event.FriendId,
                AccountConfirmed = false,
                Friends = new[] {@event.Id}
            };

            accountRepository.AddInvitedAccount(account);
        }

        public void Handle(VerifyAccountEvent @event) {
            var account = accountRepository.FindById(@event.Id);
            if (account.EmailAddress != @event.EmailAddress) {
                return;
            }

            account.AccountConfirmed = true;
            accountRepository.Update(account);
            eventBus.Send(new AccountSuccessfullyAuthenticatedEvent { Id = account.Id });
        }
    }
}