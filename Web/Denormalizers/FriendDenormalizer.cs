using Database;
using Events.Friends;
using Events.Handler;

namespace Denormalizers {
    public class FriendDenormalizer : IHandleDomainEvents<FacebookFriendFoundEvent> {
        private readonly IDatabase database;

        public FriendDenormalizer(IDatabase database) {
            this.database = database;
        }

        public void Handle(FacebookFriendFoundEvent @event) {
            database.GetTheDatabase().Friends.Insert(@event);
        }
    }
}