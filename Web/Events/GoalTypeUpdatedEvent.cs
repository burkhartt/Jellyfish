namespace Events {
    public class GoalTypeUpdatedEvent : DomainEvent {
        public string Type { get; set; }
    }
}