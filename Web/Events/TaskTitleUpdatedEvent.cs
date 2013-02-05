namespace Events {
    public class TaskTitleUpdatedEvent : DomainEvent {
        public string Title { get; set; }
    }
}