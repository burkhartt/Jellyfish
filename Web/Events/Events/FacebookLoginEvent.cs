namespace Events.Events {
    public class FacebookLoginEvent : DomainEvent {
        public int FacebookId { get; set; }
    }
}