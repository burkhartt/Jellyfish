using Database;
using Events;
using Events.Handler;

namespace Denormalizers {
    public class GroupMembersDenormalizer : IHandleDomainEvents<AccountAddedToGroupEvent> {
        private readonly IDatabase database;

        public GroupMembersDenormalizer(IDatabase database) {
            this.database = database;
        }

        public void Handle(AccountAddedToGroupEvent @event) {
            database.GetTheDatabase().GroupMembers.Insert(GroupId: @event.GroupId, AccountId: @event.AccountId);
        }
    }
}