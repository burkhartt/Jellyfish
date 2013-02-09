using System;

namespace Events.Friends {
    [Serializable]
    public class FacebookFriendFoundEvent : DomainEvent {
        public Guid AccountId { get; set; }

        public Guid FriendId { get; set; }
    }
}