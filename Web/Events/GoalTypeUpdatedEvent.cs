using Entities;

namespace Events {
    public class GoalTypeUpdatedEvent : GoalDomainEvent {
        public string Type { get; set; }
        public override string GetMessage(Account account) {
            return account.FullName + " changed the goal type to " + Type + " at " + EventDate.ToString("MMMM d, yyyy H:mm tt");
        }
    }
}