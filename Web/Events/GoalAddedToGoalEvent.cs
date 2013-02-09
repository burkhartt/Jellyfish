using System;
using Entities;

namespace Events {
    public class GoalAddedToGoalEvent : GoalDomainEvent {
        public Guid GoalId { get; set; }
        public Guid ParentGoalId { get; set; }

        public override string GetMessage(Account account) {
            return account.FullName + " created the goal at " + EventDate.ToString("MMMM d, yyyy H:mm tt");
        }
    }
}