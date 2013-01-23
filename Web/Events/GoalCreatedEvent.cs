namespace Events {
    public class GoalCreatedEvent : DomainEvent {
        public string Title { get; set; }
    }
}