using Entities;

namespace Events {
    public class GoalDescriptionUpdatedEvent : GoalDomainEvent {
        public string Description { get; set; }

        public override string GetMessage(Account account) {
            return account.FullName + " changed the description at " + EventDate.ToString("MMMM d, yyyy H:mm tt");
        }
    }
}