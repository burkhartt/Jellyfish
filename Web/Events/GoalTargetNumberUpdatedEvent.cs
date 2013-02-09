using Entities;

namespace Events {
    public class GoalTargetNumberUpdatedEvent : GoalDomainEvent {
        public decimal Number { get; set; }

        public override string GetMessage(Account account) {
            return account.FullName + " changed the goal target number to " + Number + " at " + EventDate.ToString("MMMM d, yyyy H:mm tt");
        }
    }
}