using System;

namespace Events {
    [Serializable]
    public class FacebookAccountCreatedEvent : DomainEvent {
        public int FacebookId { get; set; }
    }
}