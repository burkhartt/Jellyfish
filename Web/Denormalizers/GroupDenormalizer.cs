using System;
using Database;
using Events;
using Events.Bus;
using Events.Handler;

namespace Denormalizers {
    public class GroupDenormalizer : IHandleDomainEvents<GroupCreatedEvent>, IHandleDomainEvents<FacebookAccountCreatedEvent> {
        private readonly IDatabase database;
        private readonly IEventBus eventBus;

        public GroupDenormalizer(IDatabase database, IEventBus eventBus) {
            this.database = database;
            this.eventBus = eventBus;
        }

        public void Handle(GroupCreatedEvent @event) {
            database.GetTheDatabase().Groups.Insert(Id: @event.Id, Title: @event.Title, IsAlone: @event.IsAlone);
        }

        public void Handle(FacebookAccountCreatedEvent @event) {
            var groupId = Guid.NewGuid();
            eventBus.Send(new GroupCreatedEvent { Id = groupId, Title = "Myself", IsAlone = true });
            eventBus.Send(new AccountAddedToGroupEvent { AccountId = @event.Id, GroupId = groupId });
        }
    }
}