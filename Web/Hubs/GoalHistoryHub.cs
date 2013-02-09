using System;
using Domain.Repositories;
using Events;
using Events.Handler;
using Microsoft.AspNet.SignalR;
using ServiceStack.Text;

namespace Hubs {
    public class GoalHistoryHub : Hub, IHandleDomainEvents<GoalCreatedEvent>,
                                  IHandleDomainEvents<GoalAddedToGoalEvent>,
                                  IHandleDomainEvents<GoalDescriptionUpdatedEvent>,
                                  IHandleDomainEvents<GoalDeadlineUpdatedEvent>,
                                  IHandleDomainEvents<GoalTypeUpdatedEvent>,
                                  IHandleDomainEvents<GoalCurrentNumberUpdatedEvent>,
                                  IHandleDomainEvents<GoalTargetNumberUpdatedEvent>,
                                  IHandleDomainEvents<TaskCreatedEvent>, IHandleDomainEvents<TaskStatusUpdatedEvent>,
                                  IHandleDomainEvents<TaskTitleUpdatedEvent> {
        private readonly IGoalRepository goalRepository;
        public GoalHistoryHub() {}

        public GoalHistoryHub(IGoalRepository goalRepository) {
            this.goalRepository = goalRepository;
        }

        public void Handle(GoalAddedToGoalEvent @event) {
            SendHistoryToClient(@event.Id);
        }

        public void Handle(GoalCreatedEvent @event) {
            SendHistoryToClient(@event.Id);
        }

        public void Handle(GoalCurrentNumberUpdatedEvent @event) {
            SendHistoryToClient(@event.Id);
        }

        public void Handle(GoalDeadlineUpdatedEvent @event) {
            SendHistoryToClient(@event.Id);
        }

        public void Handle(GoalDescriptionUpdatedEvent @event) {
            SendHistoryToClient(@event.Id);
        }

        public void Handle(GoalTargetNumberUpdatedEvent @event) {
            SendHistoryToClient(@event.Id);
        }

        public void Handle(GoalTypeUpdatedEvent @event) {
            SendHistoryToClient(@event.Id);
        }

        public void Handle(TaskCreatedEvent @event) {
            SendHistoryToClient(@event.GoalId);
        }

        public void Handle(TaskStatusUpdatedEvent @event) {
            SendHistoryToClient(@event.GoalId);
        }

        public void Handle(TaskTitleUpdatedEvent @event) {
            SendHistoryToClient(@event.GoalId);
        }

        private void SendHistoryToClient(Guid goalId) {
            var goal = goalRepository.GetById(goalId);
            var context = GlobalHost.ConnectionManager.GetHubContext<GoalHistoryHub>();
            context.Clients.All.setGoalHistory(goal.Logs.ToJson());
        }
    }
}