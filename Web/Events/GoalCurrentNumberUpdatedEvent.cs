using Entities;

namespace Events {
    public class GoalCurrentNumberUpdatedEvent : GoalDomainEvent {
        public decimal Number { get; set; }

        public override string GetMessage(Account account) {
            return account.FullName + " changed the current number to " + Number + " at " + EventDate.ToString("MMMM d, yyyy H:mm tt");
        }
    }
}