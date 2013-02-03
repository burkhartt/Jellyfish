using Database;
using Events;
using Events.Handler;

namespace Denormalizers {
    public class GoalDenormalizer : IHandleDomainEvents<GoalCreatedEvent>, IHandleDomainEvents<GoalUpdatedEvent>, IHandleDomainEvents<GoalAddedToBucketEvent> {
        private readonly IDatabase database;

        public GoalDenormalizer(IDatabase database) {
            this.database = database;
        }

        public void Handle(GoalCreatedEvent @event) {
            database.GetTheDatabase().Goals.Insert(Id: @event.Id, AccountId: @event.AccountId, Title: @event.Title);
        }

        public void Handle(GoalUpdatedEvent @event) {
            database.GetTheDatabase().Goals.UpdateById(Id: @event.Id, Description: @event.Description, Deadline: @event.Deadline);
        }

        public void Handle(GoalAddedToBucketEvent @event) {
            database.GetTheDatabase().Goals.UpdateById(Id: @event.GoalId, BucketId: @event.BucketId);
        }
    }
}