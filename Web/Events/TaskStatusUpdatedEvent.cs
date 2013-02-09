using System;
using Entities;

namespace Events {
    public class TaskStatusUpdatedEvent : GoalDomainEvent {
        public Guid GoalId { get; set; }
        public bool IsComplete { get; set; }

        public override string GetMessage(Account account) {
            return account.FullName + " marked a task as " + (IsComplete ? "complete" : "incomplete") + " at " + EventDate.ToString("MMMM d, yyyy H:mm tt");
        }
    }
}