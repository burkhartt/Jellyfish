using Domain.Repositories;
using Events.Friends;
using Events.Handler;

namespace Denormalizers {
    public class AddFriendDenormalizer : IHandleDomainEvents<InviteFriendEvent> {
        private readonly IFriendRepository friendRepository;

        public AddFriendDenormalizer(IFriendRepository friendRepository) {
            this.friendRepository = friendRepository;
        }

        public void Handle(InviteFriendEvent @event) {
            friendRepository.AddFriend(@event.Id, @event.FriendId);
        }
    }
}