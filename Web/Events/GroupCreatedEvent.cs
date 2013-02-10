namespace Events {
    public class GroupCreatedEvent : DomainEvent {
        public string Title { get; set; }
        public bool IsAlone { get; set; }
    }
}