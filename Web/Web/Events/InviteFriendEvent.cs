using System;

namespace Web.Events {
    public class InviteFriendEvent : IEvent {
        public Guid Id { get; set; }
        public string EmailAddress { get; set; }
        public Guid FriendId { get; set; }
    }
}