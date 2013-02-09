using System;
using Entities;

namespace Events {
    public class GoalDeadlineUpdatedEvent : GoalDomainEvent {
        public DateTime? Deadline { get; set; }

        public override string GetMessage(Account account) {
            if (Deadline.HasValue) {
                return account.FullName + " changed the deadline to " + Deadline.Value.ToString("MMMM d, yyyy H:mm tt") + " at " + EventDate.ToString("MMMM d, yyyy H:mm tt");
            }

            return account.FullName + " removed the deadline at " + EventDate.ToString("MMMM d, yyyy H:mm tt");
        }
    }
}