using System;
using Entities;

namespace Events {
    public class GoalAddedToGroupEvent : GoalDomainEvent {
        public Guid GroupId { get; set; }

        public override string GetMessage(Account account) {
            return account.FullName + " added the goal to the group at " + EventDate.ToString("MMMM d, yyyy H:mm tt");
        }
    }
}