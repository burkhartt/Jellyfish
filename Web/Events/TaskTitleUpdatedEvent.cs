using System;
using Entities;

namespace Events {
    public class TaskTitleUpdatedEvent : GoalDomainEvent {
        public string Title { get; set; }
        public Guid GoalId { get; set; }

        public override string GetMessage(Account account) {
            return account.FullName + " updated a task title at " + EventDate.ToString("MMMM d, yyyy H:mm tt");
        }
    }
}