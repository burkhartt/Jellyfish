using Database;
using Events;
using Events.Handler;

namespace Denormalizers {
    public class GroupDenormalizer : IHandleDomainEvents<GroupCreatedEvent> {
        private readonly IDatabase database;

        public GroupDenormalizer(IDatabase database) {
            this.database = database;
        }

        public void Handle(GroupCreatedEvent @event) {
            database.GetTheDatabase().Groups.Insert(Id: @event.Id, Title: @event.Title);
        }
    }
}