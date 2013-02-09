using Entities;

namespace Events {
    public class GoalCurrentNumberUpdatedDeltaEvent : GoalDomainEvent {
        public decimal Delta { get; set; }

        public override string GetMessage(Account account) {
            return account.FullName + " changed the current number by " + Delta + " at " + EventDate.ToString("MMMM d, yyyy H:mm tt");
        }
    }
}