namespace Events {
    public class TaskStatusUpdatedEvent : DomainEvent {
        public bool IsComplete { get; set; }
    }
}