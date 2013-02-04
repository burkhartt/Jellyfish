using Database;
using Events;
using Events.Handler;

namespace Denormalizers {
    public class GroupGoalsDenormalizer : IHandleDomainEvents<GoalAddedToGroupEvent> {
        private readonly IDatabase database;

        public GroupGoalsDenormalizer(IDatabase database) {
            this.database = database;
        }

        public void Handle(GoalAddedToGroupEvent @event) {
            database.GetTheDatabase().GroupGoals.Insert(GroupId: @event.GroupId, GoalId: @event.GoalId);
        }
    }
}