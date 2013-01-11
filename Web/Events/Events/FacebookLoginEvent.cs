namespace Events.Events {
    public class FacebookLoginEvent : IEvent {
        public int FacebookId { get; set; }
    }
}