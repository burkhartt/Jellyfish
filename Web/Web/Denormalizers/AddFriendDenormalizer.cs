using Web.Events;
using Web.Repositories;

namespace Web.Denormalizers {
    public class AddFriendDenormalizer : IHandleEvents<InviteFriendEvent> {
        private readonly IAccountRepository accountRepository;

        public AddFriendDenormalizer(IAccountRepository accountRepository) {
            this.accountRepository = accountRepository;
        }

        public void Handle(InviteFriendEvent @event) {
            accountRepository.AddFriend(@event.Id, @event.FriendId);
        }
    }
}