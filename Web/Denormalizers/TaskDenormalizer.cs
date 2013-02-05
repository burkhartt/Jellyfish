using Database;
using Events;
using Events.Handler;

namespace Denormalizers {
    public class TaskDenormalizer : IHandleDomainEvents<TaskCreatedEvent>, IHandleDomainEvents<TaskStatusUpdatedEvent>, IHandleDomainEvents<TaskTitleUpdatedEvent> {
        private readonly IDatabase database;

        public TaskDenormalizer(IDatabase database) {
            this.database = database;
        }

        public void Handle(TaskCreatedEvent @event) {
            database.GetTheDatabase().Tasks.Insert(Id: @event.Id, GoalId: @event.GoalId);
        }

        public void Handle(TaskStatusUpdatedEvent @event) {
            database.GetTheDatabase().Tasks.UpdateById(Id: @event.Id, IsComplete: @event.IsComplete);
        }

        public void Handle(TaskTitleUpdatedEvent @event) {
            database.GetTheDatabase().Tasks.UpdateById(Id: @event.Id, Title: @event.Title);
        }
    }
}