using System;
using Events.Events;

namespace Events.Friends {
    [Serializable]
    public class InviteFriendEvent : DomainEvent {
        public Guid Id { get; set; }
        public string EmailAddress { get; set; }
        public Guid FriendId { get; set; }
    }
}