namespace Events {
    public class GoalDescriptionUpdatedEvent : DomainEvent {
        public string Description { get; set; }
    }
}