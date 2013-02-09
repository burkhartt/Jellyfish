using Entities;

namespace Events {
    public class GoalTargetNumberUpdatedDeltaEvent : GoalDomainEvent {
        public decimal Delta { get; set; }

        public override string GetMessage(Account account) {
            return account.FullName + " changed the goal target by " + Delta + " at " + EventDate.ToString("MMMM d, yyyy H:mm tt");
        }
    }
}