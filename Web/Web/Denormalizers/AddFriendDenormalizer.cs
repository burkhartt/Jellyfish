using Web.Events;
using Web.Repositories;

namespace Web.Denormalizers {
    public class AddFriendDenormalizer : IHandleEvents<InviteFriendEvent> {
        private readonly IFriendRepository friendRepository;

        public AddFriendDenormalizer(IFriendRepository friendRepository) {
            this.friendRepository = friendRepository;
        }

        public void Handle(InviteFriendEvent @event) {
            friendRepository.AddFriend(@event.Id, @event.FriendId);
        }
    }
}