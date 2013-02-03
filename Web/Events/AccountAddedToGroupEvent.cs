using System;

namespace Events {
    public class AccountAddedToGroupEvent : DomainEvent {
        public Guid GroupId { get; set; }
        public Guid AccountId { get; set; }
    }
}