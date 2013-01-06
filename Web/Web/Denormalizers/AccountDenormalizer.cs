using Web.Events;
using Web.Models;
using Web.Repositories;

namespace Web.Denormalizers {
    public class AccountDenormalizer : GenericDenormalizer<Account>, IHandleEvents<InviteFriendEvent> {
        private readonly IAccountRepository accountRepository;

        public AccountDenormalizer(IAccountRepository accountRepository) : base(accountRepository) {
            this.accountRepository = accountRepository;
        }

        public void Handle(InviteFriendEvent @event) {
            var account = new Account {
                EmailAddress = @event.EmailAddress,
                Id = @event.FriendId,
                AccountConfirmed = false,
                Friends = new [] { @event.Id }
            };

            accountRepository.AddInvitedAccount(account);
        }
    }
}