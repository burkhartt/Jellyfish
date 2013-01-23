namespace Events {
    public class GoalUpdatedEvent : DomainEvent {
        public string Description { get; set; }
    }
}