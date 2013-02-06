using System;

namespace Events {
    public class GoalAddedToGroupEvent : DomainEvent {
        public Guid GroupId { get; set; }
        public Guid AccountId { get; set; }
    }
}