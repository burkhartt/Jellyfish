using System;
using Entities;

namespace Events {
    public class TaskCreatedEvent : GoalDomainEvent {
        public Guid GoalId { get; set; }

        public override string GetMessage(Account account) {
            return account.FullName + " created a task at " + EventDate.ToString("MMMM d, yyyy H:mm tt");
        }
    }
}