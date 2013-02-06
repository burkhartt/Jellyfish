using System;
using Database;
using Domain.Repositories;
using Events;
using Events.Handler;
using ServiceStack.Text;

namespace Denormalizers {
    public class GoalLogDenormalizer : IHandleDomainEvents<GoalCreatedEvent>,
                                       IHandleDomainEvents<GoalUpdatedEvent>,
                                       IHandleDomainEvents<GoalAddedToGoalEvent>,
                                       IHandleDomainEvents<GoalDescriptionUpdatedEvent>,
                                       IHandleDomainEvents<GoalDeadlineUpdatedEvent>,
                                       IHandleDomainEvents<GoalTypeUpdatedEvent>,
                                       IHandleDomainEvents<GoalCurrentNumberUpdatedEvent>,
                                       IHandleDomainEvents<GoalTargetNumberUpdatedEvent>,
                                       IHandleDomainEvents<TaskCreatedEvent>,
                                       IHandleDomainEvents<TaskTitleUpdatedEvent>,
                                       IHandleDomainEvents<TaskStatusUpdatedEvent> {
        private readonly IDatabase database;
        private readonly ITaskRepository taskRepository;

        public GoalLogDenormalizer(IDatabase database, ITaskRepository taskRepository) {
            this.database = database;
            this.taskRepository = taskRepository;
        }

        public void Handle(GoalAddedToGoalEvent @event) {
            InsertGoal(@event);
        }

        public void Handle(GoalCreatedEvent @event) {
            InsertGoal(@event);
        }

        public void Handle(GoalCurrentNumberUpdatedEvent @event) {
            InsertGoal(@event);
        }

        public void Handle(GoalDeadlineUpdatedEvent @event) {
            InsertGoal(@event);
        }

        public void Handle(GoalDescriptionUpdatedEvent @event) {
            InsertGoal(@event);
        }

        public void Handle(GoalTargetNumberUpdatedEvent @event) {
            InsertGoal(@event);
        }

        public void Handle(GoalTypeUpdatedEvent @event) {
            InsertGoal(@event);
        }

        public void Handle(GoalUpdatedEvent @event) {
            InsertGoal(@event);
        }

        public void Handle(TaskCreatedEvent @event) {
            database.GetTheDatabase()
                    .GoalLog.Insert(Id: Guid.NewGuid(), GoalId: @event.GoalId, EventDate: @event.EventDate,
                                    Event: @event.GetType().FullName, Data: @event.ToJson());
        }

        public void Handle(TaskStatusUpdatedEvent @event) {
            var task = taskRepository.GetById(@event.Id);
            database.GetTheDatabase()
                    .GoalLog.Insert(Id: Guid.NewGuid(), GoalId: task.GoalId, EventDate: @event.EventDate,
                                    Event: @event.GetType().FullName, Data: @event.ToJson());
        }

        public void Handle(TaskTitleUpdatedEvent @event) {
            var task = taskRepository.GetById(@event.Id);
            database.GetTheDatabase()
                    .GoalLog.Insert(Id: Guid.NewGuid(), GoalId: task.GoalId, EventDate: @event.EventDate,
                                    Event: @event.GetType().FullName, Data: @event.ToJson());
        }

        public void InsertGoal(DomainEvent @event) {
            database.GetTheDatabase()
                    .GoalLog.Insert(Id: Guid.NewGuid(), GoalId: @event.Id, EventDate: @event.EventDate,
                                    Event: @event.GetType().FullName, Data: @event.ToJson());
        }
    }
}