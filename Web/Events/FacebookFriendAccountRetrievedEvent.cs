using System;

namespace Events {
    [Serializable]
    public class FacebookFriendAccountRetrievedEvent : DomainEvent {
        public long FacebookId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Picture { get; set; }
    }
}