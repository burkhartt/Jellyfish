using Database;
using Events;
using Events.Handler;

namespace Denormalizers {
    public class GoalDenormalizer : IHandleDomainEvents<GoalCreatedEvent>, IHandleDomainEvents<GoalUpdatedEvent>,
                                    IHandleDomainEvents<GoalAddedToGoalEvent>,
                                    IHandleDomainEvents<GoalDescriptionUpdatedEvent>,
                                    IHandleDomainEvents<GoalDeadlineUpdatedEvent> {
        private readonly IDatabase database;

        public GoalDenormalizer(IDatabase database) {
            this.database = database;
        }

        public void Handle(GoalAddedToGoalEvent @event) {
            database.GetTheDatabase().Goals.UpdateById(Id: @event.GoalId, BucketId: @event.ParentGoalId);
        }

        public void Handle(GoalCreatedEvent @event) {
            database.GetTheDatabase()
                    .Goals.Insert(Id: @event.Id, AccountId: @event.AccountId, Title: @event.Title,
                                  ParentGoalId: @event.ParentGoalId);
        }

        public void Handle(GoalDeadlineUpdatedEvent @event) {
            database.GetTheDatabase().Goals.UpdateById(Id: @event.Id, Deadline: @event.Deadline);
        }

        public void Handle(GoalDescriptionUpdatedEvent @event) {
            database.GetTheDatabase().Goals.UpdateById(Id: @event.Id, Description: @event.Description);
        }

        public void Handle(GoalUpdatedEvent @event) {
            database.GetTheDatabase()
                    .Goals.UpdateById(Id: @event.Id, Description: @event.Description, Deadline: @event.Deadline);
        }
    }
}